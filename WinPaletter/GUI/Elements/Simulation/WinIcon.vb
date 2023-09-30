Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.Simulation

    <Description("Simulated Windows desktop icons")> Public Class WinIcon : Inherits Panel

        Sub New()
            DoubleBuffered = True
            BackColor = Color.Transparent
            SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.UserPaint, True)
        End Sub

#Region "Properties"

        Public Property ColorText As Color = Color.White
        Public Property ColorGlow As Color = Color.FromArgb(50, 0, 0, 0)
        Public Property Icon As Icon

        Private _IconSize As Integer = 32
        Public Property IconSize As Integer
            Get
                Return _IconSize
            End Get
            Set(value As Integer)
                _IconSize = value
                Invalidate()
            End Set
        End Property


        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String = ""

        Protected Overrides ReadOnly Property CreateParams As CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                cp.ExStyle = cp.ExStyle Or &H20
                Return cp
            End Get
        End Property

#End Region

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.HighQuality
            G.TextRenderingHint = My.RenderingHint
            DoubleBuffered = True

            Dim IconRect As New Rectangle(0, 0, Width - 1, Height - 30)

            Dim LabelRect As New Rectangle(0, Height - 35, Width - 1, 30)
            Dim LabelRectShadow As New Rectangle(1, Height - 34, Width - 1, 30)

            If _IconSize < 16 Then _IconSize = 16
            If _IconSize > 256 Then _IconSize = 256

            Dim IconRectX As New Rectangle(IconRect.X + (IconRect.Width - _IconSize) / 2, IconRect.Y + (IconRect.Height - _IconSize) / 2, _IconSize, _IconSize)

            If Icon IsNot Nothing Then
                Dim ico As New Icon(Icon, _IconSize, _IconSize)
                G.DrawIcon(ico, IconRectX)
                ico.Dispose()
            End If

            If ColorGlow.A > 0 Then G.DrawString(Text, Me.Font, Brushes.Black, LabelRectShadow, ContentAlignment.MiddleCenter.ToStringFormat)

            G.DrawGlowString(1, Text, Font, ColorText, ColorGlow, New Rectangle(0, 0, Width - 1, Height - 1), LabelRect, ContentAlignment.MiddleCenter.ToStringFormat)
        End Sub

    End Class

End Namespace