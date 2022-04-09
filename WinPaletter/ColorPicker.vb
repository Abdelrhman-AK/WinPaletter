Imports System.ComponentModel
Imports Cyotek.Windows.Forms
Imports WinPaletter.XenonCore

Public Class ColorPicker
    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        ColorWheel1.Visible = True
        ColorGrid1.Visible = False
        XenonGroupBox3.Visible = False
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        ColorWheel1.Visible = False
        ColorGrid1.Visible = True
        XenonGroupBox3.Visible = False
    End Sub

    Private Sub ColorPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        Me.Left = fr.Right - 14
        Me.Top = fr.Top
        Me.Height = fr.Height

        ColorWheel1.Location = ColorGrid1.Location
    End Sub


    Private Sub XenonButton4_Click(sender As Object, e As EventArgs)
        ColorWheel1.Visible = False
        ColorGrid1.Visible = False
    End Sub

    Dim ls As New List(Of Control)

    Private Sub ScreenColorPicker1_MouseDown(sender As Object, e As MouseEventArgs) Handles ScreenColorPicker1.MouseDown
        MainForm.Visible = False
        Win32UI.Visible = False

        ls.Clear()

        For Each ctrl As Control In Me.Controls
            If Not TypeOf ctrl Is ScreenColorPicker And ctrl.Visible Then
                ctrl.Visible = False
                ls.Add(ctrl)
            End If
        Next

        Me.FormBorderStyle = FormBorderStyle.None
        Me.TransparencyKey = BackColor
    End Sub

    Private Sub ScreenColorPicker1_MouseUp(sender As Object, e As MouseEventArgs) Handles ScreenColorPicker1.MouseUp
        MainForm.Visible = True
        Win32UI.Visible = True

        For Each ctrl As Control In ls
            ctrl.Visible = True
        Next

        ls.Clear()
        Me.FormBorderStyle = FormBorderStyle.SizableToolWindow
        Me.TransparencyKey = Nothing
    End Sub

    Dim GetIfWindow_InactiveTitlebar As Boolean = False

    Dim GetIfAppUnderlineOnly As Boolean = False

    Dim GetIfAppBackgroundOnly As Boolean = False

    Dim GetIfStartColorOnly As Boolean = False

    Dim GetIfStartSearchOnly As Boolean = False

    Dim GetIfActionCenterBtn As Boolean = False

    Dim GetIfActionCenterLink As Boolean = False

    Dim InitColor As Color

    Dim PreviousWidth, DestinatedWidth As Integer

    Dim fr As Form

    Function Pick(ByVal Ctrl As List(Of Control), Optional ByVal Window_InactiveTitlebar As Boolean = False, Optional ByVal AppUnderlineOnly As Boolean = False,
                  Optional ByVal AppBackgroundOnly As Boolean = False, Optional ByVal StartColorOnly As Boolean = False,
                  Optional StartSearchOnly As Boolean = False, Optional ActionCenterBtn As Boolean = False, Optional ByVal ActionCenterLink As Boolean = False) As Color

        fr = Ctrl(0).FindForm

        If fr Is MainForm Then
            My.Application.AnimatorX.Hide(MainForm.PaletteContainer, True)
            My.Application.AnimatorX.Hide(MainForm.XenonGroupBox2, True)
            My.Application.AnimatorX.Hide(MainForm.apply_btn, True)
            My.Application.AnimatorX.Hide(MainForm.XenonButton4, True)
            My.Application.AnimatorX.Hide(MainForm.XenonButton13, True)
            My.Application.AnimatorX.Hide(MainForm.XenonSeparator1, True)

            PreviousWidth = MainForm.Width
            DestinatedWidth = MainForm.XenonGroupBox8.Width + MainForm.XenonGroupBox2.Left * 3.25
            MainForm.Width = DestinatedWidth
        End If

        Dim c As Color = Ctrl(0).BackColor
        ColorEditorManager1.Color = c
        InitColor = c

        CList = Ctrl

        GetIfWindow_InactiveTitlebar = Window_InactiveTitlebar
        GetIfAppUnderlineOnly = AppUnderlineOnly
        GetIfAppBackgroundOnly = AppBackgroundOnly
        GetIfStartColorOnly = StartColorOnly
        GetIfStartSearchOnly = StartSearchOnly
        GetIfActionCenterBtn = ActionCenterBtn
        GetIfActionCenterLink = ActionCenterLink

        AddHandler ColorEditorManager1.ColorChanged, AddressOf CHANGECOLORPREVIEW


        If Me.ShowDialog() = DialogResult.OK Then
            c = ColorEditorManager1.Color
        Else
            ColorEditorManager1.Color = InitColor
            CHANGECOLORPREVIEW()
            c = InitColor
        End If

        GetIfWindow_InactiveTitlebar = False
        GetIfAppUnderlineOnly = False
        GetIfAppBackgroundOnly = False
        GetIfStartColorOnly = False
        GetIfStartSearchOnly = False
        GetIfActionCenterBtn = False
        GetIfActionCenterLink = False

        RemoveHandler ColorEditorManager1.ColorChanged, AddressOf CHANGECOLORPREVIEW

        If fr Is MainForm Then
            MainForm.Width = PreviousWidth
            My.Application.AnimatorX.Show(MainForm.PaletteContainer, True)
            My.Application.AnimatorX.Show(MainForm.XenonGroupBox2, True)
            My.Application.AnimatorX.Show(MainForm.apply_btn, True)
            My.Application.AnimatorX.Show(MainForm.XenonButton4, True)
            My.Application.AnimatorX.Show(MainForm.XenonButton13, True)
            My.Application.AnimatorX.Show(MainForm.XenonSeparator1, True)
        End If

        fr = Nothing
        Return c
    End Function

    Dim CList As New List(Of Control)

    Sub CHANGECOLORPREVIEW()
        Dim steps As Integer = 30
        Dim delay As Integer = 1

        For Each ctrl As Control In CList

            If TypeOf ctrl Is XenonWindow Then
                With TryCast(ctrl, XenonWindow)
                    If Not GetIfWindow_InactiveTitlebar Then
                        .AccentColor_Active = ColorEditorManager1.Color
                    Else
                        .AccentColor_Inactive = ColorEditorManager1.Color
                    End If
                    .Invalidate()
                End With

            ElseIf TypeOf ctrl Is XenonToggle Then
                With TryCast(ctrl, XenonToggle)
                    .ColorPalette.BaseColor = Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color)
                    .AccentColor = Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color)
                    .Invalidate()
                End With

            ElseIf TypeOf ctrl Is XenonAcrylic Then

                With TryCast(ctrl, XenonAcrylic)

                    If .UseItAsTaskbar Then
                        If GetIfAppUnderlineOnly Then
                            Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "AppUnderline", .AppUnderline, ControlPaint.Light(Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color)), steps, delay)
                            .Invalidate()
                        ElseIf GetIfAppBackgroundOnly Then
                            Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "AppBackground", .AppBackground, Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color), steps, delay)
                            .Invalidate()
                        ElseIf GetIfStartColorOnly Then
                            Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "StartColor", .StartColor, Color.FromArgb(255, ColorEditorManager1.Color), steps, delay)
                            .Invalidate()
                        Else
                            Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "BackColor", .BackColor, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                            .Invalidate()
                        End If
                        .Invalidate()

                    ElseIf .UseItAsStartMenu And GetIfStartSearchOnly Then
                        Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "SearchBoxAccent", .SearchBoxAccent, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                        .Invalidate()

                    ElseIf .UseItAsActionCenter And GetIfActionCenterBtn Then
                        Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "ActionCenterButton_Normal", .ActionCenterButton_Normal, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                        .Invalidate()

                    ElseIf .UseItAsActionCenter And GetIfActionCenterLink Then
                        Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "LinkColor", .LinkColor, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                        .Invalidate()
                    Else
                        Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "BackColor", .BackColor, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                        .Invalidate()
                    End If
                End With

            ElseIf TypeOf ctrl Is Label Then
                Visual.FadeColor(ctrl, "Forecolor", ctrl.ForeColor, Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color), steps, delay)
            Else
                Visual.FadeColor(ctrl, "BackColor", ctrl.BackColor, Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color), steps, delay)
            End If

        Next
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        Select Case XenonRadioButton1.Checked
            Case True
                img = My.Application.GetCurrentWallpaper()
            Case False
                If IO.File.Exists(TextBox1.Text) Then
                    Try
                        fs = New IO.FileStream(TextBox1.Text, IO.FileMode.OpenOrCreate, IO.FileAccess.Read)
                        img = Image.FromStream(fs)
                        fs.Close()
                    Catch
                        img = Nothing
                    End Try
                Else
                    img = Nothing
                End If
        End Select

        If img IsNot Nothing Then
            Label4.Text = "Extracting palette from image depends on your device's performance, maximum palette colors number, image quality and its resolution ..."
            My.Application.AnimatorX.HideSync(XenonButton6, True)
            My.Application.AnimatorX.HideSync(ImgPaletteContainer, True)
            ProgressBar1.Visible = True
            ColorsList.Clear()

            Try
                BackgroundWorker1.CancelAsync()
                BackgroundWorker1.RunWorkerAsync()
            Catch : End Try
        End If

    End Sub


    Dim ColorsList As New List(Of Color)
    Dim img As Image
    Dim colorThiefX As New ColorThiefDotNet.ColorThief

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If img IsNot Nothing Then
            For Each C As ColorThiefDotNet.QuantizedColor In colorThiefX.GetPalette(img, XenonNumericUpDown1.Value, XenonNumericUpDown2.Value, XenonCheckBox1.Checked)
                ColorsList.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B))
            Next

            XenonCore.SetCtrlTxt("Sorting Colors in Palette ...", Label4)

        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ImgPaletteContainer.Controls.Clear()

        For Each C As Color In ColorsList
            Dim pnl As XenonGroupBox = New XenonGroupBox With {.Size = New Drawing.Size(30, 25), .CustomColor = True}
            pnl.BackColor = Color.FromArgb(255, C)
            ImgPaletteContainer.Controls.Add(pnl)
            AddHandler pnl.Click, AddressOf pnl_click
        Next

        ProgressBar1.Visible = False
        ColorsList.Clear()
        My.Application.AnimatorX.ShowSync(ImgPaletteContainer, True)
        My.Application.AnimatorX.ShowSync(XenonButton6, True)
    End Sub

    Private Sub pnl_click(ByVal sender As Object, ByVal e As EventArgs)
        With CType(sender, XenonGroupBox)
            ColorEditorManager1.Color = .BackColor
            ColorEditor1.Color = .BackColor
            ColorGrid1.Color = .BackColor
            ColorWheel1.Color = .BackColor
        End With

        CHANGECOLORPREVIEW()
    End Sub

    Public Shared fs As System.IO.FileStream

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        ColorWheel1.Visible = False
        ColorGrid1.Visible = False
        XenonGroupBox3.Visible = True
    End Sub

    Private Sub XenonButton4_Click_1(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub XenonRadioButton1_CheckedChanged(sender As Object)

    End Sub

    Private Sub ScreenColorPicker1_ColorChanged(sender As Object, e As EventArgs) Handles ScreenColorPicker1.ColorChanged

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class