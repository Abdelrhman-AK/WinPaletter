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
        Toggle1.Checked = [CP].Cursor_Enabled
        CheckBox9.Checked = [CP].Cursor_Shadow
        Trackbar2.Value = [CP].Cursor_Trails
        CheckBox10.Checked = [CP].Cursor_Sonar
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
        [CP].Cursor_Enabled = Toggle1.Checked
        [CP].Cursor_Shadow = CheckBox9.Checked
        [CP].Cursor_Trails = Trackbar2.Value
        [CP].Cursor_Sonar = CheckBox10.Checked
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
        ApplyStyle(Me)
        FlowLayoutPanel1.DoubleBuffer

        AnimateList.Clear()
        _CopiedControl = Nothing
        _Shown = False

        Angle = 180
        Cycles = 0
        Timer1.Enabled = False
        Timer1.Stop()

        'Remove handler to avoid doubling/tripling events
        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then
                Try
                    RemoveHandler i.Click, AddressOf Clicked
                Catch
                End Try
            End If
        Next

        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then
                AddHandler i.Click, AddressOf Clicked
                If i.Prop_Cursor = CursorType.AppLoading Or i.Prop_Cursor = CursorType.Busy Then AnimateList.Add(i)
            End If
        Next

        Button8.Image = MainFrm.Button20.Image.Resize(16, 16)

        LoadFromCP(My.CP)
    End Sub

    Protected Overrides Sub OnDragOver(e As DragEventArgs)
        If TypeOf e.Data.GetData("WinPaletter.UI.Controllers.ColorItem") Is UI.Controllers.ColorItem Then
            Focus()
            BringToFront()
        Else
            Exit Sub
        End If

        MyBase.OnDragOver(e)
    End Sub

    Private Sub CursorsStudio_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub Clicked(sender As Object, e As MouseEventArgs)
        _SelectedControl = DirectCast(sender, CursorControl)
        ApplyColorsFromCursor(_SelectedControl)
        Button1.Enabled = True
        GroupBox9.Enabled = True
        GroupBox6.Enabled = True
        GroupBox5.Enabled = True
        GroupBox2.Enabled = True
        GroupBox10.Enabled = True
    End Sub

    Sub ApplyColorsFromCursor([CursorControl] As CursorControl)
        With [CursorControl]
            ComboBox5.SelectedIndex = .Prop_ArrowStyle
            ComboBox6.SelectedIndex = .Prop_CircleStyle

            PrimaryColor1.BackColor = .Prop_PrimaryColor1
            PrimaryColor2.BackColor = .Prop_PrimaryColor2
            CheckBox1.Checked = .Prop_PrimaryColorGradient
            ComboBox1.SelectedItem = ReturnStringFromGradientMode(.Prop_PrimaryColorGradientMode)
            CheckBox5.Checked = .Prop_PrimaryNoise
            Trackbar3.Value = .Prop_PrimaryNoiseOpacity * 100

            SecondaryColor1.BackColor = .Prop_SecondaryColor1
            SecondaryColor2.BackColor = .Prop_SecondaryColor2
            CheckBox4.Checked = .Prop_SecondaryColorGradient
            ComboBox2.SelectedItem = ReturnStringFromGradientMode(.Prop_SecondaryColorGradientMode)
            CheckBox3.Checked = .Prop_SecondaryNoise
            Trackbar4.Value = .Prop_SecondaryNoiseOpacity * 100

            CircleColor1.BackColor = .Prop_LoadingCircleBack1
            CircleColor2.BackColor = .Prop_LoadingCircleBack2
            CheckBox8.Checked = .Prop_LoadingCircleBackGradient
            ComboBox4.SelectedItem = ReturnStringFromGradientMode(.Prop_LoadingCircleBackGradientMode)
            CheckBox7.Checked = .Prop_LoadingCircleBackNoise
            Trackbar5.Value = .Prop_LoadingCircleBackNoiseOpacity * 100

            LoadingColor1.BackColor = .Prop_LoadingCircleHot1
            LoadingColor2.BackColor = .Prop_LoadingCircleHot2
            CheckBox2.Checked = .Prop_LoadingCircleHotGradient
            ComboBox3.SelectedItem = ReturnStringFromGradientMode(.Prop_LoadingCircleHotGradientMode)
            CheckBox6.Checked = .Prop_LoadingCircleHotNoise
            Trackbar6.Value = .Prop_LoadingCircleHotNoiseOpacity * 100

            CheckBox11.Checked = .Prop_Shadow_Enabled
            ColorItem1.BackColor = .Prop_Shadow_Color
            Trackbar7.Value = .Prop_Shadow_Blur
            Trackbar8.Value = .Prop_Shadow_Opacity * 100
            Trackbar9.Value = .Prop_Shadow_OffsetX
            Trackbar10.Value = .Prop_Shadow_OffsetY
        End With

    End Sub

    Sub ApplyColorsToPreview([CursorControl] As CursorControl)
        With [CursorControl]
            .Prop_ArrowStyle = ComboBox5.SelectedIndex
            .Prop_CircleStyle = ComboBox6.SelectedIndex

            .Prop_PrimaryColor1 = PrimaryColor1.BackColor
            .Prop_PrimaryColor2 = PrimaryColor2.BackColor
            .Prop_PrimaryColorGradient = CheckBox1.Checked
            .Prop_PrimaryColorGradientMode = ReturnGradientModeFromString(ComboBox1.SelectedItem)
            .Prop_PrimaryNoise = CheckBox5.Checked
            .Prop_PrimaryNoiseOpacity = Trackbar3.Value / 100

            .Prop_SecondaryColor1 = SecondaryColor1.BackColor
            .Prop_SecondaryColor2 = SecondaryColor2.BackColor
            .Prop_SecondaryColorGradient = CheckBox4.Checked
            .Prop_SecondaryColorGradientMode = ReturnGradientModeFromString(ComboBox2.SelectedItem)
            .Prop_SecondaryNoise = CheckBox3.Checked
            .Prop_SecondaryNoiseOpacity = Trackbar4.Value / 100

            .Prop_LoadingCircleBack1 = CircleColor1.BackColor
            .Prop_LoadingCircleBack2 = CircleColor2.BackColor
            .Prop_LoadingCircleBackGradient = CheckBox8.Checked
            .Prop_LoadingCircleBackGradientMode = ReturnGradientModeFromString(ComboBox4.SelectedItem)
            .Prop_LoadingCircleBackNoise = CheckBox7.Checked
            .Prop_LoadingCircleBackNoiseOpacity = Trackbar5.Value / 100

            .Prop_LoadingCircleHot1 = LoadingColor1.BackColor
            .Prop_LoadingCircleHot2 = LoadingColor2.BackColor
            .Prop_LoadingCircleHotGradient = CheckBox2.Checked
            .Prop_LoadingCircleHotGradientMode = ReturnGradientModeFromString(ComboBox3.SelectedItem)
            .Prop_LoadingCircleHotNoise = CheckBox6.Checked
            .Prop_LoadingCircleHotNoiseOpacity = Trackbar6.Value / 100

            .Prop_Shadow_Enabled = CheckBox11.Checked
            .Prop_Shadow_Color = ColorItem1.BackColor
            .Prop_Shadow_Blur = Trackbar7.Value
            .Prop_Shadow_Opacity = Trackbar8.Value / 100
            .Prop_Shadow_OffsetX = Trackbar9.Value
            .Prop_Shadow_OffsetY = Trackbar10.Value
        End With

    End Sub

    Private Sub TaskbarFrontAndFoldersOnStart_picker_Click(sender As Object, e As EventArgs) Handles PrimaryColor1.Click, PrimaryColor1.DragDrop

        If TypeOf e Is DragEventArgs Then
            _SelectedControl.Prop_PrimaryColor1 = sender.BackColor
            _SelectedControl.Invalidate()
            Exit Sub
        End If

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_PrimaryColor1 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, UI.Controllers.ColorItem), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorBack1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_PrimaryColor1 = c
        _SelectedControl.Invalidate()

        DirectCast(sender, UI.Controllers.ColorItem).BackColor = c
        DirectCast(sender, UI.Controllers.ColorItem).Invalidate()

        CList.Clear()

    End Sub

    Private Sub GroupBox3_Click(sender As Object, e As EventArgs) Handles PrimaryColor2.Click, PrimaryColor2.DragDrop

        If TypeOf e Is DragEventArgs Then
            _SelectedControl.Prop_PrimaryColor2 = sender.BackColor
            _SelectedControl.Invalidate()
            Exit Sub
        End If

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_PrimaryColor2 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, UI.Controllers.ColorItem), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorBack2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_PrimaryColor2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, UI.Controllers.ColorItem).BackColor = c
        DirectCast(sender, UI.Controllers.ColorItem).Invalidate()

        CList.Clear()

    End Sub

    Private Sub GroupBox5_Click(sender As Object, e As EventArgs) Handles SecondaryColor1.Click, SecondaryColor1.DragDrop

        If TypeOf e Is DragEventArgs Then
            _SelectedControl.Prop_SecondaryColor1 = sender.BackColor
            _SelectedControl.Invalidate()
            Exit Sub
        End If

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_SecondaryColor1 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, UI.Controllers.ColorItem), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorLine1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_SecondaryColor1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, UI.Controllers.ColorItem).BackColor = c
        DirectCast(sender, UI.Controllers.ColorItem).Invalidate()

        CList.Clear()

    End Sub

    Private Sub GroupBox4_Click(sender As Object, e As EventArgs) Handles SecondaryColor2.Click, SecondaryColor2.DragDrop

        If TypeOf e Is DragEventArgs Then
            _SelectedControl.Prop_SecondaryColor2 = sender.BackColor
            _SelectedControl.Invalidate()
            Exit Sub
        End If

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_SecondaryColor2 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, UI.Controllers.ColorItem), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorLine2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_SecondaryColor2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, UI.Controllers.ColorItem).BackColor = c
        DirectCast(sender, UI.Controllers.ColorItem).Invalidate()

        CList.Clear()

    End Sub

    Private Sub GroupBox10_Click(sender As Object, e As EventArgs) Handles CircleColor1.Click, CircleColor1.DragDrop

        If TypeOf e Is DragEventArgs Then
            _SelectedControl.Prop_LoadingCircleBack1 = sender.BackColor
            _SelectedControl.Invalidate()
            Exit Sub
        End If

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_LoadingCircleBack1 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, UI.Controllers.ColorItem), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorCircle1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleBack1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, UI.Controllers.ColorItem).BackColor = c
        DirectCast(sender, UI.Controllers.ColorItem).Invalidate()

        CList.Clear()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object) Handles CheckBox1.CheckedChanged
        If Not _Shown Then Exit Sub
        _SelectedControl.Prop_PrimaryColorGradient = CheckBox1.Checked
        _SelectedControl.Invalidate()
        CheckBox1.Invalidate()
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object) Handles CheckBox4.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryColorGradient = CheckBox4.Checked
        _SelectedControl.Invalidate()
        CheckBox4.Invalidate()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_PrimaryColorGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryColorGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object) Handles CheckBox5.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_PrimaryNoise = CheckBox5.Checked
        _SelectedControl.Invalidate()
        CheckBox5.Invalidate()

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object) Handles CheckBox3.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryNoise = CheckBox3.Checked
        _SelectedControl.Invalidate()
        CheckBox3.Invalidate()

    End Sub

    Private Sub Trackbar1_Scroll(sender As Object) Handles Trackbar1.Scroll
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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Angle = 180
        Cycles = 0
        Timer1.Enabled = True
        Timer1.Start()
    End Sub

    Private Sub GroupBox9_Click(sender As Object, e As EventArgs) Handles CircleColor2.Click, CircleColor2.DragDrop

        If TypeOf e Is DragEventArgs Then
            _SelectedControl.Prop_LoadingCircleBack1 = sender.BackColor
            _SelectedControl.Invalidate()
            Exit Sub
        End If

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_LoadingCircleBack1 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, UI.Controllers.ColorItem), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorCircle2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleBack2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, UI.Controllers.ColorItem).BackColor = c
        DirectCast(sender, UI.Controllers.ColorItem).Invalidate()

        CList.Clear()

    End Sub

    Private Sub GroupBox8_Click(sender As Object, e As EventArgs) Handles LoadingColor1.Click, LoadingColor1.DragDrop

        If TypeOf e Is DragEventArgs Then
            _SelectedControl.Prop_LoadingCircleHot1 = sender.BackColor
            _SelectedControl.Invalidate()
            Exit Sub
        End If

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_LoadingCircleHot1 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, UI.Controllers.ColorItem), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorCircleHot1 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleHot1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, UI.Controllers.ColorItem).BackColor = c
        DirectCast(sender, UI.Controllers.ColorItem).Invalidate()

        CList.Clear()

    End Sub

    Private Sub GroupBox7_Click(sender As Object, e As EventArgs) Handles LoadingColor2.Click, LoadingColor2.DragDrop

        If TypeOf e Is DragEventArgs Then
            _SelectedControl.Prop_LoadingCircleHot2 = sender.BackColor
            _SelectedControl.Invalidate()
            Exit Sub
        End If

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_LoadingCircleHot2 = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, UI.Controllers.ColorItem), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorCircleHot2 = True, .Win7 = False, .Win7LivePreview_AfterGlow = False, .Win7LivePreview_Colorization = False}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleHot2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, UI.Controllers.ColorItem).BackColor = c
        DirectCast(sender, UI.Controllers.ColorItem).Invalidate()

        CList.Clear()

    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object) Handles CheckBox8.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackGradient = CheckBox8.Checked
        _SelectedControl.Invalidate()
        CheckBox8.Invalidate()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object) Handles CheckBox2.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotGradient = CheckBox2.Checked
        _SelectedControl.Invalidate()
        CheckBox2.Invalidate()
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object) Handles CheckBox7.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackNoise = CheckBox7.Checked
        _SelectedControl.Invalidate()
        CheckBox7.Invalidate()

    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object) Handles CheckBox6.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotNoise = CheckBox6.Checked
        _SelectedControl.Invalidate()
        CheckBox6.Invalidate()

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotGradientMode = ReturnGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        _CopiedControl = _SelectedControl
        Button2.Enabled = True
        Button6.Enabled = True

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ApplyColorsFromCursor(_CopiedControl)
        ApplyColorsToPreview(_SelectedControl)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then
                ApplyColorsFromCursor(_CopiedControl)
                ApplyColorsToPreview(i)
                i.Invalidate()
            End If
        Next
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SaveToCP(My.CP)
        Me.Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
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

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
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

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
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

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        MsgBox(My.Lang.ScalingTip, MsgBoxStyle.Information)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
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
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar2.Maximum), Trackbar2.Minimum) : Trackbar2.Value = Val(sender.Text)
    End Sub

    Private Sub Trackbar2_Scroll(sender As Object) Handles Trackbar2.Scroll
        trails_btn.Text = sender.Value.ToString
    End Sub

    Private Sub Toggle1_CheckedChanged(sender As Object, e As EventArgs) Handles Toggle1.CheckedChanged
        checker_img.Image = If(sender.Checked, My.Resources.checker_enabled, My.Resources.checker_disabled)
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_ArrowStyle = ComboBox5.SelectedIndex
        _SelectedControl.Invalidate()
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_CircleStyle = ComboBox6.SelectedIndex
        _SelectedControl.Invalidate()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar3.Maximum), Trackbar3.Minimum) : Trackbar3.Value = Val(sender.Text)
    End Sub

    Private Sub Trackbar3_Scroll(sender As Object) Handles Trackbar3.Scroll
        Button12.Text = sender.Value

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

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar4.Maximum), Trackbar4.Minimum) : Trackbar4.Value = Val(sender.Text)
    End Sub

    Private Sub Trackbar4_Scroll(sender As Object) Handles Trackbar4.Scroll
        Button13.Text = sender.Value

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

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar5.Maximum), Trackbar5.Minimum) : Trackbar5.Value = Val(sender.Text)
    End Sub

    Private Sub Trackbar5_Scroll(sender As Object) Handles Trackbar5.Scroll
        Button14.Text = sender.Value

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

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar6.Maximum), Trackbar6.Minimum) : Trackbar6.Value = Val(sender.Text)
    End Sub

    Private Sub Trackbar6_Scroll(sender As Object) Handles Trackbar6.Scroll
        Button15.Text = sender.Value

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

    Private Sub ColorItem1_Click(sender As Object, e As EventArgs) Handles ColorItem1.Click, ColorItem1.DragDrop

        If TypeOf e Is DragEventArgs Then
            _SelectedControl.Prop_Shadow_Color = sender.BackColor
            _SelectedControl.Invalidate()
            Exit Sub
        End If

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                _SelectedControl.Prop_Shadow_Color = sender.BackColor
                _SelectedControl.Invalidate()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {DirectCast(sender, UI.Controllers.ColorItem), _SelectedControl}

        Dim _Condition As New Conditions With {.CursorShadow = True}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition)

        _SelectedControl.Prop_Shadow_Color = c
        _SelectedControl.Invalidate()
        DirectCast(sender, UI.Controllers.ColorItem).BackColor = c
        DirectCast(sender, UI.Controllers.ColorItem).Invalidate()

        CList.Clear()

    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar7.Maximum), Trackbar7.Minimum) : Trackbar7.Value = Val(sender.Text)
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar8.Maximum), Trackbar8.Minimum) : Trackbar8.Value = Val(sender.Text)
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar9.Maximum), Trackbar9.Minimum) : Trackbar9.Value = Val(sender.Text)
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Dim response As String = InputBox(My.Lang.InputValue, sender.text, My.Lang.ItMustBeNumerical)
        sender.Text = Math.Max(Math.Min(Val(response), Trackbar10.Maximum), Trackbar10.Minimum) : Trackbar10.Value = Val(sender.Text)
    End Sub

    Private Sub Trackbar7_Scroll(sender As Object) Handles Trackbar7.Scroll
        Button16.Text = sender.Value

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

    Private Sub Trackbar8_Scroll(sender As Object) Handles Trackbar8.Scroll
        Button17.Text = sender.Value

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

    Private Sub Trackbar9_Scroll(sender As Object) Handles Trackbar9.Scroll
        Button18.Text = sender.Value

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

    Private Sub Trackbar10_Scroll(sender As Object) Handles Trackbar10.Scroll
        Button19.Text = sender.Value

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

    Private Sub CheckBox11_CheckedChanged(sender As Object) Handles CheckBox11.CheckedChanged
        If Not _Shown Then Exit Sub
        _SelectedControl.Prop_Shadow_Enabled = CheckBox11.Checked
        _SelectedControl.Invalidate()
    End Sub

    Private Sub CursorsStudio_HelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.HelpButtonClicked
        Process.Start(My.Resources.Link_Wiki & "/Edit-Windows-cursors")
    End Sub
End Class