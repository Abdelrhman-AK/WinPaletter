Imports System.Drawing.Imaging
Imports System.Runtime.CompilerServices
Imports Devcorp.Controls.VisualStyles

Public Class VisualStylesRes
    Sub New(themeFile As String)
        _VisualStyleFile = New VisualStyleFile(themeFile)
        Try
            Colors = _VisualStyleFile.Metrics.Colors
        Catch
        End Try

        Try
            Metrics = _VisualStyleFile.Metrics.Sizes
        Catch
        End Try

    End Sub

    Private _VisualStyleFile As VisualStyleFile
    Public Property Colors As New VisualStyleMetricColors
    Public Property Metrics As New VisualStyleMetricSizes

    Enum Element
        Titlebar
        RightEdge
        LeftEdge
        BottomEdge
        Taskbar
        CloseButton
    End Enum

    Public Sub Draw(G As Graphics, [Rectangle] As Rectangle, element As Element, Active As Boolean, ToolWindow As Boolean)
        Dim el As VisualStyleElement

        Select Case element
            Case Element.Titlebar
                Dim CS As WindowCaptionState = If(Active, WindowCaptionState.Active, WindowCaptionState.Inactive)

                If Not ToolWindow Then
                    el = VisualStyleElement.Window.Caption.GetElement(_VisualStyleFile, CS)
                Else
                    el = VisualStyleElement.Window.SmallCaption.GetElement(_VisualStyleFile, CS)
                End If

            Case Element.RightEdge
                Dim FS As WindowFrameState = If(Active, WindowFrameState.Active, WindowFrameState.Inactive)

                If Not ToolWindow Then
                    el = VisualStyleElement.Window.FrameRight.GetElement(_VisualStyleFile, FS)
                Else
                    el = VisualStyleElement.Window.SmallFrameRight.GetElement(_VisualStyleFile, FS)
                End If

            Case Element.LeftEdge
                Dim FS As WindowFrameState = If(Active, WindowFrameState.Active, WindowFrameState.Inactive)

                If Not ToolWindow Then
                    el = VisualStyleElement.Window.FrameLeft.GetElement(_VisualStyleFile, FS)
                Else
                    el = VisualStyleElement.Window.SmallFrameLeft.GetElement(_VisualStyleFile, FS)
                End If

            Case Element.BottomEdge
                Dim FS As WindowFrameState = If(Active, WindowFrameState.Active, WindowFrameState.Inactive)

                If Not ToolWindow Then
                    el = VisualStyleElement.Window.FrameBottom.GetElement(_VisualStyleFile, FS)
                Else
                    el = VisualStyleElement.Window.SmallFrameBottom.GetElement(_VisualStyleFile, FS)
                End If

            Case Element.CloseButton
                Dim BS As WindowButtonState = If(Active, WindowButtonState.Normal, WindowButtonState.Disabled)

                If Not ToolWindow Then
                    el = VisualStyleElement.Window.CloseButton.GetElement(_VisualStyleFile, BS)
                Else
                    el = VisualStyleElement.Window.SmallCloseButton.GetElement(_VisualStyleFile, BS)
                End If

            Case Element.Taskbar
                el = VisualStyleElement.TaskBar.BackgroundBottom.GetElement(_VisualStyleFile)

            Case Else
                el = Nothing

        End Select

        Try
            Dim renderer As New VisualStyleRenderer(el)
            renderer.DrawBackground(G, [Rectangle])
        Catch
        End Try

    End Sub

End Class
