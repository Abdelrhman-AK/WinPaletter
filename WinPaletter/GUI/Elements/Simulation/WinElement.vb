Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

Namespace UI.Simulation

    <Description("Simulated Windows elements")> Public Class WinElement : Inherits ContainerControl

        Sub New()
            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            BackColor = Color.Transparent
        End Sub

#Region "Variables"

        Dim Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.15))
        Dim Noise7 As Bitmap = My.Resources.AeroGlass
        Dim Noise7Start As Bitmap = My.Resources.Start7Glass
        Dim adaptedBackBlurred As Bitmap
        Dim Button1 As Rectangle
        Dim Button2 As Rectangle

        Private _State_Btn1, _State_Btn2 As MouseState
        Enum MouseState
            Normal
            Hover
            Pressed
        End Enum

        Enum Styles
            Start11
            Taskbar11
            ActionCenter11
            AltTab11
            Start10
            Taskbar10
            ActionCenter10
            AltTab10
            Start8
            Taskbar8Aero
            Taskbar8Lite
            AltTab8Aero
            AltTab8AeroLite
            Start7Aero
            Taskbar7Aero
            Start7Opaque
            Taskbar7Opaque
            Start7Basic
            Taskbar7Basic
            AltTab7Aero
            AltTab7Opaque
            AltTab7Basic
            StartVistaAero
            TaskbarVistaAero
            StartVistaOpaque
            TaskbarVistaOpaque
            StartVistaBasic
            TaskbarVistaBasic
            StartXP
            TaskbarXP
        End Enum

#End Region

#Region "Properties"

        Private _Style As Styles = Styles.Start11
        Public Property Style As Styles
            Get
                Return _Style
            End Get
            Set(value As Styles)
                _Style = value
                'ProcessBack()
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _BackColorAlpha As Byte = 130
        Public Property BackColorAlpha() As Integer
            Get
                Return _BackColorAlpha
            End Get
            Set(value As Integer)
                _BackColorAlpha = value
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _NoisePower As Single = 0.15
        Public Property NoisePower() As Single
            Get
                Return _NoisePower
            End Get
            Set(value As Single)
                Me._NoisePower = value

                If Style = Styles.Taskbar7Aero Then
                    Try : Noise7 = My.Resources.AeroGlass.Fade(NoisePower / 100) : Catch : End Try
                End If

                If Style = Styles.Start7Aero Then
                    Try : Noise7Start = My.Resources.Start7Glass.Fade(NoisePower / 100) : Catch : End Try
                End If

                If Not SuspendRefresh Then NoiseBack()
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _BlurPower As Integer = 8
        Public Property BlurPower() As Integer
            Get
                Return _BlurPower
            End Get
            Set(value As Integer)
                _BlurPower = value
                GetBack()
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _Transparency As Boolean = True
        Public Property Transparency() As Boolean
            Get
                Return _Transparency
            End Get
            Set(value As Boolean)
                _Transparency = value
                'ProcessBack()
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _DarkMode As Boolean = True
        Public Property DarkMode() As Boolean
            Get
                Return _DarkMode
            End Get
            Set(value As Boolean)
                _DarkMode = value
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _AppUnderline As Color
        Public Property AppUnderline() As Color
            Get
                Return _AppUnderline
            End Get
            Set(value As Color)
                _AppUnderline = value
                Try : If Not SuspendRefresh Then Refresh()
                Catch : End Try
            End Set
        End Property

        Private _AppBackground As Color
        Public Property AppBackground() As Color
            Get
                Return _AppBackground
            End Get
            Set(value As Color)
                _AppBackground = value
                Try : If Not SuspendRefresh Then Refresh()
                Catch : End Try
            End Set
        End Property

        Private _ActionCenterButton_Normal As Color
        Public Property ActionCenterButton_Normal() As Color
            Get
                Return _ActionCenterButton_Normal
            End Get
            Set(value As Color)
                _ActionCenterButton_Normal = value
                Try : If Not SuspendRefresh Then Refresh()
                Catch : End Try
            End Set
        End Property

        Private _ActionCenterButton_Hover As Color
        Public Property ActionCenterButton_Hover() As Color
            Get
                Return _ActionCenterButton_Hover
            End Get
            Set(value As Color)
                _ActionCenterButton_Hover = value
                Try : If Not SuspendRefresh Then Refresh()
                Catch : End Try
            End Set
        End Property

        Private _ActionCenterButton_Pressed As Color
        Public Property ActionCenterButton_Pressed() As Color
            Get
                Return _ActionCenterButton_Pressed
            End Get
            Set(value As Color)
                _ActionCenterButton_Pressed = value
                Try : If Not SuspendRefresh Then Refresh()
                Catch : End Try
            End Set
        End Property

        Private _StartColor As Color
        Public Property StartColor() As Color
            Get
                Return _StartColor
            End Get
            Set(value As Color)
                _StartColor = value
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _LinkColor As Color
        Public Property LinkColor() As Color
            Get
                Return _LinkColor
            End Get
            Set(value As Color)
                _LinkColor = value
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _Background As Color
        Public Property Background() As Color
            Get
                Return _Background
            End Get
            Set(value As Color)
                _Background = value
                If Not SuspendRefresh Then
                    Try : Refresh() : Catch : End Try
                End If
            End Set
        End Property

        Private _Background2 As Color
        Public Property Background2() As Color
            Get
                Return _Background2
            End Get
            Set(value As Color)
                _Background2 = value
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _Win7ColorBal As Integer = 0.15
        Public Property Win7ColorBal() As Integer
            Get
                Return _Win7ColorBal
            End Get
            Set(value As Integer)
                _Win7ColorBal = value
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _Win7GlowBal As Integer = 0.15
        Public Property Win7GlowBal() As Integer
            Get
                Return _Win7GlowBal
            End Get
            Set(value As Integer)
                _Win7GlowBal = value
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Public Property UseWin11ORB_WithWin10 As Boolean = False
        Public Property UseWin11RoundedCorners_WithWin10_Level1 As Boolean = False
        Public Property UseWin11RoundedCorners_WithWin10_Level2 As Boolean = False
        Public Property Shadow As Boolean = True
        Public Property SuspendRefresh As Boolean = False

        Protected Overrides ReadOnly Property CreateParams As CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                cp.ExStyle = cp.ExStyle Or &H20
                Return cp
            End Get
        End Property

#End Region

#Region "Events"

        Private Sub WinElement_BackColorChanged(sender As Object, e As EventArgs) Handles Me.BackColorChanged
            If Not BackColor = Color.Transparent Then BackColor = Color.Transparent
        End Sub

        Private Sub WinElement_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove, Me.MouseDown, Me.MouseUp
            If Style = Styles.ActionCenter11 Then

                If Button1.Contains(PointToClient(MousePosition)) Then
                    If e.Button = MouseButtons.None Then _State_Btn1 = MouseState.Hover Else _State_Btn1 = MouseState.Pressed
                    If Not SuspendRefresh Then Refresh()
                Else
                    If Not _State_Btn1 = MouseState.Normal Then
                        _State_Btn1 = MouseState.Normal
                        If Not SuspendRefresh Then Refresh()
                    End If
                End If

                If Button2.Contains(PointToClient(MousePosition)) Then
                    If e.Button = MouseButtons.None Then _State_Btn2 = MouseState.Hover Else _State_Btn2 = MouseState.Pressed
                    If Not SuspendRefresh Then Refresh()
                Else
                    If Not _State_Btn2 = MouseState.Normal Then
                        _State_Btn2 = MouseState.Normal
                        If Not SuspendRefresh Then Refresh()
                    End If
                End If

            End If
        End Sub

        Private Sub WinElement_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
            If Style = Styles.ActionCenter11 Then
                _State_Btn1 = MouseState.Normal
                _State_Btn2 = MouseState.Normal
                If Not SuspendRefresh Then Refresh()
            End If
        End Sub

        Private Sub WinElement_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            If Not DesignMode Then
                Try : AddHandler Parent.BackgroundImageChanged, AddressOf ProcessBack_EventHandler : Catch : End Try
                GetBack()
            End If
        End Sub

        Private Sub WinElement_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            If Not DesignMode Then
                Try : RemoveHandler Parent.BackgroundImageChanged, AddressOf ProcessBack_EventHandler : Catch : End Try
            End If
        End Sub

        Sub ProcessBack_EventHandler(sender As Object, e As EventArgs)
            ProcessBack()
        End Sub

        Sub ProcessBack()
            GetBack()
        End Sub

        Sub GetBack()
            Try
                If Style = Styles.Taskbar11 Or Style = Styles.Start11 Or Style = Styles.ActionCenter11 Or Style = Styles.AltTab11 Then

                    If Transparency Then
                        Dim b As Bitmap = Nothing
                        If My.Wallpaper IsNot Nothing Then
                            b = My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat).Blur(BlurPower)
                        End If

                        If DarkMode Then
                            If b IsNot Nothing Then
                                Using ImgF As New ImageProcessor.ImageFactory
                                    ImgF.Load(b)
                                    ImgF.Saturation(60)
                                    adaptedBackBlurred = ImgF.Image.Clone
                                End Using
                            End If

                        Else
                            adaptedBackBlurred = b
                        End If
                    Else
                        adaptedBackBlurred = Nothing
                    End If

                ElseIf Style = Styles.Taskbar10 Or Style = Styles.Start10 Or Style = Styles.ActionCenter10 Then
                    If Transparency AndAlso My.Wallpaper IsNot Nothing Then
                        adaptedBackBlurred = My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat).Blur(BlurPower)
                    Else
                        adaptedBackBlurred = Nothing
                    End If

                ElseIf Style = Styles.Start7Aero Or Style = Styles.Taskbar7Aero Or Style = Styles.StartVistaAero Or Style = Styles.TaskbarVistaAero Or Style = Styles.AltTab7Aero Then
                    If My.Wallpaper IsNot Nothing Then
                        adaptedBackBlurred = My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat).Blur(1)
                    Else
                        adaptedBackBlurred = Nothing
                    End If

                Else
                    adaptedBackBlurred = Nothing

                End If

            Catch
                adaptedBackBlurred = Nothing

            End Try
        End Sub

        Sub NoiseBack()

            If Style = Styles.ActionCenter11 Or Style = Styles.Start11 Or Style = Styles.Taskbar11 Or Style = Styles.AltTab11 Then
                If Transparency Then Noise = New TextureBrush(My.Resources.GaussianBlur.Fade(NoisePower))

            ElseIf Style = Styles.ActionCenter10 Or Style = Styles.Start10 Or Style = Styles.Taskbar10 Then
                If Transparency Then Noise = New TextureBrush(My.Resources.GaussianBlur.Fade(NoisePower))

            ElseIf Style = Styles.Start7Aero Or Style = Styles.Taskbar7Aero Or Style = Styles.AltTab7Aero Or Style = Styles.AltTab7Opaque Then
                Try : Noise7 = My.Resources.AeroGlass.Fade(NoisePower / 100) : Catch : End Try
                Try : Noise7Start = My.Resources.Start7Glass.Fade(NoisePower / 100) : Catch : End Try

            End If

        End Sub

#End Region

#Region "Subs/Functions"
        Public Sub CopycatFrom(element As WinElement)
            Style = element.Style
            _NoisePower = element.NoisePower
            _BlurPower = element.BlurPower
            _Transparency = element.Transparency
            _DarkMode = element.DarkMode
            _AppUnderline = element.AppUnderline
            _AppBackground = element.AppBackground
            _ActionCenterButton_Normal = element.ActionCenterButton_Normal
            _ActionCenterButton_Hover = element.ActionCenterButton_Hover
            _ActionCenterButton_Pressed = element.ActionCenterButton_Pressed
            _StartColor = element.StartColor
            _LinkColor = element.LinkColor
            _BackColorAlpha = element.BackColorAlpha
            BackColor = element.BackColor
            _Background2 = element.Background2
            _Win7ColorBal = element.Win7ColorBal
            _Win7GlowBal = element.Win7GlowBal
            UseWin11ORB_WithWin10 = element.UseWin11ORB_WithWin10
            UseWin11RoundedCorners_WithWin10_Level1 = element.UseWin11RoundedCorners_WithWin10_Level1
            UseWin11RoundedCorners_WithWin10_Level2 = element.UseWin11RoundedCorners_WithWin10_Level2
            Shadow = element.Shadow

            Dock = element.Dock
            Size = element.Size
            Location = element.Location
            Text = element.Text

            Try
                If Not SuspendRefresh Then
                    'ProcessBack()
                    Refresh()
                End If
            Catch : End Try
        End Sub

#End Region

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = My.RenderingHint
            DoubleBuffered = True
            Dim Rect As New Rectangle(-1, -1, Width + 2, Height + 2)
            Dim RRect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim Radius As Integer = 5

            Select Case Style
                Case Styles.Start11
#Region "Start 11"
                    If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, True)

                    If DarkMode Then
                        Using br As New SolidBrush(Color.FromArgb(85, 28, 28, 28)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                    Else
                        Using br As New SolidBrush(Color.FromArgb(75, 255, 255, 255)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                    End If

                    Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                    If Transparency Then G.FillRoundedRect(Noise, RRect, Radius, True)
                    Dim SearchRect As New Rectangle(8, 10, Width - (8) * 2, 15)

                    G.DrawRoundImage(If(DarkMode, My.Resources.Start11_Dark, My.Resources.Start11_Light), RRect, Radius, True)

                    Dim SearchColor, SearchBorderColor As Color
                    If DarkMode Then
                        SearchColor = Color.FromArgb(150, 28, 28, 28)
                        SearchBorderColor = Color.FromArgb(150, 65, 65, 65)
                    Else
                        SearchColor = Color.FromArgb(175, 255, 255, 255)
                        SearchBorderColor = Color.FromArgb(175, 200, 200, 200)
                    End If

                    Using br As New SolidBrush(SearchColor) : G.FillRoundedRect(br, SearchRect, 8, True) : End Using
                    Using P As New Pen(SearchBorderColor) : G.DrawRoundedRect(P, SearchRect, 8, True) : End Using

                    Using P As New Pen(Color.FromArgb(150, 90, 90, 90)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
#End Region

                Case Styles.ActionCenter11
#Region "Action Center 11"
                    If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, True)

                    If DarkMode Then
                        Using br As New SolidBrush(Color.FromArgb(85, 28, 28, 28)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                    Else
                        Using br As New SolidBrush(Color.FromArgb(75, 255, 255, 255)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                    End If

                    Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                    If Transparency Then G.FillRoundedRect(Noise, RRect, Radius, True)
                    Button1 = New Rectangle(8, 8, 49, 20)
                    Button2 = New Rectangle(62, 8, 49, 20)

                    G.DrawRoundImage(If(DarkMode, My.Resources.AC_11_Dark, My.Resources.AC_11_Light), RRect, Radius, True)

                    Dim Cx1, Cx2 As Color

                    Select Case _State_Btn1
                        Case MouseState.Normal
                            Cx1 = ActionCenterButton_Normal
                        Case MouseState.Hover
                            Cx1 = ActionCenterButton_Hover
                        Case MouseState.Pressed
                            Cx1 = ActionCenterButton_Pressed
                    End Select

                    Select Case _State_Btn2
                        Case MouseState.Normal
                            Cx2 = If(DarkMode, Color.FromArgb(190, 70, 70, 70), Color.FromArgb(180, 140, 140, 140))
                        Case MouseState.Hover
                            Cx2 = If(DarkMode, Color.FromArgb(190, 90, 90, 90), Color.FromArgb(210, 230, 230, 230))
                        Case MouseState.Pressed
                            Cx2 = If(DarkMode, Color.FromArgb(190, 75, 75, 75), Color.FromArgb(210, 210, 210, 210))
                    End Select

                    Using br As New SolidBrush(Cx1) : G.FillRoundedRect(br, Button1, Radius, True) : End Using
                    Using P As New Pen(Cx1.Light(0.15)) : G.DrawRoundedRect_LikeW11(P, Button1, Radius, True) : End Using
                    Using br As New SolidBrush(Cx2) : G.FillRoundedRect(br, Button2, Radius, True) : End Using
                    Using P As New Pen(Cx2.CB(If(DarkMode, 0.05, -0.05))) : G.DrawRoundedRect(P, Button2, Radius, True) : End Using
                    Using P As New Pen(Color.FromArgb(150, 90, 90, 90)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
#End Region

                Case Styles.Taskbar11
#Region "Taskbar 11"
                    If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)

                    If DarkMode Then
                        Using br As New SolidBrush(Color.FromArgb(110, 28, 28, 28)) : G.FillRectangle(br, Rect) : End Using
                    Else
                        Using br As New SolidBrush(Color.FromArgb(90, 255, 255, 255)) : G.FillRectangle(br, Rect) : End Using
                    End If

                    Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRectangle(br, Rect) : End Using
                    If Transparency Then G.FillRoundedRect(Noise, RRect, Radius, True)

                    Dim StartBtnRect As New Rectangle(8, 3, 36, 36)
                    Dim StartImgRect As New Rectangle(8, 3, 37, 37)

                    Dim App2BtnRect As New Rectangle(StartBtnRect.Right + 5, 3, 36, 36)
                    Dim App2ImgRect As New Rectangle(StartBtnRect.Right + 5, 3, 37, 37)
                    Dim App2BtnRectUnderline As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - 8) / 2, App2BtnRect.Y + App2BtnRect.Height - 3, 8, 3)

                    Dim AppBtnRect As New Rectangle(App2BtnRect.Right + 5, 3, 36, 36)
                    Dim AppImgRect As New Rectangle(App2BtnRect.Right + 5, 3, 37, 37)
                    Dim AppBtnRectUnderline As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - 18) / 2, AppBtnRect.Y + AppBtnRect.Height - 3, 18, 3)

                    Dim BackC As Color
                    Dim BorderC As Color

                    If DarkMode Then
                        BackC = Color.FromArgb(45, 130, 130, 130)
                        BorderC = Color.FromArgb(45, 130, 130, 130)
                    Else
                        BackC = Color.FromArgb(35, 255, 255, 255)
                        BorderC = Color.FromArgb(35, 255, 255, 255)
                    End If

                    Using br As New SolidBrush(BackC) : G.FillRoundedRect(br, StartBtnRect, 3, True) : End Using
                    Using P As New Pen(BorderC) : G.DrawRoundedRect_LikeW11(P, StartBtnRect, 3) : End Using
                    G.DrawImage(If(DarkMode, My.Resources.StartBtn_11Dark, My.Resources.StartBtn_11Light), StartImgRect)

                    Using br As New SolidBrush(BackC) : G.FillRoundedRect(br, AppBtnRect, 3, True) : End Using
                    Using P As New Pen(BorderC) : G.DrawRoundedRect_LikeW11(P, AppBtnRect, 3) : End Using
                    G.DrawImage(My.Resources.SampleApp_Active, AppImgRect)
                    Using br As New SolidBrush(_AppUnderline) : G.FillRoundedRect(br, AppBtnRectUnderline, 2, True) : End Using

                    G.DrawImage(My.Resources.SampleApp_Inactive, App2ImgRect)
                    Using br As New SolidBrush(Color.FromArgb(255, BackC)) : G.FillRoundedRect(br, App2BtnRectUnderline, 2, True) : End Using

                    Using P As New Pen(Color.FromArgb(100, 100, 100, 100)) : G.DrawLine(P, New Point(0, 0), New Point(Width - 1, 0)) : End Using
#End Region

                Case Styles.AltTab11
#Region "Alt+Tab 11"
                    If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, True)

                    If Transparency Then
                        If DarkMode Then
                            Using br As New SolidBrush(Color.FromArgb(100, 175, 175, 175)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                        Else
                            Using br As New SolidBrush(Color.FromArgb(120, 185, 185, 185)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                        End If

                        G.FillRoundedRect(Noise, RRect, Radius, True)
                    Else
                        If DarkMode Then
                            Using br As New SolidBrush(Color.FromArgb(32, 32, 32)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                            Using P As New Pen(Color.FromArgb(65, 65, 65)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
                        Else
                            Using br As New SolidBrush(Color.FromArgb(243, 243, 243)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                            Using P As New Pen(Color.FromArgb(171, 171, 171)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
                        End If
                    End If

                    Dim AppHeight As Single = 0.75 * RRect.Height
                    Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                    Dim appsNumber As Integer = 3
                    Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                    Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                    Dim Rects As New List(Of Rectangle)
                    Rects.Clear()

                    For x = 0 To appsNumber - 1
                        If x = 0 Then
                            Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding, AppWidth, AppHeight))
                        Else
                            Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding, AppWidth, AppHeight))
                        End If
                    Next

                    For x = 0 To Rects.Count - 1
                        Dim r As Rectangle = Rects(x)

                        Dim back As Color = If(DarkMode, Color.FromArgb(23, 23, 23), Color.FromArgb(233, 234, 234))
                        Dim back2 As Color = If(DarkMode, Color.FromArgb(39, 39, 39), Color.FromArgb(255, 255, 255))

                        If x = 0 Then
                            Dim surround As New Rectangle(r.X - 5, r.Y - 5, r.Width + 10, r.Height + 10)
                            Using P As New Pen(Color.FromArgb(75, 182, 237), 3) : G.DrawRoundedRect(P, surround, Radius * 2 + 5 / 2, True) : End Using
                        End If

                        Using br As New SolidBrush(back) : G.FillRoundedRect(br, r, Radius * 2, True) : End Using
                        G.DrawImage(My.Resources.SampleApp_Active, New Rectangle(r.X + 5, r.Y + 5, 20, 20))

                        Using br As New SolidBrush(Color.FromArgb(150, back2)) : G.FillRectangle(br, New Rectangle(r.X + 5 + 20 + 5, r.Y + 5 + (20 - 4) / 2, 20, 4)) : End Using

                        Using br As New SolidBrush(back2) : G.FillRoundedRect(br, New Rectangle(r.X + 1, r.Y + 5 + 20 + 5, r.Width - 2, r.Height - 5 - 20 - 5), Radius * 2, True) : End Using
                    Next
#End Region

                Case Styles.Start10
#Region "Start 10"
                    If Not UseWin11RoundedCorners_WithWin10_Level1 And Not UseWin11RoundedCorners_WithWin10_Level2 Then
                        If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                        If Transparency Then G.FillRectangle(Noise, Rect)
                        Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRectangle(br, Rect) : End Using
                        G.DrawImage(If(DarkMode, My.Resources.Start10_Dark, My.Resources.Start10_Light), New Rectangle(0, 0, Width - 1, Height - 1))

                    ElseIf UseWin11RoundedCorners_WithWin10_Level1 Then
                        If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                        If Transparency Then G.FillRectangle(Noise, Rect)
                        Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRectangle(br, Rect) : End Using
                        G.DrawImage(If(DarkMode, My.Resources.Start11_EP_Rounded_Dark, My.Resources.Start11_EP_Rounded_Light), New Rectangle(0, 0, Width - 1, Height - 1))

                    ElseIf UseWin11RoundedCorners_WithWin10_Level2 Then
                        If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                        If Transparency Then G.FillRoundedRect(Noise, Rect, Radius, True)
                        Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
                        G.DrawRoundImage(If(DarkMode, My.Resources.Start11_EP_Rounded_Dark, My.Resources.Start11_EP_Rounded_Light), New Rectangle(0, 0, Width - 1, Height - 1), Radius, True)

                    End If

#End Region

                Case Styles.ActionCenter10
#Region "Action Center 10"
                    If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)

                    If Transparency Then G.FillRectangle(Noise, Rect)
                    Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRectangle(br, Rect) : End Using

                    Dim rect1 As New Rectangle(85, 6, 30, 3)
                    Dim rect2 As New Rectangle(5, 190, 30, 3)
                    Dim rect3 As New Rectangle(42, 201, 34, 24)

                    Using br As New SolidBrush(ActionCenterButton_Normal) : G.FillRectangle(br, rect3) : End Using
                    G.DrawImage(If(DarkMode, My.Resources.AC_10_Dark, My.Resources.AC_10_Light), New Rectangle(0, 0, Width - 1, Height - 1))
                    Using br As New SolidBrush(LinkColor) : G.FillRectangle(br, rect1) : End Using
                    Using br As New SolidBrush(LinkColor) : G.FillRectangle(br, rect2) : End Using
                    Using P As New Pen(Color.FromArgb(150, 100, 100, 100)) : G.DrawLine(P, New Point(0, 0), New Point(0, Height - 1)) : End Using

                    Using P As New Pen(Color.FromArgb(150, 76, 76, 76)) : G.DrawRectangle(P, Rect) : End Using
#End Region

                Case Styles.Taskbar10
#Region "Taskbar 10"
                    G.SmoothingMode = SmoothingMode.HighSpeed
                    If Not DesignMode AndAlso Transparency AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                    Using br As New SolidBrush(Color.FromArgb(If(Transparency, BackColorAlpha, 255), Background)) : G.FillRectangle(br, Rect) : End Using

                    Dim StartBtnRect As New Rectangle(-1, -1, 42, Height + 2)
                    Dim StartBtnImgRect As New Rectangle

                    If Not UseWin11ORB_WithWin10 Then
                        StartBtnImgRect = New Rectangle(StartBtnRect.X + (StartBtnRect.Width - My.Resources.StartBtn_10Dark.Width) / 2, StartBtnRect.Y + (StartBtnRect.Height - My.Resources.StartBtn_10Dark.Height) / 2, My.Resources.StartBtn_10Dark.Width, My.Resources.StartBtn_10Dark.Height)
                    Else
                        StartBtnImgRect = New Rectangle(StartBtnRect.X + (StartBtnRect.Width - My.Resources.StartBtn_11_EP.Width) / 2, StartBtnRect.Y + (StartBtnRect.Height - My.Resources.StartBtn_11_EP.Height) / 2, My.Resources.StartBtn_11_EP.Width, My.Resources.StartBtn_11_EP.Height)
                    End If

                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right, -1, 40, Height + 2)
                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2 - 1, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)
                    Dim AppBtnRectUnderline As New Rectangle(AppBtnRect.X, AppBtnRect.Y + AppBtnRect.Height - 3, AppBtnRect.Width, 2)
                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right, -1, 40, Height + 2)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)
                    Dim App2BtnRectUnderline As New Rectangle(App2BtnRect.X + 14 / 2, App2BtnRect.Y + App2BtnRect.Height - 3, App2BtnRect.Width - 14, 2)
                    Dim StartColor As Color = _StartColor
                    Using br As New SolidBrush(StartColor) : G.FillRectangle(br, StartBtnRect) : End Using

                    If Not UseWin11ORB_WithWin10 Then
                        G.DrawImage(If(DarkMode, My.Resources.StartBtn_10Dark, My.Resources.StartBtn_10Light), StartBtnImgRect)
                    Else
                        G.DrawImage(My.Resources.StartBtn_11_EP, StartBtnImgRect)
                    End If

                    Dim AppColor As Color = _AppBackground
                    Using br As New SolidBrush(AppColor) : G.FillRectangle(br, AppBtnRect) : End Using
                    Using br As New SolidBrush(_AppUnderline.Light) : G.FillRectangle(br, AppBtnRectUnderline) : End Using
                    G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                    Using br As New SolidBrush(_AppUnderline.Light) : G.FillRectangle(br, App2BtnRectUnderline) : End Using
                    G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

                Case Styles.AltTab10
#Region "Alt+Tab 10"
                    Dim a As Integer = Math.Max(Math.Min(255, (BackColorAlpha / 100) * 255), 0)

                    Using br As New SolidBrush(Color.FromArgb(a, 23, 23, 23)) : G.FillRectangle(br, RRect) : End Using

                    Dim AppHeight As Single = 0.75 * RRect.Height
                    Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                    Dim appsNumber As Integer = 3
                    Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                    Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                    Dim Rects As New List(Of Rectangle)
                    Rects.Clear()

                    For x = 0 To appsNumber - 1
                        If x = 0 Then
                            Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding, AppWidth, AppHeight))
                        Else
                            Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding, AppWidth, AppHeight))
                        End If
                    Next

                    For x = 0 To Rects.Count - 1
                        Dim r As Rectangle = Rects(x)

                        Dim back As Color = If(DarkMode, Color.FromArgb(60, 60, 60), Color.FromArgb(255, 255, 255))

                        If x = 0 Then
                            Dim surround As New Rectangle(r.X - 5, r.Y - 5, r.Width + 10, r.Height + 10)
                            Using P As New Pen(Color.White, 2) : G.DrawRectangle(P, surround) : End Using
                        End If

                        G.DrawImage(My.Resources.SampleApp_Active, New Rectangle(r.X + 5, r.Y + 5, 20, 20))

                        G.FillRectangle(Brushes.White, New Rectangle(r.X + 5 + 20 + 5, r.Y + 5 + (20 - 4) / 2, 20, 4))

                        Using br As New SolidBrush(back) : G.FillRectangle(br, New Rectangle(r.X + 1, r.Y + 5 + 20 + 5, r.Width - 2, r.Height - 5 - 20 - 5)) : End Using
                    Next
#End Region

                Case Styles.Taskbar8Aero
#Region "Taskbar 8 Aero"
                    Dim c As Color = Color.FromArgb((Win7ColorBal / 100) * 255, Background)
                    Dim bc As Color = Color.FromArgb(217, 217, 217)

                    Using P As New Pen(Color.FromArgb(80, 0, 0, 0)) : G.DrawLine(P, New Point(0, 0), New Point(Width - 1, 0)) : End Using

                    Using br As New SolidBrush(Color.FromArgb(BackColorAlpha, bc)) : G.FillRectangle(br, Rect) : End Using
                    Using br As New SolidBrush(Color.FromArgb(BackColorAlpha * (Win7ColorBal / 100), c)) : G.FillRectangle(br, Rect) : End Using

                    Dim StartORB As New Bitmap(My.Resources.Win8ORB)
                    Dim StartBtnRect As New Rectangle((35 - 27) / 2 + 2, (35 - 27) / 2 - 1, 27, 27)
                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 8, 0, 45, Height - 1)
                    Dim AppBtnRectInner As New Rectangle(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2)

                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)
                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 2, 0, 45, Height - 1)
                    Dim App2BtnRectInner As New Rectangle(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)

                    G.DrawImage(StartORB, StartBtnRect)

                    Using br As New SolidBrush(Color.FromArgb(100, Color.White)) : G.FillRectangle(br, AppBtnRect) : End Using
                    Using P As New Pen(Color.FromArgb(200, c.CB(-0.5))) : G.DrawRectangle(P, AppBtnRect) : End Using
                    Using P As New Pen(Color.FromArgb(215, Color.White)) : G.DrawRectangle(P, AppBtnRectInner) : End Using

                    G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                    Using br As New SolidBrush(Color.FromArgb(50, Color.White)) : G.FillRectangle(br, App2BtnRect) : End Using
                    Using P As New Pen(Color.FromArgb(100, c.CB(-0.5))) : G.DrawRectangle(P, App2BtnRect) : End Using
                    Using P As New Pen(Color.FromArgb(100, Color.White)) : G.DrawRectangle(P, App2BtnRectInner) : End Using

                    G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

                Case Styles.Taskbar8Lite
#Region "Taskbar 8 Lite"
                    Dim c As Color = Color.FromArgb((Win7ColorBal / 100) * 255, Background)
                    Dim bc As Color = Color.FromArgb(217, 217, 217)

                    Using P As New Pen(Color.FromArgb(89, 89, 89)) : G.DrawRectangle(P, New Rectangle(0, 0, Width - 1, Height - 1)) : End Using

                    Using br As New SolidBrush(Color.FromArgb(255, bc)) : G.FillRectangle(br, Rect) : End Using
                    Using br As New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c)) : G.FillRectangle(br, Rect) : End Using

                    Dim StartORB As New Bitmap(My.Resources.Win8ORB)
                    Dim StartBtnRect As New Rectangle((35 - 27) / 2 + 2, (35 - 27) / 2 - 1, 27, 27)
                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 8, 0, 45, Height - 1)
                    Dim AppBtnRectInner As New Rectangle(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2)

                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)
                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 2, 0, 45, Height - 1)
                    Dim App2BtnRectInner As New Rectangle(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)

                    G.DrawImage(StartORB, StartBtnRect)

                    Using br As New SolidBrush(Color.FromArgb(255, bc.CB(0.5))) : G.FillRectangle(br, AppBtnRect) : End Using
                    Using br As New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c.CB(0.5))) : G.FillRectangle(br, AppBtnRect) : End Using
                    Using P As New Pen(Color.FromArgb(100, bc.CB(-0.5))) : G.DrawRectangle(P, AppBtnRect) : End Using
                    Using P As New Pen(Color.FromArgb(100 * (Win7ColorBal / 100), c.CB(-0.5))) : G.DrawRectangle(P, AppBtnRect) : End Using

                    G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                    Using br As New SolidBrush(Color.FromArgb(255, bc.Light(0.1))) : G.FillRectangle(br, App2BtnRect) : End Using
                    Using br As New SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c.Light(0.1))) : G.FillRectangle(br, App2BtnRect) : End Using
                    Using P As New Pen(Color.FromArgb(100, bc.Dark(0.1))) : G.DrawRectangle(P, App2BtnRect) : End Using
                    Using P As New Pen(Color.FromArgb(100 * (Win7ColorBal / 100), c.Dark(0.1))) : G.DrawRectangle(P, App2BtnRect) : End Using
                    G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

                Case Styles.AltTab8Aero
#Region "Alt+Tab 8 Aero"
                    Using br As New SolidBrush(Background) : G.FillRectangle(br, RRect) : End Using

                    Dim AppHeight As Single = 0.75 * RRect.Height
                    Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                    Dim appsNumber As Integer = 3
                    Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                    Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                    Dim Rects As New List(Of Rectangle)
                    Rects.Clear()

                    For x = 0 To appsNumber - 1
                        If x = 0 Then
                            Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                        Else
                            Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                        End If
                    Next

                    For x = 0 To Rects.Count - 1
                        Dim r As Rectangle = Rects(x)

                        If x = 0 Then
                            Dim surround As New Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20)
                            Using P As New Pen(Color.White, 2) : G.DrawRectangle(P, surround) : End Using
                        End If

                        G.FillRectangle(Brushes.White, r)
                        Dim icon_w As Integer = My.Resources.SampleApp_Active.Width
                        Dim icon_rect As New Rectangle(r.X + r.Width - 0.7 * icon_w, r.Y + r.Height - 0.6 * icon_w, icon_w, icon_w)
                        G.DrawImage(My.Resources.SampleApp_Active, icon_rect)
                    Next

                    Dim TextRect As New Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5)
                    G.DrawString("______", Font, Brushes.White, TextRect, ContentAlignment.MiddleCenter.ToStringFormat)
#End Region

                Case Styles.AltTab8AeroLite
#Region "Alt+Tab 8 Opaque"
                    Using br As New SolidBrush(Background) : G.FillRectangle(br, RRect) : End Using

                    Using P As New Pen(LinkColor, 2) : G.DrawRectangle(P, RRect) : End Using

                    Dim AppHeight As Single = 0.75 * RRect.Height
                    Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                    Dim appsNumber As Integer = 3
                    Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                    Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                    Dim Rects As New List(Of Rectangle)
                    Rects.Clear()

                    For x = 0 To appsNumber - 1
                        If x = 0 Then
                            Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                        Else
                            Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                        End If
                    Next

                    For x = 0 To Rects.Count - 1
                        Dim r As Rectangle = Rects(x)

                        If x = 0 Then
                            Dim surround As New Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20)
                            Using P As New Pen(Background2, 2) : G.DrawRectangle(P, surround) : End Using
                        End If

                        G.FillRectangle(Brushes.White, r)
                        Dim icon_w As Integer = My.Resources.SampleApp_Active.Width
                        Dim icon_rect As New Rectangle(r.X + r.Width - 0.7 * icon_w, r.Y + r.Height - 0.6 * icon_w, icon_w, icon_w)
                        G.DrawImage(My.Resources.SampleApp_Active, icon_rect)
                    Next

                    Dim TextRect As New Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5)
                    Using br As New SolidBrush(ForeColor) : G.DrawString("______", Font, br, TextRect, ContentAlignment.MiddleCenter.ToStringFormat) : End Using
#End Region

                Case Styles.Start7Aero
#Region "Start 7 Aero"
                    Dim RestRect As New Rectangle(0, 14, Width - 5, Height - 10)

                    If Not DesignMode AndAlso adaptedBackBlurred IsNot Nothing Then

                        'To dismiss upper part above start menu and make there is no blur bug
                        G.SetClip(RestRect)
                        G.DrawImage(adaptedBackBlurred, Rect)
                        G.ResetClip()

                        Dim alphaX As Single = 1 - BackColorAlpha / 100  'ColorBlurBalance
                        If alphaX < 0 Then alphaX = 0
                        If alphaX > 1 Then alphaX = 1

                        Dim ColBal As Single = Win7ColorBal / 100   'ColorBalance
                        Dim GlowBal As Single = Win7GlowBal / 100   'AfterGlowBalance
                        Dim Color1 As Color = Background
                        Dim Color2 As Color = Background2

                        G.DrawAeroEffect(RestRect, Nothing, Color1, ColBal, Color2, GlowBal, alphaX, 5, True)
                    End If

                    G.DrawRoundImage(Noise7Start, Rect, 5, True)

                    G.DrawRoundImage(My.Resources.Start7, Rect, 5, True)
#End Region

                Case Styles.Start7Opaque
#Region "Start 7 Opaque"
                    Dim RestRect As New Rectangle(0, 14, Width - 5, Height - 10)
                    Using br As New SolidBrush(Color.White) : G.FillRoundedRect(br, RestRect, 5, True) : End Using
                    Using br As New SolidBrush(Color.FromArgb(255 * BackColorAlpha / 100, Background)) : G.FillRoundedRect(br, RestRect, 5, True) : End Using
                    G.DrawRoundImage(Noise7Start, Rect, 5, True)
                    G.DrawRoundImage(My.Resources.Start7, Rect, 5, True)
#End Region

                Case Styles.Start7Basic
#Region "Start 7 Basic"
                    G.DrawImage(My.Resources.Start7Basic, Rect)
#End Region

                Case Styles.Taskbar7Aero
#Region "Taskbar 7 Aero"

                    If Not DesignMode AndAlso adaptedBackBlurred IsNot Nothing Then
                        G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, True)

                        Dim alphaX As Single = 1 - BackColorAlpha / 100  'ColorBlurBalance
                        If alphaX < 0 Then alphaX = 0
                        If alphaX > 1 Then alphaX = 1

                        Dim ColBal As Single = Win7ColorBal / 100        'ColorBalance
                        Dim GlowBal As Single = Win7GlowBal / 100        'AfterGlowBalance
                        Dim Color1 As Color = Background
                        Dim Color2 As Color = Background2

                        G.DrawAeroEffect(Rect, adaptedBackBlurred, Color1, ColBal, Color2, GlowBal, alphaX, 0, False)
                    End If

                    G.DrawImage(My.Resources.Win7TaskbarSides, Rect)

                    G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, True)

                    Using P As New Pen(Color.FromArgb(80, 0, 0, 0)) : G.DrawLine(P, New Point(0, 0), New Point(Width - 1, 0)) : End Using
                    Using P As New Pen(Color.FromArgb(80, 255, 255, 255)) : G.DrawLine(P, New Point(0, 1), New Point(Width - 1, 1)) : End Using

                    G.DrawImage(My.Resources.AeroPeek, New Rectangle(Width - 10, 0, 10, Height))

                    Dim StartORB As New Bitmap(My.Resources.Win7ORB)

                    Dim StartBtnRect As New Rectangle(3, -3, 39, 39)

                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 5, 0, 45, 35)
                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)

                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 1, 0, 45, 35)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)

                    G.DrawImage(StartORB, StartBtnRect)

                    Using P As New Pen(Color.FromArgb(150, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, True) : End Using
                    G.DrawImage(My.Resources.Taskbar_ActiveApp7, AppBtnRect)
                    G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                    Using P As New Pen(Color.FromArgb(110, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, True) : End Using
                    G.DrawImage(My.Resources.Taskbar_InactiveApp7, App2BtnRect)
                    G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

                Case Styles.Taskbar7Opaque
#Region "Taskbar 7 Opaque"
                    Using br As New SolidBrush(Color.White) : G.FillRectangle(br, Rect) : End Using
                    Using br As New SolidBrush(Color.FromArgb(255 * BackColorAlpha / 100, Background)) : G.FillRectangle(br, Rect) : End Using
                    G.DrawImage(My.Resources.Win7TaskbarSides, Rect)

                    G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, True)

                    Using P As New Pen(Color.FromArgb(80, 0, 0, 0)) : G.DrawLine(P, New Point(0, 0), New Point(Width - 1, 0)) : End Using
                    Using P As New Pen(Color.FromArgb(80, 255, 255, 255)) : G.DrawLine(P, New Point(0, 1), New Point(Width - 1, 1)) : End Using

                    G.DrawImage(My.Resources.AeroPeek, New Rectangle(Width - 10, 0, 10, Height))

                    Dim StartORB As New Bitmap(My.Resources.Win7ORB)

                    Dim StartBtnRect As New Rectangle(3, -3, 39, 39)

                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 5, 0, 45, 35)
                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)

                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 1, 0, 45, 35)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)

                    G.DrawImage(StartORB, StartBtnRect)

                    Using P As New Pen(Color.FromArgb(150, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, True) : End Using
                    G.DrawImage(My.Resources.Taskbar_ActiveApp7, AppBtnRect)
                    G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                    Using P As New Pen(Color.FromArgb(110, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, True) : End Using
                    G.DrawImage(My.Resources.Taskbar_InactiveApp7, App2BtnRect)
                    G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

                Case Styles.Taskbar7Basic
#Region "Taskbar 7 Basic"
                    G.DrawImage(My.Resources.BasicTaskbar, Rect)

                    G.DrawImage(My.Resources.AeroPeek, New Rectangle(Width - 10, 0, 10, Height))

                    Dim StartORB As New Bitmap(My.Resources.Win7ORB)

                    Dim StartBtnRect As New Rectangle(3, -3, 39, 39)

                    Dim AppBtnRect As New Rectangle(StartBtnRect.Right + 5, 0, 45, 35)
                    Dim AppBtnImgRect As New Rectangle(AppBtnRect.X + (AppBtnRect.Width - My.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)

                    Dim App2BtnRect As New Rectangle(AppBtnRect.Right + 1, 0, 45, 35)
                    Dim App2BtnImgRect As New Rectangle(App2BtnRect.X + (App2BtnRect.Width - My.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - My.Resources.SampleApp_Inactive.Height) / 2, My.Resources.SampleApp_Inactive.Width, My.Resources.SampleApp_Inactive.Height)

                    G.DrawImage(StartORB, StartBtnRect)

                    Using P As New Pen(Color.FromArgb(150, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, True) : End Using
                    G.DrawImage(My.Resources.Taskbar_ActiveApp7, AppBtnRect)
                    G.DrawImage(My.Resources.SampleApp_Active, AppBtnImgRect)

                    Using P As New Pen(Color.FromArgb(110, 0, 0, 0)) : G.DrawRoundedRect(P, New Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, True) : End Using
                    G.DrawImage(My.Resources.Taskbar_InactiveApp7, App2BtnRect)
                    G.DrawImage(My.Resources.SampleApp_Inactive, App2BtnImgRect)
#End Region

                Case Styles.AltTab7Aero
#Region "Alt+Tab 7 Aero"
                    If Shadow And Not DesignMode Then G.DrawGlow(RRect, Color.FromArgb(150, 0, 0, 0), 5, 15)

                    Dim inner As New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)
                    Dim Color1 As Color = Background
                    Dim Color2 As Color = Background2

                    If Not DesignMode AndAlso adaptedBackBlurred IsNot Nothing Then
                        Dim alpha As Single = 1 - BackColorAlpha / 100   'ColorBlurBalance
                        Dim ColBal As Single = Win7ColorBal / 100   'ColorBalance
                        Dim GlowBal As Single = Win7GlowBal / 100   'AfterGlowBalance
                        G.DrawAeroEffect(RRect, adaptedBackBlurred, Color1, ColBal, Color2, GlowBal, alpha, Radius, True)
                    End If

                    G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), RRect, Radius, True)
                    Using P As New Pen(Color.FromArgb(200, 25, 25, 25)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
                    Using P As New Pen(Color.FromArgb(70, 200, 200, 200)) : G.DrawRoundedRect(P, inner, Radius, True) : End Using


                    Dim AppHeight As Single = 0.75 * RRect.Height
                    Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                    Dim appsNumber As Integer = 3
                    Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                    Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                    Dim Rects As New List(Of Rectangle)
                    Rects.Clear()

                    For x = 0 To appsNumber - 1
                        If x = 0 Then
                            Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                        Else
                            Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                        End If
                    Next

                    For x = 0 To Rects.Count - 1
                        Dim r As Rectangle = Rects(x)

                        If x = 0 Then
                            Dim surround As New Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20)
                            Using br As New SolidBrush(Color.FromArgb(75, 200, 200, 200)) : G.FillRoundedRect(br, surround, 1, True) : End Using
                            G.DrawRoundImage(My.Resources.Win7_TitleTopL.Fade(0.35), surround, 2, True)
                            G.DrawRoundImage(My.Resources.Win7_TitleTopR.Fade(0.35), surround, 2, True)

                            Using P As New Pen(Color1) : G.DrawRoundedRect(P, surround, 1, True) : End Using
                            Using P As New Pen(Color.FromArgb(229, 240, 250)) : G.DrawRectangle(P, New Rectangle(surround.X + 1, surround.Y + 1, surround.Width - 2, surround.Height - 2)) : End Using

                        End If

                        G.FillRoundedRect(Brushes.White, r, 2, True)
                        G.DrawRoundedRect(Pens.Black, r, 2, True)

                        Dim icon_w As Integer = My.Resources.SampleApp_Active.Width

                        Dim icon_rect As New Rectangle(r.X + r.Width - 0.7 * icon_w, r.Y + r.Height - 0.6 * icon_w, icon_w, icon_w)

                        G.DrawImage(My.Resources.SampleApp_Active, icon_rect)
                    Next

                    Dim TextRect As New Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5)
                    G.DrawGlowString(2, "______", Font, Color.Black, Color.FromArgb(185, 225, 225, 225), RRect, TextRect, ContentAlignment.MiddleCenter.ToStringFormat)

#End Region

                Case Styles.AltTab7Opaque
#Region "Alt+Tab 7 Opaque"
                    If Shadow And Not DesignMode Then
                        G.DrawGlow(RRect, Color.FromArgb(150, 0, 0, 0), 5, 15)
                    End If
                    Dim inner As New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)

                    Using br As New SolidBrush(Color.White) : G.FillRoundedRect(br, RRect, Radius, True) : End Using
                    Using br As New SolidBrush(Color.FromArgb(255 * Win7ColorBal / 100, Background)) : G.FillRoundedRect(br, RRect, Radius, True) : End Using

                    G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), RRect, Radius, True)
                    Using P As New Pen(Color.FromArgb(200, 25, 25, 25)) : G.DrawRoundedRect(P, RRect, Radius, True) : End Using
                    Using P As New Pen(Color.FromArgb(70, 200, 200, 200)) : G.DrawRoundedRect(P, inner, Radius, True) : End Using

                    Dim AppHeight As Single = 0.75 * RRect.Height
                    Dim _padding As Integer = (RRect.Height - AppHeight) / 2

                    Dim appsNumber As Integer = 3
                    Dim AllAppsWidthWithPadding As Single = RRect.Width - 2 * _padding
                    Dim AppWidth As Single = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber

                    Dim Rects As New List(Of Rectangle)
                    Rects.Clear()

                    For x = 0 To appsNumber - 1
                        If x = 0 Then
                            Rects.Add(New Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                        Else
                            Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5))
                        End If
                    Next

                    For x = 0 To Rects.Count - 1
                        Dim r As Rectangle = Rects(x)

                        If x = 0 Then
                            Dim surround As New Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20)
                            Using br As New SolidBrush(Color.FromArgb(75, 200, 200, 200)) : G.FillRoundedRect(br, surround, 1, True) : End Using
                            G.DrawRoundImage(My.Resources.Win7_TitleTopL.Fade(0.35), surround, 2, True)
                            G.DrawRoundImage(My.Resources.Win7_TitleTopR.Fade(0.35), surround, 2, True)

                            Using P As New Pen(Background) : G.DrawRoundedRect(P, surround, 1, True) : End Using
                            Using P As New Pen(Color.FromArgb(229, 240, 250)) : G.DrawRectangle(P, New Rectangle(surround.X + 1, surround.Y + 1, surround.Width - 2, surround.Height - 2)) : End Using

                        End If

                        G.FillRoundedRect(Brushes.White, r, 2, True)
                        G.DrawRoundedRect(Pens.Black, r, 2, True)

                        Dim icon_w As Integer = My.Resources.SampleApp_Active.Width

                        Dim icon_rect As New Rectangle(r.X + r.Width - 0.7 * icon_w, r.Y + r.Height - 0.6 * icon_w, icon_w, icon_w)

                        G.DrawImage(My.Resources.SampleApp_Active, icon_rect)
                    Next

                    Dim TextRect As New Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5)
                    G.DrawGlowString(2, "______", Font, Color.Black, Color.FromArgb(185, 225, 225, 225), RRect, TextRect, ContentAlignment.MiddleCenter.ToStringFormat)

#End Region

                Case Styles.AltTab7Basic
#Region "Alt+Tab 7 Basic"
                    Dim Titlebar_Background1 As Color = Color.FromArgb(152, 180, 208)
                    Dim Titlebar_BackColor2 As Color = Color.FromArgb(186, 210, 234)
                    Dim Titlebar_OuterBorder As Color = Color.FromArgb(52, 52, 52)
                    Dim Titlebar_InnerBorder As Color = Color.FromArgb(255, 255, 255)
                    Dim Titlebar_Turquoise As Color = Color.FromArgb(40, 207, 228)
                    Dim OuterBorder As Color = Color.FromArgb(0, 0, 0)
                    Dim UpperPart As New Rectangle(RRect.X, RRect.Y, RRect.Width + 1, 25)
                    G.SetClip(UpperPart)
                    Dim pth_back As New LinearGradientBrush(UpperPart, Titlebar_Background1, Titlebar_BackColor2, LinearGradientMode.Vertical)
                    Dim pth_line As New LinearGradientBrush(UpperPart, Titlebar_InnerBorder, Titlebar_Turquoise, LinearGradientMode.Vertical)
                    '### Render Titlebar
                    G.FillRectangle(pth_back, RRect)
                    Using P As New Pen(Titlebar_OuterBorder) : G.DrawRectangle(P, RRect) : End Using
                    Using P As New Pen(Titlebar_InnerBorder) : G.DrawRectangle(P, New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)) : End Using
                    G.SetClip(New Rectangle(UpperPart.X + UpperPart.Width * 0.75, UpperPart.Y, UpperPart.Width * 0.75, UpperPart.Height))
                    Using P As New Pen(pth_line) : G.DrawRectangle(P, New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)) : End Using
                    G.ResetClip()
                    G.ExcludeClip(UpperPart)
                    '### Render Rest of WindowR
                    Using br As New SolidBrush(Titlebar_BackColor2) : G.FillRectangle(br, RRect) : End Using
                    Using P As New Pen(Titlebar_Turquoise) : G.DrawRectangle(P, New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)) : End Using
                    Using P As New Pen(OuterBorder) : G.DrawRectangle(P, RRect) : End Using
                    G.ResetClip()
                    Using P As New Pen(Color.FromArgb(52, 52, 52)) : G.DrawRectangle(P, RRect) : End Using
                    Using P As New Pen(Color.FromArgb(255, 225, 225, 225)) : G.DrawRectangle(P, New Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2)) : End Using


                    Dim AppHeight As Single = My.Resources.Win7AltTabBasicButton.Height
                    Dim _padding As Integer = 5

                    Dim appsNumber As Integer = 3
                    Dim AppWidth As Single = My.Resources.Win7AltTabBasicButton.Width

                    Dim _paddingOuter As Integer = (RRect.Width - AppWidth * appsNumber - _padding * (appsNumber - 1)) / 2

                    Dim Rects As New List(Of Rectangle)
                    Rects.Clear()

                    For x = 0 To appsNumber - 1
                        If x = 0 Then
                            Rects.Add(New Rectangle(RRect.X + _paddingOuter, RRect.Y + RRect.Height - 5 - AppHeight, AppWidth, AppHeight))
                        Else
                            Rects.Add(New Rectangle(Rects(x - 1).Right + _padding, RRect.Y + RRect.Height - 5 - AppHeight, AppWidth, AppHeight))
                        End If
                    Next

                    For x = 0 To Rects.Count - 1
                        Dim r As Rectangle = Rects(x)
                        If x = 0 Then G.DrawImage(My.Resources.Win7AltTabBasicButton, r)

                        Dim imgrect As New Rectangle(r.X + (r.Width - My.Resources.SampleApp_Active.Width) / 2, r.Y + (r.Height - My.Resources.SampleApp_Active.Height) / 2, My.Resources.SampleApp_Active.Width, My.Resources.SampleApp_Active.Height)

                        G.DrawImage(My.Resources.SampleApp_Active, imgrect)
                    Next

                    Dim TextRect As New Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, 30)
                    G.DrawString("______", Font, Brushes.Black, TextRect, ContentAlignment.MiddleCenter.ToStringFormat)
#End Region

                Case Styles.StartVistaAero
#Region "Start Vista Aero"
                    Dim RestRect As New Rectangle(0, 14, Width - 6, Height - 14)

                    'To dismiss upper part above start menu and make there is no blur bug
                    G.SetClip(RestRect)
                    If Not DesignMode AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                    G.ResetClip()

                    Using br As New SolidBrush(Color.FromArgb(BackColorAlpha, Background)) : G.FillRoundedRect(br, RestRect, 4, True) : End Using
                    G.DrawImage(My.Resources.Vista_StartAero, New Rectangle(0, 0, Width, Height))
#End Region

                Case Styles.StartVistaOpaque
#Region "Start Vista Opaque"
                    Dim RestRect As New Rectangle(0, 14, Width - 6, Height - 14)
                    G.FillRoundedRect(Brushes.White, RestRect, 4, True)
                    Using br As New SolidBrush(Color.FromArgb(BackColorAlpha, Background)) : G.FillRoundedRect(br, RestRect, 4, True) : End Using
                    G.DrawImage(My.Resources.Vista_StartAero, New Rectangle(0, 0, Width, Height))
#End Region

                Case Styles.StartVistaBasic
#Region "Start Vista Basic"
                    G.DrawImage(My.Resources.Vista_StartBasic, New Rectangle(0, 0, Width, Height))
#End Region

                Case Styles.TaskbarVistaAero
#Region "Taskbar Vista Aero"
                    If Not DesignMode AndAlso adaptedBackBlurred IsNot Nothing Then G.DrawImage(adaptedBackBlurred, Rect)
                    Using br As New SolidBrush(Color.FromArgb(BackColorAlpha, Background)) : G.FillRectangle(br, Rect) : End Using
                    G.FillRectangle(New TextureBrush(My.Resources.Vista_Taskbar), Rect)
                    Dim orb As Bitmap = My.Resources.Vista_StartLowerORB
                    G.DrawImage(orb, New Rectangle(0, 0, orb.Width, Height))

                    Dim apprect1 As New Rectangle(Rect.X + 60, 1, 140, Rect.Height - 4)
                    Dim apprect2 As New Rectangle(apprect1.Right + 2, 1, 140, Rect.Height - 4)
                    Dim appIcon1 As New Rectangle(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20)
                    Dim appIcon2 As New Rectangle(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20)
                    Dim appLabel1 As New Rectangle(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height)
                    Dim appLabel2 As New Rectangle(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height)

                    G.DrawImage(My.Resources.Vista_ActiveApp, apprect1)
                    G.DrawImage(My.Resources.Vista_InactiveApp, apprect2)

                    G.DrawImage(My.Resources.SampleApp_Active, appIcon1)
                    G.DrawImage(My.Resources.SampleApp_Inactive, appIcon2)

                    G.DrawString("App Preview", Font, Brushes.White, appLabel1, ContentAlignment.MiddleLeft.ToStringFormat)
                    G.DrawString("Inactive app", Font, Brushes.White, appLabel2, ContentAlignment.MiddleLeft.ToStringFormat)
#End Region

                Case Styles.TaskbarVistaOpaque
#Region "Taskbar Vista Opaque"
                    Dim orb As Bitmap = My.Resources.Vista_StartLowerORB
                    G.FillRectangle(Brushes.White, Rect)
                    Using br As New SolidBrush(Color.FromArgb(BackColorAlpha, Background)) : G.FillRectangle(br, Rect) : End Using
                    G.FillRectangle(New TextureBrush(My.Resources.Vista_Taskbar), Rect)
                    G.DrawImage(orb, New Rectangle(0, 0, orb.Width, Height))

                    Dim apprect1 As New Rectangle(Rect.X + 60, 1, 140, Rect.Height - 4)
                    Dim apprect2 As New Rectangle(apprect1.Right + 2, 1, 140, Rect.Height - 4)
                    Dim appIcon1 As New Rectangle(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20)
                    Dim appIcon2 As New Rectangle(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20)
                    Dim appLabel1 As New Rectangle(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height)
                    Dim appLabel2 As New Rectangle(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height)

                    G.DrawImage(My.Resources.Vista_ActiveApp, apprect1)
                    G.DrawImage(My.Resources.Vista_InactiveApp, apprect2)

                    G.DrawImage(My.Resources.SampleApp_Active, appIcon1)
                    G.DrawImage(My.Resources.SampleApp_Inactive, appIcon2)

                    G.DrawString("App Preview", Font, Brushes.White, appLabel1, ContentAlignment.MiddleLeft.ToStringFormat)
                    G.DrawString("Inactive app", Font, Brushes.White, appLabel2, ContentAlignment.MiddleLeft.ToStringFormat)
#End Region

                Case Styles.TaskbarVistaBasic
#Region "Taskbar Vista Basic"
                    Dim orb As Bitmap = My.Resources.Vista_StartLowerORB
                    G.FillRectangle(New TextureBrush(My.Resources.Vista_Taskbar), Rect)
                    G.DrawImage(orb, New Rectangle(0, 0, orb.Width, Height))

                    Dim apprect1 As New Rectangle(Rect.X + 60, 1, 140, Rect.Height - 4)
                    Dim apprect2 As New Rectangle(apprect1.Right + 2, 1, 140, Rect.Height - 4)
                    Dim appIcon1 As New Rectangle(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20)
                    Dim appIcon2 As New Rectangle(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20)
                    Dim appLabel1 As New Rectangle(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height)
                    Dim appLabel2 As New Rectangle(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height)

                    G.DrawImage(My.Resources.Vista_ActiveApp, apprect1)
                    G.DrawImage(My.Resources.Vista_InactiveApp, apprect2)

                    G.DrawImage(My.Resources.SampleApp_Active, appIcon1)
                    G.DrawImage(My.Resources.SampleApp_Inactive, appIcon2)

                    G.DrawString("App Preview", Font, Brushes.White, appLabel1, ContentAlignment.MiddleLeft.ToStringFormat)
                    G.DrawString("Inactive app", Font, Brushes.White, appLabel2, ContentAlignment.MiddleLeft.ToStringFormat)
#End Region

                Case Styles.StartXP
#Region "Start XP"
               'Empty
#End Region

                Case Styles.TaskbarXP
#Region "Taskbar XP"
                    Try
                        Dim sm As SmoothingMode = G.SmoothingMode
                        G.SmoothingMode = SmoothingMode.HighSpeed

                        My.resVS.Draw(G, Rect, VisualStylesRes.Element.Taskbar, True, False)

                        G.SmoothingMode = sm
                    Catch
                    End Try
#End Region

            End Select

        End Sub

    End Class

End Namespace