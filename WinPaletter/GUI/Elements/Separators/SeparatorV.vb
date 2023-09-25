Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.WP

    <Description("Vertical separator for WinPaletter UI")> Public Class SeparatorV : Inherits Control

        Sub New()
            TabStop = False
            DoubleBuffered = True
            Text = ""
        End Sub

#Region "Properties"

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String = ""
        Public Property AlternativeLook As Boolean = False

#End Region

#Region "Events"

        Protected Overrides Sub OnResize(e As EventArgs)
            MyBase.OnResize(e)
            Size = New Size(If(Not AlternativeLook, 1, 2), Height)
        End Sub

#End Region

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            DoubleBuffered = True
            MyBase.OnPaint(e)

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
                If My.Style.DarkMode Then IdleLine = Color.FromArgb(60, 60, 60) Else IdleLine = Color.FromArgb(210, 210, 210)
            End If
            '################################################################################# Customizer

            Using C As New Pen(IdleLine, If(Not AlternativeLook, 1, 2))
                G.DrawLine(C, New Point(0, 0), New Point(0, Height))
                G.DrawLine(C, New Point(1, 0), New Point(1, Height))
            End Using

        End Sub

    End Class

End Namespace
