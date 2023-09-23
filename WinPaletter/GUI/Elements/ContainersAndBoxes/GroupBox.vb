Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.WP

    <Description("Themed GroupBox for WinPaletter UI")> <DefaultEvent("Click")> Public Class GroupBox : Inherits Panel

        Sub New()
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
            DoubleBuffered = True
            Text = ""
        End Sub

        Private LineColor As Color

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String = ""

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics
            DoubleBuffered = True
            G.SmoothingMode = SmoothingMode.AntiAlias
            Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim ParentColor As Color = MyBase.GetParentColor

            G.Clear(ParentColor)
            BackColor = ParentColor.CB(If(ParentColor.IsDark, 0.04, -0.05))
            LineColor = ParentColor.CB(If(ParentColor.IsDark, 0.06, -0.07))
            Using br As New SolidBrush(BackColor) : G.FillRoundedRect(br, Rect) : End Using
            Using P As New Pen(LineColor) : G.DrawRoundedRect(P, Rect) : End Using
        End Sub



    End Class

End Namespace

