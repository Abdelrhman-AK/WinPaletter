Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.Controllers

    <DefaultEvent("Click")> Public Class ColorItem : Inherits Panel

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
            DoubleBuffered = True
            Text = ""
            ColorsHistory.Clear()
        End Sub

#Region "Properties"
        Public Property DefaultColor As Color = Color.Black
        Public Property DontShowInfo As Boolean = False

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        <DefaultValue("")>
        Public Overrides Property Text As String = ""

#End Region

#Region "Rectangles"
        Private Rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Private RectInner As New Rectangle(1, 1, Width - 3, Height - 3)
        Private Rect_DefColor As New Rectangle(0, 0, Height, Height)
        Protected Overrides Sub OnSizeChanged(e As EventArgs)
            Rect = New Rectangle(0, 0, Width - 1, Height - 1)
            RectInner = New Rectangle(1, 1, Width - 3, Height - 3)
            Rect_DefColor = New Rectangle(0, 0, Height, Height)
            MyBase.OnSizeChanged(e)
        End Sub
#End Region

#Region "Variables"
        Public ColorPickerOpened As Boolean = False
        Public ColorsHistory As New List(Of Color)
        Private LineColor As Color
        Public State As MouseState = MouseState.None
        Public PauseColorsHistory As Boolean = False

        Enum MouseState
            None
            Over
            Down
        End Enum
#End Region

#Region "Drag and drop"
        Private SwapNotCopy As Boolean = False
        Private InitializeDrag As Boolean = False
        Private AfterDropEffect As AfterDropEffects = ColorItem.AfterDropEffects.None
        Private DragDefaultColor As Boolean = False
        Private DraggedColor As Color
        Private DragDropMouseHovering As Boolean = False
        Private MakeAfterDropEffect As Boolean = False
        Private BeforeDropColor As Color
        Private BeforeDropMousePosition As Point
        Private HoverOverDefColorDot As Boolean = False

        Public Enum AfterDropEffects
            None
            Invert
            Darker
            Lighter
            Mix
        End Enum

        Protected Overrides Sub OnMouseMove(e As MouseEventArgs)

            If InitializeDrag Then
                DragDefaultColor = CanRaiseEventsForDefColorDot()
                DoDragDrop(Me, DragDropEffects.Copy)
            End If

            InitializeDrag = False

            If Not DesignMode AndAlso My.Settings.NerdStats.DotDefaultChangedIndicator Then
                HoverOverDefColorDot = CanRaiseEventsForDefColorDot()
                Refresh()
            End If

            MyBase.OnMouseMove(e)
        End Sub

        Protected Overrides Sub OnDragDrop(e As DragEventArgs)
            If AllowDrop AndAlso My.Settings.NerdStats.DragAndDrop Then

                BeforeDropColor = BackColor
                BeforeDropMousePosition = PointToClient(MousePosition)
                Tmr2_factor = 0
                MakeAfterDropEffect = True
                Tmr2.Enabled = True
                Tmr2.Start()

                If Not SwapNotCopy Then
                    e.Effect = DragDropEffects.Copy
                    If Not CType(e.Data.GetData("WinPaletter.UI.Controllers.ColorItem"), UI.Controllers.ColorItem).DragDefaultColor Then
                        BackColor = CType(e.Data.GetData("WinPaletter.UI.Controllers.ColorItem"), UI.Controllers.ColorItem).BackColor
                    Else
                        BackColor = CType(e.Data.GetData("WinPaletter.UI.Controllers.ColorItem"), UI.Controllers.ColorItem).DefaultColor
                    End If

                Else
                    e.Effect = DragDropEffects.Link
                    Dim Color_From As Color = CType(e.Data.GetData("WinPaletter.UI.Controllers.ColorItem"), UI.Controllers.ColorItem).BackColor

                    Dim Color_To As Color
                    Select Case AfterDropEffect
                        Case AfterDropEffects.Invert
                            Color_To = BackColor.Invert

                        Case AfterDropEffects.Darker
                            Color_To = BackColor.Dark

                        Case AfterDropEffects.Lighter
                            Color_To = BackColor.Light

                        Case Else
                            Color_To = BackColor

                    End Select

                    BackColor = Color_From
                    CType(e.Data.GetData("WinPaletter.UI.Controllers.ColorItem"), UI.Controllers.ColorItem).BackColor = Color_To

                End If

                Select Case AfterDropEffect
                    Case AfterDropEffects.Invert
                        BackColor = BackColor.Invert

                    Case AfterDropEffects.Darker
                        BackColor = BackColor.Dark

                    Case AfterDropEffects.Lighter
                        BackColor = BackColor.Light

                    Case AfterDropEffects.Mix
                        BackColor = BackColor.Blend(BeforeDropColor, 100)

                End Select

                ColorInfoDragDrop.Close()

                MyBase.OnDragDrop(e)
            End If

            DragDropMouseHovering = False
            Refresh()

        End Sub

        Protected Overrides Sub OnDragEnter(e As DragEventArgs)
            If AllowDrop AndAlso My.Settings.NerdStats.DragAndDrop Then
                DragDropMouseHovering = True

                e.Effect = DragDropEffects.Copy

                SwapNotCopy = False
                AfterDropEffect = AfterDropEffects.None

                If (e.KeyState And 32) = 32 Then
                    'Alt is pressed
                    AfterDropEffect = AfterDropEffects.Invert
                End If

                If (e.KeyState And 16) = 16 Then
                    'Middle mouse button is pressed

                End If

                If (e.KeyState And 8) = 8 Then
                    'Ctrl is pressed
                    AfterDropEffect = AfterDropEffects.Darker

                End If

                If (e.KeyState And 4) = 4 Then
                    'Shift is pressed
                    AfterDropEffect = AfterDropEffects.Lighter

                End If

                If (e.KeyState And 32 + 8) = 32 + 8 Then
                    'Ctrl+Alt are pressed
                    AfterDropEffect = AfterDropEffects.Mix

                End If

                If (e.KeyState And 2) = 2 Then
                    'Right mouse button is pressed
                    SwapNotCopy = True
                End If

                If (e.KeyState And 1) = 1 Then
                    'Left mouse button is pressed

                End If

                If Not SwapNotCopy Then
                    Select Case AfterDropEffect
                        Case AfterDropEffects.Invert
                            ColorInfoDragDrop.Label1.Text = My.Lang.ColorItem_Copy_Invert

                        Case AfterDropEffects.Darker
                            ColorInfoDragDrop.Label1.Text = My.Lang.ColorItem_Copy_Darker

                        Case AfterDropEffects.Lighter
                            ColorInfoDragDrop.Label1.Text = My.Lang.ColorItem_Copy_Lighter

                        Case AfterDropEffects.Mix
                            ColorInfoDragDrop.Label1.Text = My.Lang.ColorItem_Copy_Mix

                        Case Else
                            ColorInfoDragDrop.Label1.Text = My.Lang.ColorItem_Copy
                    End Select

                Else
                    Select Case AfterDropEffect
                        Case AfterDropEffects.Invert
                            ColorInfoDragDrop.Label1.Text = My.Lang.ColorItem_Swap_Invert

                        Case AfterDropEffects.Darker
                            ColorInfoDragDrop.Label1.Text = My.Lang.ColorItem_Swap_Darker

                        Case AfterDropEffects.Lighter
                            ColorInfoDragDrop.Label1.Text = My.Lang.ColorItem_Swap_Lighter

                        Case Else
                            ColorInfoDragDrop.Label1.Text = My.Lang.ColorItem_Swap

                    End Select

                End If

                If Not CType(e.Data.GetData("WinPaletter.UI.Controllers.ColorItem"), UI.Controllers.ColorItem).DragDefaultColor Then
                    DraggedColor = CType(e.Data.GetData("WinPaletter.UI.Controllers.ColorItem"), UI.Controllers.ColorItem).BackColor
                Else
                    DraggedColor = CType(e.Data.GetData("WinPaletter.UI.Controllers.ColorItem"), UI.Controllers.ColorItem).DefaultColor
                End If

                Select Case AfterDropEffect
                    Case AfterDropEffects.Invert
                        ColorInfoDragDrop.Color_From.BackColor = DraggedColor.Invert

                    Case AfterDropEffects.Darker
                        ColorInfoDragDrop.Color_From.BackColor = DraggedColor.Dark

                    Case AfterDropEffects.Lighter
                        ColorInfoDragDrop.Color_From.BackColor = DraggedColor.Light

                    Case AfterDropEffects.Mix
                        ColorInfoDragDrop.Color_From.BackColor = DraggedColor.Blend(BackColor, 100)

                    Case Else
                        ColorInfoDragDrop.Color_From.BackColor = DraggedColor

                End Select

                DraggedColor = ColorInfoDragDrop.Color_From.BackColor

                If Not SwapNotCopy Then
                    ColorInfoDragDrop.Color_To.BackColor = BackColor
                Else
                    Select Case AfterDropEffect
                        Case AfterDropEffects.Invert
                            ColorInfoDragDrop.Color_To.BackColor = BackColor.Invert

                        Case AfterDropEffects.Darker
                            ColorInfoDragDrop.Color_To.BackColor = BackColor.Dark

                        Case AfterDropEffects.Lighter
                            ColorInfoDragDrop.Color_To.BackColor = BackColor.Light

                        Case Else
                            ColorInfoDragDrop.Color_To.BackColor = BackColor

                    End Select
                End If

                If My.Settings.NerdStats.DragAndDropColorsGuide Then
                    ColorInfoDragDrop.Location = New Point(e.X + 15, e.Y + 15)
                    ColorInfoDragDrop.Visible = True
                End If

            Else
                DragDropMouseHovering = False
                Refresh()

                e.Effect = DragDropEffects.None
                ColorInfoDragDrop.Visible = False
            End If

            MyBase.OnDragEnter(e)
        End Sub

        Protected Overrides Sub OnDragLeave(e As EventArgs)
            MyBase.OnDragLeave(e)
            DragDropMouseHovering = False
            Refresh()
            ColorInfoDragDrop.Visible = False
        End Sub

        Protected Overrides Sub OnDragOver(e As DragEventArgs)
            If AllowDrop AndAlso My.Settings.NerdStats.DragAndDrop Then
                DragDropMouseHovering = True
                Refresh()
                MyBase.OnDragOver(e)
                ColorInfoDragDrop.Location = New Point(e.X + 15, e.Y + 15)
            Else
                e.Effect = DragDropEffects.None
                ColorInfoDragDrop.Visible = False
            End If

        End Sub

#End Region

#Region "Subs/Functions"
        Public Sub UpdateColorsHistory()
            If Not PauseColorsHistory Then
                If ColorsHistory.Count > 0 Then
                    If ColorsHistory.Last <> BackColor Then ColorsHistory.Add(BackColor)
                Else
                    ColorsHistory.Add(BackColor)
                End If
            End If
        End Sub

        Public Function GetMiniColorItemSize() As Size
            Return New Size(If(My.Settings.NerdStats.Enabled, 80, 30), 24)
        End Function

        Public Function CanRaiseEventsForDefColorDot() As Boolean
            Return My.Settings.NerdStats.DotDefaultChangedIndicator AndAlso Rect_DefColor.Contains(PointToClient(MousePosition)) AndAlso BackColor <> DefaultColor
        End Function
#End Region

#Region "Events"
        Protected Overrides Sub OnBackColorChanged(e As EventArgs)
            UpdateColorsHistory()
            MyBase.OnBackColorChanged(e)
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            InitializeDrag = My.Settings.NerdStats.DragAndDrop
            State = MouseState.Down
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            InitializeDrag = False
            State = MouseState.Over
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()

            MyBase.OnMouseUp(e)
        End Sub

        Private Sub XenonCP_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
            State = MouseState.Over
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub XenonCP_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
            InitializeDrag = False
            HoverOverDefColorDot = False
            State = MouseState.None
            Tmr.Enabled = True
            Tmr.Start()
            Invalidate()
        End Sub

        Private Sub XenonCP_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            alpha = 0
            Tmr2_factor = 0
        End Sub
#End Region

#Region "Animators"
        Private alpha As Integer
        ReadOnly Factor As Integer = 15
        Private WithEvents Tmr, Tmr2 As New Timer With {.Enabled = False, .Interval = 1}
        Private Tmr2_factor As Integer = 0

        Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
            If Not DesignMode Then

                If State = MouseState.Over Then
                    If alpha + Factor <= 255 Then
                        alpha += Factor
                    ElseIf alpha + Factor > 255 Then
                        alpha = 255
                        Tmr.Enabled = False
                        Tmr.Stop()
                    End If

                    If Not Tmr2.Enabled Then
                        Threading.Thread.Sleep(1)
                        Refresh()
                    End If
                End If

                If Not State = MouseState.Over Then
                    If alpha - Factor >= 0 Then
                        alpha -= Factor
                    ElseIf alpha - Factor < 0 Then
                        alpha = 0
                        Tmr.Enabled = False
                        Tmr.Stop()
                    End If

                    If Not Tmr2.Enabled Then
                        Threading.Thread.Sleep(1)
                        Refresh()
                    End If
                End If
            End If
        End Sub

        Private Sub Tmr2_Tick(sender As Object, e As EventArgs) Handles Tmr2.Tick
            If Not DesignMode AndAlso MakeAfterDropEffect Then
                Tmr2_factor += Math.Min(Width, Height) * 3.5
                Threading.Thread.Sleep(1)
                Refresh()
            Else
                Tmr2_factor = 0
                MakeAfterDropEffect = False
                Refresh()
                Tmr2.Enabled = False
                Tmr2.Stop()
            End If
        End Sub
#End Region

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            DoubleBuffered = True

            Dim R As Integer = 5

            G.Clear(GetParentColor)

            Select Case State
                Case MouseState.None
                    LineColor = If(BackColor.IsDark, BackColor.CB(0.05), BackColor.CB(-0.05))

                Case MouseState.Over
                    LineColor = If(BackColor.IsDark, BackColor.CB(0.15), BackColor.CB(-0.15))

                Case MouseState.Down
                    LineColor = If(BackColor.IsDark, BackColor.CB(0.1), BackColor.CB(-0.1))

            End Select

            LineColor = Color.FromArgb(255, LineColor)

            If BackColor.A < 255 Then
                Using br As New TextureBrush(My.Resources.BackgroundOpacity) : G.FillRoundedRect(br, RectInner, R) : End Using
                Using br As New TextureBrush(My.Resources.BackgroundOpacity.Fade(alpha / 255)) : G.FillRoundedRect(br, Rect, R) : End Using
            End If

            If Not DesignMode AndAlso MakeAfterDropEffect AndAlso My.Settings.NerdStats.DragAndDropRippleEffect Then
                'Make ripple effect on dropping a color

                Using br As New SolidBrush(BeforeDropColor) : G.FillRoundedRect(br, RectInner, R) : End Using

                Using path As GraphicsPath = RectInner.Round(R)
                    Dim reg As New Region(path)
                    G.Clip = reg
                    Dim i As Integer = Math.Max(Width, Height) + Tmr2_factor
                    Dim px As Point = BeforeDropMousePosition
                    Dim MouseCircle As New Rectangle(px.X - 0.5 * i, px.Y - 0.5 * i, i, i)
                    Dim gp As GraphicsPath = New GraphicsPath()
                    gp.AddEllipse(MouseCircle)
                    Dim pgb As New PathGradientBrush(gp) With {
                                    .CenterPoint = px,
                                    .CenterColor = BackColor,
                                    .SurroundColors = New Color() {Color.Transparent}
                                    }
                    G.FillEllipse(pgb, MouseCircle)

                    G.ResetClip()

                    If i / 2 > (Width * Height) Then
                        Tmr2.Enabled = False
                        Tmr2.Stop()
                        Tmr2_factor = 0
                        MakeAfterDropEffect = False
                        Invalidate()
                    End If

                End Using

                Using P As New Pen(LineColor) : G.DrawRoundedRect_LikeW11(P, RectInner, R) : End Using

            ElseIf Not DesignMode AndAlso DragDropMouseHovering AndAlso My.Settings.NerdStats.DragAndDropRippleEffect Then
                'Make circle hover effect on dragging over a color

                Using br As New SolidBrush(BackColor) : G.FillRoundedRect(br, Rect, R) : End Using

                Using path As GraphicsPath = Rect.Round(R)
                    Dim reg As New Region(path)
                    G.Clip = reg
                    Dim i As Integer = Math.Max(Width, Height)
                    Dim px As Point = PointToClient(MousePosition)
                    Dim MouseCircle As New Rectangle(px.X - 0.5 * i, px.Y - 0.5 * i, i, i)
                    Dim gp As GraphicsPath = New GraphicsPath()
                    gp.AddEllipse(MouseCircle)
                    Dim pgb As New PathGradientBrush(gp) With {
                                            .CenterPoint = px,
                                            .CenterColor = DraggedColor,
                                            .SurroundColors = New Color() {Color.Transparent}
                                            }
                    G.FillEllipse(pgb, MouseCircle)
                    G.ResetClip()
                End Using

                Using P As New Pen(If(BackColor.IsDark, Color.White, Color.Black), 1.5F) With {.DashStyle = DashStyle.Dot} : G.DrawRoundedRect_LikeW11(P, Rect, R) : End Using

            Else
                'Normal appearance

                Using br As New SolidBrush(BackColor) : G.FillRoundedRect(br, RectInner, R) : End Using
                Using br As New SolidBrush(Color.FromArgb((alpha / 255) * BackColor.A, BackColor)) : G.FillRoundedRect(br, Rect, R) : End Using

                Using P As New Pen(Color.FromArgb(alpha, LineColor)) : G.DrawRoundedRect_LikeW11(P, Rect, R) : End Using
                Using P As New Pen(Color.FromArgb(255 - alpha, LineColor)) : G.DrawRoundedRect_LikeW11(P, RectInner, R) : End Using
            End If

            Try
                If Not DesignMode AndAlso My.Settings.NerdStats.DotDefaultChangedIndicator Then
                    Using br As New SolidBrush(DefaultColor)

                        Dim L As Integer = Math.Max(6, RectInner.Height - 10)
                        Dim Y As Integer = RectInner.Y + (RectInner.Height - L) / 2
                        Dim DefDotRect As Rectangle

                        If Not HoverOverDefColorDot Then
                            DefDotRect = New Rectangle(Y, Y, L, L)
                        Else
                            DefDotRect = New Rectangle(Y - 1, Y - 1, L + 2, L + 2)
                        End If

                        G.FillEllipse(New SolidBrush(DefaultColor), DefDotRect)
                    End Using
                End If
            Catch
            End Try

            If Not DesignMode Then
                If My.Settings.NerdStats.Enabled And Not DontShowInfo Then
                    G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)

                    Dim TargetColor As Color = If(Not HoverOverDefColorDot Or Not My.Settings.NerdStats.DotDefaultChangedIndicator, BackColor, DefaultColor)
                    Dim FC0 As Color = If(TargetColor.IsDark, LineColor.LightLight, LineColor.Dark(0.9))
                    Dim FC1 As Color = If(TargetColor.IsDark, LineColor.LightLight, LineColor.Dark(0.9))

                    FC0 = Color.FromArgb(If(My.Settings.NerdStats.MoreLabelTransparency, 75, 125), FC0)
                    FC1 = Color.FromArgb(alpha, FC1)

                    Dim RectX As Rectangle = Rect
                    RectX.Y += 1

                    Dim CF As ColorFormat = ColorFormat.HEX
                    If My.Settings.NerdStats.Type = XeSettings.Structures.NerdStats.Formats.HEX Then CF = ColorFormat.HEX
                    If My.Settings.NerdStats.Type = XeSettings.Structures.NerdStats.Formats.RGB Then CF = ColorFormat.RGB
                    If My.Settings.NerdStats.Type = XeSettings.Structures.NerdStats.Formats.HSL Then CF = ColorFormat.HSL
                    If My.Settings.NerdStats.Type = XeSettings.Structures.NerdStats.Formats.Dec Then CF = ColorFormat.Dec

                    Dim S As String = TargetColor.ReturnFormat(CF, My.Settings.NerdStats.ShowHexHash, Not (TargetColor.A = 255))
                    Dim F As Font

                    If Not My.Settings.NerdStats.UseWindowsMonospacedFont Then
                        F = My.Application.ConsoleFont
                    Else
                        F = New Font(FontFamily.GenericMonospace.Name, 8.5, FontStyle.Regular)
                    End If

                    Using br As New SolidBrush(FC0) : G.DrawString(S, F, br, RectX, ContentAlignment.MiddleCenter.ToStringFormat) : End Using
                    Using br As New SolidBrush(FC1) : G.DrawString(S, F, br, RectX, ContentAlignment.MiddleCenter.ToStringFormat) : End Using

                    If ColorPickerOpened Then
                        Using br As New SolidBrush(FC0) : G.DrawString("▼", F, br, New Rectangle(RectX.X, RectX.Y, RectX.Width - 5, RectX.Height), ContentAlignment.MiddleRight.ToStringFormat) : End Using
                        Using br As New SolidBrush(FC1) : G.DrawString("▼", F, br, New Rectangle(RectX.X, RectX.Y, RectX.Width - 5, RectX.Height), ContentAlignment.MiddleRight.ToStringFormat) : End Using
                    End If

                End If
            End If

        End Sub
    End Class

End Namespace
