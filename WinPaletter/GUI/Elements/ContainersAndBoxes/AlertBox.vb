Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.WP

    <Description("AlertBox for WinPaletter UI")> Public Class AlertBox : Inherits ContainerControl

        Private borderColor, innerColor, textColor As Color

        Sub New()
            TabStop = False
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
            DoubleBuffered = True
            Font = New Font("Segoe UI", 9)
            Size = New Size(200, 40)
            CenterText = False
            CustomColor = Color.FromArgb(0, 81, 210)
        End Sub

#Region "Properties"
        Enum Style
            Adaptive
            Simple
            Success
            Notice
            Warning
            Information
            Indigo
            Custom
        End Enum

        Enum Close
            Yes
            No
        End Enum

        Public Property AlertStyle As Style
            Get
                Return _alertStyle
            End Get
            Set(value As Style)
                _alertStyle = value
                Invalidate()
            End Set
        End Property

        Private _alertStyle As Style

        Public Property Image As Image
        Public Property CustomColor As Color
        Public Property CenterText As Boolean = False

        <Browsable(True)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(System.Drawing.Design.UITypeEditor))>
        <Bindable(True)>
        Public Overrides Property Text As String
#End Region

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaint(e)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = TextRenderingHint.SystemDefault
            DoubleBuffered = True
            Dim RTL As Boolean = (RightToLeft = 1)
            Dim DM As Boolean = My.Style.DarkMode

            Select Case _alertStyle
                Case Style.Simple
                    If DM Then
                        borderColor = Color.FromArgb(60, 60, 60)
                        innerColor = Color.FromArgb(50, 50, 50)
                        textColor = Color.FromArgb(150, 150, 150)
                    Else
                        borderColor = Color.FromArgb(190, 190, 190)
                        innerColor = Color.FromArgb(150, 150, 150)
                        textColor = Color.FromArgb(250, 250, 250)
                    End If

                Case Style.Success
                    If DM Then
                        borderColor = Color.FromArgb(60, 98, 79)
                        innerColor = Color.FromArgb(60, 85, 79)
                        textColor = Color.FromArgb(35, 169, 110)
                    Else
                        borderColor = Color.FromArgb(160, 198, 179)
                        innerColor = Color.FromArgb(140, 170, 155)
                        textColor = Color.FromArgb(135, 255, 210)
                    End If

                Case Style.Notice
                    If DM Then
                        borderColor = Color.FromArgb(70, 91, 107)
                        innerColor = Color.FromArgb(70, 91, 94)
                        textColor = Color.FromArgb(97, 185, 186)
                    Else
                        borderColor = Color.FromArgb(170, 191, 207)
                        innerColor = Color.FromArgb(130, 155, 155)
                        textColor = Color.FromArgb(180, 255, 255)
                    End If

                Case Style.Warning
                    If DM Then
                        borderColor = Color.FromArgb(202, 41, 56)
                        innerColor = Color.FromArgb(125, 20, 30)
                        textColor = Color.FromArgb(254, 142, 122)
                    Else
                        borderColor = Color.FromArgb(200, 171, 171)
                        innerColor = Color.FromArgb(150, 75, 75)
                        textColor = Color.FromArgb(255, 175, 175)
                    End If

                Case Style.Information
                    If DM Then
                        borderColor = Color.FromArgb(133, 133, 71)
                        innerColor = Color.FromArgb(120, 120, 71)
                        textColor = Color.FromArgb(254, 224, 122)
                    Else
                        borderColor = Color.FromArgb(233, 233, 171)
                        innerColor = Color.FromArgb(195, 195, 150)
                        textColor = Color.FromArgb(250, 250, 150)
                    End If


                Case Style.Indigo
                    If DM Then
                        borderColor = Color.FromArgb(65, 0, 170)
                        innerColor = Color.FromArgb(60, 0, 140)
                        textColor = Color.FromArgb(140, 0, 255).CB(0.35)
                    Else
                        borderColor = Color.FromArgb(165, 0, 225)
                        innerColor = Color.FromArgb(129, 0, 200)
                        textColor = Color.FromArgb(210, 110, 255)
                    End If

                Case Style.Custom

                    If DM Then
                        borderColor = CustomColor.CB(0.03)
                        innerColor = CustomColor.CB(0.01)
                        textColor = CustomColor.LightLight
                    Else
                        borderColor = CustomColor.CB(0.3)
                        innerColor = CustomColor.CB(0.1)
                        textColor = CustomColor.CB(0.7)
                    End If

                Case Style.Adaptive
                    If Image IsNot Nothing Then
                        Dim cc As Color = Image.AverageColor

                        If DM Then
                            borderColor = cc.Light(0.01)
                            innerColor = cc.Dark(0.05)
                            textColor = cc.LightLight
                        Else
                            borderColor = cc.Light(1)
                            innerColor = cc.LightLight.CB(0.35)
                            textColor = cc
                        End If

                    Else
                        If DM Then
                            borderColor = CustomColor.CB(0.03)
                            innerColor = CustomColor.CB(0.01)
                            textColor = CustomColor.LightLight
                        Else
                            borderColor = CustomColor.CB(0.3)
                            innerColor = CustomColor.CB(0.1)
                            textColor = CustomColor.CB(0.7)
                        End If
                    End If

            End Select

            G.Clear(GetParentColor)

            BackColor = innerColor

            Using br As New SolidBrush(innerColor) : G.FillRoundedRect(br, New Rectangle(0, 0, Width - 1, Height - 1)) : End Using
            Using P As New Pen(borderColor) : G.DrawRoundedRect_LikeW11(P, New Rectangle(0, 0, Width - 1, Height - 1)) : End Using

            Dim textY As Integer = CInt((Height - Text.Measure(Font).Height) / 2)
            Dim TextX As Integer = 5

            If Image IsNot Nothing Then G.DrawImage(Image, New Rectangle(If(Not RTL, 5, Width - 5 - Image.Width), 5, Image.Width, Image.Height))

            If Not CenterText Then
                If Image Is Nothing Then
                    Using br As New SolidBrush(textColor) : G.DrawString(Text, Font, br, New Rectangle(TextX, 0, Width, Height), ContentAlignment.MiddleLeft.ToStringFormat(RTL)) : End Using
                Else
                    If Not RTL Then
                        Using br As New SolidBrush(textColor) : G.DrawString(Text, Font, br, New Rectangle(10 + Image.Width, 7, Width - (5 + Image.Width), Height - 10), ContentAlignment.TopLeft.ToStringFormat) : End Using
                    Else
                        Using br As New SolidBrush(textColor) : G.DrawString(Text, Font, br, New Rectangle(0, 7, Width - (10 + Image.Width), Height - 10), ContentAlignment.TopLeft.ToStringFormat(RTL)) : End Using
                    End If
                End If
            Else
                Using br As New SolidBrush(textColor) : G.DrawString(Text, Font, br, New Rectangle(1, 0, Width, Height), ContentAlignment.MiddleCenter.ToStringFormat(RTL)) : End Using
            End If

        End Sub

    End Class

End Namespace
