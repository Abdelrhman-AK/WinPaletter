Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports ImageProcessor
Imports WinPaletter.XenonCore

<DefaultEvent("Click")>
Public Class StoreItem : Inherits Panel

    Sub New()
        Font = New Font("Segoe UI", 9)
        DoubleBuffered = True
    End Sub

#Region "Properties"
    Public Event CPChanged(sender As Object, e As EventArgs)
    Private _CP As CP
    Public Property CP As CP
        Get
            Return _CP
        End Get
        Set(value As CP)
            If value IsNot Nothing Then
                _CP = value.Clone
                UpdateBadges()
                UpdatePattern(_CP.Info.Pattern)
                RaiseEvent CPChanged(Me, New EventArgs())
            End If
        End Set
    End Property

    Public Property MD5_ThemeFile As String
    Public Property MD5_PackFile As String
    Public Property URL_ThemeFile As String
    Public Property URL_PackFile As String
    Public Property FileName As String
    Public Property DoneByWinPaletter As Boolean = False

    Public Sub UpdateBadges()
        DesignedFor_Badges.Clear()
        If _CP IsNot Nothing Then
            If _CP.Info.DesignedFor_Win11 Then DesignedFor_Badges.Add(My.Resources.Store_DesignedFor11)
            If _CP.Info.DesignedFor_Win10 Then DesignedFor_Badges.Add(My.Resources.Store_DesignedFor10)
            If _CP.Info.DesignedFor_Win81 Then DesignedFor_Badges.Add(My.Resources.Store_DesignedFor8)
            If _CP.Info.DesignedFor_Win7 Then DesignedFor_Badges.Add(My.Resources.Store_DesignedFor7)
            If _CP.Info.DesignedFor_WinVista Then DesignedFor_Badges.Add(My.Resources.Store_DesignedForVista)
            If _CP.Info.DesignedFor_WinXP Then DesignedFor_Badges.Add(My.Resources.Store_DesignedForXP)
        End If
        Refresh()
    End Sub

    Public Sub UpdatePattern(val As Integer)
        Dim bmp As Bitmap

        Select Case val
            Case 0
                Using x As New Bitmap(Width, Height)
                    bmp = x.Clone
                End Using

            Case 1
                bmp = My.Resources.Store_Pattern1
            Case 2
                bmp = My.Resources.Store_Pattern2
            Case 3
                bmp = My.Resources.Store_Pattern3
            Case 4
                bmp = My.Resources.Store_Pattern4
            Case 5
                bmp = My.Resources.Store_Pattern5
            Case 6
                bmp = My.Resources.Store_Pattern6
            Case 7
                bmp = My.Resources.Store_Pattern7
            Case 8
                bmp = My.Resources.Store_Pattern8
            Case 9
                bmp = My.Resources.Store_Pattern9
            Case 10
                bmp = My.Resources.Store_Pattern10
            Case Else
                Using x As New Bitmap(Width, Height)
                    bmp = x.Clone
                End Using

        End Select

        If Not GetDarkMode() Then
            Using imgF As New ImageFactory
                imgF.Load(bmp)
                imgF.Contrast(50)
                bmp = CType(imgF.Image, Bitmap).Invert
            End Using
        End If
        pattern = New TextureBrush(bmp)

        Refresh()
    End Sub
#End Region

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
    ReadOnly Factor As Integer = 40
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

    Private Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.65))
    Private DesignedFor_Badges As New List(Of Bitmap)
    Private pattern As New TextureBrush(New Bitmap(Width, Height))

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics

        G.SmoothingMode = SmoothingMode.AntiAlias

        If CP IsNot Nothing Then
            G.TextRenderingHint = If(CP.MetricsFonts.Fonts_SingleBitPP, TextRenderingHint.SingleBitPerPixelGridFit, TextRenderingHint.ClearTypeGridFit)
        Else
            G.TextRenderingHint = My.RenderingHint
        End If

        DoubleBuffered = True
        G.Clear(GetParentColor)

        Dim rect_outer As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim rect_inner As New Rectangle(1, 1, Width - 3, Height - 3)

        Dim bkC As Color = Color.FromArgb(255 - alpha, Style.Colors.Back)
        Dim bkCC As Color = bkC

        If CP IsNot Nothing Then
            If GetDarkMode() Then
                bkCC = Color.FromArgb(alpha, CP.Info.Color1.Dark)
            Else
                bkCC = Color.FromArgb(alpha, CP.Info.Color1.Light)
            End If
        End If

        G.FillRoundedRect(New SolidBrush(bkC), rect_inner)
        G.FillRoundedRect(New SolidBrush(bkCC), rect_outer)
        If pattern IsNot Nothing Then G.FillRoundedRect(pattern, rect_inner)

        Dim factor_max As Single = 20
        Dim factor1 As Single = factor_max * (alpha / 255)
        Dim factor2 As Single = factor_max * ((255 - alpha) / 255)
        Dim CircleR As Integer = rect_inner.Height * 0.4

        Dim Circle1 As New Rectangle(rect_inner.X + 10 + factor_max - factor1, rect_inner.Y + (rect_inner.Height - CircleR) / 2, CircleR, CircleR)
        Dim Circle2 As New Rectangle(rect_inner.X + 10 + CircleR + CircleR * 0.4 - factor2, rect_inner.Y + (rect_inner.Height - CircleR) / 2, CircleR, CircleR)

        If CP IsNot Nothing Then
            G.FillEllipse(New SolidBrush(Color.FromArgb(150, CP.Info.Color2)), Circle2)
            G.DrawEllipse(New Pen(CP.Info.Color2.CB(0.3)), Circle2)

            G.FillEllipse(New SolidBrush(Color.FromArgb(150, CP.Info.Color1)), Circle1)
            G.DrawEllipse(New Pen(CP.Info.Color1.CB(0.3)), Circle1)
        End If

        If BackgroundImage IsNot Nothing Then
            Select Case State
                Case MouseState.Over
                    G.DrawRoundImage(BackgroundImage, rect_outer)

                Case Else
                    G.DrawRoundImage(BackgroundImage, rect_inner)

            End Select
        End If

        If State <> MouseState.None Then G.FillRoundedRect(Noise, rect_inner)

        Dim lC As Color = Color.FromArgb(255 - alpha, If(State <> MouseState.None, Style.Colors.Border_Checked, Style.Colors.Border))
        Dim lCC As Color = Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)

        G.DrawRoundedRect_LikeW11(New Pen(lC), rect_inner)
        G.DrawRoundedRect_LikeW11(New Pen(lCC), rect_outer)

        Dim ThemeName_Rect As New Rectangle(rect_inner.X, rect_inner.Y, rect_inner.Width - 10, 25)
        Dim Author_Rect As New Rectangle(ThemeName_Rect.X, ThemeName_Rect.Bottom, ThemeName_Rect.Width - 20, 15)

        If CP IsNot Nothing Then
            Dim FC As Color = Color.FromArgb(Math.Max(125, alpha), If(bkC.IsDark, Color.White, Color.Black))
            G.DrawString(CP.Info.ThemeName, New Font(CP.MetricsFonts.CaptionFont.Name, 11, FontStyle.Bold), New SolidBrush(FC), ThemeName_Rect, StringAligner(ContentAlignment.MiddleRight))

            Dim BadgeRect As New Rectangle(Author_Rect.Right + 2, Author_Rect.Y, 16, 16)

            If DoneByWinPaletter Then
                G.DrawImage(My.Resources.Store_DoneByWinPaletter, BadgeRect)
            Else
                G.DrawImage(My.Resources.Store_DoneByUser, BadgeRect)
            End If

            Dim author As String
            author = If(DoneByWinPaletter, My.Application.Info.ProductName, CP.Info.Author)
            G.DrawString(My.Lang.By & " " & author, New Font(CP.MetricsFonts.CaptionFont.Name, 9, FontStyle.Regular), New SolidBrush(FC), Author_Rect, StringAligner(ContentAlignment.MiddleRight))

            For i = 0 To DesignedFor_Badges.Count - 1
                G.DrawImage(DesignedFor_Badges(i), New Rectangle(BadgeRect.Right - 16 - 18 * i, Author_Rect.Bottom + 7, 16, 16))
            Next

        End If


    End Sub

End Class



