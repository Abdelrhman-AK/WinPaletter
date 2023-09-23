Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.WP

    <Description("Themed TabControl for WinPaletter UI")> Public Class TabControl : Inherits Windows.Forms.TabControl
        Public Property LineColor As Color = Color.FromArgb(0, 81, 210)

        Sub New()
            SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.Opaque, True)
            ItemSize = New Size(40, 150)
            DrawMode = TabDrawMode.OwnerDrawFixed
            SizeMode = TabSizeMode.Fixed
            Font = New Font("Segoe UI", 9)
        End Sub

        Protected Overrides Sub OnDragOver(drgevent As DragEventArgs)
            If TypeOf drgevent.Data.GetData("WinPaletter.UI.Controllers.ColorItem") Is UI.Controllers.ColorItem Then
                drgevent.Effect = DragDropEffects.None
                For i = 0 To TabCount - 1
                    If Not SelectedIndex = i AndAlso GetTabRect(i).Contains(PointToClient(MousePosition)) Then
                        SelectedIndex = i
                        Invalidate()
                    End If
                Next
            Else
                Exit Sub
            End If

            MyBase.OnDragOver(drgevent)
        End Sub
        Protected Overrides Sub CreateHandle()
            MyBase.CreateHandle()

            Dim X1 As Integer = ItemSize.Width
            Dim X2 As Integer = ItemSize.Height

            If Alignment = TabAlignment.Top Or Alignment = TabAlignment.Bottom Then
                If X1 >= X2 Then
                    ItemSize = New Size(X1, X2)
                Else
                    ItemSize = New Size(X2, X1)
                End If
            Else
                If X2 >= X1 Then
                    ItemSize = New Size(X1, X2)
                Else
                    ItemSize = New Size(X2, X1)
                End If
            End If
        End Sub

        ReadOnly Noise As New TextureBrush(My.Resources.GaussianBlur.Fade(0.4))
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias

            G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)

            DoubleBuffered = True

            Dim SelectColor As Color
            Dim TextColor As Color
            Dim ParentColor As Color = MyBase.GetParentColor
            Dim RTL As Boolean = (RightToLeft = 1)
            Dim img As Image = Nothing

            G.Clear(ParentColor)
            Dim Dark As Boolean = My.Style.DarkMode

            For i = 0 To TabCount - 1
                Dim TabRect As Rectangle = GetTabRect(i)
                Dim SideTapeH As Integer = TabRect.Height * 0.5
                Dim SideTapeW As Integer = 3
                Dim SideTape As Rectangle

                If Alignment = TabAlignment.Right Or Alignment = TabAlignment.Left Then
                    SideTape = New Rectangle(TabRect.X + 1, TabRect.Y + (TabRect.Height - SideTapeH) / 2, SideTapeW, SideTapeH)
                ElseIf Alignment = TabAlignment.Top Then
                    SideTape = New Rectangle(TabRect.X + TabRect.Width * 0.125, TabRect.Y + TabRect.Height - SideTapeW - 1, TabRect.Width * 0.75, SideTapeW)
                Else
                    SideTape = New Rectangle(TabRect.X, TabRect.Y, TabRect.Width, SideTapeW)
                End If

                Try
                    If Me.ImageList IsNot Nothing Then
                        Dim ls As ImageList = ImageList
                        img = ls.Images.Item(i)
                        SelectColor = img.AverageColor
                        SelectColor = SelectColor.Light(0.5)
                    Else
                        SelectColor = If(Dark, LineColor, LineColor.LightLight)
                    End If
                Catch
                    SelectColor = If(Dark, LineColor, LineColor.LightLight)
                End Try

                If i = SelectedIndex Then
                    Using br As New SolidBrush(ParentColor.CB(If(Dark, 0.08, -0.08))) : G.FillRoundedRect(br, TabRect) : End Using
                    G.FillRoundedRect(Noise, TabRect)
                    Using br As New SolidBrush(SelectColor) : G.FillRoundedRect(br, SideTape, 2) : End Using
                End If

                TextColor = If(Dark, Color.White, Color.Black)

                Try
                    If Not DesignMode Then TabPages.Item(i).BackColor = ParentColor
                Catch
                End Try

                Dim imgRect As Rectangle

                If img IsNot Nothing Then
                    imgRect = New Rectangle(TabRect.X + 10, TabRect.Y + (TabRect.Height - img.Height) / 2, img.Width, img.Height)
                    If RTL Then img.RotateFlip(RotateFlipType.Rotate180FlipY)
                    G.DrawImage(img, imgRect)
                End If

                If img IsNot Nothing And (Alignment = TabAlignment.Right Or Alignment = TabAlignment.Left) Then
                    If Not RTL Then
                        Dim tr As New Rectangle(imgRect.Right + 10, TabRect.Y, TabRect.Width - imgRect.Width - 10, TabRect.Height)
                        Using br As New SolidBrush(TextColor) : G.DrawString(TabPages(i).Text, Font, br, tr, ContentAlignment.MiddleLeft.ToStringFormat) : End Using
                    Else
                        Dim b As New Bitmap(TabRect.Width, TabRect.Height)
                        Dim gx As Graphics = Graphics.FromImage(b)
                        gx.SmoothingMode = G.SmoothingMode
                        gx.TextRenderingHint = G.TextRenderingHint
                        Using br As New SolidBrush(TextColor) : gx.DrawString(TabPages(i).Text, Font, br, New Rectangle(0, 0, b.Width - imgRect.Right - 10, b.Height - 1), ContentAlignment.MiddleLeft.ToStringFormat(RTL)) : End Using
                        gx.Flush()
                        b.RotateFlip(RotateFlipType.Rotate180FlipY)
                        G.DrawImage(b, TabRect)
                        gx.Dispose()
                        b.Dispose()
                    End If
                Else

                    If Not RTL Then
                        If (Alignment = TabAlignment.Right Or Alignment = TabAlignment.Left) Then
                            Using br As New SolidBrush(TextColor) : G.DrawString(TabPages(i).Text, Font, br, New Rectangle(TabRect.X + SideTape.Right + 2, TabRect.Y + 1, TabRect.Width - SideTape.Right - 2, TabRect.Height), ContentAlignment.MiddleLeft.ToStringFormat) : End Using
                        Else
                            Using br As New SolidBrush(TextColor) : G.DrawString(TabPages(i).Text, Font, br, TabRect, ContentAlignment.MiddleCenter.ToStringFormat) : End Using
                        End If
                    Else
                        Dim b As New Bitmap(TabRect.Width, TabRect.Height)
                        Dim gx As Graphics = Graphics.FromImage(b)
                        gx.SmoothingMode = G.SmoothingMode
                        gx.TextRenderingHint = G.TextRenderingHint
                        Using br As New SolidBrush(TextColor) : gx.DrawString(TabPages(i).Text, Font, br, New Rectangle(0, 0, b.Width - 1, b.Height - 1), ContentAlignment.MiddleCenter.ToStringFormat(RTL)) : End Using
                        gx.Flush()
                        b.RotateFlip(RotateFlipType.Rotate180FlipY)
                        G.DrawImage(b, TabRect)
                        gx.Dispose()
                        b.Dispose()
                    End If
                End If

            Next
        End Sub

    End Class

End Namespace


