Imports WinPaletter.XenonCore

Public Class CursorsStudio
    Private _Shown As Boolean = False
    Private _SelectedControl As CursorControl
    Private _CopiedControl As CursorControl
    Private ReadOnly AnimateList As New List(Of CursorControl)

    Sub CursorCP_to_Cursor([CursorControl] As CursorControl, [Cursor] As CP.Structures.Cursor)
        [CursorControl].Prop_ArrowStyle = [Cursor].ArrowStyle
        [CursorControl].Prop_CircleStyle = [Cursor].CircleStyle
        [CursorControl].Prop_PrimaryColor1 = [Cursor].PrimaryColor1
        [CursorControl].Prop_PrimaryColor2 = [Cursor].PrimaryColor2
        [CursorControl].Prop_PrimaryColorGradient = [Cursor].PrimaryColorGradient
        [CursorControl].Prop_PrimaryColorGradientMode = [Cursor].PrimaryColorGradientMode
        [CursorControl].Prop_PrimaryNoise = [Cursor].PrimaryColorNoise
        [CursorControl].Prop_PrimaryNoiseOpacity = [Cursor].PrimaryColorNoiseOpacity
        [CursorControl].Prop_SecondaryColor1 = [Cursor].SecondaryColor1
        [CursorControl].Prop_SecondaryColor2 = [Cursor].SecondaryColor2
        [CursorControl].Prop_SecondaryColorGradient = [Cursor].SecondaryColorGradient
        [CursorControl].Prop_SecondaryColorGradientMode = [Cursor].SecondaryColorGradientMode
        [CursorControl].Prop_SecondaryNoise = [Cursor].SecondaryColorNoise
        [CursorControl].Prop_SecondaryNoiseOpacity = [Cursor].SecondaryColorNoiseOpacity
        [CursorControl].Prop_LoadingCircleBack1 = [Cursor].LoadingCircleBack1
        [CursorControl].Prop_LoadingCircleBack2 = [Cursor].LoadingCircleBack2
        [CursorControl].Prop_LoadingCircleBackGradient = [Cursor].LoadingCircleBackGradient
        [CursorControl].Prop_LoadingCircleBackGradientMode = [Cursor].LoadingCircleBackGradientMode
        [CursorControl].Prop_LoadingCircleBackNoise = [Cursor].LoadingCircleBackNoise
        [CursorControl].Prop_LoadingCircleBackNoiseOpacity = [Cursor].LoadingCircleBackNoiseOpacity
        [CursorControl].Prop_LoadingCircleHot1 = [Cursor].LoadingCircleHot1
        [CursorControl].Prop_LoadingCircleHot2 = [Cursor].LoadingCircleHot2
        [CursorControl].Prop_LoadingCircleHotGradient = [Cursor].LoadingCircleHotGradient
        [CursorControl].Prop_LoadingCircleHotGradientMode = [Cursor].LoadingCircleHotGradientMode
        [CursorControl].Prop_LoadingCircleHotNoise = [Cursor].LoadingCircleHotNoise
        [CursorControl].Prop_LoadingCircleHotNoiseOpacity = [Cursor].LoadingCircleHotNoiseOpacity
        [CursorControl].Prop_Shadow_Enabled = [Cursor].Shadow_Enabled
        [CursorControl].Prop_Shadow_Color = [Cursor].Shadow_Color
        [CursorControl].Prop_Shadow_Blur = [Cursor].Shadow_Blur
        [CursorControl].Prop_Shadow_Opacity = [Cursor].Shadow_Opacity
        [CursorControl].Prop_Shadow_OffsetX = [Cursor].Shadow_OffsetX
        [CursorControl].Prop_Shadow_OffsetY = [Cursor].Shadow_OffsetY
    End Sub

    Function Cursor_to_CursorCP([CursorControl] As CursorControl) As CP.Structures.Cursor
        Dim [Cursor] As CP.Structures.Cursor
        [Cursor].ArrowStyle = [CursorControl].Prop_ArrowStyle
        [Cursor].CircleStyle = [CursorControl].Prop_CircleStyle
        [Cursor].PrimaryColor1 = [CursorControl].Prop_PrimaryColor1
        [Cursor].PrimaryColor2 = [CursorControl].Prop_PrimaryColor2
        [Cursor].PrimaryColorGradient = [CursorControl].Prop_PrimaryColorGradient
        [Cursor].PrimaryColorGradientMode = [CursorControl].Prop_PrimaryColorGradientMode
        [Cursor].PrimaryColorNoise = [CursorControl].Prop_PrimaryNoise
        [Cursor].PrimaryColorNoiseOpacity = [CursorControl].Prop_PrimaryNoiseOpacity
        [Cursor].SecondaryColor1 = [CursorControl].Prop_SecondaryColor1
        [Cursor].SecondaryColor2 = [CursorControl].Prop_SecondaryColor2
        [Cursor].SecondaryColorGradient = [CursorControl].Prop_SecondaryColorGradient
        [Cursor].SecondaryColorGradientMode = [CursorControl].Prop_SecondaryColorGradientMode
        [Cursor].SecondaryColorNoise = [CursorControl].Prop_SecondaryNoise
        [Cursor].SecondaryColorNoiseOpacity = [CursorControl].Prop_SecondaryNoiseOpacity
        [Cursor].LoadingCircleBack1 = [CursorControl].Prop_LoadingCircleBack1
        [Cursor].LoadingCircleBack2 = [CursorControl].Prop_LoadingCircleBack2
        [Cursor].LoadingCircleBackGradient = [CursorControl].Prop_LoadingCircleBackGradient
        [Cursor].LoadingCircleBackGradientMode = [CursorControl].Prop_LoadingCircleBackGradientMode
        [Cursor].LoadingCircleBackNoise = [CursorControl].Prop_LoadingCircleBackNoise
        [Cursor].LoadingCircleBackNoiseOpacity = [CursorControl].Prop_LoadingCircleBackNoiseOpacity
        [Cursor].LoadingCircleHot1 = [CursorControl].Prop_LoadingCircleHot1
        [Cursor].LoadingCircleHot2 = [CursorControl].Prop_LoadingCircleHot2
        [Cursor].LoadingCircleHotGradient = [CursorControl].Prop_LoadingCircleHotGradient
        [Cursor].LoadingCircleHotGradientMode = [CursorControl].Prop_LoadingCircleHotGradientMode
        [Cursor].LoadingCircleHotNoise = [CursorControl].Prop_LoadingCircleHotNoise
        [Cursor].LoadingCircleHotNoiseOpacity = [CursorControl].Prop_LoadingCircleHotNoiseOpacity
        [Cursor].Shadow_Enabled = [CursorControl].Prop_Shadow_Enabled
        [Cursor].Shadow_Color = [CursorControl].Prop_Shadow_Color
        [Cursor].Shadow_Blur = [CursorControl].Prop_Shadow_Blur
        [Cursor].Shadow_Opacity = [CursorControl].Prop_Shadow_Opacity
        [Cursor].Shadow_OffsetX = [CursorControl].Prop_Shadow_OffsetX
        [Cursor].Shadow_OffsetY = [CursorControl].Prop_Shadow_OffsetY

        Return [Cursor]
    End Function

    Sub LoadFromCP([CP] As CP)
        XenonToggle1.Checked = [CP].Cursor_Enabled
        XenonCheckBox9.Checked = [CP].Cursor_Shadow
        XenonTrackbar2.Value = [CP].Cursor_Trails
        XenonCheckBox10.Checked = [CP].Cursor_Sonar
        CursorCP_to_Cursor(Arrow, [CP].Cursor_Arrow)
        CursorCP_to_Cursor(Help, [CP].Cursor_Help)
        CursorCP_to_Cursor(AppLoading, [CP].Cursor_AppLoading)
        CursorCP_to_Cursor(Busy, [CP].Cursor_Busy)
        CursorCP_to_Cursor(Move_Cur, [CP].Cursor_Move)
        CursorCP_to_Cursor(NS, [CP].Cursor_NS)
        CursorCP_to_Cursor(EW, [CP].Cursor_EW)
        CursorCP_to_Cursor(NESW, [CP].Cursor_NESW)
        CursorCP_to_Cursor(NWSE, [CP].Cursor_NWSE)
        CursorCP_to_Cursor(Up, [CP].Cursor_Up)
        CursorCP_to_Cursor(Pen, [CP].Cursor_Pen)
        CursorCP_to_Cursor(None, [CP].Cursor_None)
        CursorCP_to_Cursor(Link, [CP].Cursor_Link)
        CursorCP_to_Cursor(Pin, [CP].Cursor_Pin)
        CursorCP_to_Cursor(Person, [CP].Cursor_Person)
        CursorCP_to_Cursor(IBeam, [CP].Cursor_IBeam)
        CursorCP_to_Cursor(Cross, [CP].Cursor_Cross)

        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then
                i.Invalidate()
            End If
        Next
    End Sub

    Sub SaveToCP([CP] As CP)
        [CP].Cursor_Enabled = XenonToggle1.Checked
        [CP].Cursor_Shadow = XenonCheckBox9.Checked
        [CP].Cursor_Trails = XenonTrackbar2.Value
        [CP].Cursor_Sonar = XenonCheckBox10.Checked
        [CP].Cursor_Arrow = Cursor_to_CursorCP(Arrow)
        [CP].Cursor_Help = Cursor_to_CursorCP(Help)
        [CP].Cursor_AppLoading = Cursor_to_CursorCP(AppLoading)
        [CP].Cursor_Busy = Cursor_to_CursorCP(Busy)
        [CP].Cursor_Move = Cursor_to_CursorCP(Move_Cur)
        [CP].Cursor_NS = Cursor_to_CursorCP(NS)
        [CP].Cursor_EW = Cursor_to_CursorCP(EW)
        [CP].Cursor_NESW = Cursor_to_CursorCP(NESW)
        [CP].Cursor_NWSE = Cursor_to_CursorCP(NWSE)
        [CP].Cursor_Up = Cursor_to_CursorCP(Up)
        [CP].Cursor_Pen = Cursor_to_CursorCP(Pen)
        [CP].Cursor_None = Cursor_to_CursorCP(None)
        [CP].Cursor_Link = Cursor_to_CursorCP(Link)
        [CP].Cursor_Pin = Cursor_to_CursorCP(Pin)
        [CP].Cursor_Person = Cursor_to_CursorCP(Person)
        [CP].Cursor_IBeam = Cursor_to_CursorCP(IBeam)
        [CP].Cursor_Cross = Cursor_to_CursorCP(Cross)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyDarkMode(Me)
        MainFrm.Visible = False
        FlowLayoutPanel1.DoubleBuffer

        AnimateList.Clear()
        _CopiedControl = Nothing
        _Shown = False

        Angle = 180
        Cycles = 0
        Timer1.Enabled = False
        Timer1.Stop()

        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then
                AddHandler i.Click, AddressOf Clicked
                If i.Prop_Cursor = CursorType.AppLoading Or i.Prop_Cursor = CursorType.Busy Then AnimateList.Add(i)
            End If
        Next

        XenonButton8.Image = MainFrm.XenonButton20.Image.Resize(16, 16)

        LoadFromCP(My.CP)
    End Sub

    Private Sub CursorsStudio_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub

    Private Sub CursorsStudio_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub Clicked(sender As Object, e As MouseEventArgs)
        _SelectedControl = DirectCast(sender, CursorControl)
        ApplyColorsFromCursor(_SelectedControl)
        XenonButton1.Enabled = True
        XenonGroupBox9.Enabled = True
        XenonGroupBox6.Enabled = True
        XenonGroupBox5.Enabled = True
        XenonGroupBox2.Enabled = True
        XenonGroupBox10.Enabled = True
    End Sub

    Sub ApplyColorsFromCursor([CursorControl] As CursorControl)
        With [CursorControl]
            XenonComboBox5.SelectedIndex = .Prop_ArrowStyle
            XenonComboBox6.SelectedIndex = .Prop_CircleStyle

            PrimaryColor1.BackColor = .Prop_PrimaryColor1
            PrimaryColor2.BackColor = .Prop_PrimaryColor2
            XenonCheckBox1.Checked = .Prop_PrimaryColorGradient
            XenonComboBox1.SelectedItem = ReturnStringFromGradientMode(.Prop_PrimaryColorGradientMode)
            XenonCheckBox5.Checked = .Prop_PrimaryNoise
            XenonTrackbar3.Value = .Prop_PrimaryNoiseOpacity * 100

            SecondaryColor1.BackColor = .Prop_SecondaryColor1
            SecondaryColor2.BackColor = .Prop_SecondaryColor2
            XenonCheckBox4.Checked = .Prop_SecondaryColorGradient
            XenonComboBox2.SelectedItem = ReturnStringFromGradientMode(.Prop_SecondaryColorGradientMode)
            XenonCheckBox3.Checked = .Prop_SecondaryNoise
            XenonTrackbar4.Value = .Prop_SecondaryNoiseOpacity * 100

            CircleColor1.BackColor = .Prop_LoadingCircleBack1
            CircleColor2.BackColor = .Prop_LoadingCircleBack2
            XenonCheckBox8.Checked = .Prop_LoadingCircleBackGradient
            XenonComboBox4.SelectedItem = ReturnStringFromGradientMode(.Prop_LoadingCircleBackGradientMode)
            XenonCheckBox7.Checked = .Prop_LoadingCircleBackNoise
            XenonTrackbar5.Value = .Prop_LoadingCircleBackNoiseOpacity * 100

            LoadingColor1.BackColor = .Prop_LoadingCircleHot1
            LoadingColor2.BackColor = .Prop_LoadingCircleHot2
            XenonCheckBox2.Checked = .Prop_LoadingCircleHotGradient
            XenonComboBox3.SelectedItem = ReturnStringFromGradientMode(.Prop_LoadingCircleHotGradientMode)
            XenonCheckBox6.Checked = .Prop_LoadingCircleHotNoise
            XenonTrackbar6.Value = .Prop_LoadingCircleHotNoiseOpacity * 100

            XenonCheckBox11.Checked = .Prop_Shadow_Enabled
            XenonCP1.BackColor = .Prop_Shadow_Color
            XenonTrackbar7.Value = .Prop_Shadow_Blur
            XenonTrackbar8.Value = .Prop_Shadow_Opacity * 100
            XenonTrackbar9.Value = .Prop_Shadow_OffsetX
            XenonTrackbar10.Value = .Prop_Shadow_OffsetY
        End With

    End Sub

    Sub ApplyColorsToPreview([CursorControl] As CursorControl)
        With [CursorControl]
            .Prop_ArrowStyle = XenonComboBox5.SelectedIndex
            .Prop_CircleStyle = XenonComboBox6.SelectedIndex

            .Prop_PrimaryColor1 = PrimaryColor1.BackColor
            .Prop_PrimaryColor2 = PrimaryColor2.BackColor
            .Prop_PrimaryColorGradient = XenonCheckBox1.Checked
            .Prop_PrimaryColorGradientMode = ReturnGradientModeFromString(XenonComboBox1.SelectedItem)
            .Prop_PrimaryNoise = XenonCheckBox5.Checked
            .Prop_PrimaryNoiseOpacity = XenonTrackbar3.Value / 100

            .Prop_SecondaryColor1 = SecondaryColor1.BackColor
            .Prop_SecondaryColor2 = SecondaryColor2.BackColor
            .Prop_SecondaryColorGradient = XenonCheckBox4.Checked
            .Prop_SecondaryColorGradientMode = ReturnGradientModeFromString(XenonComboBox2.SelectedItem)
            .Prop_SecondaryNoise = XenonCheckBox3.Checked
            .Prop_SecondaryNoiseOpacity = XenonTrackbar4.Value / 100

            .Prop_LoadingCircleBack1 = CircleColor1.BackColor
            .Prop_LoadingCircleBack2 = CircleColor2.BackColor
            .Prop_LoadingCircleBackGradient = XenonCheckBox8.Checked
            .Prop_LoadingCircleBackGradientMode = ReturnGradientModeFromString(XenonComboBox4.SelectedItem)
            .Prop_LoadingCircleBackNoise = XenonCheckBox7.Checked
            .Prop_LoadingCircleBackNoiseOpacity = XenonTrackbar5.Value / 100

            .Prop_LoadingCircleHot1 = LoadingColor1.BackColor
            .Prop_LoadingCircleHot2 = LoadingColor2.BackColor
            .Prop_LoadingCircleHotGradient = XenonCheckBox2.Checked
            .Prop_LoadingCircleHotGradientMode = ReturnGradientModeFromString(XenonComboBox3.SelectedItem)
            .Prop_LoadingCircleHotNoise = XenonCheckBox6.Checked
            .Prop_LoadingCircleHotNoiseOpacity = XenonTrackbar6.Value / 100

            .Prop_Shadow_Enabled = XenonCheckBox11.Checked
            .Prop_Shadow_Color = XenonCP1.BackColor
            .Prop_Shadow_Blur = XenonTrackbar7.Value
            .Prop_Shadow_Opacity = XenonTrackbar8.Value / 100
            .Prop_Shadow_OffsetX = XenonTrackbar9.Value
            .Prop_Shadow_OffsetY = XenonTrackbar10.Value
        End With

    End Sub

    Private Sub TaskbarFrontAndFoldersOnStart_picker_Click(sender As Object, e As EventArgs) Handles PrimaryColor1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_PrimaryColor1 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, XenonCP), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorBack1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_PrimaryColor1 = c
        _SelectedControl.Invalidate()

        DirectCast(sender, XenonCP).BackColor = c
        DirectCast(sender, XenonCP).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox3_Click(sender As Object, e As EventArgs) Handles PrimaryColor2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_PrimaryColor2 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, XenonCP), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorBack2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_PrimaryColor2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonCP).BackColor = c
        DirectCast(sender, XenonCP).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox5_Click(sender As Object, e As EventArgs) Handles SecondaryColor1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_SecondaryColor1 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, XenonCP), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorLine1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_SecondaryColor1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonCP).BackColor = c
        DirectCast(sender, XenonCP).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox4_Click(sender As Object, e As EventArgs) Handles SecondaryColor2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_SecondaryColor2 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, XenonCP), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorLine2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_SecondaryColor2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonCP).BackColor = c
        DirectCast(sender, XenonCP).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox10_Click(sender As Object, e As EventArgs) Handles CircleColor1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_LoadingCircleBack1 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, XenonCP), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorCircle1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleBack1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonCP).BackColor = c
        DirectCast(sender, XenonCP).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If Not _Shown Then Exit Sub
        _SelectedControl.Prop_PrimaryColorGradient = XenonCheckBox1.Checked
        _SelectedControl.Invalidate()
        XenonCheckBox1.Invalidate()
    End Sub

    Private Sub XenonCheckBox4_CheckedChanged(sender As Object) Handles XenonCheckBox4.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryColorGradient = XenonCheckBox4.Checked
        _SelectedControl.Invalidate()
        XenonCheckBox4.Invalidate()

    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_PrimaryColorGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox2.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryColorGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonCheckBox5_CheckedChanged(sender As Object) Handles XenonCheckBox5.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_PrimaryNoise = XenonCheckBox5.Checked
        _SelectedControl.Invalidate()
        XenonCheckBox5.Invalidate()

    End Sub

    Private Sub XenonCheckBox3_CheckedChanged(sender As Object) Handles XenonCheckBox3.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryNoise = XenonCheckBox3.Checked
        _SelectedControl.Invalidate()
        XenonCheckBox3.Invalidate()

    End Sub

    Private Sub XenonNumericUpDown3_Click(sender As Object, e As EventArgs)
        '_SelectedControl.Prop_LineThickness = sender.Value / 10
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonTrackbar1_Scroll(sender As Object) Handles XenonTrackbar1.Scroll
        If Not _Shown Then Exit Sub

        For Each i As CursorControl In FlowLayoutPanel1.Controls
            i.Prop_Scale = sender.value / 100
            i.Width = 32 * i.Prop_Scale + 32
            i.Height = i.Width
            i.Refresh()
        Next

        Label5.Text = String.Format("{0} ({1}x)", My.Lang.Scaling, sender.value / 100)
    End Sub

    Dim Angle As Single = 180
    ReadOnly Increment As Single = 5
    Dim Cycles As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not _Shown Then Exit Sub

        Try
            For Each i As CursorControl In AnimateList
                i.Angle = Angle
                i.Refresh()

                If Angle + Increment >= 360 Then Angle = 0
                Angle += Increment

                If Angle = 180 And Cycles >= 2 Then
                    i.Angle = 180
                    Timer1.Enabled = False
                    Timer1.Stop()
                ElseIf Angle = 180 Then
                    Cycles += 1
                End If

            Next
        Catch
        End Try
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Angle = 180
        Cycles = 0
        Timer1.Enabled = True
        Timer1.Start()
    End Sub

    Private Sub XenonGroupBox9_Click(sender As Object, e As EventArgs) Handles CircleColor2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_LoadingCircleBack1 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, XenonCP), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorCircle2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleBack2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonCP).BackColor = c
        DirectCast(sender, XenonCP).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox8_Click(sender As Object, e As EventArgs) Handles LoadingColor1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_LoadingCircleHot1 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, XenonCP), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorCircleHot1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleHot1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonCP).BackColor = c
        DirectCast(sender, XenonCP).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox7_Click(sender As Object, e As EventArgs) Handles LoadingColor2.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_LoadingCircleHot2 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, XenonCP), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorCircleHot2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleHot2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonCP).BackColor = c
        DirectCast(sender, XenonCP).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonCheckBox8_CheckedChanged(sender As Object) Handles XenonCheckBox8.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackGradient = XenonCheckBox8.Checked
        _SelectedControl.Invalidate()
        XenonCheckBox8.Invalidate()
    End Sub

    Private Sub XenonCheckBox2_CheckedChanged(sender As Object) Handles XenonCheckBox2.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotGradient = XenonCheckBox2.Checked
        _SelectedControl.Invalidate()
        XenonCheckBox2.Invalidate()
    End Sub

    Private Sub XenonCheckBox7_CheckedChanged(sender As Object) Handles XenonCheckBox7.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackNoise = XenonCheckBox7.Checked
        _SelectedControl.Invalidate()
        XenonCheckBox7.Invalidate()

    End Sub

    Private Sub XenonCheckBox6_CheckedChanged(sender As Object) Handles XenonCheckBox6.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotNoise = XenonCheckBox6.Checked
        _SelectedControl.Invalidate()
        XenonCheckBox6.Invalidate()

    End Sub

    Private Sub XenonComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox4.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox3.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        _CopiedControl = _SelectedControl
        XenonButton2.Enabled = True
        XenonButton6.Enabled = True

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        ApplyColorsFromCursor(_CopiedControl)
        ApplyColorsToPreview(_SelectedControl)
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then
                ApplyColorsFromCursor(_CopiedControl)
                ApplyColorsToPreview(i)
                i.Invalidate()
            End If
        Next
    End Sub


    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Me.Close()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        SaveToCP(My.CP)
        Me.Close()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            LoadFromCP(CPx)
            CPx.Dispose()

            For Each x In FlowLayoutPanel1.Controls.OfType(Of CursorControl)
                If x._Focused Then
                    ApplyColorsFromCursor(x)
                    Exit For
                End If
            Next

        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.CP_Type.Registry)
        LoadFromCP(CPx)
        CPx.Dispose()

        For Each x In FlowLayoutPanel1.Controls.OfType(Of CursorControl)
            If x._Focused Then
                ApplyColorsFromCursor(x)
                Exit For
            End If
        Next
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
            LoadFromCP(_Def)
        End Using

        For Each x In FlowLayoutPanel1.Controls.OfType(Of CursorControl)
            If x._Focused Then
                ApplyColorsFromCursor(x)
                Exit For
            End If
        Next
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        MsgBox(My.Lang.ScalingTip, MsgBoxStyle.Information)
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        SaveToCP(CPx)
        SaveToCP(My.CP)
        CPx.Apply_Cursors()
        CPx.Win32.Update_UPM_DEFAULT()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub Ttl_h_Click(sender As Object, e As EventArgs) Handles trails_btn.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar2.Maximum), XenonTrackbar2.Minimum) : XenonTrackbar2.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar2_Scroll(sender As Object) Handles XenonTrackbar2.Scroll
        trails_btn.Text = sender.Value.ToString
    End Sub

    Private Sub XenonToggle1_CheckedChanged(sender As Object, e As EventArgs) Handles XenonToggle1.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub XenonComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox5.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_ArrowStyle = XenonComboBox5.SelectedIndex
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox6.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_CircleStyle = XenonComboBox6.SelectedIndex
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonButton12_Click(sender As Object, e As EventArgs) Handles XenonButton12.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar3.Maximum), XenonTrackbar3.Minimum) : XenonTrackbar3.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar3_Scroll(sender As Object) Handles XenonTrackbar3.Scroll
        XenonButton12.Text = sender.Value

        If Not _Shown Then Exit Sub

        Dim valX As Single = sender.Value
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_PrimaryNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonButton13_Click(sender As Object, e As EventArgs) Handles XenonButton13.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar4.Maximum), XenonTrackbar4.Minimum) : XenonTrackbar4.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar4_Scroll(sender As Object) Handles XenonTrackbar4.Scroll
        XenonButton13.Text = sender.Value

        If Not _Shown Then Exit Sub

        Dim valX As Single = sender.Value
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_SecondaryNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonButton14_Click(sender As Object, e As EventArgs) Handles XenonButton14.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar5.Maximum), XenonTrackbar5.Minimum) : XenonTrackbar5.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar5_Scroll(sender As Object) Handles XenonTrackbar5.Scroll
        XenonButton14.Text = sender.Value

        If Not _Shown Then Exit Sub

        Dim valX As Single = sender.Value
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_LoadingCircleBackNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonButton15_Click(sender As Object, e As EventArgs) Handles XenonButton15.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar6.Maximum), XenonTrackbar6.Minimum) : XenonTrackbar6.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar6_Scroll(sender As Object) Handles XenonTrackbar6.Scroll
        XenonButton15.Text = sender.Value

        If Not _Shown Then Exit Sub

        Dim valX As Single = sender.Value
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_LoadingCircleHotNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonCP1_Click(sender As Object, e As EventArgs) Handles XenonCP1.Click
        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_Shadow_Color = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, XenonCP), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorShadow = True}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition)

        _SelectedControl.Prop_Shadow_Color = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonCP).BackColor = c
        DirectCast(sender, XenonCP).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar7.Maximum), XenonTrackbar7.Minimum) : XenonTrackbar7.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton17_Click(sender As Object, e As EventArgs) Handles XenonButton17.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar8.Maximum), XenonTrackbar8.Minimum) : XenonTrackbar8.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton18_Click(sender As Object, e As EventArgs) Handles XenonButton18.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar9.Maximum), XenonTrackbar9.Minimum) : XenonTrackbar9.Value = Val(sender.Text)
    End Sub

    Private Sub XenonButton19_Click(sender As Object, e As EventArgs) Handles XenonButton19.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), XenonTrackbar10.Maximum), XenonTrackbar10.Minimum) : XenonTrackbar10.Value = Val(sender.Text)
    End Sub

    Private Sub XenonTrackbar7_Scroll(sender As Object) Handles XenonTrackbar7.Scroll
        XenonButton16.Text = sender.Value

        If Not _Shown Then Exit Sub

        Dim valX As Single = sender.Value
        If valX > 10 Then
            valX = 10
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_Shadow_Blur = valX
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonTrackbar8_Scroll(sender As Object) Handles XenonTrackbar8.Scroll
        XenonButton17.Text = sender.Value

        If Not _Shown Then Exit Sub

        Dim valX As Single = sender.Value
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_Shadow_Opacity = valX / 100
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonTrackbar9_Scroll(sender As Object) Handles XenonTrackbar9.Scroll
        XenonButton18.Text = sender.Value

        If Not _Shown Then Exit Sub

        Dim valX As Single = sender.Value
        If valX > 5 Then
            valX = 5
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_Shadow_OffsetX = valX
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonTrackbar10_Scroll(sender As Object) Handles XenonTrackbar10.Scroll
        XenonButton19.Text = sender.Value

        If Not _Shown Then Exit Sub

        Dim valX As Single = sender.Value
        If valX > 5 Then
            valX = 5
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_Shadow_OffsetY = valX
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonCheckBox11_CheckedChanged(sender As Object) Handles XenonCheckBox11.CheckedChanged
        If Not _Shown Then Exit Sub
        _SelectedControl.Prop_Shadow_Enabled = XenonCheckBox11.Checked
        _SelectedControl.Invalidate()
    End Sub
End Class