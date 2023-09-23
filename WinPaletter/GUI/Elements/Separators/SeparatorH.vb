Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.WP

    <Description("Horizontal separator for WinPaletter UI")>
    Public Class SeparatorH : Inherits Control

        Sub New()
            TabStop = False
            DoubleBuffered = True
            Text = ""
        End Sub

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String = ""

        Public Property AlternativeLook As Boolean = False

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Size = New Size(Width, If(Not AlternativeLook, 1, 2))
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            DoubleBuffered = True
            MyBase.OnPaint(e)

            Dim clr As Color
            clr = Color.FromArgb(75, 75, 75)

            '################################################################################# Customizer
            Dim IdleLine As Color

            If Parent IsNot Nothing Then

                If My.Style.DarkMode Then
                    If Not AlternativeLook Then
                        IdleLine = Parent.BackColor.CB(0.1)
                    Else
                        IdleLine = Color.DarkRed
                    End If
                Else
                    If Not AlternativeLook Then
                        IdleLine = Parent.BackColor.CB(-0.1)
                    Else
                        IdleLine = Color.DarkRed
                    End If
                End If

            Else
                If My.Style.DarkMode Then IdleLine = Color.FromArgb(76, 76, 76) Else IdleLine = Color.FromArgb(210, 210, 210)
            End If

            '################################################################################# Customizer

            Using C As New Pen(IdleLine, If(Not AlternativeLook, 1, 2))
                G.DrawLine(C, New Point(0, 0), New Point(Width, 0))
                G.DrawLine(C, New Point(0, 1), New Point(Width, 1))
            End Using
        End Sub

    End Class

End Namespace