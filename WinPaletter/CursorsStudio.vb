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
    End Sub

    Private Sub TaskbarFrontAndFoldersOnStart_picker_Click(sender As Object, e As EventArgs) Handles TaskbarFrontAndFoldersOnStart_picker.Click
        Dim CList As New List(Of Control)
        CList.Clear()
        CList.Add(sender)

        Dim c As Color = ColorPickerDlg.Pick(CList)
        _SelectedControl.Prop_BackColor1 = c
        _SelectedControl.Invalidate()
        sender.backcolor = c

    End Sub

    'Dim fs As FileStream = New FileStream("D:\cur.cur", FileMode.Create)
    'Dim EO As New EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor)

    'For i As Single = 1 To 4 Step 0.5
    'EO.WriteBitmap(Draw(i), Nothing, New Point(5 * i - 0.5 * i, 10 * i - 0.5 * i)) 'New Point(1, 1)
    'Next

    'fs.Close()


End Class