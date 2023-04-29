Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

Public Module Paths
    Enum CursorType
        Arrow
        Help
        Busy
        AppLoading
        None
        Move
        Up
        NS
        EW
        NESW
        NWSE
        Pen
        Link
        Pin
        Person
        IBeam
        Cross
    End Enum

    Enum GradientMode
        Vertical
        Horizontal
        ForwardDiagonal
        BackwardDiagonal
        Circle
    End Enum

    Dim Noise As New TextureBrush(My.Resources.GaussianBlurOpaque.Fade(0.2))

    Function ReturnGradientModeFromString([String] As String) As GradientMode
        If [String].Trim.ToLower = "vertical" Then
            Return GradientMode.Vertical

        ElseIf [String].Trim.ToLower = "horizontal" Then
            Return GradientMode.Horizontal

        ElseIf [String].Trim.ToLower = "forward diagonal" Then
            Return GradientMode.ForwardDiagonal

        ElseIf [String].Trim.ToLower = "backward diagonal" Then
            Return GradientMode.BackwardDiagonal

        ElseIf [String].Trim.ToLower = "circle" Then
            Return GradientMode.Circle

        Else
            Return Nothing

        End If

    End Function

    Function ReturnStringFromGradientMode([GradientMode] As GradientMode) As String
        If [GradientMode] = GradientMode.Horizontal Then
            Return "Horizontal"

        ElseIf [GradientMode] = GradientMode.Vertical Then
            Return "Vertical"

        ElseIf [GradientMode] = GradientMode.ForwardDiagonal Then
            Return "Forward Diagonal"

        ElseIf [GradientMode] = GradientMode.BackwardDiagonal Then
            Return "Backward Diagonal"

        ElseIf [GradientMode] = GradientMode.Circle Then
            Return "Circle"

        Else
            Return Nothing

        End If

    End Function

    Function ReturnGradience([Rectangle] As [Rectangle], [Color1] As Color, [Color2] As Color, [GradientMode] As GradientMode, Optional Angle As Single = 0) As Brush

        If [GradientMode] = GradientMode.Horizontal Then
            Return New LinearGradientBrush([Rectangle], [Color1], [Color2], LinearGradientMode.Horizontal)

        ElseIf [GradientMode] = GradientMode.Vertical Then
            Return New LinearGradientBrush([Rectangle], [Color1], [Color2], LinearGradientMode.Vertical)

        ElseIf [GradientMode] = GradientMode.ForwardDiagonal Then
            Return New LinearGradientBrush([Rectangle], [Color1], [Color2], LinearGradientMode.ForwardDiagonal)

        ElseIf [GradientMode] = GradientMode.BackwardDiagonal Then
            Return New LinearGradientBrush([Rectangle], [Color1], [Color2], LinearGradientMode.BackwardDiagonal)

        ElseIf [GradientMode] = GradientMode.Circle Then
            Return New LinearGradientBrush([Rectangle], [Color1], [Color2], Angle, True)

        Else
            Return New SolidBrush([Color1])

        End If

    End Function

    Public Function Draw([CursorOptions] As CursorOptions) As Bitmap
        Dim b As New Bitmap(32 * [CursorOptions].Scale, 32 * [CursorOptions].Scale, PixelFormat.Format32bppPArgb)
        Dim G As Graphics = Graphics.FromImage(b)
        G.SmoothingMode = SmoothingMode.HighQuality
        G.Clear(Color.Transparent)

#Region "Rectangles Helpers"
        Dim _Arrow As New Rectangle(0, 0, b.Width, b.Height)
        Dim _Help As New Rectangle(11, 6, b.Width, b.Height)
        Dim _Busy As New Rectangle(0, 0, 22, 22)
        Dim _CurRect As New Rectangle(0, 8, b.Width, b.Height)
        Dim _LoadRect As New Rectangle(6, 0, 22 * [CursorOptions].Scale, 22 * [CursorOptions].Scale)
        Dim _Pin As New Rectangle(15, 11, b.Width, b.Height)
        Dim _Person As New Rectangle(19, 17, b.Width, b.Height)
#End Region

        Select Case [CursorOptions].[Cursor]
            Case CursorType.Arrow
#Region "Arrow"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If

                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, DefaultCursor(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, DefaultCursor(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, DefaultCursor(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), DefaultCursor(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.Help
#Region "Help"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                Dim BB_H, BL_H As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB_H = ReturnGradience(_Help, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB_H = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL_H = ReturnGradience(_Help, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL_H = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL_H As New Pen(BL_H, [CursorOptions].LineThickness)


                G.FillPath(BB, DefaultCursor(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, DefaultCursor(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, DefaultCursor(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), DefaultCursor(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                G.FillPath(BB_H, Help(_Help, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, Help(_Help, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                If [CursorOptions].ArrowStyle <> ArrowStyle.Classic Then G.DrawPath(PL_H, Help(_Help, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Help(_Help, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

#End Region
            Case CursorType.Busy
#Region "Busy"
                Dim BC, BH As Brush
                If [CursorOptions].[LoadingCircleBackGradient] Then
                    BC = ReturnGradience(_Arrow, [CursorOptions].[LoadingCircleBack1], [CursorOptions].[LoadingCircleBack2], [CursorOptions].[LoadingCircleBackGradientMode], [CursorOptions]._Angle)
                Else
                    BC = New SolidBrush([CursorOptions].[LoadingCircleBack1])
                End If
                If [CursorOptions].[LoadingCircleHotGradient] Then
                    BH = ReturnGradience(_Arrow, [CursorOptions].[LoadingCircleHot1], [CursorOptions].[LoadingCircleHot2], [CursorOptions].[LoadingCircleHotGradientMode], [CursorOptions]._Angle)
                Else
                    BH = New SolidBrush([CursorOptions].[LoadingCircleHot1])
                End If

                If [CursorOptions].CircleStyle = CircleStyle.Classic Then
                    Dim PL As New Pen(BH, [CursorOptions].LineThickness)

                    G.FillPath(BC, Busy(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    G.DrawPath(PL, BusyLoader(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    G.DrawPath(PL, Busy(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))

                    If [CursorOptions].[LoadingCircleBackNoise] Then
                        Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[LoadingCircleBackNoiseOpacity]))
                        G.FillPath(Noise, Busy(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    End If

                    If [CursorOptions].[LoadingCircleHotNoise] Then
                        Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[LoadingCircleHotNoiseOpacity]))
                        G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), BusyLoader(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                        G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Busy(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    End If

                Else
                    G.FillPath(BC, Busy(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))

                    If [CursorOptions].[LoadingCircleBackNoise] Then
                        Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[LoadingCircleBackNoiseOpacity]))
                        G.FillPath(Noise, Busy(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    End If

                    G.FillPath(BH, BusyLoader(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))

                    If [CursorOptions].[LoadingCircleHotNoise] Then
                        Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[LoadingCircleHotNoiseOpacity]))
                        G.FillPath(Noise, BusyLoader(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    End If

                End If


#End Region
            Case CursorType.AppLoading
#Region "AppLoading"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode], [CursorOptions]._Angle)
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode], [CursorOptions]._Angle)
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                Dim BC, BH As Brush
                If [CursorOptions].[LoadingCircleBackGradient] Then
                    BC = ReturnGradience(_LoadRect, [CursorOptions].[LoadingCircleBack1], [CursorOptions].[LoadingCircleBack2], [CursorOptions].[LoadingCircleBackGradientMode], [CursorOptions]._Angle)
                Else
                    BC = New SolidBrush([CursorOptions].[LoadingCircleBack1])
                End If
                If [CursorOptions].[LoadingCircleHotGradient] Then
                    BH = ReturnGradience(_LoadRect, [CursorOptions].[LoadingCircleHot1], [CursorOptions].[LoadingCircleHot2], [CursorOptions].[LoadingCircleHotGradientMode], [CursorOptions]._Angle)
                Else
                    BH = New SolidBrush([CursorOptions].[LoadingCircleHot1])
                End If

                G.FillPath(BB, DefaultCursor(_CurRect, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, DefaultCursor(_CurRect, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, DefaultCursor(_CurRect, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), DefaultCursor(_CurRect, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                If [CursorOptions].CircleStyle = CircleStyle.Classic Then
                    Dim PLx As New Pen(BH, [CursorOptions].LineThickness)

                    G.FillPath(BC, AppLoading(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    G.DrawPath(PLx, AppLoaderCircle(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    G.DrawPath(PLx, AppLoading(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))

                    If [CursorOptions].[LoadingCircleBackNoise] Then
                        Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[LoadingCircleBackNoiseOpacity]))
                        G.FillPath(Noise, AppLoading(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    End If

                    If [CursorOptions].[LoadingCircleHotNoise] Then
                        Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[LoadingCircleHotNoiseOpacity]))
                        G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), AppLoaderCircle(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                        G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), AppLoading(_Busy, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    End If

                Else
                    G.FillPath(BC, AppLoading(_LoadRect, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))

                    If [CursorOptions].[LoadingCircleBackNoise] Then
                        Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[LoadingCircleBackNoiseOpacity]))
                        G.FillPath(Noise, AppLoading(_LoadRect, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    End If

                    G.FillPath(BH, AppLoaderCircle(_LoadRect, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))

                    If [CursorOptions].[LoadingCircleHotNoise] Then
                        Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[LoadingCircleHotNoiseOpacity]))
                        G.FillPath(Noise, AppLoaderCircle(_LoadRect, [CursorOptions]._Angle, [CursorOptions].CircleStyle, [CursorOptions].Scale))
                    End If
                End If

#End Region
            Case CursorType.None
#Region "None"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)


                G.FillPath(BB, NoneBackground(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, NoneBackground(_Arrow, [CursorOptions].Scale))
                End If

                G.FillPath(BL, None(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.FillPath(Noise, None(_Arrow, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.Move
#Region "Move"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, Move(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, Move(_Arrow, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, Move(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Move(_Arrow, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.Up
#Region "Up"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, Up(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, Up(_Arrow, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, Up(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Up(_Arrow, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.NS
#Region "NS"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, NS(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, NS(_Arrow, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, NS(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), NS(_Arrow, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.EW
#Region "EW"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, EW(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, EW(_Arrow, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, EW(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), EW(_Arrow, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.NESW
#Region "NESW"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, NESW(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, NESW(_Arrow, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, NESW(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), NESW(_Arrow, [CursorOptions].Scale))
                End If

#End Region
            Case CursorType.NWSE
#Region "NWSE"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, NWSE(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, NWSE(_Arrow, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, NWSE(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), NWSE(_Arrow, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.Pen
#Region "Pen"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, PenBackground(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, PenBackground(_Arrow, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, Pen(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Pen(_Arrow, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.Link
#Region "Link"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.Pin
#Region "Pin"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                Dim BB_P, BL_P As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB_P = ReturnGradience(_Pin, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB_P = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL_P = ReturnGradience(_Pin, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL_P = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If

                G.FillPath(BB, Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                G.FillPath(BB_P, Pin(_Pin, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, Pin(_Pin, [CursorOptions].Scale))
                End If

                G.FillPath(BL_P, Pin_CenterPoint(_Pin, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, Pin_CenterPoint(_Pin, [CursorOptions].Scale))
                End If

                G.DrawPath(New Pen(BL_P, 2), Pin(_Pin, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Pin(_Pin, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.Person
#Region "Person"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                Dim BB_P, BL_P As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB_P = ReturnGradience(_Person, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB_P = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL_P = ReturnGradience(_Person, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL_P = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If

                G.FillPath(BB, Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Hand(_Arrow, [CursorOptions].ArrowStyle, [CursorOptions].Scale))
                End If

                G.FillPath(BB_P, Person(_Person, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, Person(_Person, [CursorOptions].Scale))
                End If

                G.DrawPath(New Pen(BL_P, 2), Person(_Person, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Person(_Person, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.IBeam
#Region "IBeam"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, IBeam(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, IBeam(_Arrow, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, IBeam(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), IBeam(_Arrow, [CursorOptions].Scale))
                End If
#End Region
            Case CursorType.Cross
#Region "Cross"
                Dim BB, BL As Brush
                If [CursorOptions].[PrimaryColorGradient] Then
                    BB = ReturnGradience(_Arrow, [CursorOptions].[PrimaryColor1], [CursorOptions].[PrimaryColor2], [CursorOptions].[PrimaryColorGradientMode])
                Else
                    BB = New SolidBrush([CursorOptions].[PrimaryColor1])
                End If
                If [CursorOptions].[SecondaryColorGradient] Then
                    BL = ReturnGradience(_Arrow, [CursorOptions].[SecondaryColor1], [CursorOptions].[SecondaryColor2], [CursorOptions].[SecondaryColorGradientMode])
                Else
                    BL = New SolidBrush([CursorOptions].[SecondaryColor1])
                End If
                Dim PL As New Pen(BL, [CursorOptions].LineThickness)

                G.FillPath(BB, Cross(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[PrimaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[PrimaryNoiseOpacity]))
                    G.FillPath(Noise, Cross(_Arrow, [CursorOptions].Scale))
                End If

                G.DrawPath(PL, Cross(_Arrow, [CursorOptions].Scale))

                If [CursorOptions].[SecondaryNoise] Then
                    Noise = New TextureBrush(My.Resources.GaussianBlurOpaque.Fade([CursorOptions].[SecondaryNoiseOpacity]))
                    G.DrawPath(New Pen(Noise, [CursorOptions].LineThickness), Cross(_Arrow, [CursorOptions].Scale))
                End If
#End Region
        End Select

        G.Flush()
        G.Save()


        Dim B_Final As New Bitmap(b.Width, b.Height)
        Dim G_Final As Graphics = Graphics.FromImage(B_Final)

        If [CursorOptions].Shadow_Enabled Then
            Dim shadowedBMP As New Bitmap(b)

            For x As Integer = 0 To b.Width - 1
                For y As Integer = 0 To b.Height - 1
                    shadowedBMP.SetPixel(x, y, Color.FromArgb(b.GetPixel(x, y).A, [CursorOptions].Shadow_Color))
                Next
            Next

            Using ImgF As New ImageProcessor.ImageFactory
                ImgF.Load(shadowedBMP)
                ImgF.GaussianBlur([CursorOptions].Shadow_Blur)
                ImgF.Alpha([CursorOptions].Shadow_Opacity * 100)
                G_Final.DrawImage(ImgF.Image, New Rectangle(0 + [CursorOptions].Shadow_OffsetX, 0 + [CursorOptions].Shadow_OffsetY, b.Width, b.Height))
            End Using
        End If

        G_Final.DrawImage(b, New Rectangle(0, 0, b.Width, b.Height))

        Return New Bitmap(B_Final)
        b.Dispose()
        G.Dispose()
    End Function

    Enum ArrowStyle
        Aero
        Modern
        Classic
    End Enum

    Enum CircleStyle
        Aero
        Dot
        Classic
    End Enum

    Public Function DefaultCursor([Rectangle] As Rectangle, Style As ArrowStyle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        Dim R As New Rectangle([Rectangle].X, [Rectangle].Y, 12, 18)

        Select Case Style
            Case ArrowStyle.Aero
                '#### Left Border
                Dim LLine1 As New Point(R.X, R.Y)
                Dim LLine2 As New Point(R.X, R.Y + R.Height - 2)
                path.AddLine(LLine1, LLine2)

                '#### Left Down Border
                Dim DLLine1 As Point = LLine2 + New Point(1, 0)
                Dim DLLine2 As New Point(DLLine1.X + 3, DLLine1.Y - 3)
                path.AddLine(DLLine1, DLLine2)

                '#### Left Down Handle Border
                Dim DLHLine1 As Point = DLLine2
                Dim DLHLine2 As New Point(DLHLine1.X + 3, DLHLine1.Y + 5)
                path.AddLine(DLHLine1, DLHLine2)

                '#### Down Handle Border
                Dim DHLine1 As Point = DLHLine2
                Dim DHLine2 As New Point(DHLine1.X + 2, DHLine1.Y - 1)

                '#### Right Down Handle Border
                Dim DRHLine1 As Point = DHLine2
                Dim DRHLine2 As New Point(DLHLine1.X + 3, DLHLine1.Y - 1)
                path.AddLine(DRHLine1, DRHLine2)

                '#### Right Down Border
                Dim DRLine1 As Point = DRHLine2
                Dim DRLine2 As New Point(R.X + R.Width - 1, DLHLine1.Y - 1)
                path.AddLine(DRLine1, DRLine2)

                '#### Right Border
                Dim RLine1 As Point = DRLine2 + New Point(0, -1)
                Dim RLine2 As Point = LLine1
                path.AddLine(RLine1, RLine2)

            Case ArrowStyle.Classic
                '#### Left Border
                Dim LLine1 As New Point(R.X, R.Y)
                Dim LLine2 As New Point(R.X, R.Y + R.Height - 2)
                path.AddLine(LLine1, LLine2)

                '#### Left Down Border
                Dim DLLine1 As Point = LLine2 + New Point(1, -1)
                Dim DLLine2 As New Point(DLLine1.X + 3, DLLine1.Y - 3)
                path.AddLine(DLLine1, DLLine2)

                '#### Left Down Handle Border
                Dim DLHLine1 As Point = DLLine2
                Dim DLHLine2 As New Point(DLHLine1.X + 4, DLHLine1.Y + 8)
                path.AddLine(DLHLine1, DLHLine2)

                '#### Down Handle Border
                Dim DHLine1 As Point = DLHLine2
                Dim DHLine2 As New Point(DHLine1.X + 2, DHLine1.Y - 1)

                '#### Right Down Handle Border
                Dim DRHLine1 As Point = DHLine2
                Dim DRHLine2 As New Point(DLHLine1.X + 3, DLHLine1.Y)
                path.AddLine(DRHLine1, DRHLine2)

                '#### Right Down Border
                Dim DRLine1 As Point = DRHLine2 + New Point(0, -1)
                Dim DRLine2 As New Point(R.X + R.Width - 1, DRLine1.Y)
                path.AddLine(DRLine1, DRLine2)

                '#### Right Border
                Dim RLine1 As Point = DRLine2 + New Point(-1, -1)
                Dim RLine2 As Point = LLine1
                path.AddLine(RLine1, RLine2)

            Case ArrowStyle.Modern
                '#### Left Border
                Dim LLine1 As New Point(R.X, R.Y)
                Dim LLine2 As New Point(R.X, R.Y + R.Height - 2)
                path.AddLine(LLine1, LLine2)

                '#### Left Down Border
                Dim DLLine1 As Point = LLine2 + New Point(1, 0)
                Dim DLLine2 As New Point(DLLine1.X + 4, DLLine1.Y - 4)
                path.AddLine(DLLine1, DLLine2)

                '#### Right Down Border
                Dim DRLine1 As Point = DLLine2
                Dim DRLine2 As New Point(R.X + R.Width - 1, DRLine1.Y)
                path.AddLine(DRLine1, DRLine2)

                '#### Right Border
                Dim RLine1 As Point = DRLine2 + New Point(0, -1)
                Dim RLine2 As Point = LLine1
                path.AddLine(RLine1, RLine2)

        End Select

        'path.CloseFigure()

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)
        Return path

    End Function

    Public Function Busy([Rectangle] As Rectangle, Angle As Single, Style As CircleStyle, Optional Scale As Single = 1) As GraphicsPath
        [Rectangle].Width = 22
        [Rectangle].Height = 22
        Dim path As New GraphicsPath

        Select Case Style
            Case CircleStyle.Aero
                path.AddEllipse([Rectangle].X, [Rectangle].Y, 22, 22)
                Dim R As New Rectangle([Rectangle].X + 5, [Rectangle].Y + 5, 12, 12)
                path.AddEllipse(R)
                path.CloseFigure()

            Case CircleStyle.Classic
                path.AddLine(New Point([Rectangle].X + 12, [Rectangle].Y + 0), New Point([Rectangle].X + 6, [Rectangle].Y + 0))
                path.AddLine(New Point([Rectangle].X + 6, [Rectangle].Y + 0), New Point([Rectangle].X + 6, [Rectangle].Y + 2))
                path.AddLine(New Point([Rectangle].X + 6, [Rectangle].Y + 2), New Point([Rectangle].X + 12, [Rectangle].Y + 2))

                path.AddLine(New Point([Rectangle].X + 7, [Rectangle].Y + 2), New Point([Rectangle].X + 7, [Rectangle].Y + 7))
                path.AddLine(New Point([Rectangle].X + 7, [Rectangle].Y + 7), New Point([Rectangle].X + 11, [Rectangle].Y + 10))
                path.AddLine(New Point([Rectangle].X + 11, [Rectangle].Y + 11), New Point([Rectangle].X + 7, [Rectangle].Y + 14))
                path.AddLine(New Point([Rectangle].X + 7, [Rectangle].Y + 14), New Point([Rectangle].X + 7, [Rectangle].Y + 19))

                path.AddLine(New Point([Rectangle].X + 12, [Rectangle].Y + 19), New Point([Rectangle].X + 6, [Rectangle].Y + 19))
                path.AddLine(New Point([Rectangle].X + 6, [Rectangle].Y + 19), New Point([Rectangle].X + 6, [Rectangle].Y + 21))
                path.AddLine(New Point([Rectangle].X + 6, [Rectangle].Y + 21), New Point([Rectangle].X + 12, [Rectangle].Y + 21))

                path.AddPath(MirrorRight(path), False)

                If Angle >= 270 Then
                    Dim m_rotate As New Matrix()
                    m_rotate.RotateAt((Angle - 180) * 3, New Point([Rectangle].X + [Rectangle].Width / 2, [Rectangle].Y + [Rectangle].Height / 2))
                    path.Transform(m_rotate)
                End If

            Case CircleStyle.Dot
                path.AddEllipse([Rectangle].X, [Rectangle].Y, 22, 22)
                Dim R As New Rectangle([Rectangle].X + 5, [Rectangle].Y + 5, 12, 12)
                path.AddEllipse(R)
                path.CloseFigure()

        End Select

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function BusyLoader([Rectangle] As Rectangle, Angle As Single, Style As CircleStyle, Optional Scale As Single = 1) As GraphicsPath
        [Rectangle].X += 0
        [Rectangle].Y += 0

        [Rectangle].Width = 22
        [Rectangle].Height = 22

        Dim path As New GraphicsPath
        Dim CPoint As New Point([Rectangle].X + [Rectangle].Width / 2, [Rectangle].Y + [Rectangle].Height / 2)
        Dim R As New Rectangle([Rectangle].X + 5, [Rectangle].Y + 5, 12, 12)

        Dim innerR = 15
        Dim thickness = 10
        Dim arcLength = 70
        Dim outerR = innerR + thickness

        Select Case Style
            Case CircleStyle.Aero
                Dim outerRect = [Rectangle]
                Dim innerRect = R
                path.AddArc(outerRect, Angle, arcLength)
                path.AddArc(innerRect, Angle + arcLength, -arcLength)
                path.CloseFigure()

            Case CircleStyle.Classic
                Dim PU1 As New Point([Rectangle].X + 12, [Rectangle].Y + 5)
                Dim PU3 As New Point([Rectangle].X + 10, [Rectangle].Y + 5)
                Dim PU4 As Point = PU3 + New Point(2, 2)

                path.AddLine(PU1, PU1)
                path.CloseFigure()
                path.AddLine(PU3, PU4)
                path.CloseFigure()

                Dim PL1 As New Point([Rectangle].X + 12, [Rectangle].Y + 17)
                Dim PL2 As Point = PL1 - New Point(1, -1)

                Dim PL3 As Point = PL1 - New Point(0, 2)
                Dim PL4 As Point = PL3 - New Point(3, -3)

                path.AddLine(PL1, PL2)
                path.CloseFigure()

                path.AddLine(PL3, PL4)
                path.CloseFigure()

                path.AddPath(MirrorRight(path), False)

                Dim C1 As Point = PU1 + New Point(0, 4)
                path.AddLine(C1, C1)
                path.CloseFigure()

                Dim C2 As Point = C1 + New Point(0, 4)
                path.AddLine(C2, C2)
                path.CloseFigure()

                If Angle >= 270 Then
                    Dim m_rotate As New Matrix()
                    m_rotate.RotateAt((Angle - 180) * 3, New Point([Rectangle].X + [Rectangle].Width / 2, [Rectangle].Y + [Rectangle].Height / 2))
                    path.Transform(m_rotate)
                End If

            Case CircleStyle.Dot
                Dim x As Single = 0.85 * CPoint.X + (0.65 * R.Width * CSng(Math.Cos((Angle / 180) * Math.PI)))
                Dim y As Single = 0.85 * CPoint.Y + (0.65 * R.Height * CSng(Math.Sin((Angle / 180) * Math.PI)))
                x = Math.Max([Rectangle].Left, Math.Min(x, [Rectangle].Right))
                y = Math.Max([Rectangle].Top, Math.Min(y, [Rectangle].Bottom))
                path.AddEllipse(New Rectangle(x, y, 5, 5))

        End Select

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function AppLoading([Rectangle] As Rectangle, Angle As Single, Style As CircleStyle, Optional Scale As Single = 1) As GraphicsPath
        [Rectangle].Width = 16
        [Rectangle].Height = 16
        Dim path As New GraphicsPath

        Select Case Style
            Case CircleStyle.Aero
                path.AddEllipse([Rectangle])
                Dim R As New Rectangle([Rectangle].X + 4, [Rectangle].Y + 4, 8, 8)
                path.AddEllipse(R)
                path.CloseFigure()

            Case CircleStyle.Classic
                Dim UpperRectangle As New Rectangle([Rectangle].X + 12, [Rectangle].Y + 9, 9, 2)
                Dim LowerRectangle As New Rectangle([Rectangle].X + 12, [Rectangle].Y + 23, 9, 2)
                Dim Container As New Rectangle(UpperRectangle.X, UpperRectangle.Y, UpperRectangle.Width, LowerRectangle.Bottom - UpperRectangle.Top)

                Dim pL1 As New Point(UpperRectangle.X + 1, UpperRectangle.Y + UpperRectangle.Height)
                Dim pL2 As New Point(pL1.X, pL1.Y + 4)
                path.AddLine(pL1, pL2)

                Dim pL3 As Point = pL2 + New Point(1, 0)
                Dim pL4 As Point = pL3 + New Point(1, 1)
                path.AddLine(pL3, pL4)

                Dim pL5 As Point = pL4 + New Point(0, 1)
                path.AddLine(pL5, pL5)

                Dim pL6 As Point = pL5 + New Point(0, 1)
                Dim pl7 As Point = pL6 + New Point(-1, 1)
                path.AddLine(pL6, pl7)

                Dim pL8 As Point = pl7 - New Point(1, 0)
                Dim pL9 As Point = pL8 + New Point(0, 4)
                path.AddLine(pL8, pL9)

                Dim pL10 As Point = pL9 + New Point(7, 0)
                Dim pL11 As Point = pL10 - New Point(0, 4)
                path.AddLine(pL10, pL11)

                Dim pL12 As Point = pL11 + New Point(-1, 0)
                Dim pL13 As Point = pL12 + New Point(-1, -1)
                path.AddLine(pL12, pL13)

                Dim pL14 As Point = pL13 + New Point(0, -1)
                path.AddLine(pL14, pL14)

                Dim pL15 As Point = pL14 + New Point(0, -1)
                Dim pl16 As Point = pL15 + New Point(1, -1)
                path.AddLine(pL15, pl16)

                Dim pL17 As Point = pl16 + New Point(1, 0)
                Dim pL18 As Point = pL17 + New Point(0, -4)
                path.AddLine(pL17, pL18)

                path.AddRectangle(UpperRectangle)

                path.AddRectangle(LowerRectangle)

                path.CloseFigure()

                Dim FixerL0 As Point = pL3 + New Point(0, 1)
                Dim FixerL1 As Point = pL3 + New Point(0, 3)

                Dim FixerR0 As Point = pL12 - New Point(0, 1)
                Dim FixerR1 As Point = pL12 - New Point(0, 3)

                path.AddLine(FixerL0, FixerL0)
                path.CloseFigure()

                path.AddLine(FixerL1, FixerL1)
                path.CloseFigure()

                path.AddLine(FixerR0, FixerR0)
                path.CloseFigure()

                path.AddLine(FixerR1, FixerR1)
                path.CloseFigure()

                If Angle >= 270 Then
                    Dim m_rotate As New Matrix()
                    m_rotate.RotateAt((Angle - 180) * 3, New Point(Container.X + Container.Width / 2, Container.Y + Container.Height / 2))
                    path.Transform(m_rotate)
                End If

            Case CircleStyle.Dot
                path.AddEllipse([Rectangle])
                Dim R As New Rectangle([Rectangle].X + 4, [Rectangle].Y + 4, 8, 8)
                path.AddEllipse(R)
                path.CloseFigure()

        End Select


        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function AppLoaderCircle([Rectangle] As Rectangle, Angle As Single, Style As CircleStyle, Optional Scale As Single = 1) As GraphicsPath
        [Rectangle].Width = 16
        [Rectangle].Height = 16

        Dim path As New GraphicsPath
        Dim CPoint As New Point([Rectangle].X + [Rectangle].Width / 2, [Rectangle].Y + [Rectangle].Height / 2)
        Dim R As New Rectangle([Rectangle].X + 4, [Rectangle].Y + 4, 8, 8)

        Dim innerR = 15
        Dim thickness = 6
        Dim arcLength = 70
        Dim outerR = innerR + thickness

        Select Case Style
            Case CircleStyle.Aero
                Dim outerRect = [Rectangle]
                Dim innerRect = R
                path.AddArc(outerRect, Angle, arcLength)
                path.AddArc(innerRect, Angle + arcLength, -arcLength)

            Case CircleStyle.Classic
                Dim UpperRectangle As New Rectangle([Rectangle].X + 12, [Rectangle].Y + 9, 9, 2)
                Dim LowerRectangle As New Rectangle([Rectangle].X + 12, [Rectangle].Y + 23, 9, 2)
                Dim Container As New Rectangle(UpperRectangle.X, UpperRectangle.Y, UpperRectangle.Width, LowerRectangle.Bottom - UpperRectangle.Top)

                Dim PU1 As New Point([Rectangle].X + 17, [Rectangle].Y + 14)
                Dim PU3 As New Point([Rectangle].X + 14, [Rectangle].Y + 15)
                Dim PU4 As Point = PU3 + New Point(2, 2)

                path.AddLine(PU1, PU3)
                path.CloseFigure()
                path.AddLine(PU3, PU4)
                path.CloseFigure()


                Dim PL1 As New Point([Rectangle].X + 16, [Rectangle].Y + 20)
                Dim PL2 As Point = PL1 - New Point(2, -2)
                path.AddLine(PL1, PL2)
                path.CloseFigure()

                Dim PL3 As Point = PL1 + New Point(1, 1)
                Dim PL4 As Point = PL3 - New Point(1, -1)
                path.AddLine(PL3, PL4)
                path.CloseFigure()

                Dim C1 As Point = PL3 + New Point(1, 1)
                path.AddLine(C1, C1)
                path.CloseFigure()

                If Angle >= 270 Then
                    Dim m_rotate As New Matrix()
                    m_rotate.RotateAt((Angle - 180) * 3, New Point(Container.X + Container.Width / 2, Container.Y + Container.Height / 2))
                    path.Transform(m_rotate)
                End If

            Case CircleStyle.Dot
                Dim x As Single = 0.85 * CPoint.X + (0.65 * R.Width * CSng(Math.Cos((Angle / 180) * Math.PI)))
                Dim y As Single = 0.85 * CPoint.Y + (0.65 * R.Height * CSng(Math.Sin((Angle / 180) * Math.PI)))
                x = Math.Max([Rectangle].Left, Math.Min(x, [Rectangle].Right))
                y = Math.Max([Rectangle].Top, Math.Min(y, [Rectangle].Bottom))
                path.AddEllipse(New Rectangle(x, y, 5, 5))

        End Select


        path.CloseFigure()

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Move([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 21
        [Rectangle].Height = 21
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim UL1 As New Point([Rectangle].X + 11, [Rectangle].Y)
        Dim UL2 As New Point(UL1.X - 4, [Rectangle].Y + 4)
        path.AddLine(UL1, UL2)

        Dim ULX1 As New Point(UL2.X, UL2.Y + 1)
        Dim ULX2 As New Point(ULX1.X + 3, ULX1.Y)
        path.AddLine(ULX1, ULX2)

        Dim MUL1 As Point = ULX2
        Dim MUL2 As New Point(MUL1.X, ULX2.Y + 4)
        path.AddLine(MUL1, MUL2)

        Dim MULX1 As New Point(MUL2.X, MUL2.Y + 1)
        Dim MULX2 As New Point(MULX1.X - 5, MULX1.Y)
        path.AddLine(MULX1, MULX2)

        Dim LU1 As Point = MULX2
        Dim LU2 As New Point(MULX2.X, MULX2.Y - 3)
        path.AddLine(LU1, LU2)

        Dim LUX1 As New Point(LU2.X - 1, LU2.Y)
        Dim LUX2 As New Point([Rectangle].X, [Rectangle].Y + 11)
        path.AddLine(LUX1, LUX2)

        Dim LDX1 As Point = LUX2
        Dim LDX2 As New Point(LDX1.X + 4, LDX1.Y + 4)
        path.AddLine(LDX1, LDX2)

        Dim LD1 As New Point(LDX2.X + 1, LDX2.Y)
        Dim LD2 As New Point(LD1.X, LD1.Y - 2)
        path.AddLine(LD1, LD2)

        Dim L1 As New Point(LD2.X, LD2.Y - 1)
        Dim L2 As New Point(L1.X + 5, L1.Y)
        path.AddLine(L1, L2)

        Dim DL1 As Point = L2
        Dim DL2 As New Point(L2.X, LD2.Y + 3)
        path.AddLine(DL1, DL2)

        Dim DX1 As New Point(DL2.X, DL2.Y + 1)
        Dim DX2 As New Point(DX1.X - 3, DX1.Y)
        path.AddLine(DX1, DX2)

        Dim DLX1 As New Point(DX2.X, DX2.Y + 1)
        Dim DLX2 As New Point(DLX1.X + 4, DLX1.Y + 4)
        path.AddLine(DLX1, DLX2)

        path.AddPath(MirrorRight(path), False)

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Help([Rectangle] As Rectangle, Style As ArrowStyle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 7
        [Rectangle].Height = 11
        [Rectangle].X = 11
        [Rectangle].Y = 6

        Dim F As FontFamily

        Select Case Style
            Case ArrowStyle.Classic
                F = New FontFamily("Marlett")

                path.AddString("s", F, FontStyle.Bold, 15, [Rectangle], StringFormat.GenericDefault)

            Case Else

                If My.WXP Then
                    F = New FontFamily("Tahoma")

                ElseIf My.W7 Or My.WVista Then
                    F = New FontFamily("Segoe UI")
                Else
                    F = New FontFamily("Segoe UI Black")
                End If

                path.AddString("?", F, FontStyle.Bold, 15, [Rectangle], StringFormat.GenericDefault)

        End Select

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function None([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 17
        [Rectangle].Height = 17
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim R As New Rectangle([Rectangle].X + 2, [Rectangle].Y + 2, [Rectangle].Width - 4, [Rectangle].Height - 4)

        path.AddArc(R, 50, 160)
        path.CloseFigure()

        path.AddArc(R, 230, 160)
        path.CloseFigure()

        path.AddEllipse([Rectangle])

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function NoneBackground([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 17
        [Rectangle].Height = 17
        [Rectangle].X = 0
        [Rectangle].Y = 0

        path.AddEllipse([Rectangle])

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Up([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 9
        [Rectangle].Height = 19
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim UL1 As New Point([Rectangle].X + 4, [Rectangle].Y)
        Dim UL2 As New Point([Rectangle].X, [Rectangle].Y + 4)
        path.AddLine(UL1, UL2)

        Dim ULX1 As New Point(UL2.X, UL2.Y + 1)
        Dim ULX2 As New Point(ULX1.X + 3, ULX1.Y)
        path.AddLine(ULX1, ULX2)

        Dim MUL1 As Point = ULX2
        Dim MUL2 As New Point(MUL1.X, MUL1.Y + 12)
        path.AddLine(MUL1, MUL2)

        Dim D1 As New Point(MUL2.X, MUL2.Y + 1)
        Dim D2 As New Point(D1.X + 1, D1.Y)
        path.AddLine(D1, D2)

        path.AddPath(MirrorRight(path), False)

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function NS([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 9
        [Rectangle].Height = 23
        [Rectangle].X = 0
        [Rectangle].Y = 0


        Dim UL1 As New Point([Rectangle].X + 4, [Rectangle].Y)
        Dim UL2 As New Point([Rectangle].X, [Rectangle].Y + 4)
        path.AddLine(UL1, UL2)

        Dim ULX1 As New Point(UL2.X, UL2.Y + 1)
        Dim ULX2 As New Point(ULX1.X + 3, ULX1.Y)
        path.AddLine(ULX1, ULX2)

        Dim MUL1 As Point = ULX2
        Dim MUL2 As New Point(MUL1.X, MUL1.Y + 12)
        path.AddLine(MUL1, MUL2)

        Dim DL1 As Point = MUL2
        Dim DL2 As New Point(MUL2.X - 3, MUL2.Y)
        path.AddLine(DL1, DL2)

        Dim DX1 As New Point(DL2.X, DL2.Y + 1)
        Dim DX2 As New Point(DX1.X + 4, DX1.Y + 4)
        path.AddLine(DX1, DX2)

        path.AddPath(MirrorRight(path), False)

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function NESW([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 17
        [Rectangle].Height = 17
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim UR1 As New Point([Rectangle].X + [Rectangle].Width - 1, [Rectangle].Y)
        Dim UR2 As New Point(UR1.X - 6, UR1.Y)
        path.AddLine(UR1, UR2)

        Dim RX1 As New Point(UR2.X, UR2.Y + 1)
        Dim RX2 As New Point(RX1.X + 1, RX1.Y + 1)
        path.AddLine(RX1, RX2)

        Dim LX1 As New Point(RX2.X + 1, RX2.Y + 1)
        Dim LX2 As New Point(LX1.X - 9, LX1.Y + 9)
        path.AddLine(LX1, LX2)

        Dim DX1 As New Point(LX2.X - 1, LX2.Y - 1)
        Dim DX2 As New Point(DX1.X - 1, DX1.Y - 1)
        path.AddLine(DX1, DX2)

        Dim L1 As New Point(DX2.X - 1, DX2.Y)
        Dim L2 As New Point(L1.X, L1.Y + 6)
        path.AddLine(L1, L2)

        Dim D1 As New Point(L2.X + 1, L2.Y)
        Dim D2 As New Point(D1.X + 5, D1.Y)
        path.AddLine(D1, D2)

        Dim DL1 As New Point(D2.X, D2.Y - 1)
        Dim DL2 As New Point(DL1.X - 1, DL1.Y - 1)
        path.AddLine(DL1, DL2)

        Dim LX3 As New Point(DL2.X - 1, DL2.Y - 1)
        Dim LX4 As New Point(LX3.X + 9, LX3.Y - 9)
        path.AddLine(LX3, LX4)

        Dim DR1 As New Point(LX4.X + 1, LX4.Y + 1)
        Dim DR2 As New Point(DR1.X + 1, DR1.Y + 1)
        path.AddLine(DR1, DR2)

        Dim R1 As New Point(DR2.X + 1, DR2.Y)
        Dim R2 As New Point(R1.X, R1.Y - 6)
        path.AddLine(R1, R2)

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function NWSE([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As GraphicsPath = NESW([Rectangle])
        [Rectangle].Width = 17
        [Rectangle].Height = 17
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim flipXMatrix As New Matrix(-1, 0, 0, 1, [Rectangle].Width, -1)
        Dim transformMatrix As New Matrix()
        transformMatrix.Multiply(flipXMatrix)
        path.Transform(transformMatrix)

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function EW([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 23
        [Rectangle].Height = 9
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim L1 As New Point([Rectangle].X, [Rectangle].Y + 4)
        Dim L2 As New Point(L1.X + 4, L1.Y - 4)
        path.AddLine(L1, L2)

        Dim LX1 As New Point(L2.X + 1, L2.Y)
        Dim LX2 As New Point(LX1.X, LX1.Y + 2)
        path.AddLine(LX1, LX2)

        Dim U1 As New Point(LX2.X, LX2.Y + 1)
        Dim U2 As New Point(U1.X + 12, U1.Y)
        path.AddLine(U1, U2)

        Dim RX1 As New Point(U2.X, U2.Y - 1)
        Dim RX2 As New Point(RX1.X, RX1.Y - 2)
        path.AddLine(RX1, RX2)

        Dim R1 As New Point(RX2.X + 1, RX2.Y)
        Dim R2 As New Point(R1.X + 4, R1.Y + 4)
        path.AddLine(R1, R2)

        Dim R3 As New Point(R2.X, R2.Y)
        Dim R4 As New Point(R3.X - 4, R3.Y + 4)
        path.AddLine(R3, R4)

        Dim RX3 As New Point(R4.X - 1, R4.Y)
        Dim RX4 As New Point(RX3.X, RX3.Y - 2)
        path.AddLine(RX3, RX4)

        Dim D1 As New Point(RX4.X, RX4.Y - 1)
        Dim D2 As New Point(D1.X - 12, D1.Y)
        path.AddLine(D1, D2)

        Dim LX3 As New Point(D2.X, D2.Y + 1)
        Dim LX4 As New Point(LX3.X, LX3.Y + 2)
        path.AddLine(LX3, LX4)

        Dim L3 As New Point(LX4.X - 1, LX4.Y)
        Dim L4 As New Point(L3.X - 4, L3.Y - 4)
        path.AddLine(L3, L4)

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Pen([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 22
        [Rectangle].Height = 22
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim T1 As New Point([Rectangle].X, [Rectangle].Y)
        Dim T2 As Point = T1 + New Point(6, 2)
        path.AddLine(T1, T2)

        Dim R1 As New Point(T2.X, T2.Y)
        Dim R2 As New Point(R1.X + 15, R1.Y + 15)
        path.AddLine(R1, R2)

        Dim B1 As New Point(R2.X, R2.Y + 1)
        Dim B2 As New Point(B1.X - 3, B1.Y + 3)
        path.AddLine(B1, B2)

        Dim L1 As New Point(B2.X - 1, B2.Y)
        Dim L2 As New Point(L1.X - 15, L1.Y - 15)
        path.AddLine(L1, L2)

        Dim LX1 As New Point(L2.X, L2.Y)
        path.AddLine(LX1, T1)

        path.CloseFigure()

        Dim S1 As New Point([Rectangle].X + 14, [Rectangle].Y + 18)
        Dim S2 As New Point(S1.X + 4, S1.Y - 4)
        path.AddLine(S2, S1)

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function PenBackground([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 22
        [Rectangle].Height = 22
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim T1 As New Point([Rectangle].X, [Rectangle].Y)
        Dim T2 As Point = T1 + New Point(6, 2)
        path.AddLine(T1, T2)

        Dim R1 As New Point(T2.X, T2.Y)
        Dim R2 As New Point(R1.X + 15, R1.Y + 15)
        path.AddLine(R1, R2)

        Dim B1 As New Point(R2.X, R2.Y + 1)
        Dim B2 As New Point(B1.X - 3, B1.Y + 3)
        path.AddLine(B1, B2)

        Dim L1 As New Point(B2.X - 1, B2.Y)
        Dim L2 As New Point(L1.X - 15, L1.Y - 15)
        path.AddLine(L1, L2)

        Dim LX1 As New Point(L2.X, L2.Y)
        path.AddLine(LX1, T1)

        path.CloseFigure()


        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Hand([Rectangle] As Rectangle, Style As ArrowStyle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 18
        [Rectangle].Height = 24
        [Rectangle].X = 0
        [Rectangle].Y = 0

        Select Case Style
            Case ArrowStyle.Classic
                Dim Index_LB1 As New Point([Rectangle].X + 5, [Rectangle].Y + 14)
                Dim Index_LB2 As New Point(Index_LB1.X, Index_LB1.Y - 11)
                path.AddLine(Index_LB1, Index_LB2)

                Dim Index_RB1 As New Point(Index_LB1.X + 3, Index_LB1.Y - 4)
                Dim Index_RB2 As New Point(Index_RB1.X, Index_RB1.Y - 8)
                path.AddArc(Index_LB2.X, Index_LB2.Y - 2, 3, 2, 180, 180)

                path.AddLine(Index_RB1, Index_RB2)
                path.AddArc(Index_RB2.X, Index_LB2.Y + 3, 3, 2, 180, 180)

                Dim Middle_RB1 As New Point(Index_RB1.X + 3, Index_RB1.Y)
                Dim Middle_RB2 As New Point(Middle_RB1.X, Middle_RB1.Y - 3)
                path.AddLine(Middle_RB1, Middle_RB2)
                path.AddArc(Middle_RB2.X, Index_LB2.Y + 4, 3, 2, 180, 180)

                Dim Ring_RB1 As New Point(Middle_RB1.X + 3, Middle_RB1.Y + 1)
                Dim Ring_RB2 As New Point(Ring_RB1.X, Ring_RB1.Y - 2)
                path.AddLine(Ring_RB1, Ring_RB2)
                path.AddArc(Ring_RB2.X, Index_LB2.Y + 6, 3, 2, 180, 180)

                Dim FreeBorder1 As New Point(Ring_RB1.X + 3, Ring_RB1.Y - 1)
                Dim FreeBorder2 As New Point(FreeBorder1.X, FreeBorder1.Y + 6)
                path.AddLine(FreeBorder1, FreeBorder2)

                Dim LW1 As Point = FreeBorder2 + New Point(0, 1)
                Dim RW1 As New Point(LW1.X - 14, LW1.Y)
                Dim Btm As New Rectangle(RW1.X + 3, RW1.Y - 8, 9, 13)
                path.AddLine(FreeBorder2, New Point(Btm.X + Btm.Width, Btm.Y + Btm.Height))
                path.AddLine(New Point(Btm.X + Btm.Width, Btm.Y + Btm.Height), New Point(Btm.X, Btm.Y + Btm.Height))

                Dim L1 As Point = RW1 - New Point(0, 1)
                Dim L2 As New Point(L1.X - 1, L1.Y - 3)
                Dim Thumb As New Rectangle(L2.X - 1, L2.Y - 3, 2, 3)
                path.AddArc(Thumb, 90, 180)

                Dim LastBorder1 As New Point(Thumb.X + Thumb.Width, Thumb.Y)
                Dim LastBorder2 As New Point(LastBorder1.X + 2, LastBorder1.Y + 1)
                path.AddLine(LastBorder1, LastBorder2)

                path.CloseFigure()

            Case Else
                Dim Index_LB1 As New Point([Rectangle].X + 5, [Rectangle].Y + 14)
                Dim Index_LB2 As New Point(Index_LB1.X, Index_LB1.Y - 12)
                path.AddLine(Index_LB1, Index_LB2)

                Dim Index_RB1 As New Point(Index_LB1.X + 3, Index_LB1.Y - 4)
                Dim Index_RB2 As New Point(Index_RB1.X, Index_RB1.Y - 8)

                path.AddArc(Index_LB2.X, Index_LB2.Y - 2, 3, 2, 180, 180)

                path.AddLine(Index_RB1, Index_RB2)

                path.AddArc(Index_RB2.X, Index_LB2.Y + 3, 3, 2, 180, 180)

                Dim Middle_RB1 As New Point(Index_RB1.X + 3, Index_RB1.Y)
                Dim Middle_RB2 As New Point(Middle_RB1.X, Middle_RB1.Y - 3)
                path.AddLine(Middle_RB1, Middle_RB2)

                path.AddArc(Middle_RB2.X, Index_LB2.Y + 4, 3, 2, 180, 180)

                Dim Ring_RB1 As New Point(Middle_RB1.X + 3, Middle_RB1.Y)
                Dim Ring_RB2 As New Point(Ring_RB1.X, Ring_RB1.Y - 2)
                path.AddLine(Ring_RB1, Ring_RB2)

                path.AddArc(Ring_RB2.X, Index_LB2.Y + 5, 3, 2, 180, 180)

                Dim FreeBorder1 As New Point(Ring_RB1.X + 3, Ring_RB1.Y - 1)
                Dim FreeBorder2 As New Point(FreeBorder1.X, FreeBorder1.Y + 8)
                path.AddLine(FreeBorder1, FreeBorder2)

                Dim LW1 As Point = FreeBorder2 + New Point(0, 1)
                Dim RW1 As New Point(LW1.X - 14, LW1.Y)
                Dim Btm As New Rectangle(RW1.X, RW1.Y - 8, 14, 13)
                path.AddArc(Btm, 0, 180)

                Dim L1 As Point = RW1 - New Point(0, 1)
                Dim L2 As New Point(L1.X - 2, L1.Y - 2)
                Dim Thumb As New Rectangle(L2.X - 1, L2.Y - 3, 2, 3)
                path.AddArc(Thumb, 90, 180)
                'path.AddRectangle(Thumb)

                Dim LastBorder1 As New Point(Thumb.X + Thumb.Width, Thumb.Y)
                Dim LastBorder2 As New Point(LastBorder1.X + 2, LastBorder1.Y + 1)
                path.AddLine(LastBorder1, LastBorder2)

                path.CloseFigure()
        End Select

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Pin([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 13
        [Rectangle].Height = 20
        [Rectangle].X = 15
        [Rectangle].Y = 11

        Dim U As New Rectangle([Rectangle].X, [Rectangle].Y, 12, 10)
        path.AddArc(U, 180, 180)

        Dim C As New Point([Rectangle].X + 6, [Rectangle].Y + 18)
        Dim p1 As New Point([Rectangle].X + 0, [Rectangle].Y + 6)
        Dim p2 As New Point([Rectangle].X + 12, [Rectangle].Y + 6)
        path.AddLine(p2, C)
        path.AddLine(C, p1)
        path.CloseFigure()

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Person([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 10
        [Rectangle].Height = 13
        [Rectangle].X = 19
        [Rectangle].Y = 17

        Dim Face As New Rectangle([Rectangle].X, [Rectangle].Y, 5, 6)
        path.AddEllipse(Face)

        Dim TrunkUpper As New Rectangle(Face.X - 2, Face.Y + Face.Height, 9, 9)
        path.AddArc(TrunkUpper, 180, 180)

        Dim TrunkLower As New Rectangle(TrunkUpper.X, TrunkUpper.Y + 3, 9, 3)
        path.AddArc(TrunkLower, 0, 180)

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function IBeam([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath

        [Rectangle].X = 0
        [Rectangle].Y = 0

        Dim L1 As New Point([Rectangle].X, [Rectangle].Y)
        Dim L2 As New Point(L1.X, L1.Y + 2)
        path.AddLine(L1, L2)

        Dim BU1 As New Point(L2.X + 3, L2.Y)
        path.AddLine(L2, BU1)

        Dim LX As New Point(BU1.X, BU1.Y + 13)
        path.AddLine(BU1, LX)

        Dim BU2 As New Point(LX.X - 3, LX.Y)
        path.AddLine(LX, BU2)

        Dim L3 As New Point(BU2.X, BU2.Y + 2)
        path.AddLine(BU2, L3)

        Dim Bl As New Point(L3.X + 3, L3.Y)
        path.AddLine(L3, Bl)

        Dim XB As New Point(Bl.X + 1, Bl.Y - 1)
        path.AddLine(Bl, XB)

        Dim Br As New Point(XB.X + 1, XB.Y + 1)
        path.AddLine(XB, Br)

        Dim RB As New Point(Br.X + 3, Br.Y)
        path.AddLine(Br, RB)

        Dim R1 As New Point(RB.X, RB.Y - 2)
        path.AddLine(RB, R1)

        Dim BU3 As New Point(R1.X - 3, R1.Y)
        path.AddLine(R1, BU3)

        Dim RX As New Point(BU3.X, BU3.Y - 13)
        path.AddLine(BU3, RX)

        Dim TU As New Point(RX.X + 3, RX.Y)
        path.AddLine(RX, TU)

        Dim RR As New Point(TU.X, TU.Y - 2)
        path.AddLine(TU, RR)

        Dim T As New Point(RR.X - 3, RR.Y)
        path.AddLine(RR, T)

        Dim Tx As New Point(T.X - 1, T.Y + 1)
        path.AddLine(T, Tx)

        Dim TXL As New Point(Tx.X - 1, Tx.Y - 1)
        path.AddLine(Tx, TXL)

        Dim TL As New Point(TXL.X - 3, TXL.Y)
        path.AddLine(TXL, TL)


        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Cross([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 19
        [Rectangle].Height = 19

        Dim L1 As New Point(9, 0)
        Dim L2 As New Point(L1.X - 1, L1.Y)
        path.AddLine(L1, L2)

        Dim L3 As New Point(L2.X, L2.Y + 8)
        path.AddLine(L2, L3)

        Dim L4 As New Point(L3.X - 8, L3.Y)
        path.AddLine(L3, L4)

        Dim L5 As New Point(L4.X, L4.Y + 2)
        path.AddLine(L4, L5)

        Dim L6 As New Point(L5.X + 8, L5.Y)
        path.AddLine(L5, L6)

        Dim L7 As New Point(L6.X, L6.Y + 8)
        path.AddLine(L6, L7)

        Dim L8 As New Point(L7.X + 1, L7.Y)
        path.AddLine(L7, L8)

        path.AddPath(MirrorRight(path), False)

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Public Function Pin_CenterPoint([Rectangle] As Rectangle, Optional Scale As Single = 1) As GraphicsPath
        Dim path As New GraphicsPath
        [Rectangle].Width = 13
        [Rectangle].Height = 20
        [Rectangle].X = 15
        [Rectangle].Y = 11

        Dim o As New Rectangle([Rectangle].X, [Rectangle].Y, 12, 12)
        Dim o1 As New Rectangle([Rectangle].X, [Rectangle].Y, 6, 6)
        o1.X = [Rectangle].X + (o.Width - o1.Width) / 2
        o1.Y = [Rectangle].Y + (o.Height - o1.Height) / 2
        path.AddEllipse(o1)

        Dim m As New Matrix()
        m.Scale(Scale, Scale, MatrixOrder.Append)
        m.Translate(1, 1, MatrixOrder.Append)
        path.Transform(m)

        Return path
    End Function

    Private Function MirrorRight(ByVal path As GraphicsPath) As GraphicsPath
        Dim r = path.GetBounds()
        Dim p = CType(path.Clone(), GraphicsPath)
        p.Transform(New Matrix(-1, 0, 0, 1, 2 * (r.Left + r.Width), 0))
        Return p
    End Function

End Module

Public Class CursorOptions

    Sub New(Optional Cursor As CP.Structures.Cursor = Nothing)
        If Cursor <> Nothing Then
            ArrowStyle = Cursor.ArrowStyle
            CircleStyle = Cursor.CircleStyle
            PrimaryColor1 = Cursor.PrimaryColor1
            PrimaryColor2 = Cursor.PrimaryColor2
            PrimaryColorGradient = Cursor.PrimaryColorGradient
            PrimaryColorGradientMode = Cursor.PrimaryColorGradientMode
            SecondaryColor1 = Cursor.SecondaryColor1
            SecondaryColor2 = Cursor.SecondaryColor2
            SecondaryColorGradient = Cursor.SecondaryColorGradient
            SecondaryColorGradientMode = Cursor.SecondaryColorGradientMode
            LoadingCircleBack1 = Cursor.LoadingCircleBack1
            LoadingCircleBack2 = Cursor.LoadingCircleBack2
            LoadingCircleBackGradient = Cursor.LoadingCircleBackGradient
            LoadingCircleBackGradientMode = Cursor.LoadingCircleBackGradientMode
            LoadingCircleHot1 = Cursor.LoadingCircleHot1
            LoadingCircleHot2 = Cursor.LoadingCircleHot2
            LoadingCircleHotGradient = Cursor.LoadingCircleHotGradient
            LoadingCircleHotGradientMode = Cursor.LoadingCircleHotGradientMode
            PrimaryNoise = Cursor.PrimaryColorNoise
            PrimaryNoiseOpacity = Cursor.PrimaryColorNoiseOpacity
            SecondaryNoise = Cursor.SecondaryColorNoise
            SecondaryNoiseOpacity = Cursor.SecondaryColorNoiseOpacity
            LoadingCircleBackNoise = Cursor.LoadingCircleBackNoise
            LoadingCircleBackNoiseOpacity = Cursor.LoadingCircleBackNoiseOpacity
            LoadingCircleHotNoise = Cursor.LoadingCircleHotNoise
            LoadingCircleHotNoiseOpacity = Cursor.LoadingCircleHotNoiseOpacity
            Shadow_Enabled = Cursor.Shadow_Enabled
            Shadow_Color = Cursor.Shadow_Color
            Shadow_Blur = Cursor.Shadow_Blur
            Shadow_Opacity = Cursor.Shadow_Opacity
            Shadow_OffsetX = Cursor.Shadow_OffsetX
            Shadow_OffsetY = Cursor.Shadow_OffsetY
        End If
    End Sub

    Public [Cursor] As CursorType
    Public ArrowStyle As Paths.ArrowStyle
    Public CircleStyle As Paths.CircleStyle
    Public [PrimaryColor1] As Color
    Public [PrimaryColor2] As Color
    Public [PrimaryColorGradient] As Boolean
    Public [PrimaryColorGradientMode] As GradientMode
    Public [SecondaryColor1] As Color
    Public [SecondaryColor2] As Color
    Public [SecondaryColorGradient] As Boolean
    Public [SecondaryColorGradientMode] As GradientMode
    Public [LoadingCircleBack1] As Color
    Public [LoadingCircleBack2] As Color
    Public [LoadingCircleBackGradient] As Boolean
    Public [LoadingCircleBackGradientMode] As GradientMode
    Public [LoadingCircleHot1] As Color
    Public [LoadingCircleHot2] As Color
    Public [LoadingCircleHotGradient] As Boolean
    Public [LoadingCircleHotGradientMode] As GradientMode
    Public [PrimaryNoise] As Boolean
    Public [PrimaryNoiseOpacity] As Single
    Public [SecondaryNoise] As Boolean
    Public [SecondaryNoiseOpacity] As Single
    Public [LoadingCircleBackNoise] As Boolean
    Public [LoadingCircleBackNoiseOpacity] As Single
    Public [LoadingCircleHotNoise] As Boolean
    Public [LoadingCircleHotNoiseOpacity] As Single
    Public [LineThickness] As Single = 1
    Public Scale As Single = 1
    Public _Angle As Single = 180
    Public Shadow_Enabled As Boolean = False
    Public Shadow_Color As Color = Color.Black
    Public Shadow_Blur As Integer = 5
    Public Shadow_Opacity As Single = 0.3
    Public Shadow_OffsetX As Integer = 2
    Public Shadow_OffsetY As Integer = 2

End Class

Public Class CursorControl : Inherits ContainerControl
    Sub New()

    End Sub

    Public Property Prop_Cursor As Paths.CursorType = CursorType.Arrow
    Public Property Prop_ArrowStyle As Paths.ArrowStyle = ArrowStyle.Aero
    Public Property Prop_CircleStyle As Paths.CircleStyle = CircleStyle.Aero
    Public Property Prop_PrimaryColor1 As Color = Color.White
    Public Property Prop_PrimaryColor2 As Color = Color.White
    Public Property Prop_PrimaryColorGradient As Boolean = False
    Public Property Prop_PrimaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Prop_PrimaryNoise As Boolean = False
    Public Property Prop_PrimaryNoiseOpacity As Single = 0.25

    Public Property Prop_SecondaryColor1 As Color = Color.FromArgb(64, 65, 75)
    Public Property Prop_SecondaryColor2 As Color = Color.FromArgb(64, 65, 75)
    Public Property Prop_SecondaryColorGradient As Boolean = False
    Public Property Prop_SecondaryColorGradientMode As GradientMode = GradientMode.Vertical
    Public Property Prop_SecondaryNoise As Boolean = False
    Public Property Prop_SecondaryNoiseOpacity As Single = 0.25

    Public Property Prop_LoadingCircleBack1 As Color = Color.FromArgb(42, 151, 243)
    Public Property Prop_LoadingCircleBack2 As Color = Color.FromArgb(42, 151, 243)
    Public Property Prop_LoadingCircleBackGradient As Boolean = False
    Public Property Prop_LoadingCircleBackGradientMode As GradientMode = GradientMode.Vertical
    Public Property Prop_LoadingCircleBackNoise As Boolean = False
    Public Property Prop_LoadingCircleBackNoiseOpacity As Single = 0.25

    Public Property Prop_LoadingCircleHot1 As Color = Color.FromArgb(37, 204, 255)
    Public Property Prop_LoadingCircleHot2 As Color = Color.FromArgb(37, 204, 255)
    Public Property Prop_LoadingCircleHotGradient As Boolean = False
    Public Property Prop_LoadingCircleHotGradientMode As GradientMode = GradientMode.Vertical
    Public Property Prop_LoadingCircleHotNoise As Boolean = False
    Public Property Prop_LoadingCircleHotNoiseOpacity As Single = 0.25

    Public Property Prop_Shadow_Enabled As Boolean = False
    Public Property Prop_Shadow_Color As Color = Color.Black
    Public Property Prop_Shadow_Blur As Integer = 5
    Public Property Prop_Shadow_Opacity As Single = 0.3
    Public Property Prop_Shadow_OffsetX As Integer = 2
    Public Property Prop_Shadow_OffsetY As Integer = 2


    Public Property Prop_Scale As Single = 1

    Private _Shown As Boolean = False

    Public _Focused As Boolean = False

    Dim bmp As Bitmap

    Public Angle As Single = 180

    Private Sub XenonRadioButton_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated

        Try
            If Not DesignMode Then
                AddHandler FindForm.Load, AddressOf Loaded
                AddHandler FindForm.Shown, AddressOf Showed
                AddHandler Parent.BackColorChanged, AddressOf RefreshColorPalette
            End If
        Catch
        End Try

    End Sub

    Sub Loaded()
        _Shown = False
    End Sub

    Sub Showed()
        _Shown = True
        Invalidate()
    End Sub

    Public Sub RefreshColorPalette()
        If _Shown Then
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        Dim CurOptions As New CursorOptions With {
            .Cursor = Prop_Cursor,
            .ArrowStyle = Prop_ArrowStyle,
            .CircleStyle = Prop_CircleStyle,
            .PrimaryColor1 = Prop_PrimaryColor1,
            .PrimaryColor2 = Prop_PrimaryColor2,
            .PrimaryColorGradient = Prop_PrimaryColorGradient,
            .PrimaryColorGradientMode = Prop_PrimaryColorGradientMode,
            .SecondaryColor1 = Prop_SecondaryColor1,
            .SecondaryColor2 = Prop_SecondaryColor2,
            .SecondaryColorGradient = Prop_SecondaryColorGradient,
            .SecondaryColorGradientMode = Prop_SecondaryColorGradientMode,
            .LoadingCircleBack1 = Prop_LoadingCircleBack1,
            .LoadingCircleBack2 = Prop_LoadingCircleBack2,
            .LoadingCircleBackGradient = Prop_LoadingCircleBackGradient,
            .LoadingCircleBackGradientMode = Prop_LoadingCircleBackGradientMode,
            .LoadingCircleHot1 = Prop_LoadingCircleHot1,
            .LoadingCircleHot2 = Prop_LoadingCircleHot2,
            .LoadingCircleHotGradient = Prop_LoadingCircleHotGradient,
            .LoadingCircleHotGradientMode = Prop_LoadingCircleHotGradientMode,
            .PrimaryNoise = Prop_PrimaryNoise,
            .PrimaryNoiseOpacity = Prop_PrimaryNoiseOpacity,
            .SecondaryNoise = Prop_SecondaryNoise,
            .SecondaryNoiseOpacity = Prop_SecondaryNoiseOpacity,
            .LoadingCircleBackNoise = Prop_LoadingCircleBackNoise,
            .LoadingCircleBackNoiseOpacity = Prop_LoadingCircleBackNoiseOpacity,
            .LoadingCircleHotNoise = Prop_LoadingCircleHotNoise,
            .LoadingCircleHotNoiseOpacity = Prop_LoadingCircleHotNoiseOpacity,
            .LineThickness = 1,
            .Scale = Prop_Scale,
            ._Angle = Angle,
            .Shadow_Enabled = Prop_Shadow_Enabled,
            .Shadow_Blur = Prop_Shadow_Blur,
            .Shadow_Color = Prop_Shadow_Color,
            .Shadow_Opacity = Prop_Shadow_Opacity,
            .Shadow_OffsetX = Prop_Shadow_OffsetX,
            .Shadow_OffsetY = Prop_Shadow_OffsetY}

        bmp = New Bitmap(32 * Prop_Scale, 32 * Prop_Scale, PixelFormat.Format32bppPArgb)

        bmp = Draw(CurOptions)

        DoubleBuffered = True

        Dim MainRect As New Rectangle(0, 0, Width - 1, Height - 1)

        Dim CenterRect As New Rectangle(MainRect.X + (MainRect.Width - bmp.Width) / 2,
                                        MainRect.Y + (MainRect.Height - bmp.Height) / 2,
                                        bmp.Width, bmp.Height)

        e.Graphics.Clear(GetParentColor)
        e.Graphics.FillRoundedRect(New SolidBrush(If(_Focused, Style.Colors.Back_Checked, Style.Colors.Back)), MainRect)
        e.Graphics.DrawRoundedRect_LikeW11(New Pen(If(_Focused, Style.Colors.Border_Checked_Hover, Style.Colors.Border)), MainRect)
        e.Graphics.DrawImage(bmp, CenterRect)

    End Sub

    Private Sub CursorControl_Click(sender As Object, e As EventArgs) Handles Me.Click

        For Each c As CursorControl In Parent.Controls.OfType(Of CursorControl)
            If c Is sender Then
                c._Focused = True
                c.Invalidate()
            Else
                c._Focused = False
                c.Invalidate()
            End If
        Next

    End Sub
End Class
