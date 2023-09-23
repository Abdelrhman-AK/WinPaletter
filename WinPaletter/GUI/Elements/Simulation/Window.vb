Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

Namespace UI.Simulation

    <Description("A simulated window")>
    Public Class Window : Inherits Panel
        Sub New()
            AdjustPadding()
            Font = New Font("Segoe UI", 9)
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
        Public Property Shadow As Boolean = True
        Public Property Radius As Integer = 5
        Public Property AccentColor_Active As Color = Color.FromArgb(0, 120, 212)
        Public Property AccentColor_Inactive As Color = Color.FromArgb(32, 32, 32)
        Public Property AccentColor2_Active As Color = Color.FromArgb(0, 120, 212)
        Public Property AccentColor2_Inactive As Color = Color.FromArgb(32, 32, 32)
        Public Property Active As Boolean = True
        Public Property Preview As Preview_Enum = Window.Preview_Enum.W11
        Public Property Win7Alpha As Integer = 100
        Public Property Win7ColorBal As Integer = 100
        Public Property Win7GlowBal As Integer = 100
        Public Property ToolWindow As Boolean = False
        Public Property WinVista As Boolean = False
        Public Property SuspendRefresh As Boolean = False

        Public Event MetricsChanged()
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

        Private _AccentColor_Enabled As Boolean = True
        Public Property AccentColor_Enabled() As Boolean
            Get
                Return _AccentColor_Enabled
            End Get
            Set(value As Boolean)
                _AccentColor_Enabled = value
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _Win7Noise As Single = 1
        Public Property Win7Noise() As Single
            Get
                Return _Win7Noise
            End Get
            Set(value As Single)
                _Win7Noise = value
                If Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Or Preview = Preview_Enum.W7Basic Then
                    Try : Noise7 = My.Resources.AeroGlass.Fade(Win7Noise / 100) : Catch : End Try
                End If
                If Not SuspendRefresh Then Refresh()
            End Set
        End Property

        Private _Metrics_CaptionHeight As Integer = 22
        Public Property Metrics_CaptionHeight As Integer
            Get
                Return _Metrics_CaptionHeight
            End Get
            Set(value As Integer)
                _Metrics_CaptionHeight = value
                AdjustPadding()
                If Not SuspendRefresh Then Refresh()
                RaiseEvent MetricsChanged()
            End Set
        End Property

        Private _Metrics_BorderWidth As Integer = 1
        Public Property Metrics_BorderWidth As Integer
            Get
                Return _Metrics_BorderWidth
            End Get
            Set(value As Integer)
                _Metrics_BorderWidth = value
                AdjustPadding()
                If Not SuspendRefresh Then Refresh()
                RaiseEvent MetricsChanged()
            End Set
        End Property

        Private _Metrics_PaddedBorderWidth As Integer = 4
        Public Property Metrics_PaddedBorderWidth As Integer
            Get
                Return _Metrics_PaddedBorderWidth
            End Get
            Set(value As Integer)
                _Metrics_PaddedBorderWidth = value
                AdjustPadding()
                If Not SuspendRefresh Then Refresh()
                RaiseEvent MetricsChanged()
            End Set
        End Property

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String
#End Region

#Region "Helpers"
        Dim AdaptedBackBlurred As Bitmap
        Dim Noise7 As Bitmap = My.Resources.AeroGlass
        ReadOnly FreeMargin As Integer = 8

        Public Sub CopycatFrom(Window As Window, Optional IgnoreLocationSizesAndText As Boolean = False)
            Shadow = Window.Shadow
            Radius = Window.Radius
            AccentColor_Active = Window.AccentColor_Active
            AccentColor_Inactive = Window.AccentColor_Inactive
            AccentColor2_Active = Window.AccentColor2_Active
            AccentColor2_Inactive = Window.AccentColor2_Inactive
            Active = Window.Active
            Preview = Window.Preview
            Win7Alpha = Window.Win7Alpha
            Win7ColorBal = Window.Win7ColorBal
            Win7GlowBal = Window.Win7GlowBal
            WinVista = Window.WinVista
            _DarkMode = Window.DarkMode
            _AccentColor_Enabled = Window.AccentColor_Enabled
            _Win7Noise = Window.Win7Noise

            If Not IgnoreLocationSizesAndText Then
                ToolWindow = Window.ToolWindow
                Dock = Window.Dock
                Size = Window.Size
                Location = Window.Location
                Text = Window.Text
            End If

            ProcessBack()
            Refresh()
        End Sub
        Public Sub SetMetrics([Window] As Window)
            [Window].Metrics_BorderWidth = Metrics_BorderWidth
            [Window].Metrics_CaptionHeight = Metrics_CaptionHeight
            [Window].Metrics_PaddedBorderWidth = Metrics_PaddedBorderWidth
            [Window].Refresh()
        End Sub
        Sub AdjustPadding()
            Dim i, iTop As Integer

            Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
            TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
            TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
            TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

            If Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Or Preview = Preview_Enum.W7Basic Or Preview = Preview_Enum.W8 Or Preview = Preview_Enum.W8Lite Or Preview = Preview_Enum.WXP Then

                i = FreeMargin + If(Not Preview = Preview_Enum.WXP, _Metrics_PaddedBorderWidth, 0) + _Metrics_BorderWidth
                iTop = i + TitleTextH_Sum + _Metrics_CaptionHeight

                i += 4
                iTop += 3
            Else
                i = FreeMargin
                iTop = i + TitleTextH_Sum + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth + _Metrics_CaptionHeight

                i += 1
                iTop += 4
            End If

            Padding = New Padding(i, iTop, i, i)
        End Sub
        Enum Preview_Enum
            W11
            W10
            W8
            W8Lite
            W7Aero
            W7Opaque
            W7Basic
            WXP
        End Enum
        Public Sub FillSemiRect([Graphics] As Graphics, [Brush] As Brush, [Rectangle] As Rectangle, Optional [Radius] As Integer = -1)
            Try
                If [Radius] = -1 Then [Radius] = 6

                If Graphics Is Nothing Then Throw New ArgumentNullException("graphics")
                [Graphics].SmoothingMode = SmoothingMode.AntiAlias

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
#End Region

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = My.RenderingHint
            DoubleBuffered = True

            If Win7Alpha > 255 Then Win7Alpha = 255
            If Win7Alpha < 0 Then Win7Alpha = 0

            Dim Rect As New Rectangle(FreeMargin, FreeMargin, Width - (FreeMargin * 2 + 1), Height - (FreeMargin * 2 + 1))
            Dim RectBK As New Rectangle(0, 0, Width, Height)
            Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
            TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
            TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
            TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)
            Dim TitlebarRect As New Rectangle(Rect.X, Rect.Y, Rect.Width, TitleTextH_Sum + _Metrics_BorderWidth + _Metrics_CaptionHeight + _Metrics_PaddedBorderWidth + 3)
            'If TitlebarRect.Height < 25 Then TitlebarRect.Height = 25
            Dim IconSize As Integer = 14
            If _Metrics_CaptionHeight <= 17 Then IconSize = 12
            Dim IconRect As New Rectangle(Rect.X + 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, Rect.Y + (TitlebarRect.Height - IconSize) / 2, IconSize, IconSize)
            Dim LabelRect As New Rectangle(IconRect.Right + 4, Rect.Y, TitlebarRect.Width - (IconRect.Right + 4), TitlebarRect.Height)
            If ToolWindow Then LabelRect.X = IconRect.X
            Dim LabelRect8 As New Rectangle(Rect.X, Rect.Y + 2, TitlebarRect.Width - 1, TitlebarRect.Height - 3)
            Dim XRect As New Rectangle(Rect.Right - 35, Rect.Y, 35, TitlebarRect.Height)

            'G.Clear(Color.Transparent)


            If Preview = Preview_Enum.W11 Then
#Region "Windows 11"
                If Shadow And Active And Not DesignMode Then
                    G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15)
                End If

                If Not AccentColor_Enabled AndAlso Active Then
                    G.SetClip(TitlebarRect)
                    G.DrawRoundImage(AdaptedBackBlurred, Rect, Radius, True)
                    G.ResetClip()
                End If

                G.ExcludeClip(TitlebarRect)
                If DarkMode Then
                    Using br As New SolidBrush(Color.FromArgb(20, 20, 20)) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
                Else
                    Using br As New SolidBrush(Color.FromArgb(240, 240, 240)) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
                End If
                G.ResetClip()

                If AccentColor_Enabled Then
                    If Active Then
                        Using P As New Pen(Color.FromArgb(200, AccentColor_Active)) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                    Else
                        Using P As New Pen(Color.FromArgb(200, AccentColor_Inactive)) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                    End If
                Else
                    If DarkMode Then
                        Using P As New Pen(Color.FromArgb(200, 100, 100, 100)) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                    Else
                        Using P As New Pen(Color.FromArgb(200, 220, 220, 220)) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                    End If
                End If

                If AccentColor_Enabled Then
                    If Active Then
                        Using br As New SolidBrush(AccentColor_Active) : FillSemiRect(G, br, TitlebarRect, Radius) : End Using
                        Using P As New Pen(AccentColor_Active) : G.DrawLine(P, New Point(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), New Point(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height)) : End Using
                    Else
                        Using br As New SolidBrush(AccentColor_Inactive) : FillSemiRect(G, br, TitlebarRect, Radius) : End Using
                        Using P As New Pen(AccentColor_Inactive) : G.DrawLine(P, New Point(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), New Point(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height)) : End Using
                    End If
                Else
                    Dim a As Integer = If(Active, If(DarkMode, 180, 245), 255)
                    If DarkMode Then
                        Using br As New SolidBrush(Color.FromArgb(a, 32, 32, 32)) : FillSemiRect(G, br, TitlebarRect, Radius) : End Using
                    Else
                        Using br As New SolidBrush(Color.FromArgb(a, 245, 245, 245)) : FillSemiRect(G, br, TitlebarRect, Radius) : End Using
                    End If
                End If
#End Region

            ElseIf Preview = Preview_Enum.W10 Then
#Region "Windows 10"
                If Shadow And Active And Not DesignMode Then
                    G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15)
                End If

                If DarkMode Then
                    Using br As New SolidBrush(Color.FromArgb(20, 20, 20)) : G.FillRectangle(br, Rect) : End Using
                Else
                    Using br As New SolidBrush(Color.FromArgb(240, 240, 240)) : G.FillRectangle(br, Rect) : End Using
                End If

                If AccentColor_Enabled Then
                    If Active Then
                        Using br As New SolidBrush(AccentColor_Active) : G.FillRectangle(br, TitlebarRect) : End Using
                    Else
                        Using br As New SolidBrush(AccentColor_Inactive) : G.FillRectangle(br, TitlebarRect) : End Using
                    End If
                Else
                    If Active Then
                        If DarkMode Then
                            G.FillRectangle(Brushes.Black, TitlebarRect)
                        Else
                            G.FillRectangle(Brushes.White, TitlebarRect)
                        End If
                    Else
                        If DarkMode Then
                            Using br As New SolidBrush(Color.FromArgb(43, 43, 43)) : G.FillRectangle(br, TitlebarRect) : End Using
                        Else
                            G.FillRectangle(Brushes.White, TitlebarRect)
                        End If
                    End If
                End If

                If AccentColor_Enabled Then
                    If Active Then
                        Using P As New Pen(Color.FromArgb(200, AccentColor_Active)) : G.DrawRectangle(P, Rect) : End Using
                    Else
                        Using P As New Pen(Color.FromArgb(200, AccentColor_Inactive)) : G.DrawRectangle(P, Rect) : End Using
                    End If
                Else
                    If DarkMode Then
                        Using P As New Pen(Color.FromArgb(125, 100, 100, 100)) : G.DrawRectangle(P, Rect) : End Using
                    Else
                        Using P As New Pen(Color.FromArgb(125, 220, 220, 220)) : G.DrawRectangle(P, Rect) : End Using
                    End If
                End If
#End Region

            ElseIf Preview = Preview_Enum.W8 Or Preview = Preview_Enum.W8Lite Then
#Region "Windows 8/8.1"
                Dim InnerWindow_1 As New Rectangle
                Dim InnerWindow_2 As New Rectangle
                Dim Sum As Integer = Metrics_BorderWidth + Metrics_PaddedBorderWidth
                If Sum < 2 Then Sum = 2
                TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
                TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
                TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)
                Dim Sum_Ttl As Integer = Sum + Metrics_CaptionHeight + TitleTextH_Sum

                IconRect.X += 2

                With Rect
                    InnerWindow_1 = New Rectangle(.X + Sum + If(Not Preview = Preview_Enum.W8Lite, 2, 3), .Y + Sum_Ttl + 3, .Width - (Sum) * 2 - If(Not Preview = Preview_Enum.W8Lite, 4, 6), .Height - (Sum + Sum_Ttl) - If(Not Preview = Preview_Enum.W8Lite, 5, 5))
                    InnerWindow_2 = New Rectangle(InnerWindow_1.X + 1, InnerWindow_1.Y + 1, InnerWindow_1.Width - 2, InnerWindow_1.Height - 2)
                End With

                Dim CloseRectH As Integer = Metrics_CaptionHeight + TitleTextH_Sum - 2 + If(Preview = Preview_Enum.W8Lite, 1, 0)

                Dim CloseRectW As Integer = If(Not ToolWindow, CloseRectH * 3 / 2, CloseRectH) + If(Preview = Preview_Enum.W8Lite, 2, 0)

                Dim CloseRect As New Rectangle

                If Not ToolWindow Then

                    If Not Preview = Preview_Enum.W8Lite Then
                        CloseRect = New Rectangle(InnerWindow_1.Right - CloseRectW + 1, Rect.Y + 1, CloseRectW, CloseRectH)
                    Else
                        CloseRect = New Rectangle(InnerWindow_1.Right - CloseRectW + 2, Rect.Y, CloseRectW, CloseRectH)
                    End If

                Else
                    CloseRect = New Rectangle(InnerWindow_1.Right - CloseRectW + 1, Rect.Y + 1, CloseRectW, CloseRectH)
                End If

                Dim InC As Color = If(Not Preview = Preview_Enum.W8Lite, Color.FromArgb(235, 235, 235), Color.FromArgb((Win7ColorBal / 100) * 255, AccentColor_Active.CB(0.8)))

                Dim c As Color = If(Active, Color.FromArgb((Win7ColorBal / 100) * 255, AccentColor_Active), InC)

                Dim bc As Color = Color.FromArgb(217, 217, 217)

                Using br As New SolidBrush(bc) : G.FillRectangle(br, Rect) : End Using
                Using br As New SolidBrush(c) : G.FillRectangle(br, Rect) : End Using

                Using br As New SolidBrush(Color.White) : G.FillRectangle(br, InnerWindow_1) : End Using

                Dim CloseBtn As Image

                If Not ToolWindow Then
                    If CloseRect.Height >= 27 Then
                        CloseBtn = My.Resources.Win8_Close_3
                    ElseIf CloseRect.Height >= 24 Then
                        CloseBtn = My.Resources.Win8_Close_2
                    ElseIf CloseRect.Height >= 21 Then
                        CloseBtn = My.Resources.Win8_Close_1
                    Else
                        CloseBtn = My.Resources.Win8_Close_0
                    End If

                Else
                    CloseBtn = My.Resources.Win8_Close_ToolWindow
                End If

                If Preview = Preview_Enum.W8Lite Then CloseBtn = CloseBtn.ReplaceColor(Color.FromArgb(255, 255, 255), Color.Black)

                If Not Preview = Preview_Enum.W8Lite Then
                    Using P As New Pen(Color.FromArgb(170, bc.CB(-0.2))) : G.DrawRectangle(P, InnerWindow_1) : End Using
                    Using P As New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c.CB(-0.2))) : G.DrawRectangle(P, InnerWindow_1) : End Using

                    G.SmoothingMode = SmoothingMode.HighSpeed
                    Using br As New SolidBrush(If(Active, Color.FromArgb(199, 80, 80), Color.FromArgb(188, 188, 188))) : G.FillRectangle(br, CloseRect) : End Using
                    G.SmoothingMode = SmoothingMode.AntiAlias

                    G.DrawImage(CloseBtn, New Rectangle(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2, CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2, CloseBtn.Width, CloseBtn.Height))

                    Using P As New Pen(Color.FromArgb(200, bc.CB(-0.3))) : G.DrawRectangle(P, Rect) : End Using
                    Using P As New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c.CB(-0.3))) : G.DrawRectangle(P, Rect) : End Using

                Else

                    Using P As New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c).LightLight) : G.DrawLine(P, New Point(InnerWindow_1.X, InnerWindow_1.Y), New Point(InnerWindow_1.X + InnerWindow_1.Width, InnerWindow_1.Y)) : End Using

                    Using P As New Pen(Color.FromArgb((Win7ColorBal / 100) * 255, c).LightLight) : G.DrawLine(P, New Point(InnerWindow_1.X, InnerWindow_1.Y + InnerWindow_1.Height), New Point(InnerWindow_1.X + InnerWindow_1.Width, InnerWindow_1.Y + InnerWindow_1.Height)) : End Using

                    Using br As New SolidBrush(If(Active, Color.FromArgb(195, 90, 80), Color.Transparent)) : G.FillRectangle(br, CloseRect) : End Using

                    G.SmoothingMode = SmoothingMode.HighSpeed
                    Using P As New Pen(If(Active, Color.FromArgb(92, 58, 55), Color.FromArgb(93, 96, 102))) : G.DrawRectangle(P, CloseRect) : End Using
                    G.SmoothingMode = SmoothingMode.AntiAlias

                    G.DrawImage(CloseBtn, New Rectangle(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2, CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2, CloseBtn.Width, CloseBtn.Height))

                    G.DrawRectangle(New Drawing.Pen(Color.FromArgb(47, 48, 51)), Rect)
                End If
#End Region

            ElseIf Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Or Preview = Preview_Enum.W7Basic Then
#Region "Windows 7\Vista"
                Dim InnerWindow_1 As New Rectangle
                Dim InnerWindow_2 As New Rectangle
                Dim RectSide1 As New Rectangle
                Dim RectSide2 As New Rectangle
                Dim Sum As Integer = Metrics_BorderWidth + Metrics_PaddedBorderWidth
                If Sum < 2 Then Sum = 2

                TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
                TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
                TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)
                Dim Sum_Ttl As Integer = Sum + Metrics_CaptionHeight + TitleTextH_Sum

                With Rect
                    InnerWindow_1 = New Rectangle(.X + Sum + 1, .Y + Sum_Ttl, .Width - (Sum) * 2 - 2, .Height - (Sum + Sum_Ttl) - 2)
                    InnerWindow_2 = New Rectangle(InnerWindow_1.X + 1, InnerWindow_1.Y + 1, InnerWindow_1.Width - 2, InnerWindow_1.Height - 2)

                    RectSide1 = New Rectangle(.X + 1, InnerWindow_1.Y, Sum, InnerWindow_1.Height * 0.5)
                    RectSide2 = New Rectangle(InnerWindow_1.Right - 1, RectSide1.Y, RectSide1.Width + 1, RectSide1.Height)
                End With


                If Preview <> Preview_Enum.W7Basic Then

#Region "Aero"
                    If Shadow And Active And Not DesignMode Then
                        G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15)
                    End If

                    Dim Radius As Integer = 5

                    If Not Preview = Preview_Enum.W7Opaque Then
                        Dim bk As Bitmap = AdaptedBackBlurred

                        Dim alpha As Single = 1 - Win7Alpha / 100   'ColorBlurBalance
                        Dim ColBal As Single = Win7ColorBal / 100   'ColorBalance
                        Dim GlowBal As Single = Win7GlowBal / 100   'AfterGlowBalance

                        Dim Color1 As Color = If(Active, AccentColor_Active, AccentColor_Inactive)
                        Dim Color2 As Color = If(Active, AccentColor2_Active, AccentColor2_Inactive)
                        G.ExcludeClip(InnerWindow_1)
                        G.DrawAeroEffect(Rect, bk, Color1, ColBal, Color2, GlowBal, alpha, Radius, Not ToolWindow)
                        G.ResetClip()
                    Else

                        If Not ToolWindow Then
                            Using br As New SolidBrush(Color.White) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
                            Using br As New SolidBrush(Color.FromArgb(255 * Win7Alpha / 100, If(Active, AccentColor_Active, AccentColor_Inactive))) : G.FillRoundedRect(br, Rect, Radius, True) : End Using
                        Else
                            Using br As New SolidBrush(Color.White) : G.FillRectangle(br, Rect) : End Using
                            Using br As New SolidBrush(Color.FromArgb(255 * Win7Alpha / 100, If(Active, AccentColor_Active, AccentColor_Inactive))) : G.FillRectangle(br, Rect) : End Using
                        End If

                    End If

                    If Active Then
                        G.DrawImage(My.Resources.Win7Sides, RectSide1)
                        G.DrawImage(My.Resources.Win7Sides, RectSide2)

                        Dim TitleTopW As Integer = Rect.Width * 0.6
                        Dim TitleTopH As Integer = Rect.Height * 0.6

                        G.DrawImage(My.Resources.Win7_TitleTopL, New Rectangle(Rect.X + If(ToolWindow, 0, 1), Rect.Y + If(ToolWindow, 1, 0), TitleTopW, TitleTopH))
                        G.DrawImage(My.Resources.Win7_TitleTopR, New Rectangle(Rect.X + Rect.Width - TitleTopW, Rect.Y + If(ToolWindow, 1, 0), TitleTopW, TitleTopH))
                    End If

                    Dim inner As New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)

                    If Not ToolWindow Then
                        G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, True)
                        Using P As New Pen(Color.FromArgb(If(Active, 130, 100), 25, 25, 25)) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                        Using P As New Pen(Color.FromArgb(100, 255, 255, 255)) : G.DrawRoundedRect(P, inner, Radius, True) : End Using
                        'Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor, 0.2))) : DrawRect(G, P, Rect, Radius, True) : End Using
                        Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Light(0.2))) : G.DrawRoundedRect(P, InnerWindow_1, 1, True) : End Using
                        Using br As New SolidBrush(Color.White) : G.FillRoundedRect(br, InnerWindow_1, 1, True) : End Using
                        Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Dark(0.2))) : G.DrawRoundedRect(P, InnerWindow_2, 1, True) : End Using
                    Else
                        G.DrawImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect)
                        Using P As New Pen(Color.FromArgb(If(Active, 130, 100), 25, 25, 25)) : G.DrawRectangle(P, Rect) : End Using
                        Using P As New Pen(Color.FromArgb(100, 255, 255, 255)) : G.DrawRectangle(P, inner) : End Using
                        'Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor, 0.2))) : G.DrawRectangle(P, Rect) : End Using
                        Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Light(0.2))) : G.DrawRectangle(P, InnerWindow_1) : End Using
                        Using br As New SolidBrush(Color.White) : G.FillRectangle(br, InnerWindow_1) : End Using
                        Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor.Dark(0.2))) : G.DrawRectangle(P, InnerWindow_2) : End Using
                    End If


                    If Not ToolWindow Then
                        Dim closeBtn As Image
                        Dim CloseRect As New Rectangle

                        If Active Then
                            If Not WinVista Then
                                closeBtn = My.Resources.Win7_Close_Active
                            Else
                                closeBtn = My.Resources.Vista_Close_Active
                            End If
                        Else
                            If Not WinVista Then
                                closeBtn = My.Resources.Win7_Close_inactive
                            Else
                                closeBtn = My.Resources.Vista_Close_inactive
                            End If
                        End If

                        CloseRect = New Rectangle(Rect.X + Rect.Width - closeBtn.Width - 5, Rect.Y + 1, closeBtn.Width, closeBtn.Height)

                        G.DrawImage(closeBtn, CloseRect)

                    Else

                        Dim CloseUpperAccent1 As Color
                        Dim CloseUpperAccent2 As Color
                        Dim CloseLowerAccent1 As Color
                        Dim CloseLowerAccent2 As Color
                        Dim CloseOuterBorder As Color
                        Dim CloseInnerBorder As Color

                        If Active Then
                            CloseUpperAccent1 = Color.FromArgb(233, 169, 156)
                            CloseUpperAccent2 = Color.FromArgb(223, 149, 135)
                            CloseLowerAccent1 = Color.FromArgb(184, 67, 44)
                            CloseLowerAccent2 = Color.FromArgb(210, 127, 110)
                            CloseOuterBorder = Color.FromArgb(67, 20, 34)
                            CloseInnerBorder = Color.FromArgb(100, 255, 255, 255)
                        Else
                            CloseUpperAccent1 = Color.FromArgb(50, 189, 203, 218)
                            CloseLowerAccent2 = Color.FromArgb(50, 205, 219, 234)
                            CloseOuterBorder = Color.FromArgb(131, 142, 168)
                            CloseInnerBorder = Color.FromArgb(50, 209, 219, 229)
                        End If

                        Dim Btn_Height As Integer = Metrics_CaptionHeight + TitleTextH_Sum - 5
                        Dim Btn_Width As Integer = Btn_Height

                        Dim CloseRect As New Rectangle(InnerWindow_1.Right - Btn_Width - 3, Rect.Y + (Sum_Ttl - Btn_Height) / 2, Btn_Width, Btn_Height)

                        If Active Then
                            Dim Factor As Single = 0.5

                            Dim UH As Single = Factor * CloseRect.Height
                            Dim LH As Single = CloseRect.Height - UH
                            Dim Interlapping As Single = (UH / CloseRect.Height) * 10

                            Dim CloseRectUpperHalf As New Rectangle(CloseRect.X, CloseRect.Y, CloseRect.Width, UH + Interlapping)
                            Dim CloseUpperPath As New LinearGradientBrush(CloseRectUpperHalf, CloseUpperAccent1, CloseUpperAccent2, LinearGradientMode.Vertical)

                            Dim CloseRectLowerHalf As New Rectangle(CloseRect.X, CloseRectUpperHalf.Bottom - Interlapping, CloseRect.Width, LH)
                            Dim CloseLowerPath As New LinearGradientBrush(CloseRectLowerHalf, CloseLowerAccent1, CloseLowerAccent2, LinearGradientMode.Vertical)

                            G.FillRoundedRect(CloseUpperPath, CloseRectUpperHalf, 1, True)
                            G.FillRoundedRect(CloseLowerPath, CloseRectLowerHalf, 1, True)
                        Else
                            Dim ClosePath As New LinearGradientBrush(CloseRect, CloseUpperAccent1, CloseLowerAccent2, LinearGradientMode.Vertical)
                            G.FillRectangle(ClosePath, CloseRect)
                        End If


                        Dim CloseBtn As Image

                        If Not ToolWindow Then
                            If CloseRect.Height >= 22 Then
                                CloseBtn = My.Resources.Win7_Basic_Close_2
                            ElseIf CloseRect.Height >= 18 Then
                                CloseBtn = My.Resources.Win7_Basic_Close_1
                            Else
                                CloseBtn = My.Resources.Win7_Basic_Close_0
                            End If
                        Else
                            CloseBtn = My.Resources.Win7_Basic_Close_ToolWindow
                        End If

                        Dim xW As Integer = If(CloseRect.Width Mod 2 = 0, CloseBtn.Width + 1, CloseBtn.Width)
                        Dim xH As Integer = If(CloseRect.Height Mod 2 = 0, CloseBtn.Height + 1, CloseBtn.Height)


                        Dim closerenderrect As New Rectangle(CloseRect.X + (CloseRect.Width - xW) / 2, CloseRect.Y + (CloseRect.Height - xH) / 2, xW, xH)

                        G.DrawImage(CloseBtn, closerenderrect)

                        Using P As New Pen(CloseOuterBorder) : G.DrawRoundedRect(P, CloseRect, 1, True) : End Using
                        Using P As New Pen(CloseInnerBorder) : G.DrawRectangle(P, New Rectangle(CloseRect.X + 1, CloseRect.Y + 1, CloseRect.Width - 2, CloseRect.Height - 2)) : End Using

                    End If

#End Region

                Else

#Region "Basic"
                    Sum = Metrics_BorderWidth + Metrics_PaddedBorderWidth
                    TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
                    TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
                    TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)
                    Sum_Ttl = Sum + Metrics_CaptionHeight + TitleTextH_Sum

                    With Rect
                        InnerWindow_1 = New Rectangle(.X + Sum + 2, .Y + Sum_Ttl + 3, .Width - (Sum) * 2 - 4, .Height - (Sum + Sum_Ttl) - 5)
                        InnerWindow_2 = New Rectangle(InnerWindow_1.X + 1, InnerWindow_1.Y + 1, InnerWindow_1.Width - 2, InnerWindow_1.Height - 2)
                        RectSide1 = New Rectangle(.X + 1, InnerWindow_1.Y, Sum + 1, InnerWindow_1.Height * 0.5)
                        RectSide2 = New Rectangle(InnerWindow_1.Right - 1, RectSide1.Y, RectSide1.Width + 1, RectSide1.Height)
                    End With


                    Dim Titlebar_Backcolor1 As Color
                    Dim Titlebar_Backcolor2 As Color
                    Dim Titlebar_OuterBorder As Color
                    Dim Titlebar_InnerBorder As Color
                    Dim Titlebar_Turquoise As Color
                    Dim OuterBorder As Color

                    Dim CloseUpperAccent1 As Color
                    Dim CloseUpperAccent2 As Color
                    Dim CloseLowerAccent1 As Color
                    Dim CloseLowerAccent2 As Color
                    Dim CloseOuterBorder As Color
                    Dim CloseInnerBorder As Color

                    If Active Then
                        Titlebar_Backcolor1 = Color.FromArgb(152, 180, 208)
                        Titlebar_Backcolor2 = Color.FromArgb(186, 210, 234)
                        Titlebar_OuterBorder = Color.FromArgb(52, 52, 52)
                        Titlebar_InnerBorder = Color.FromArgb(255, 255, 255)
                        Titlebar_Turquoise = Color.FromArgb(40, 207, 228)
                        OuterBorder = Color.FromArgb(0, 0, 0)

                        CloseUpperAccent1 = Color.FromArgb(233, 169, 156)
                        CloseUpperAccent2 = Color.FromArgb(223, 149, 135)
                        CloseLowerAccent1 = Color.FromArgb(184, 67, 44)
                        CloseLowerAccent2 = Color.FromArgb(210, 127, 110)
                        CloseOuterBorder = Color.FromArgb(67, 20, 34)
                        CloseInnerBorder = Color.FromArgb(100, 255, 255, 255)

                    Else
                        Titlebar_Backcolor1 = Color.FromArgb(191, 205, 219)
                        Titlebar_Backcolor2 = Color.FromArgb(215, 228, 242)
                        Titlebar_OuterBorder = Color.FromArgb(76, 76, 76)
                        Titlebar_InnerBorder = Color.FromArgb(226, 230, 239)
                        Titlebar_Turquoise = Color.FromArgb(226, 230, 239)
                        OuterBorder = Color.FromArgb(76, 76, 76)

                        CloseUpperAccent1 = Color.FromArgb(189, 203, 218)
                        CloseLowerAccent2 = Color.FromArgb(205, 219, 234)
                        CloseOuterBorder = Color.FromArgb(131, 142, 168)
                        CloseInnerBorder = Color.FromArgb(209, 219, 229)
                    End If

                    Dim UpperPart As New Rectangle(Rect.X, Rect.Y, Rect.Width + 1, Sum_Ttl + 4)

                    G.SetClip(UpperPart)

                    Dim pth_back As New LinearGradientBrush(UpperPart, Titlebar_Backcolor1, Titlebar_Backcolor2, LinearGradientMode.Vertical)
                    Dim pth_line As New LinearGradientBrush(UpperPart, Titlebar_InnerBorder, Titlebar_Turquoise, LinearGradientMode.Vertical)

                    '### Render Titlebar
                    If Not ToolWindow Then
                        G.FillRoundedRect(pth_back, Rect, Radius, True)
                        Using P As New Pen(Titlebar_OuterBorder) : G.DrawRoundedRect(P, Rect, Radius, True) : End Using
                        Using P As New Pen(Titlebar_InnerBorder) : G.DrawRoundedRect(P, New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), Radius, True) : End Using
                        G.SetClip(New Rectangle(UpperPart.X + UpperPart.Width * 0.75, UpperPart.Y, UpperPart.Width * 0.75, UpperPart.Height))
                        Using P As New Pen(pth_line) : G.DrawRoundedRect(P, New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), Radius, True) : End Using
                    Else
                        G.FillRectangle(pth_back, Rect)
                        Using P As New Pen(Titlebar_OuterBorder) : G.DrawRectangle(P, Rect) : End Using
                        Using P As New Pen(Titlebar_InnerBorder) : G.DrawRectangle(P, New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)) : End Using
                        G.SetClip(New Rectangle(UpperPart.X + UpperPart.Width * 0.75, UpperPart.Y, UpperPart.Width * 0.75, UpperPart.Height))
                        Using P As New Pen(pth_line) : G.DrawRectangle(P, New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)) : End Using
                    End If

                    G.ResetClip()
                    G.ExcludeClip(UpperPart)

                    '### Render Rest of WindowR
                    Using br As New SolidBrush(Titlebar_Backcolor2) : G.FillRectangle(br, Rect) : End Using
                    Using P As New Pen(Titlebar_Turquoise) : G.DrawRectangle(P, New Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)) : End Using
                    Using P As New Pen(OuterBorder) : G.DrawRectangle(P, Rect) : End Using
                    Using P As New Pen(Titlebar_InnerBorder) : G.DrawLine(P, New Point(Rect.X + 1, Rect.Y), New Point(Rect.X + 1, Rect.Y + Rect.Height - 2)) : End Using
                    If Active Then
                        G.DrawImage(My.Resources.Win7Sides, RectSide1)
                        G.DrawImage(My.Resources.Win7Sides, RectSide2)
                    End If
                    G.ResetClip()
                    G.FillRectangle(Brushes.White, InnerWindow_1)
                    Using P As New Pen(Color.FromArgb(186, 210, 234)) : G.DrawRectangle(P, InnerWindow_1) : End Using
                    If Not WinVista Then
                        Using P As New Pen(Color.FromArgb(130, 135, 144)) : G.DrawRectangle(P, InnerWindow_2) : End Using
                    End If

                    '### Render Close ButtonR
                    Dim CloseRect As New Rectangle

                    Dim Btn_Height As Integer = Metrics_CaptionHeight + TitleTextH_Sum - 5
                    Dim Btn_Width As Integer

                    If Not ToolWindow Then
                        Btn_Width = (31 / 17) * Btn_Height
                    Else
                        Btn_Width = Btn_Height
                    End If

                    CloseRect = New Rectangle(InnerWindow_1.Right - Btn_Width - 3, InnerWindow_1.Top - Btn_Height - 3, Btn_Width, Btn_Height)

                    If Active Then
                        Dim Factor As Single = 0.45
                        If ToolWindow Then Factor = 0.2

                        Dim UH As Single = Factor * CloseRect.Height
                        Dim LH As Single = CloseRect.Height - UH
                        Dim Interlapping As Single = (UH / CloseRect.Height) * 10

                        Dim CloseRectUpperHalf As New Rectangle(CloseRect.X, CloseRect.Y, CloseRect.Width, UH + Interlapping)
                        Dim CloseUpperPath As New LinearGradientBrush(CloseRectUpperHalf, CloseUpperAccent1, CloseUpperAccent2, LinearGradientMode.Vertical)

                        Dim CloseRectLowerHalf As New Rectangle(CloseRect.X, CloseRectUpperHalf.Bottom - Interlapping, CloseRect.Width, LH)
                        Dim CloseLowerPath As New LinearGradientBrush(CloseRectLowerHalf, CloseLowerAccent1, CloseLowerAccent2, LinearGradientMode.Vertical)

                        G.FillRoundedRect(CloseUpperPath, CloseRectUpperHalf, 1, True)
                        G.FillRoundedRect(CloseLowerPath, CloseRectLowerHalf, 1, True)
                    Else
                        Dim ClosePath As New LinearGradientBrush(CloseRect, CloseUpperAccent1, CloseLowerAccent2, LinearGradientMode.Vertical)
                        G.FillRectangle(ClosePath, CloseRect)
                    End If


                    Dim CloseBtn As Image

                    If Not ToolWindow Then
                        If CloseRect.Height >= 22 Then
                            CloseBtn = My.Resources.Win7_Basic_Close_2
                        ElseIf CloseRect.Height >= 18 Then
                            CloseBtn = My.Resources.Win7_Basic_Close_1
                        Else
                            CloseBtn = My.Resources.Win7_Basic_Close_0
                        End If
                    Else
                        CloseBtn = My.Resources.Win7_Basic_Close_ToolWindow
                    End If


                    G.DrawImage(CloseBtn, New Point(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2 + 1, CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2))

                    Using P As New Pen(CloseOuterBorder) : G.DrawRoundedRect(P, CloseRect, 1, True) : End Using
                    Using P As New Pen(CloseInnerBorder) : G.DrawRoundedRect(P, New Rectangle(CloseRect.X + 1, CloseRect.Y + 1, CloseRect.Width - 2, CloseRect.Height - 2), 1, True) : End Using

                    IconRect = New Rectangle(InnerWindow_1.X + 4, CloseRect.Top + (CloseRect.Height - IconSize) / 2, IconSize, IconSize)

                    LabelRect = New Rectangle(IconRect.Right + 3, CloseRect.Y, UpperPart.Width - (IconRect.Right + 4), CloseRect.Height)

                    If ToolWindow Then LabelRect.X = IconRect.X
#End Region

                End If
#End Region

            ElseIf Preview = Preview_Enum.WXP Then
#Region "Windows XP"
                Dim sm As SmoothingMode = G.SmoothingMode
                G.SmoothingMode = SmoothingMode.HighSpeed

                TitlebarRect = New Rectangle(Rect.X, Rect.Y, Rect.Width, TitleTextH_Sum + _Metrics_BorderWidth + _Metrics_CaptionHeight + 5)

                Dim innerRect As New Rectangle(Rect.X, Rect.Y + TitlebarRect.Height - 1, Rect.Width - 2, Rect.Height - TitlebarRect.Height - 1)

                Using br As New SolidBrush(My.resVS.Colors.Btnface) : G.FillRectangle(br, innerRect) : End Using

                My.resVS.Draw(G, TitlebarRect, VisualStylesRes.Element.Titlebar, Active, ToolWindow)

                Dim LE As New Rectangle(Rect.X, Rect.Y + TitlebarRect.Height - 1, Math.Max(4, Metrics_BorderWidth), Rect.Height - TitlebarRect.Height - Math.Max(4, Metrics_BorderWidth) + 2)
                Dim RE As New Rectangle(Rect.X + Rect.Width - Math.Max(4, Metrics_BorderWidth) - 1, Rect.Y + TitlebarRect.Height - 1, Math.Max(4, Metrics_BorderWidth), Rect.Height - TitlebarRect.Height - Metrics_BorderWidth + 2)
                Dim BE As New Rectangle(Rect.X, Rect.Y + Rect.Height - Math.Max(4, Metrics_BorderWidth), Rect.Width - 1, Math.Max(4, Metrics_BorderWidth) + 1)
                Dim CloseBtn_W As Integer = TitleTextH_Sum + _Metrics_CaptionHeight - 4
                Dim CB As New Rectangle(Rect.X + Rect.Width - CloseBtn_W - RE.Width - 2, Rect.Y + TitlebarRect.Height - 4 - CloseBtn_W, CloseBtn_W, CloseBtn_W)

                If Not ToolWindow Then
                    LabelRect = New Rectangle(Rect.X + LE.Width + 20, Rect.Y + TitlebarRect.Height - 4 - CloseBtn_W, Rect.Width - CloseBtn_W - LE.Width - RE.Width, CloseBtn_W)
                Else
                    LabelRect = New Rectangle(Rect.X + LE.Width + 2, Rect.Y + TitlebarRect.Height - 4 - CloseBtn_W, Rect.Width - CloseBtn_W - LE.Width - RE.Width, CloseBtn_W)
                End If

                IconRect = New Rectangle(Rect.X + LE.Width + 2, Rect.Y + (TitlebarRect.Height - 14) / 2, 14, 14)

                My.resVS.Draw(G, TitlebarRect, VisualStylesRes.Element.Titlebar, Active, ToolWindow)
                My.resVS.Draw(G, LE, VisualStylesRes.Element.LeftEdge, Active, ToolWindow)
                My.resVS.Draw(G, RE, VisualStylesRes.Element.RightEdge, Active, ToolWindow)
                My.resVS.Draw(G, BE, VisualStylesRes.Element.BottomEdge, Active, ToolWindow)
                My.resVS.Draw(G, CB, VisualStylesRes.Element.CloseButton, Active, ToolWindow)

                G.SmoothingMode = sm
#End Region

            End If

            Dim ForeColorX As Color
            Dim closeImg As Bitmap

            If AccentColor_Enabled Then
                If Active Then
                    ForeColorX = If(AccentColor_Active.IsDark, Color.White, Color.Black)
                    closeImg = If(AccentColor_Active.IsDark, My.Resources.Win10x_Close_Dark, My.Resources.Win10x_Close_Light)
                Else
                    ForeColorX = If(AccentColor_Inactive.IsDark, Color.FromArgb(115, 115, 115), Color.Black)
                    closeImg = If(AccentColor_Inactive.IsDark, My.Resources.Win10x_Close_Dark, My.Resources.Win10x_Close_Light)
                End If

            Else
                If Active Then
                    If Preview = Preview_Enum.W11 Then
                        ForeColorX = If(DarkMode, Color.White, Color.Black)
                        closeImg = If(DarkMode, My.Resources.Win10x_Close_Dark, My.Resources.Win10x_Close_Light)
                    Else
                        If DarkMode Then
                            ForeColorX = Color.White
                            closeImg = My.Resources.Win10x_Close_Dark
                        Else
                            ForeColorX = Color.Black
                            closeImg = My.Resources.Win10x_Close_Light
                        End If
                    End If
                Else
                    ForeColorX = Color.FromArgb(115, 115, 115)
                    closeImg = If(DarkMode, My.Resources.Win10x_Close_Dark, My.Resources.Win10x_Close_Light)
                End If
            End If

            If Not ToolWindow Then G.DrawImage(If(Active, My.Resources.SampleApp_Small_Active, My.Resources.SampleApp_Small_Inactive), IconRect)

            If Preview = Preview_Enum.W11 Or Preview = Preview_Enum.W10 Then
                Using br As New SolidBrush(ForeColorX) : G.DrawString(Text, Font, br, LabelRect, ContentAlignment.MiddleLeft.ToStringFormat) : End Using

                If Not ToolWindow Then
                    Dim r As New Rectangle(XRect.X + (XRect.Width - closeImg.Width) / 2, XRect.Y + (XRect.Height - closeImg.Height) / 2, closeImg.Width, closeImg.Height)
                    G.DrawImage(closeImg, r)
                Else
                    Dim XXRect As New Rectangle(Rect.X + Rect.Width - 2 - (TitlebarRect.Height - 12), Rect.Y + 6, TitlebarRect.Height - 12, TitlebarRect.Height - 12)

                    Using br As New SolidBrush(Color.FromArgb(199, 80, 80)) : G.FillRectangle(br, XXRect) : End Using

                    If XXRect.Width >= 12 Then
                        If XXRect.Width Mod 2 = 0 Then
                            XXRect.X += 1
                            XXRect.Y += 1
                        End If
                    Else
                        XXRect.X += 1
                    End If

                    Using br As New SolidBrush(Color.White) : G.DrawString("r", New Font("Marlett", 6.35, FontStyle.Regular), br, New Rectangle(XXRect.X + 1, XXRect.Y + 1, XXRect.Width, XXRect.Height), ContentAlignment.MiddleCenter.ToStringFormat) : End Using
                End If

            ElseIf Preview = Preview_Enum.W8 Then
                Using br As New SolidBrush(Color.Black) : G.DrawString(Text, Font, br, LabelRect8, ContentAlignment.MiddleCenter.ToStringFormat) : End Using

            ElseIf Preview = Preview_Enum.W8Lite Then
                If Active Then
                    Using br As New SolidBrush(My.CP.Win32.TitleText) : G.DrawString(Text, Font, br, LabelRect8, ContentAlignment.MiddleCenter.ToStringFormat) : End Using
                Else
                    Using br As New SolidBrush(My.CP.Win32.InactiveTitleText) : G.DrawString(Text, Font, br, LabelRect8, ContentAlignment.MiddleCenter.ToStringFormat) : End Using
                End If

            ElseIf Preview = Preview_Enum.W7Aero Or Preview = Preview_Enum.W7Opaque Then
                Dim LabelRectModified As Rectangle = LabelRect
                LabelRectModified.X -= 2
                LabelRectModified.Y -= 1
                Dim alpha As Integer = If(Active, 120, 75)
                G.DrawGlowString(1, Text, Font, Color.Black, Color.FromArgb(alpha, Color.White), RectBK, LabelRectModified, ContentAlignment.MiddleLeft.ToStringFormat)

            ElseIf Preview = Preview_Enum.W7Basic Then
                Using br As New SolidBrush(If(Active, Color.Black, Color.FromArgb(76, 76, 76))) : G.DrawString(Text, Font, br, LabelRect, ContentAlignment.MiddleLeft.ToStringFormat) : End Using

            ElseIf Preview = Preview_Enum.WXP Then
                Using br As New SolidBrush(Color.Black) : G.DrawString(Text, Font, br, New Rectangle(LabelRect.X + 1, LabelRect.Y, LabelRect.Width, LabelRect.Height), ContentAlignment.MiddleLeft.ToStringFormat) : End Using
                Using br As New SolidBrush(Color.White) : G.DrawString(Text, Font, br, LabelRect, ContentAlignment.MiddleLeft.ToStringFormat) : End Using

            End If

        End Sub

        Private Sub Window_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            If Not DesignMode Then
                Try : AddHandler Parent.BackgroundImageChanged, AddressOf ProcessBack : Catch : End Try
                Try : AddHandler FontChanged, AddressOf AdjustPadding : Catch : End Try
            End If

            ProcessBack()
        End Sub

        Private Sub Window_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
            If Not DesignMode Then
                Try : RemoveHandler Parent.BackgroundImageChanged, AddressOf ProcessBack : Catch : End Try
                Try : RemoveHandler FontChanged, AddressOf AdjustPadding : Catch : End Try
            End If
        End Sub

        Sub ProcessBack()
            Try
                If Preview = Preview_Enum.W11 Then
                    If My.Wallpaper IsNot Nothing Then
                        Dim b As Bitmap = New Bitmap(My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat)).Blur(15)
                        If DarkMode Then
                            If b IsNot Nothing Then
                                Using ImgF As New ImageProcessor.ImageFactory
                                    ImgF.Load(b)
                                    ImgF.Saturation(15)
                                    ImgF.Brightness(-10)
                                    AdaptedBackBlurred = ImgF.Image.Clone
                                End Using
                            End If

                        Else
                            AdaptedBackBlurred = b
                        End If
                    End If

                ElseIf Preview = Preview_Enum.W7Aero Then
                    If My.Wallpaper IsNot Nothing Then AdaptedBackBlurred = New Bitmap(My.Wallpaper.Clone(Bounds, My.Wallpaper.PixelFormat)).Blur(1)
                    Try : Noise7 = My.Resources.AeroGlass.Fade(Win7Noise / 100) : Catch : End Try

                ElseIf Preview = Preview_Enum.W7Opaque Then
                    Try : Noise7 = My.Resources.AeroGlass.Fade(Win7Noise / 100) : Catch : End Try

                End If
            Catch
            End Try

        End Sub
    End Class

End Namespace