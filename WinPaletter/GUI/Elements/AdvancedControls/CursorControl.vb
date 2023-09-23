Imports System.Drawing.Imaging

Public Class CursorControl : Inherits ContainerControl
    Sub New()

    End Sub

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

    Private _Shown As Boolean = False

    Public _Focused As Boolean = False

    Dim bmp As Bitmap

    Public Angle As Single = 180

    Private Sub CursorControl_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

        Try
            If Not DesignMode Then
                AddHandler FindForm.Load, AddressOf Loaded
                AddHandler FindForm.Shown, AddressOf Showed
                AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
            End If
        Catch
        End Try

    End Sub

    Private Sub CursorControl_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
        Try
            If Not DesignMode Then
                RemoveHandler FindForm.Load, AddressOf Loaded
                RemoveHandler FindForm.Shown, AddressOf Showed
                RemoveHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
            End If
        Catch
        End Try
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

        Dim CenterRect As New Rectangle(MainRect.X + (MainRect.Width - bmp.Width) / 2,
                                        MainRect.Y + (MainRect.Height - bmp.Height) / 2,
                                        bmp.Width, bmp.Height)

        e.Graphics.Clear(GetParentColor)
        e.Graphics.FillRoundedRect(New SolidBrush(If(_Focused, My.Style.Colors.Back_Checked, My.Style.Colors.Back)), MainRect)
        e.Graphics.DrawRoundedRect_LikeW11(New Pen(If(_Focused, My.Style.Colors.Border_Checked_Hover, My.Style.Colors.Border)), MainRect)
        e.Graphics.DrawImage(bmp, CenterRect)

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

End Class