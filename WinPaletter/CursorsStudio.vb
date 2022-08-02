Imports WinPaletter.XenonCore
Imports System.IO
Imports System.Drawing.Imaging
Imports AnimCur
Imports Microsoft.Win32
Imports System.Text

Public Class CursorsStudio

    Private _SelectedControl As CursorControl

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        For Each i As CursorControl In FlowLayoutPanel1.Controls
            If TypeOf i Is CursorControl Then AddHandler i.Click, AddressOf Clicked
        Next
    End Sub

    Sub Clicked(sender As Object, e As MouseEventArgs)
        _SelectedControl = DirectCast(sender, CursorControl)

        TaskbarFrontAndFoldersOnStart_picker.BackColor = _SelectedControl.Prop_PrimaryColor1
        XenonGroupBox3.BackColor = _SelectedControl.Prop_PrimaryColor2
        XenonCheckBox1.Checked = _SelectedControl.Prop_PrimaryColorGradient
        XenonComboBox1.SelectedIndex = _SelectedControl.Prop_PrimaryColorGradientMode
        XenonCheckBox5.Checked = _SelectedControl.Prop_PrimaryNoise
        XenonNumericUpDown2.Value = _SelectedControl.Prop_PrimaryNoiseOpacity * 100


        XenonGroupBox5.BackColor = _SelectedControl.Prop_SecondaryColor1
        XenonGroupBox4.BackColor = _SelectedControl.Prop_SecondaryColor2
        XenonCheckBox4.Checked = _SelectedControl.Prop_SecondaryColorGradient
        XenonComboBox2.SelectedIndex = _SelectedControl.Prop_SecondaryColorGradientMode
        XenonCheckBox3.Checked = _SelectedControl.Prop_SecondaryNoise
        XenonNumericUpDown1.Value = _SelectedControl.Prop_SecondaryNoiseOpacity * 100
        XenonNumericUpDown3.Value = _SelectedControl.Prop_LineThickness


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


    'Dim fs As FileStream = New FileStream("D:\cur.cur", FileMode.Create)
    'Dim EO As New EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor)

    'For i As Single = 1 To 4 Step 0.5
    'EO.WriteBitmap(Draw(i), Nothing, New Point(5 * i - 0.5 * i, 10 * i - 0.5 * i)) 'New Point(1, 1)
    'Next

    'fs.Close()


End Class