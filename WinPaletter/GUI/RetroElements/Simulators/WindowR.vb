Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.Retro
    <Description("Retro window with Windows 9x style")> Public Class WindowR : Inherits Windows.Forms.Panel

        Sub New()
            DoubleBuffered = True
            Font = New Font("Microsoft Sans Serif", 8)
            BackColor = Color.FromArgb(192, 192, 192)
            ForeColor = Color.White
            BorderStyle = BorderStyle.None
            Text = "New Window"
        End Sub

#Region "Properties"

        Public Property Color1 As Color = Color.FromArgb(0, 0, 128)
        Public Property Color2 As Color = Color.FromArgb(16, 132, 208)
        Public Property ColorGradient As Boolean = True
        Public Property ColorBorder As Color = Color.FromArgb(192, 192, 192)

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String = "New window"

        Public Property UseItAsMenu As Boolean = False
        Public Property Flat As Boolean = False

        Private _ButtonShadow As Color = Color.FromArgb(128, 128, 128)
        Public Property ButtonShadow As Color
            Get
                Return _ButtonShadow
            End Get
            Set(value As Color)
                _ButtonShadow = value
                _CloseBtn.ButtonShadow = value
                _MinBtn.ButtonShadow = value
                _MaxBtn.ButtonShadow = value
                Refresh()
            End Set
        End Property

        Private _ButtonDkShadow As Color = Color.Black
        Public Property ButtonDkShadow As Color
            Get
                Return _ButtonDkShadow
            End Get
            Set(value As Color)
                _ButtonDkShadow = value
                _CloseBtn.ButtonDkShadow = value
                _MinBtn.ButtonDkShadow = value
                _MaxBtn.ButtonDkShadow = value
                Refresh()
            End Set
        End Property

        Private _ButtonHilight As Color = Color.White
        Public Property ButtonHilight As Color
            Get
                Return _ButtonHilight
            End Get
            Set(value As Color)
                _ButtonHilight = value
                _CloseBtn.ButtonHilight = value
                _MinBtn.ButtonHilight = value
                _MaxBtn.ButtonHilight = value
                Refresh()
            End Set
        End Property

        Private _ButtonLight As Color = Color.FromArgb(192, 192, 192)
        Public Property ButtonLight As Color
            Get
                Return _ButtonLight
            End Get
            Set(value As Color)
                _ButtonLight = value
                _CloseBtn.ButtonLight = value
                _MinBtn.ButtonLight = value
                _MaxBtn.ButtonLight = value
                Refresh()
            End Set
        End Property

        Private _ButtonFace As Color = Color.FromArgb(192, 192, 192)
        Public Property ButtonFace As Color
            Get
                Return _ButtonFace
            End Get
            Set(value As Color)
                _ButtonFace = value
                Refresh()
            End Set
        End Property

        Private _ButtonText As Color = Color.Black
        Public Property ButtonText As Color
            Get
                Return _ButtonText
            End Get
            Set(value As Color)
                _ButtonText = value
                _CloseBtn.ForeColor = value
                _MinBtn.ForeColor = value
                _MaxBtn.ForeColor = value
                Refresh()
            End Set
        End Property

        Private _Metrics_CaptionHeight As Integer = 22
        Public Property Metrics_CaptionHeight As Integer
            Get
                Return _Metrics_CaptionHeight
            End Get

            Set(value As Integer)
                _Metrics_CaptionHeight = value
                AdjustButtonSizes()
                AdjustControlBoxFontsSizes()
                AdjustPadding()
                Refresh()
            End Set
        End Property

        Private _Metrics_CaptionWidth As Integer = 22
        Public Property Metrics_CaptionWidth As Integer
            Get
                Return _Metrics_CaptionWidth
            End Get
            Set(value As Integer)
                _Metrics_CaptionWidth = value
                AdjustButtonSizes()
                AdjustLocations()
                AdjustControlBoxFontsSizes()
                Refresh()
            End Set
        End Property

        Private _Metrics_BorderWidth As Integer = 1
        Public Property Metrics_BorderWidth As Integer
            Get
                Return _Metrics_BorderWidth
            End Get

            Set(value As Integer)
                _Metrics_BorderWidth = value
                AdjustLocations()
                AdjustPadding()
                Refresh()
            End Set
        End Property

        Private _Metrics_PaddedBorderWidth As Integer = 4
        Public Property Metrics_PaddedBorderWidth As Integer
            Get
                Return _Metrics_PaddedBorderWidth
            End Get
            Set(value As Integer)
                _Metrics_PaddedBorderWidth = value
                AdjustLocations()
                AdjustPadding()
                Refresh()
            End Set
        End Property

        Private _ControlBox As Boolean = True
        Public Property ControlBox As Boolean
            Get
                Return _ControlBox
            End Get
            Set(value As Boolean)
                _ControlBox = value
                _CloseBtn.Visible = value
                _MinBtn.Visible = value And _MinimizeBox
                _MaxBtn.Visible = value And _MaximizeBox
                AdjustLocations()
                Refresh()
            End Set
        End Property

        Private _MinimizeBox As Boolean = True
        Public Property MinimizeBox As Boolean
            Get
                Return _MinimizeBox
            End Get
            Set(value As Boolean)
                _MinimizeBox = value
                _MinBtn.Visible = value
                AdjustLocations()
                Refresh()
            End Set
        End Property

        Private _MaximizeBox As Boolean = True
        Public Property MaximizeBox As Boolean
            Get
                Return _MaximizeBox
            End Get
            Set(value As Boolean)
                _MaximizeBox = value
                _MaxBtn.Visible = value
                AdjustLocations()
                Refresh()
            End Set
        End Property

#End Region

#Region "Events"

        Private Sub WindowR_BackColorChanged(sender As Object, e As EventArgs) Handles Me.BackColorChanged
            _CloseBtn.BackColor = BackColor
            _MinBtn.BackColor = BackColor
            _MaxBtn.BackColor = BackColor
        End Sub

        Private Sub WindowR_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
            If Not UseItAsMenu Then
                Controls.AddRange(New Control() {_CloseBtn, _MaxBtn, _MinBtn})
                _CloseBtn.Visible = _ControlBox
                _MinBtn.Visible = _ControlBox And _MinimizeBox
                _MaxBtn.Visible = _ControlBox And _MaximizeBox

                AdjustControlBoxFontsSizes()
                AdjustButtonSizes()
                AdjustLocations()
            End If
        End Sub

        Private Sub WindowR_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
            AdjustLocations()
        End Sub

        Private Sub WindowR_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged
            AdjustControlBoxFontsSizes()
            AdjustButtonSizes()
            AdjustLocations()
        End Sub

#End Region

#Region "Subs/Functions"

        Public Function GetTitleTextHeight()
            Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
            TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
            TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
            TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

            Return TitleTextH_Sum
        End Function

        Private Sub AdjustButtonSizes()
            BtnHeight = Math.Max(_Metrics_CaptionHeight + GetTitleTextHeight() - 4, 5)
            BtnWidth = Math.Max(_Metrics_CaptionWidth - 2, 5)

            _CloseBtn.Size = New Size(BtnWidth, BtnHeight)
            _MinBtn.Size = New Size(BtnWidth, BtnHeight)
            _MaxBtn.Size = New Size(BtnWidth, BtnHeight)
            Refresh()
        End Sub

        Private Sub AdjustLocations()
            _CloseBtn.Top = Metrics_PaddedBorderWidth + Metrics_BorderWidth + 5
            _MinBtn.Top = _CloseBtn.Top
            _MaxBtn.Top = _CloseBtn.Top

            _CloseBtn.Left = Width - _CloseBtn.Width - _Metrics_PaddedBorderWidth - _Metrics_BorderWidth - 5

            If MinimizeBox And MaximizeBox Then
                _MinBtn.Left = _CloseBtn.Left - 2 - _MinBtn.Width
                _MaxBtn.Left = _MinBtn.Left - _MaxBtn.Width

            ElseIf MaximizeBox Then
                _MaxBtn.Left = _CloseBtn.Left - 2 - _MaxBtn.Width

            ElseIf MinimizeBox Then
                _MinBtn.Left = _CloseBtn.Left - 2 - _MinBtn.Width

            End If

        End Sub

        Sub AdjustControlBoxFontsSizes()
            Try
                Dim i0, iFx As Single
                i0 = Math.Abs(Math.Min(_Metrics_CaptionHeight, _Metrics_CaptionWidth))
                iFx = i0 / Math.Abs(Math.Min(17, 18))
                Dim f As New Font("Marlett", 6.8 * iFx)
                _CloseBtn.Font = f
                _MinBtn.Font = f
                _MaxBtn.Font = f
            Catch

            End Try
        End Sub

        Sub AdjustPadding()
            Dim iP As Integer = 3 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth
            Dim iT As Integer = 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth + _Metrics_CaptionHeight + GetTitleTextHeight()
            Dim _Padding As New Padding(iP, iT, iP, iP)
            Padding = _Padding
        End Sub

#End Region

#Region "ControlBox"

        Private ReadOnly _CloseBtn As New UI.Retro.ButtonR With {.Text = "r", .Font = New Font("Marlett", 7.8), .Size = New Size(BtnWidth, BtnHeight), .TextAlign = ContentAlignment.MiddleCenter}
        Private ReadOnly _MinBtn As New UI.Retro.ButtonR With {.Text = "1", .Font = New Font("Marlett", 8), .Size = New Size(BtnWidth, BtnHeight), .TextAlign = ContentAlignment.MiddleCenter}
        Private ReadOnly _MaxBtn As New UI.Retro.ButtonR With {.Text = "0", .Font = New Font("Marlett", 8), .Size = New Size(BtnWidth, BtnHeight), .TextAlign = ContentAlignment.MiddleCenter}

        Private BtnHeight As Integer = Metrics_CaptionHeight + GetTitleTextHeight() - 4
        Private BtnWidth As Integer = Metrics_CaptionWidth - 2

#End Region

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.HighSpeed
            G.TextRenderingHint = My.RenderingHint
            DoubleBuffered = True

            '################################################################################# Customizer
            Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)

            Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
            TitleTextH = "ABCabc0123xYz.#".Measure(Font).Height
            TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(Font.Name, 9, Font.Style)).Height
            TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

            Dim CompinedPadding As Integer = _Metrics_BorderWidth + _Metrics_PaddedBorderWidth + 3

            Dim TRect As New Rectangle(CompinedPadding, CompinedPadding, Width - CompinedPadding * 2, _Metrics_CaptionHeight + TitleTextH_Sum)

            Dim ARect As New Rectangle(2, 2, Width - 5, Height - 5)
            '#################################################################################
            G.Clear(BackColor)

            If Not Flat Then
                With Rect
                    G.DrawLine(New Pen(ButtonShadow), New Point(.Width - 1, .X + 1), New Point(.Width - 1, .Height - 1))
                    G.DrawLine(New Pen(ButtonShadow), New Point(.X + 1, .Height - 1), New Point(.Width - 1, .Height - 1))

                    G.DrawLine(New Pen(ButtonHilight), New Point(.X + 1, .Y + 1), New Point(.Width - 2, .Y + 1))
                    G.DrawLine(New Pen(ButtonHilight), New Point(.X + 1, .Y + 1), New Point(.X + 1, .Height - 2))

                    G.DrawLine(New Pen(ButtonLight), New Point(.X, .Y), New Point(.Width - 1, .Y))
                    G.DrawLine(New Pen(ButtonLight), New Point(.X, .Y), New Point(.X, .Height - 1))

                    G.DrawLine(New Pen(ButtonDkShadow), New Point(.Width, .X), New Point(.Width, .Height))
                    G.DrawLine(New Pen(ButtonDkShadow), New Point(.X, .Height), New Point(.Width, .Height))

                    G.DrawRectangle(New Pen(ButtonFace), New Rectangle(2, 2, Width - 5, Height - 5))
                End With
            Else
                G.DrawRectangle(New Pen(ButtonShadow), Rect)
            End If

            If Not Flat And Not UseItAsMenu Then G.DrawRectangle(New Pen(ColorBorder), ARect)
            Dim F As Font

            If Not UseItAsMenu Then
                If G.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit Then
                    F = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
                Else
                    F = Font
                End If

                Dim RTL As Boolean = (RightToLeft = 1)

                Try
                    Dim gr As New LinearGradientBrush(TRect, If(RTL, Color2, Color1), If(RTL, Color1, Color2), LinearGradientMode.Horizontal)
                    If ColorGradient Then
                        G.FillRectangle(gr, TRect)

                        Dim TRectFixer As New Rectangle(TRect.X, TRect.Y, 1, TRect.Height)
                        G.FillRectangle(New SolidBrush(If(RTL, Color2, Color1)), TRectFixer)

                    Else
                        G.FillRectangle(New SolidBrush(Color1), TRect)
                    End If
                Catch
                End Try

                G.DrawString(Text, F, New SolidBrush(ForeColor), TRect, ContentAlignment.MiddleLeft.ToStringFormat(RTL))
            End If

        End Sub

    End Class

End Namespace
