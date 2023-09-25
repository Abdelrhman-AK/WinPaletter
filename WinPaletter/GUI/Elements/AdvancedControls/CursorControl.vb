Imports System.Drawing.Imaging

Public Class CursorControl : Inherits ContainerControl

    Sub New()

    End Sub

#Region "Variables"

    Public _Focused As Boolean = False
    Dim bmp As Bitmap
    Public Angle As Single = 180
    Private AnimateOnClick As Boolean = False

    Public State As MouseState = MouseState.None

    Enum MouseState
        None
        Over
        Down
    End Enum

#End Region

#Region "Properties"

    Public Property Prop_Cursor As Paths.CursorType = CursorType.Arrow
    Public Property Prop_ArrowStyle As Paths.ArrowStyle = ArrowStyle.Aero
    Public Property Prop_CircleStyle As Paths.CircleStyle = CircleStyle.Aero
    Public Property Prop_PrimaryColor1 As Color = Color.White
    Public Property Prop_PrimaryColor2 As Color = Color.White
    Public Property Prop_PrimaryColorGradient As Boolean = False
    Public Property Prop_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Prop_PrimaryNoise As Boolean = False
    Public Property Prop_PrimaryNoiseOpacity As Single = 0.25

    Public Property Prop_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Prop_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Prop_SecondaryColorGradient As Boolean = False
    Public Property Prop_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Prop_SecondaryNoise As Boolean = False
    Public Property Prop_SecondaryNoiseOpacity As Single = 0.25

    Public Property Prop_LoadingCircleBack1 As Color = Color.FromArgb(42, 151, 243)
    Public Property Prop_LoadingCircleBack2 As Color = Color.FromArgb(42, 151, 243)
    Public Property Prop_LoadingCircleBackGradient As Boolean = False
    Public Property Prop_LoadingCircleBackGradientMode As GradientMode = GradientMode.Vertical
    Public Property Prop_LoadingCircleBackNoise As Boolean = False
    Public Property Prop_LoadingCircleBackNoiseOpacity As Single = 0.25

    Public Property Prop_LoadingCircleHot1 As Color = Color.FromArgb(37, 204, 255)
    Public Property Prop_LoadingCircleHot2 As Color = Color.FromArgb(37, 204, 255)
    Public Property Prop_LoadingCircleHotGradient As Boolean = False
    Public Property Prop_LoadingCircleHotGradientMode As GradientMode = GradientMode.Vertical
    Public Property Prop_LoadingCircleHotNoise As Boolean = False
    Public Property Prop_LoadingCircleHotNoiseOpacity As Single = 0.25

    Public Property Prop_Shadow_Enabled As Boolean = False
    Public Property Prop_Shadow_Color As Color = Color.Black
    Public Property Prop_Shadow_Blur As Integer = 5
    Public Property Prop_Shadow_Opacity As Single = 0.3
    Public Property Prop_Shadow_OffsetX As Integer = 2
    Public Property Prop_Shadow_OffsetY As Integer = 2

    Public Property Prop_Scale As Single = 1

#End Region

#Region "Events"

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        AnimateOnClick = True
        _Focused = True
        State = MouseState.Down
        Timer.Enabled = True
        Timer.Start()
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        State = MouseState.Over
        Timer.Enabled = True
        Timer.Start()
        Invalidate()
    End Sub

    Private Sub CursorControl_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        State = MouseState.Over
        Timer.Enabled = True
        Timer.Start()
        Invalidate()
    End Sub

    Private Sub CursorControl_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        State = MouseState.None
        Timer.Enabled = True
        Timer.Start()
        Invalidate()
    End Sub

    Private Sub CursorControl_Click(sender As Object, e As EventArgs) Handles Me.Click

        For Each c As CursorControl In Parent.Controls.OfType(Of CursorControl)
            If c Is sender Then
                c._Focused = True
                c.Invalidate()
            Else
                c._Focused = False
                c.Invalidate()
            End If
        Next

    End Sub

#End Region

#Region "Animator"
    Dim alpha As Integer
    ReadOnly Factor As Integer = 25
    Dim WithEvents Timer As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        If Not DesignMode Then

            If State = MouseState.Over Then
                If alpha + Factor <= 255 Then
                    alpha += Factor
                ElseIf alpha + Factor > 255 Then
                    alpha = 255
                    Timer.Enabled = False
                    Timer.Stop()
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
                    Timer.Enabled = False
                    Timer.Stop()
                    AnimateOnClick = False
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If
        End If
    End Sub
#End Region

    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        Dim CurOptions As New CursorOptions With {
            .Cursor = Prop_Cursor,
            .ArrowStyle = Prop_ArrowStyle,
            .CircleStyle = Prop_CircleStyle,
            .PrimaryColor1 = Prop_PrimaryColor1,
            .PrimaryColor2 = Prop_PrimaryColor2,
            .PrimaryColorGradient = Prop_PrimaryColorGradient,
            .PrimaryColorGradientMode = Prop_PrimaryColorGradientMode,
            .SecondaryColor1 = Prop_SecondaryColor1,
            .SecondaryColor2 = Prop_SecondaryColor2,
            .SecondaryColorGradient = Prop_SecondaryColorGradient,
            .SecondaryColorGradientMode = Prop_SecondaryColorGradientMode,
            .LoadingCircleBack1 = Prop_LoadingCircleBack1,
            .LoadingCircleBack2 = Prop_LoadingCircleBack2,
            .LoadingCircleBackGradient = Prop_LoadingCircleBackGradient,
            .LoadingCircleBackGradientMode = Prop_LoadingCircleBackGradientMode,
            .LoadingCircleHot1 = Prop_LoadingCircleHot1,
            .LoadingCircleHot2 = Prop_LoadingCircleHot2,
            .LoadingCircleHotGradient = Prop_LoadingCircleHotGradient,
            .LoadingCircleHotGradientMode = Prop_LoadingCircleHotGradientMode,
            .PrimaryNoise = Prop_PrimaryNoise,
            .PrimaryNoiseOpacity = Prop_PrimaryNoiseOpacity,
            .SecondaryNoise = Prop_SecondaryNoise,
            .SecondaryNoiseOpacity = Prop_SecondaryNoiseOpacity,
            .LoadingCircleBackNoise = Prop_LoadingCircleBackNoise,
            .LoadingCircleBackNoiseOpacity = Prop_LoadingCircleBackNoiseOpacity,
            .LoadingCircleHotNoise = Prop_LoadingCircleHotNoise,
            .LoadingCircleHotNoiseOpacity = Prop_LoadingCircleHotNoiseOpacity,
            .LineThickness = 1,
            .Scale = Prop_Scale,
            ._Angle = Angle,
            .Shadow_Enabled = Prop_Shadow_Enabled,
            .Shadow_Blur = Prop_Shadow_Blur,
            .Shadow_Color = Prop_Shadow_Color,
            .Shadow_Opacity = Prop_Shadow_Opacity,
            .Shadow_OffsetX = Prop_Shadow_OffsetX,
            .Shadow_OffsetY = Prop_Shadow_OffsetY}

        bmp = New Bitmap(32 * Prop_Scale, 32 * Prop_Scale, PixelFormat.Format32bppPArgb)

        bmp = Draw(CurOptions)

        DoubleBuffered = True

        Dim MainRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim MainRectInner As New Rectangle(1, 1, Width - 3, Height - 3)

        Dim CenterRect As New Rectangle(MainRect.X + (MainRect.Width - bmp.Width) / 2,
                                        MainRect.Y + (MainRect.Height - bmp.Height) / 2,
                                        bmp.Width, bmp.Height)


        Dim bkC As Color = If(_Focused, My.Style.Colors.Back_Checked, My.Style.Colors.Back)
        Dim bkCC As Color = Color.FromArgb(alpha, My.Style.Colors.Back_Checked)

        Using br As New SolidBrush(bkC) : e.Graphics.FillRoundedRect(br, MainRectInner) : End Using
        Using br As New SolidBrush(bkCC) : e.Graphics.FillRoundedRect(br, MainRect) : End Using

        Dim lC As Color = Color.FromArgb(255 - alpha, If(_Focused, My.Style.Colors.Border_Checked, My.Style.Colors.Border))
        Dim lCC As Color = Color.FromArgb(alpha, My.Style.Colors.Border_Checked_Hover)

        Using P As New Pen(lC) : e.Graphics.DrawRoundedRect_LikeW11(P, MainRectInner) : End Using
        Using P As New Pen(lCC) : e.Graphics.DrawRoundedRect_LikeW11(P, MainRect) : End Using

        e.Graphics.DrawImage(bmp, CenterRect)

    End Sub

End Class