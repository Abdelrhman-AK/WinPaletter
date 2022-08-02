Imports WinPaletter.XenonCore
Imports System.IO
Imports System.Drawing.Imaging
Imports AnimCur
Imports Microsoft.Win32
Imports System.Text

Public Class CursorsStudio

    Private _SelectedControl As CursorControl
    Private _CopiedControl As CursorControl

    Private AnimateList As New List(Of CursorControl)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        AnimateList.Clear()
        _CopiedControl = Nothing

        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then
                AddHandler i.Click, AddressOf Clicked
                If i.Prop_Cursor = CursorType.AppLoading Or i.Prop_Cursor = CursorType.Busy Then AnimateList.Add(i)
            End If
        Next

    End Sub

    Sub Clicked(sender As Object, e As MouseEventArgs)
        _SelectedControl = DirectCast(sender, CursorControl)
        ApplyColorsFromCursor(_SelectedControl)
    End Sub

    Sub ApplyColorsFromCursor([CursorControl] As CursorControl)
        With [CursorControl]
            TaskbarFrontAndFoldersOnStart_picker.BackColor = .Prop_PrimaryColor1
            XenonGroupBox3.BackColor = .Prop_PrimaryColor2
            XenonCheckBox1.Checked = .Prop_PrimaryColorGradient
            XenonComboBox1.SelectedIndex = .Prop_PrimaryColorGradientMode
            XenonCheckBox5.Checked = .Prop_PrimaryNoise
            XenonNumericUpDown2.Value = .Prop_PrimaryNoiseOpacity * 100

            XenonGroupBox5.BackColor = .Prop_SecondaryColor1
            XenonGroupBox4.BackColor = .Prop_SecondaryColor2
            XenonCheckBox4.Checked = .Prop_SecondaryColorGradient
            XenonComboBox2.SelectedIndex = .Prop_SecondaryColorGradientMode
            XenonCheckBox3.Checked = .Prop_SecondaryNoise
            XenonNumericUpDown1.Value = .Prop_SecondaryNoiseOpacity * 100
            XenonNumericUpDown3.Value = .Prop_LineThickness

            XenonGroupBox10.BackColor = .Prop_LoadingCircleBack1
            XenonGroupBox9.BackColor = .Prop_LoadingCircleBack2
            XenonCheckBox8.Checked = .Prop_LoadingCircleBackGradient
            XenonComboBox4.SelectedIndex = .Prop_LoadingCircleBackGradientMode
            XenonCheckBox7.Checked = .Prop_LoadingCircleBackNoise
            XenonNumericUpDown6.Value = .Prop_LoadingCircleBackNoiseOpacity * 100

            XenonGroupBox8.BackColor = .Prop_LoadingCircleHot1
            XenonGroupBox7.BackColor = .Prop_LoadingCircleHot2
            XenonCheckBox2.Checked = .Prop_LoadingCircleHotGradient
            XenonComboBox3.SelectedIndex = .Prop_LoadingCircleHotGradientMode
            XenonCheckBox6.Checked = .Prop_LoadingCircleHotNoise
            XenonNumericUpDown4.Value = .Prop_LoadingCircleHotNoiseOpacity * 100
        End With

    End Sub

    Sub ApplyColorsToPreview([CursorControl] As CursorControl)
        With [CursorControl]
            .Prop_PrimaryColor1 = TaskbarFrontAndFoldersOnStart_picker.BackColor
            .Prop_PrimaryColor2 = XenonGroupBox3.BackColor
            .Prop_PrimaryColorGradient = XenonCheckBox1.Checked
            .Prop_PrimaryColorGradientMode = XenonComboBox1.SelectedIndex
            .Prop_PrimaryNoise = XenonCheckBox5.Checked
            .Prop_PrimaryNoiseOpacity = XenonNumericUpDown2.Value / 100

            .Prop_SecondaryColor1 = XenonGroupBox5.BackColor
            .Prop_SecondaryColor2 = XenonGroupBox4.BackColor
            .Prop_SecondaryColorGradient = XenonCheckBox4.Checked
            .Prop_SecondaryColorGradientMode = XenonComboBox2.SelectedIndex
            .Prop_SecondaryNoise = XenonCheckBox3.Checked
            .Prop_SecondaryNoiseOpacity = XenonNumericUpDown1.Value / 100
            .Prop_LineThickness = XenonNumericUpDown3.Value

            .Prop_LoadingCircleBack1 = XenonGroupBox10.BackColor
            .Prop_LoadingCircleBack2 = XenonGroupBox9.BackColor
            .Prop_LoadingCircleBackGradient = XenonCheckBox8.Checked
            .Prop_LoadingCircleBackGradientMode = XenonComboBox4.SelectedIndex
            .Prop_LoadingCircleBackNoise = XenonCheckBox7.Checked
            .Prop_LoadingCircleBackNoiseOpacity = XenonNumericUpDown6.Value / 100

            .Prop_LoadingCircleHot1 = XenonGroupBox8.BackColor
            .Prop_LoadingCircleHot2 = XenonGroupBox7.BackColor
            .Prop_LoadingCircleHotGradient = XenonCheckBox2.Checked
            .Prop_LoadingCircleHotGradientMode = XenonComboBox3.SelectedIndex
            .Prop_LoadingCircleHotNoise = XenonCheckBox6.Checked
            .Prop_LoadingCircleHotNoiseOpacity = XenonNumericUpDown4.Value / 100
        End With

    End Sub

    Private Sub TaskbarFrontAndFoldersOnStart_picker_Click(sender As Object, e As EventArgs) Handles TaskbarFrontAndFoldersOnStart_picker.Click
        Dim CList As New List(Of Control)
        CList.Add(DirectCast(sender, XenonGroupBox))
        CList.Add(_SelectedControl)

        Dim c As Color = ColorPickerDlg.Pick(CList)

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

        Dim c As Color = ColorPickerDlg.Pick(CList)
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

        Dim c As Color = ColorPickerDlg.Pick(CList)
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

        Dim c As Color = ColorPickerDlg.Pick(CList)
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

        Dim c As Color = ColorPickerDlg.Pick(CList)
        _SelectedControl.Prop_LoadingCircleBack1 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonCheckBox1_CheckedChanged(sender As Object) Handles XenonCheckBox1.CheckedChanged
        _SelectedControl.Prop_PrimaryColorGradient = If(XenonCheckBox1.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox1.Invalidate()
    End Sub

    Private Sub XenonCheckBox4_CheckedChanged(sender As Object) Handles XenonCheckBox4.CheckedChanged
        _SelectedControl.Prop_SecondaryColorGradient = If(XenonCheckBox4.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox4.Invalidate()

    End Sub

    Private Sub XenonNumericUpDown2_Click(sender As Object, e As EventArgs) Handles XenonNumericUpDown2.Click
        _SelectedControl.Prop_PrimaryNoiseOpacity = sender.Value / 100
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        _SelectedControl.Prop_PrimaryColorGradientMode = sender.SelectedIndex
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox2.SelectedIndexChanged
        _SelectedControl.Prop_SecondaryColorGradientMode = sender.SelectedIndex
        _SelectedControl.Invalidate()

    End Sub


    Private Sub XenonCheckBox5_CheckedChanged(sender As Object) Handles XenonCheckBox5.CheckedChanged
        _SelectedControl.Prop_PrimaryNoise = If(XenonCheckBox5.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox5.Invalidate()

    End Sub

    Private Sub XenonCheckBox3_CheckedChanged(sender As Object) Handles XenonCheckBox3.CheckedChanged
        _SelectedControl.Prop_SecondaryNoise = If(XenonCheckBox3.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox3.Invalidate()

    End Sub

    Private Sub XenonNumericUpDown3_Click(sender As Object, e As EventArgs) Handles XenonNumericUpDown3.Click
        _SelectedControl.Prop_LineThickness = sender.Value
        _SelectedControl.Invalidate()
    End Sub

    Private Sub XenonNumericUpDown1_Click(sender As Object, e As EventArgs) Handles XenonNumericUpDown1.Click
        _SelectedControl.Prop_SecondaryNoiseOpacity = sender.Value / 100
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonScrollBarHMini1_Scroll(sender As Object) Handles XenonScrollBarHMini1.Scroll
        For Each i As CursorControl In FlowLayoutPanel1.Controls
            i.Prop_Scale = sender.value / 100
            i.Width = 32 * i.Prop_Scale + 32
            i.Height = i.Width
            i.Refresh()

        Next
    End Sub

    Dim Angle As Single = 180
    Dim Increment As Single = 5
    Dim Cycles As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
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

        Dim c As Color = ColorPickerDlg.Pick(CList)
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

        Dim c As Color = ColorPickerDlg.Pick(CList)
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

        Dim c As Color = ColorPickerDlg.Pick(CList)
        _SelectedControl.Prop_LoadingCircleHot2 = c
        _SelectedControl.Invalidate()
        DirectCast(sender, XenonGroupBox).BackColor = c
        DirectCast(sender, XenonGroupBox).Invalidate()

        CList.Clear()

    End Sub

    Private Sub XenonCheckBox8_CheckedChanged(sender As Object) Handles XenonCheckBox8.CheckedChanged
        _SelectedControl.Prop_LoadingCircleBackGradient = If(XenonCheckBox8.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox8.Invalidate()
    End Sub

    Private Sub XenonCheckBox2_CheckedChanged(sender As Object) Handles XenonCheckBox2.CheckedChanged
        '### add if _shown then ...........

        _SelectedControl.Prop_LoadingCircleHotGradient = If(XenonCheckBox2.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox2.Invalidate()
    End Sub

    Private Sub XenonCheckBox7_CheckedChanged(sender As Object) Handles XenonCheckBox7.CheckedChanged
        _SelectedControl.Prop_LoadingCircleBackNoise = If(XenonCheckBox7.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox7.Invalidate()

    End Sub

    Private Sub XenonCheckBox6_CheckedChanged(sender As Object) Handles XenonCheckBox6.CheckedChanged
        _SelectedControl.Prop_LoadingCircleHotNoise = If(XenonCheckBox6.Checked, True, False)
        _SelectedControl.Invalidate()
        XenonCheckBox6.Invalidate()

    End Sub

    Private Sub XenonNumericUpDown6_Click(sender As Object, e As EventArgs) Handles XenonNumericUpDown6.Click
        _SelectedControl.Prop_LoadingCircleBackNoiseOpacity = sender.Value / 100
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonNumericUpDown4_Click(sender As Object, e As EventArgs) Handles XenonNumericUpDown4.Click
        _SelectedControl.Prop_LoadingCircleHotNoiseOpacity = sender.Value / 100
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox4.SelectedIndexChanged
        _SelectedControl.Prop_LoadingCircleBackGradientMode = sender.SelectedIndex
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox3.SelectedIndexChanged
        _SelectedControl.Prop_LoadingCircleHotGradientMode = sender.SelectedIndex
        _SelectedControl.Invalidate()

    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        _CopiedControl = _SelectedControl
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        ApplyColorsFromCursor(_CopiedControl)
        ApplyColorsToPreview(_SelectedControl)
    End Sub

    'Dim fs As FileStream = New FileStream("D:\cur.cur", FileMode.Create)
    'Dim EO As New EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor)

    'For i As Single = 1 To 4 Step 0.5
    'EO.WriteBitmap(Draw(i), Nothing, New Point(5 * i - 0.5 * i, 10 * i - 0.5 * i)) 'New Point(1, 1)
    'Next

    'fs.Close()


End Class