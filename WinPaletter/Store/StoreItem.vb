Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

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
                RaiseEvent CPChanged(Me, New EventArgs())
            End If
        End Set
    End Property

    Public Property MD5 As String
    Public Property URL As String
    Public Property FileName As String
    Public Property DoneByWinPaletter As Boolean = False

    Public Sub UpdateBadges()
        DesignedFor_Badges.Clear()
        If _CP.StoreInfo.DesignedFor_Win11 Then DesignedFor_Badges.Add(My.Resources.Store_DesignedFor11)
        If _CP.StoreInfo.DesignedFor_Win10 Then DesignedFor_Badges.Add(My.Resources.Store_DesignedFor10)
        If _CP.StoreInfo.DesignedFor_Win8 Then DesignedFor_Badges.Add(My.Resources.Store_DesignedFor8)
        If _CP.StoreInfo.DesignedFor_Win7 Then DesignedFor_Badges.Add(My.Resources.Store_DesignedFor7)
        If _CP.StoreInfo.DesignedFor_WinVista Then DesignedFor_Badges.Add(My.Resources.Store_DesignedForVista)
        If _CP.StoreInfo.DesignedFor_WinXP Then DesignedFor_Badges.Add(My.Resources.Store_DesignedForXP)
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

    Private Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.5))
    Private DesignedFor_Badges As New List(Of Bitmap)

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G As Graphics = e.Graphics

        G.SmoothingMode = SmoothingMode.AntiAlias
        G.TextRenderingHint = TextRenderingHint.SystemDefault
        DoubleBuffered = True
        G.Clear(GetParentColor)
        Dim rect_outer As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim rect_inner As New Rectangle(1, 1, Width - 3, Height - 3)

        Dim bkC As Color = If(State <> MouseState.None, Style.Colors.Back_Checked, Style.Colors.Back)
        Dim bkCC As Color = Color.FromArgb(alpha, CP.StoreInfo.Color1)

        G.FillRoundedRect(New SolidBrush(bkC), rect_inner)
        G.FillRoundedRect(New SolidBrush(bkCC), rect_outer)
        G.FillRoundedRect(New SolidBrush(CP.StoreInfo.Color1), New Rectangle(rect_inner.X, rect_inner.Y, 10, rect_inner.Height))
        G.FillRoundedRect(New SolidBrush(CP.StoreInfo.Color2), New Rectangle(rect_inner.X + 10, rect_inner.Y, 10, rect_inner.Height))

        If BackgroundImage IsNot Nothing Then G.DrawRoundImage(BackgroundImage, rect_inner)

        If State <> MouseState.None Then G.FillRoundedRect(Noise, rect_inner)

        Dim lC As Color = Color.FromArgb(255 - alpha, If(State <> MouseState.None, Style.Colors.Border_Checked, Style.Colors.Border))
        Dim lCC As Color = Color.FromArgb(alpha, Style.Colors.Border_Checked_Hover)

        G.DrawRoundedRect_LikeW11(New Pen(lC), rect_inner)
        G.DrawRoundedRect_LikeW11(New Pen(lCC), rect_outer)

        Dim ThemeName_Rect As New Rectangle(rect_inner.X, rect_inner.Y, rect_inner.Width - 10, 25)
        Dim Author_Rect As New Rectangle(ThemeName_Rect.X, ThemeName_Rect.Bottom - 5, ThemeName_Rect.Width, 15)

        If CP IsNot Nothing Then
            Dim FC As Color = Color.FromArgb(Math.Max(125, alpha), If(bkC.IsDark, Color.White, Color.Black))
            G.DrawString(CP.Info.PaletteName, New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(FC), ThemeName_Rect, StringAligner(ContentAlignment.MiddleRight))
            G.DrawString(My.Lang.By & " " & CP.Info.Author, New Font("Segoe UI", 9, FontStyle.Regular), New SolidBrush(FC), Author_Rect, StringAligner(ContentAlignment.MiddleRight))

            For i = 0 To DesignedFor_Badges.Count - 1
                G.DrawImage(DesignedFor_Badges(i), New Rectangle(Author_Rect.Right - 16 - 18 * i, Author_Rect.Bottom + 3, 16, 16))
            Next

            Dim BadgeRect As New Rectangle(Author_Rect.Right - 16, Author_Rect.Bottom + 3 + 16 + 3, 16, 16)

            If DoneByWinPaletter Then
                G.DrawImage(My.Resources.Store_DoneByWinPaletter, BadgeRect)
            Else
                G.DrawImage(My.Resources.Store_DoneByUser, BadgeRect)
            End If

        End If


    End Sub

End Class



