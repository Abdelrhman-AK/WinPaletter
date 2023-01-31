Imports WinPaletter.XenonCore

Public Class CursorsStudio
    Private _Shown As Boolean = False
    Private _SelectedControl As CursorControl
    Private _CopiedControl As CursorControl
    Private ReadOnly AnimateList As New List(Of CursorControl)

    Sub CursorCP_to_Cursor([CursorControl] As CursorControl, [Cursor] As CP.Structures.Cursor)
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
    End Sub

    Function Cursor_to_CursorCP([CursorControl] As CursorControl) As CP.Structures.Cursor
        Dim [Cursor] As CP.Structures.Cursor
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
        Return [Cursor]
    End Function

    Sub LoadFromCP([CP] As CP)
        XenonToggle1.Checked = [CP].Cursors_Enabled

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
        [CP].Cursors_Enabled = XenonToggle1.Checked
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
        ApplyDarkMode(Me)
        MainFrm.Visible = False
        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)

        MainFrm.MakeItDoubleBuffered(FlowLayoutPanel1)
        MainFrm.MakeItDoubleBuffered(Arrow)
        MainFrm.MakeItDoubleBuffered(Help)
        MainFrm.MakeItDoubleBuffered(AppLoading)
        MainFrm.MakeItDoubleBuffered(Busy)
        MainFrm.MakeItDoubleBuffered(Move_Cur)
        MainFrm.MakeItDoubleBuffered(NS)
        MainFrm.MakeItDoubleBuffered(EW)
        MainFrm.MakeItDoubleBuffered(NESW)
        MainFrm.MakeItDoubleBuffered(NWSE)
        MainFrm.MakeItDoubleBuffered(Up)
        MainFrm.MakeItDoubleBuffered(Pen)
        MainFrm.MakeItDoubleBuffered(None)
        MainFrm.MakeItDoubleBuffered(Link)
        MainFrm.MakeItDoubleBuffered(Pin)
        MainFrm.MakeItDoubleBuffered(Person)
        MainFrm.MakeItDoubleBuffered(IBeam)
        MainFrm.MakeItDoubleBuffered(Cross)

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

        LoadFromCP(MainFrm.CP)
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

        XenonGroupBox2.Enabled = True
        XenonGroupBox11.Enabled = True
        XenonButton1.Enabled = True

        If _SelectedControl.Prop_Cursor = CursorType.AppLoading Or _SelectedControl.Prop_Cursor = CursorType.Busy Then

            If _SelectedControl.Prop_Cursor = CursorType.Busy Then
                XenonGroupBox2.Enabled = False
                XenonGroupBox11.Enabled = False
            End If

            XenonGroupBox6.Enabled = True
            XenonGroupBox12.Enabled = True
        Else
            XenonGroupBox6.Enabled = False
            XenonGroupBox12.Enabled = False
        End If

    End Sub

    Sub ApplyColorsFromCursor([CursorControl] As CursorControl)
        With [CursorControl]
            TaskbarFrontAndFoldersOnStart_picker.BackColor = .Prop_PrimaryColor1
            XenonGroupBox3.BackColor = .Prop_PrimaryColor2
            XenonCheckBox1.Checked = .Prop_PrimaryColorGradient
            XenonComboBox1.SelectedItem = ReturnStringFromGradientMode(.Prop_PrimaryColorGradientMode)
            XenonCheckBox5.Checked = .Prop_PrimaryNoise
            XenonNumericUpDown2.Text = .Prop_PrimaryNoiseOpacity * 100

            XenonGroupBox5.BackColor = .Prop_SecondaryColor1
            XenonGroupBox4.BackColor = .Prop_SecondaryColor2
            XenonCheckBox4.Checked = .Prop_SecondaryColorGradient
            XenonComboBox2.SelectedItem = ReturnStringFromGradientMode(.Prop_SecondaryColorGradientMode)
            XenonCheckBox3.Checked = .Prop_SecondaryNoise
            XenonNumericUpDown1.Text = .Prop_SecondaryNoiseOpacity * 100
            'XenonNumericUpDown3.Value = .Prop_LineThickness * 10

            XenonGroupBox10.BackColor = .Prop_LoadingCircleBack1
            XenonGroupBox9.BackColor = .Prop_LoadingCircleBack2
            XenonCheckBox8.Checked = .Prop_LoadingCircleBackGradient
            XenonComboBox4.SelectedItem = ReturnStringFromGradientMode(.Prop_LoadingCircleBackGradientMode)
            XenonCheckBox7.Checked = .Prop_LoadingCircleBackNoise
            XenonNumericUpDown6.Text = .Prop_LoadingCircleBackNoiseOpacity * 100

            XenonGroupBox8.BackColor = .Prop_LoadingCircleHot1
            XenonGroupBox7.BackColor = .Prop_LoadingCircleHot2
            XenonCheckBox2.Checked = .Prop_LoadingCircleHotGradient
            XenonComboBox3.SelectedItem = ReturnStringFromGradientMode(.Prop_LoadingCircleHotGradientMode)
            XenonCheckBox6.Checked = .Prop_LoadingCircleHotNoise
            XenonNumericUpDown4.Text = .Prop_LoadingCircleHotNoiseOpacity * 100
        End With

    End Sub

    Sub ApplyColorsToPreview([CursorControl] As CursorControl)
        With [CursorControl]
            .Prop_PrimaryColor1 = TaskbarFrontAndFoldersOnStart_picker.BackColor
            .Prop_PrimaryColor2 = XenonGroupBox3.BackColor
            .Prop_PrimaryColorGradient = XenonCheckBox1.Checked
            .Prop_PrimaryColorGradientMode = ReturnGradientModeFromString(XenonComboBox1.SelectedItem)
            .Prop_PrimaryNoise = XenonCheckBox5.Checked
            .Prop_PrimaryNoiseOpacity = Val(XenonNumericUpDown2.Text) / 100

            .Prop_SecondaryColor1 = XenonGroupBox5.BackColor
            .Prop_SecondaryColor2 = XenonGroupBox4.BackColor
            .Prop_SecondaryColorGradient = XenonCheckBox4.Checked
            .Prop_SecondaryColorGradientMode = ReturnGradientModeFromString(XenonComboBox2.SelectedItem)
            .Prop_SecondaryNoise = XenonCheckBox3.Checked
            .Prop_SecondaryNoiseOpacity = Val(XenonNumericUpDown1.Text) / 100
            '.Prop_LineThickness = XenonNumericUpDown3.Value / 10

            .Prop_LoadingCircleBack1 = XenonGroupBox10.BackColor
            .Prop_LoadingCircleBack2 = XenonGroupBox9.BackColor
            .Prop_LoadingCircleBackGradient = XenonCheckBox8.Checked
            .Prop_LoadingCircleBackGradientMode = ReturnGradientModeFromString(XenonComboBox4.SelectedItem)
            .Prop_LoadingCircleBackNoise = XenonCheckBox7.Checked
            .Prop_LoadingCircleBackNoiseOpacity = Val(XenonNumericUpDown6.Text) / 100

            .Prop_LoadingCircleHot1 = XenonGroupBox8.BackColor
            .Prop_LoadingCircleHot2 = XenonGroupBox7.BackColor
            .Prop_LoadingCircleHotGradient = XenonCheckBox2.Checked
            .Prop_LoadingCircleHotGradientMode = ReturnGradientModeFromString(XenonComboBox3.SelectedItem)
            .Prop_LoadingCircleHotNoise = XenonCheckBox6.Checked
            .Prop_LoadingCircleHotNoiseOpacity = Val(XenonNumericUpDown4.Text) / 100
        End With

    End Sub

    Private Sub TaskbarFrontAndFoldersOnStart_picker_Click(sender As Object, e As EventArgs) Handles TaskbarFrontAndFoldersOnStart_picker.Click
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

    Private Sub XenonGroupBox3_Click(sender As Object, e As EventArgs) Handles XenonGroupBox3.Click
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

    Private Sub XenonGroupBox5_Click(sender As Object, e As EventArgs) Handles XenonGroupBox5.Click
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

    Private Sub XenonGroupBox4_Click(sender As Object, e As EventArgs) Handles XenonGroupBox4.Click
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

    Private Sub XenonGroupBox10_Click(sender As Object, e As EventArgs) Handles XenonGroupBox10.Click
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

    Private Sub XenonGroupBox9_Click(sender As Object, e As EventArgs) Handles XenonGroupBox9.Click
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

    Private Sub XenonGroupBox8_Click(sender As Object, e As EventArgs) Handles XenonGroupBox8.Click
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

    Private Sub XenonGroupBox7_Click(sender As Object, e As EventArgs) Handles XenonGroupBox7.Click
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

    Private Sub XenonTextBox1_TextChanged(sender As Object, e As EventArgs) Handles XenonNumericUpDown2.TextChanged
        If Not _Shown Then Exit Sub

        Dim valX As Single = Val(sender.Text)
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_PrimaryNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonTextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles XenonNumericUpDown6.TextChanged
        If Not _Shown Then Exit Sub

        Dim valX As Single = Val(sender.Text)
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_LoadingCircleBackNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonTextBox1_TextChanged_2(sender As Object, e As EventArgs) Handles XenonNumericUpDown1.TextChanged
        If Not _Shown Then Exit Sub

        Dim valX As Single = Val(sender.Text)
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_SecondaryNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonTextBox1_TextChanged_3(sender As Object, e As EventArgs) Handles XenonNumericUpDown4.TextChanged
        If Not _Shown Then Exit Sub

        Dim valX As Single = Val(sender.Text)
        If valX > 100 Then
            valX = 100
        ElseIf valX < 0 Then
            valX = 0
        End If

        _SelectedControl.Prop_LoadingCircleHotNoiseOpacity = valX / 100
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Me.Close()
    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        SaveToCP(MainFrm.CP)
        Me.Close()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim CPx As New CP(CP.Mode.File, OpenFileDialog1.FileName)
            LoadFromCP(CPx)
            CPx.Dispose()
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        Dim CPx As New CP(CP.Mode.Registry)
        LoadFromCP(CPx)
        CPx.Dispose()
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        Dim _Def As CP
        If MainFrm.PreviewConfig = MainFrm.WinVer.W11 Then
            _Def = New CP_Defaults().Default_Windows11
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W10 Then
            _Def = New CP_Defaults().Default_Windows10
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W8 Then
            _Def = New CP_Defaults().Default_Windows8
        ElseIf MainFrm.PreviewConfig = MainFrm.WinVer.W7 Then
            _Def = New CP_Defaults().Default_Windows7
        Else
            _Def = New CP_Defaults().Default_Windows11
        End If

        LoadFromCP(_Def)
        _Def.Dispose()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        MsgBox(My.Lang.ScalingTip, MsgBoxStyle.Information)
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.Mode.Registry)
        SaveToCP(CPx)
        CPx.Apply_Cursors()
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub
End Class