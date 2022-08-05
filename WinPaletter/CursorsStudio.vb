Imports WinPaletter.XenonCore
Imports System.IO

Public Class CursorsStudio
    Private _Shown As Boolean = False
    Private _SelectedControl As CursorControl
    Private _CopiedControl As CursorControl
    Private AnimateList As New List(Of CursorControl)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
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
    End Sub

    Private Sub CursorsStudio_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub Clicked(sender As Object, e As MouseEventArgs)
        _SelectedControl = DirectCast(sender, CursorControl)
        ApplyColorsFromCursor(_SelectedControl)

        XenonGroupBox2.Enabled = True
        XenonGroupBox11.Enabled = True

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
            XenonComboBox1.SelectedItem = RetrunStringFromGradientMode(.Prop_PrimaryColorGradientMode)
            XenonCheckBox5.Checked = .Prop_PrimaryNoise
            XenonNumericUpDown2.Text = .Prop_PrimaryNoiseOpacity * 100

            XenonGroupBox5.BackColor = .Prop_SecondaryColor1
            XenonGroupBox4.BackColor = .Prop_SecondaryColor2
            XenonCheckBox4.Checked = .Prop_SecondaryColorGradient
            XenonComboBox2.SelectedItem = RetrunStringFromGradientMode(.Prop_SecondaryColorGradientMode)
            XenonCheckBox3.Checked = .Prop_SecondaryNoise
            XenonNumericUpDown1.Text = .Prop_SecondaryNoiseOpacity * 100
            'XenonNumericUpDown3.Value = .Prop_LineThickness * 10

            XenonGroupBox10.BackColor = .Prop_LoadingCircleBack1
            XenonGroupBox9.BackColor = .Prop_LoadingCircleBack2
            XenonCheckBox8.Checked = .Prop_LoadingCircleBackGradient
            XenonComboBox4.SelectedItem = RetrunStringFromGradientMode(.Prop_LoadingCircleBackGradientMode)
            XenonCheckBox7.Checked = .Prop_LoadingCircleBackNoise
            XenonNumericUpDown6.Text = .Prop_LoadingCircleBackNoiseOpacity * 100

            XenonGroupBox8.BackColor = .Prop_LoadingCircleHot1
            XenonGroupBox7.BackColor = .Prop_LoadingCircleHot2
            XenonCheckBox2.Checked = .Prop_LoadingCircleHotGradient
            XenonComboBox3.SelectedItem = RetrunStringFromGradientMode(.Prop_LoadingCircleHotGradientMode)
            XenonCheckBox6.Checked = .Prop_LoadingCircleHotNoise
            XenonNumericUpDown4.Text = .Prop_LoadingCircleHotNoiseOpacity * 100
        End With

    End Sub

    Sub ApplyColorsToPreview([CursorControl] As CursorControl)
        With [CursorControl]
            .Prop_PrimaryColor1 = TaskbarFrontAndFoldersOnStart_picker.BackColor
            .Prop_PrimaryColor2 = XenonGroupBox3.BackColor
            .Prop_PrimaryColorGradient = XenonCheckBox1.Checked
            .Prop_PrimaryColorGradientMode = RetrunGradientModeFromString(XenonComboBox1.SelectedItem)
            .Prop_PrimaryNoise = XenonCheckBox5.Checked
            .Prop_PrimaryNoiseOpacity = Val(XenonNumericUpDown2.Text) / 100

            .Prop_SecondaryColor1 = XenonGroupBox5.BackColor
            .Prop_SecondaryColor2 = XenonGroupBox4.BackColor
            .Prop_SecondaryColorGradient = XenonCheckBox4.Checked
            .Prop_SecondaryColorGradientMode = RetrunGradientModeFromString(XenonComboBox2.SelectedItem)
            .Prop_SecondaryNoise = XenonCheckBox3.Checked
            .Prop_SecondaryNoiseOpacity = Val(XenonNumericUpDown1.Text) / 100
            '.Prop_LineThickness = XenonNumericUpDown3.Value / 10

            .Prop_LoadingCircleBack1 = XenonGroupBox10.BackColor
            .Prop_LoadingCircleBack2 = XenonGroupBox9.BackColor
            .Prop_LoadingCircleBackGradient = XenonCheckBox8.Checked
            .Prop_LoadingCircleBackGradientMode = RetrunGradientModeFromString(XenonComboBox4.SelectedItem)
            .Prop_LoadingCircleBackNoise = XenonCheckBox7.Checked
            .Prop_LoadingCircleBackNoiseOpacity = Val(XenonNumericUpDown6.Text) / 100

            .Prop_LoadingCircleHot1 = XenonGroupBox8.BackColor
            .Prop_LoadingCircleHot2 = XenonGroupBox7.BackColor
            .Prop_LoadingCircleHotGradient = XenonCheckBox2.Checked
            .Prop_LoadingCircleHotGradientMode = RetrunGradientModeFromString(XenonComboBox3.SelectedItem)
            .Prop_LoadingCircleHotNoise = XenonCheckBox6.Checked
            .Prop_LoadingCircleHotNoiseOpacity = Val(XenonNumericUpDown4.Text) / 100
        End With

    End Sub

    Private Sub TaskbarFrontAndFoldersOnStart_picker_Click(sender As Object, e As EventArgs) Handles TaskbarFrontAndFoldersOnStart_picker.Click

        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorBack1 = True}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_PrimaryColor1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox3_Click(sender As Object, e As EventArgs) Handles XenonGroupBox3.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorBack2 = True}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_PrimaryColor2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox5_Click(sender As Object, e As EventArgs) Handles XenonGroupBox5.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorLine1 = True}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_SecondaryColor1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox4_Click(sender As Object, e As EventArgs) Handles XenonGroupBox4.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorLine2 = True}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_SecondaryColor2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox10_Click(sender As Object, e As EventArgs) Handles XenonGroupBox10.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorCircle1 = True}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleBack1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_PrimaryColorGradient = If(XenonCheckBox1.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox1.Invalidate()
    End Sub

    Private Sub XenonCheckBox4_CheckedChanged(sender As Object) Handles XenonCheckBox4.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryColorGradient = If(XenonCheckBox4.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox4.Invalidate()

    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_PrimaryColorGradientMode = RetrunGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox2.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryColorGradientMode = RetrunGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub


    Private Sub XenonCheckBox5_CheckedChanged(sender As Object) Handles XenonCheckBox5.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_PrimaryNoise = If(XenonCheckBox5.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox5.Invalidate()

    End Sub

    Private Sub XenonCheckBox3_CheckedChanged(sender As Object) Handles XenonCheckBox3.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_SecondaryNoise = If(XenonCheckBox3.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox3.Invalidate()

    End Sub

    Private Sub XenonNumericUpDown3_Click(sender As Object, e As EventArgs)
        '_SelectedControl.Prop_LineThickness = sender.Value / 10
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonScrollBarHMini1_Scroll(sender As Object) Handles XenonScrollBarHMini1.Scroll
        If Not _Shown Then Exit Sub

        For Each i As CursorControl In FlowLayoutPanel1.Controls
            i.Prop_Scale = sender.value / 100
            i.Width = 32 * i.Prop_Scale + 32
            i.Height = i.Width
            i.Refresh()
        Next

        Label5.Text = String.Format("Scaling ({0}x)", sender.value / 100)
    End Sub

    Dim Angle As Single = 180
    Dim Increment As Single = 5
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
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorCircle2 = True}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleBack2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox8_Click(sender As Object, e As EventArgs) Handles XenonGroupBox8.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorCircleHot1 = True}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleHot1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonGroupBox7_Click(sender As Object, e As EventArgs) Handles XenonGroupBox7.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim _Condition As New Conditions With {.CursorCircleHot2 = True}
        Dim c As Color = ColorPickerDlg.Pick(CList, _Condition, True)

        _SelectedControl.Prop_LoadingCircleHot2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonCheckBox8_CheckedChanged(sender As Object) Handles XenonCheckBox8.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackGradient = If(XenonCheckBox8.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox8.Invalidate()
    End Sub

    Private Sub XenonCheckBox2_CheckedChanged(sender As Object) Handles XenonCheckBox2.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotGradient = If(XenonCheckBox2.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox2.Invalidate()
    End Sub

    Private Sub XenonCheckBox7_CheckedChanged(sender As Object) Handles XenonCheckBox7.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackNoise = If(XenonCheckBox7.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox7.Invalidate()

    End Sub

    Private Sub XenonCheckBox6_CheckedChanged(sender As Object) Handles XenonCheckBox6.CheckedChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotNoise = If(XenonCheckBox6.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox6.Invalidate()

    End Sub

    Private Sub XenonComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox4.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleBackGradientMode = RetrunGradientModeFromString(sender.SelectedItem)
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox3.SelectedIndexChanged
        If Not _Shown Then Exit Sub

        _SelectedControl.Prop_LoadingCircleHotGradientMode = RetrunGradientModeFromString(sender.SelectedItem)
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

        Dim BMPList As New List(Of Bitmap)

        Dim PList As New List(Of Point)

        BMPList.Clear()


#Region "Add angles bitmaps from 180 deg to 180 deg (Cycle)"

        For ang As Integer = 180 To 360 Step +10
            Dim bm As Bitmap

            bm = New Bitmap(Draw(CursorType.Busy, Color.Black, Color.Black, False, Drawing2D.LinearGradientMode.Vertical, Color.White, Color.White, False, Drawing2D.LinearGradientMode.Vertical,
                            Color.Red, Color.Red, False, Drawing2D.LinearGradientMode.Vertical, Color.Maroon, Color.Maroon, False, Drawing2D.LinearGradientMode.Vertical, False, 0, False, 0, False, 0, False, 0,
                             1, 1, ang))

            BMPList.Add(bm)
        Next

        For ang As Integer = 0 To 180 Step +10
            Dim bm As Bitmap

            bm = New Bitmap(Draw(CursorType.Busy, Color.Black, Color.Black, False, Drawing2D.LinearGradientMode.Vertical, Color.White, Color.White, False, Drawing2D.LinearGradientMode.Vertical,
                            Color.Red, Color.Red, False, Drawing2D.LinearGradientMode.Vertical, Color.Maroon, Color.Maroon, False, Drawing2D.LinearGradientMode.Vertical, False, 0, False, 0, False, 0, False, 0,
                             1, 1, ang))

            BMPList.Add(bm)
        Next
#End Region

        Dim Count As Integer = BMPList.Count
        Dim frameRates As UInteger() = New UInteger(Count - 1) {}
        Dim seqNums As UInteger() = New UInteger(Count - 1) {}

        For i = 0 To Count - 1
            frameRates(i) = Convert.ToUInt32(2)
            seqNums(i) = CUInt(i)
        Next


        Dim fs As New FileStream(String.Format("D:\ani\{0}.ani", 0), FileMode.Create)

        Dim AN As New EOANIWriter(fs, Count, 2, frameRates, seqNums, Nothing, Nothing, New Point(1, 1))

        For i = 0 To Count - 1
            AN.WriteFrame32(BMPList(i))
        Next

        fs.Close()

        'Me.Close()
    End Sub


    'Dim fs As FileStream = New FileStream("D:\cur.cur", FileMode.Create)
    'Dim EO As New EOIcoCurWriter(fs, 8, EOIcoCurWriter.IcoCurType.Cursor)

    'For i As Single = 1 To 4 Step 0.5
    'EO.WriteBitmap(Draw(i), Nothing, New Point(5 * i - 0.5 * i, 10 * i - 0.5 * i)) 'New Point(1, 1)
    'Next

    'fs.Close()

End Class