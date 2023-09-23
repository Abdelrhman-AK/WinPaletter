Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Namespace UI.Retro

    <Description("Raised retro panel with Windows 9x style")> Public Class PanelRaisedR : Inherits System.Windows.Forms.Panel

        Sub New()
            DoubleBuffered = True
            Font = New Font("Microsoft Sans Serif", 8)
            BackColor = Color.FromArgb(192, 192, 192)
            ForeColor = Color.Black
            BorderStyle = BorderStyle.None
        End Sub

        Public Property Flat As Boolean = False
        Public Property ButtonHilight As Color = Color.White
        Public Property ButtonShadow As Color = Color.FromArgb(128, 128, 128)
        Public Property ButtonDkShadow As Color = Color.FromArgb(105, 105, 105)
        Public Property ButtonLight As Color = Color.FromArgb(227, 227, 227)
        Public Property UseItAsWin7Taskbar As Boolean = False
        Public Property Style2 As Boolean = False

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.HighSpeed
            G.TextRenderingHint = My.RenderingHint
            DoubleBuffered = True

            '################################################################################# Customizer
            Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
            '#################################################################################
            G.Clear(BackColor)

            If Not UseItAsWin7Taskbar Then
                If Not Flat Then
                    If Not Style2 Then
                        With Rect
                            G.DrawLine(New Pen(ButtonHilight), New Point(.X, .Y), New Point(.Width - 1, .Y))
                            G.DrawLine(New Pen(ButtonHilight), New Point(.X, .Y), New Point(.X, .Height - 1))
                            G.DrawLine(New Pen(ButtonShadow), New Point(.Width, .X), New Point(.Width, .Height))
                            G.DrawLine(New Pen(ButtonShadow), New Point(.X, .Height), New Point(.Width, .Height))
                        End With
                    Else
                        With Rect
                            G.DrawLine(New Pen(ButtonDkShadow), New Point(.Width, .X), New Point(.Width, .Height))
                            G.DrawLine(New Pen(ButtonDkShadow), New Point(.X, .Height), New Point(.Width, .Height))

                            G.DrawLine(New Pen(ButtonShadow), New Point(.Width - 1, .X - 1), New Point(.Width - 1, .Height - 1))
                            G.DrawLine(New Pen(ButtonShadow), New Point(.X - 1, .Height - 1), New Point(.Width - 1, .Height - 1))

                            G.DrawLine(New Pen(ButtonLight), New Point(.X, .Y), New Point(.Width - 1, .Y))
                            G.DrawLine(New Pen(ButtonLight), New Point(.X, .Y), New Point(.X, .Height - 1))

                            G.DrawLine(New Pen(ButtonHilight), New Point(.X + 1, .Y + 1), New Point(.Width - 2, .Y + 1))
                            G.DrawLine(New Pen(ButtonHilight), New Point(.X + 1, .Y + 1), New Point(.X + 1, .Height - 2))
                        End With
                    End If

                Else
                    G.DrawRectangle(New Pen(ButtonShadow), Rect)
                End If
            Else
                With Rect
                    G.DrawLine(New Pen(ButtonHilight), New Point(.X, .Y + 1), New Point(.X + .Width, .Y + 1))
                End With
            End If

        End Sub
    End Class
End Namespace