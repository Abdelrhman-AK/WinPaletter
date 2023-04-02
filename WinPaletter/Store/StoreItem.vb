Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

<DefaultEvent("Click")>
Public Class StoreItem : Inherits Panel

    Sub New()
        Font = New Font("Segoe UI", 9)
        DoubleBuffered = True
    End Sub

    Public Event CPChanged(sender As Object, e As EventArgs)

    Private _CP As CP
    Public Property CP As CP
        Get
            Return _CP
        End Get
        Set(value As CP)
            If value IsNot Nothing Then
                _CP = value.Clone
                RaiseEvent CPChanged(Me, New EventArgs())
                Invalidate()
            End If
        End Set
    End Property

    Public MD5 As String
    Public URL As String

#Region "Events"
    Enum MouseState
        None
        Over
        Down
    End Enum

    Public State As MouseState = MouseState.None
    Private AnimateOnClick As Boolean = False

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        AnimateOnClick = True
        State = MouseState.Down
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonRadioButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        State = MouseState.Over
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub

    Private Sub XenonCheckBox_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        State = MouseState.None
        Tmr.Enabled = True
        Tmr.Start()
        Invalidate()
    End Sub
#End Region

#Region "Animator"
    Dim alpha As Integer
    ReadOnly Factor As Integer = 25
    Dim WithEvents Tmr As New Timer With {.Enabled = False, .Interval = 1}

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If Not DesignMode Then

            If State = MouseState.Over Then
                If alpha + Factor <= 255 Then
                    alpha += Factor
                ElseIf alpha + Factor > 255 Then
                    alpha = 255
                    Tmr.Enabled = False
                    Tmr.Stop()
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
                    Tmr.Enabled = False
                    Tmr.Stop()
                    AnimateOnClick = False
                End If

                Threading.Thread.Sleep(1)
                Invalidate()
            End If
        End If
    End Sub
#End Region

    Private BackgroundImageBlurred As Image
    ReadOnly Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.5))

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.SystemDefault
        DoubleBuffered = True
        G.Clear(GetParentColor)
        Dim rect_outer As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim rect_inner As New Rectangle(1, 1, Width - 3, Height - 3)


        Dim bkC As Color = If(State <> MouseState.None, Style.Colors.Back_Checked, Style.Colors.Back)
        Dim bkCC As Color = Color.FromArgb(alpha, Style.Colors.Back_Checked)

        G.FillRoundedRect(New SolidBrush(bkC), rect_inner)
        G.FillRoundedRect(New SolidBrush(bkCC), rect_outer)
        'Work here to add loading indicator
        ''
        ''

        If BackgroundImage IsNot Nothing Then
            G.DrawRoundImage(BackgroundImage, rect_inner)
        Else
            'TextRenderer.DrawText(G, "Generating preview", New Font("Segoe UI", 12, FontStyle.Regular), rect_inner, Color.White, Color.Transparent, TextFormatFlags.WordEllipsis Or TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        End If

        Dim lC As Color = Color.FromArgb(255 - alpha, If(State <> MouseState.None, Style.Colors.Border_Checked, Style.Colors.Border))
        Dim lCC As Color = Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)

        G.DrawRoundedRect_LikeW11(New Pen(lC), rect_inner)
        G.DrawRoundedRect_LikeW11(New Pen(lCC), rect_outer)


        If State <> MouseState.None Then
            If BackgroundImageBlurred IsNot Nothing Then G.DrawRoundImage(BackgroundImageBlurred.Fade(alpha / 255), rect_inner)
            G.FillRoundedRect(Noise, rect_inner)
        End If

        Dim ThemeName_Rect As New Rectangle(rect_inner.X + 5, rect_inner.Y + 5, rect_inner.Width - 10, 30)
        Dim Author_Rect As New Rectangle(ThemeName_Rect.X + 5, ThemeName_Rect.Bottom, ThemeName_Rect.Width - 5, 15)

        If CP IsNot Nothing Then
            TextRenderer.DrawText(G, CP.Info.PaletteName, New Font("Segoe UI", 12, FontStyle.Bold), ThemeName_Rect, Color.White, Color.Transparent, TextFormatFlags.WordEllipsis)
            TextRenderer.DrawText(G, My.Lang.By & " " & CP.Info.Author, New Font("Segoe UI", 9, FontStyle.Regular), Author_Rect, Color.White, Color.Transparent, TextFormatFlags.WordEllipsis)

        End If

    End Sub


    Private Sub StoreItem_BackgroundImageChanged(sender As Object, e As EventArgs) Handles Me.BackgroundImageChanged
        If BackgroundImage IsNot Nothing Then BackgroundImageBlurred = BackgroundImage.Blur(2)
        Invalidate()
    End Sub
End Class
