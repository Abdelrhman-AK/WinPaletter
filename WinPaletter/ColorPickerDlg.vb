Imports System.ComponentModel
Imports System.IO
Imports Cyotek.Windows.Forms
Imports WinPaletter.XenonCore

Public Class ColorPickerDlg

    Private Sub ColorPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        CP.PopulateThemeToListbox(XenonComboBox1)
        Me.Left = fr.Right - 14
        Me.Top = fr.Top
        Me.Height = fr.Height
    End Sub


    Private Sub XenonButton4_Click(sender As Object, e As EventArgs)
        ColorWheel1.Visible = False
        ColorGrid1.Visible = False
    End Sub

    Dim ls As New List(Of Control)

    Private Sub ScreenColorPicker1_MouseDown(sender As Object, e As MouseEventArgs) Handles ScreenColorPicker1.MouseDown

        ls.Clear()

        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl IsNot ScreenColorPicker And ctrl.Visible Then
                ctrl.Visible = False
                ls.Add(ctrl)
            End If
        Next

        Me.FormBorderStyle = FormBorderStyle.None
        Me.TransparencyKey = BackColor
    End Sub

    Private Sub ScreenColorPicker1_MouseUp(sender As Object, e As MouseEventArgs) Handles ScreenColorPicker1.MouseUp

        For Each ctrl As Control In ls
            ctrl.Visible = True
        Next

        ls.Clear()
        Me.FormBorderStyle = FormBorderStyle.SizableToolWindow
        Me.TransparencyKey = Nothing
    End Sub

    Private _Conditions As New Conditions


    Dim InitColor As Color

    Dim PreviousWidth, DestinatedWidth As Integer

    Dim fr As Form


    Function Pick(ByVal Ctrl As List(Of Control), Optional ByVal [Conditions] As Conditions = Nothing, Optional ShowAlpha As Boolean = False) As Color
        fr = Ctrl(0).FindForm

        If fr Is MainFrm Then
            My.Application.AnimatorX.Hide(MainFrm.PaletteContainer_W1x, True)
            My.Application.AnimatorX.Hide(MainFrm.XenonGroupBox2, True)
            My.Application.AnimatorX.Hide(MainFrm.apply_btn, True)
            My.Application.AnimatorX.Hide(MainFrm.XenonButton4, True)
            My.Application.AnimatorX.Hide(MainFrm.XenonButton13, True)
            My.Application.AnimatorX.Hide(MainFrm.XenonButton19, True)

            PreviousWidth = MainFrm.Width
            DestinatedWidth = MainFrm.XenonGroupBox8.Width + MainFrm.XenonGroupBox2.Left * 3.25
            MainFrm.Width = DestinatedWidth
        End If

        Dim c As Color = Ctrl(0).BackColor
        ColorEditorManager1.Color = c
        InitColor = c

        CList = Ctrl

        If [Conditions] Is Nothing Then _Conditions = New Conditions Else _Conditions = [Conditions]

        ColorEditorManager1.ColorEditor.ShowAlphaChannel = ShowAlpha

        AddHandler ColorEditorManager1.ColorChanged, AddressOf CHANGECOLORPREVIEW


        If Me.ShowDialog() = DialogResult.OK Then
            c = ColorEditorManager1.Color
        Else
            ColorEditorManager1.Color = InitColor
            CHANGECOLORPREVIEW()
            c = InitColor
        End If

        RemoveHandler ColorEditorManager1.ColorChanged, AddressOf CHANGECOLORPREVIEW

        If fr Is MainFrm Then
            MainFrm.Width = PreviousWidth
            My.Application.AnimatorX.Show(MainFrm.PaletteContainer_W1x, True)
            My.Application.AnimatorX.Show(MainFrm.XenonGroupBox2, True)
            My.Application.AnimatorX.Show(MainFrm.apply_btn, True)
            My.Application.AnimatorX.Show(MainFrm.XenonButton4, True)
            My.Application.AnimatorX.Show(MainFrm.XenonButton13, True)
            My.Application.AnimatorX.Show(MainFrm.XenonButton19, True)
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

                    If Not _Conditions.Win7 Then
                        If _Conditions.Window_ActiveTitlebar Then
                            .AccentColor_Active = ColorEditorManager1.Color
                        End If

                        If _Conditions.Window_InactiveTitlebar Then
                            .AccentColor_Inactive = ColorEditorManager1.Color
                        Else
                            .AccentColor_Active = ColorEditorManager1.Color
                        End If

                    Else

                        If _Conditions.Color1 Then
                            .AccentColor_Active = ColorEditorManager1.Color
                            .AccentColor_Inactive = ColorEditorManager1.Color
                        ElseIf _Conditions.Color2 Then
                            .AccentColor2_Active = ColorEditorManager1.Color
                            .AccentColor2_Inactive = ColorEditorManager1.Color
                        End If

                    End If

                        .Invalidate()
                End With

            ElseIf TypeOf ctrl Is XenonAcrylic Then

                With TryCast(ctrl, XenonAcrylic)

                    If .UseItAsTaskbar Then
                        If _Conditions.AppUnderlineOnly Then
                            Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "AppUnderline", .AppUnderline, ControlPaint.Light(Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color)), steps, delay)
                            .Invalidate()
                        ElseIf _Conditions.AppBackgroundOnly Then
                            Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "AppBackground", .AppBackground, Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color), steps, delay)
                            .Invalidate()
                        ElseIf _Conditions.StartColorOnly Then
                            Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "StartColor", .StartColor, Color.FromArgb(255, ColorEditorManager1.Color), steps, delay)
                            .Invalidate()
                        Else
                            If _Conditions.BackColor1 Then
                                .BackColor = Color.FromArgb(.BackColor.A, ColorEditorManager1.Color)
                            ElseIf _Conditions.BackColor2 Then
                                .BackColor2 = Color.FromArgb(.BackColor2.A, ColorEditorManager1.Color)
                            Else
                                Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "BackColor", .BackColor, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                            End If
                            .Invalidate()
                        End If
                        .Invalidate()

                    ElseIf .UseItAsStartMenu And _Conditions.StartSearchOnly Then
                        Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "SearchBoxAccent", .SearchBoxAccent, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                        .Invalidate()

                    ElseIf .UseItAsActionCenter And _Conditions.ActionCenterBtn Then
                        Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "ActionCenterButton_Normal", .ActionCenterButton_Normal, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                        .Invalidate()

                    ElseIf .UseItAsActionCenter And _Conditions.ActionCenterLink Then
                        Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "LinkColor", .LinkColor, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                        .Invalidate()
                    Else
                        If _Conditions.BackColor1 Then
                            .BackColor = Color.FromArgb(.BackColor.A, ColorEditorManager1.Color)
                        ElseIf _Conditions.BackColor2 Then
                            .BackColor2 = Color.FromArgb(.BackColor2.A, ColorEditorManager1.Color)
                        Else
                            Visual.FadeColor(TryCast(ctrl, XenonAcrylic), "BackColor", .BackColor, Color.FromArgb(.BackColor.A, ColorEditorManager1.Color), steps, delay)
                        End If

                        .Invalidate()
                    End If
                End With

            ElseIf TypeOf ctrl Is Label Then
                If _Conditions.RetroAppWorkspace Or _Conditions.RetroBackground Then
                    ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                Else
                    Visual.FadeColor(ctrl, "Forecolor", ctrl.ForeColor, Color.FromArgb(ctrl.ForeColor.A, ColorEditorManager1.Color), steps, delay)
                End If

            ElseIf TypeOf ctrl Is RetroWindow Then
                With TryCast(ctrl, RetroWindow)
                    If _Conditions.RetroWindowColor1 Then .Color1 = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroWindowColor2 Then .Color2 = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroWindowForeColor Then .ForeColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroWindowBorder Then .ColorBorder = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonShadow Then .ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonDkShadow Then .ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonHilight Then .ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonLight Then .ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonFace Then
                        If ctrl IsNot Win32UI.Menu Then
                            .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                        Else
                            .ButtonFace = Color.FromArgb(255, ColorEditorManager1.Color)
                        End If
                    End If
                    If _Conditions.RetroBackground Then .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    .Invalidate()
                End With

            ElseIf TypeOf ctrl Is RetroTextBox Then
                With TryCast(ctrl, RetroTextBox)

                    If _Conditions.RetroWindowForeColor Then .ForeColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonShadow Then .ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonDkShadow Then .ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonHilight Then .ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonLight Then .ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonFace Then .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroBackground Then .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)

                    .Invalidate()
                End With

            ElseIf TypeOf ctrl Is RetroButton Then
                With TryCast(ctrl, RetroButton)
                    If _Conditions.RetroButtonFace Then .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroWindowFrame Then .WindowFrame = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonText Then .ForeColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonShadow Then .ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonDkShadow Then .ButtonDkShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonHilight Then .ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color)
                    If _Conditions.RetroButtonLight Then .ButtonLight = Color.FromArgb(255, ColorEditorManager1.Color)
                    .Invalidate()
                End With

            ElseIf TypeOf ctrl Is RetroScrollBar Then
                With TryCast(ctrl, RetroScrollBar)
                    If _Conditions.RetroButtonHilight Then .ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color) Else .BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    .Invalidate()
                End With

            ElseIf TypeOf ctrl Is Panel Then

                If _Conditions.RetroHighlight17BitFixer And TypeOf ctrl Is RetroPanel Then
                    DirectCast(ctrl, RetroPanel).ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                Else
                    If Not TypeOf ctrl Is XenonGroupBox Then
                        If _Conditions.RetroAppWorkspace Or _Conditions.RetroBackground Then ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                        If TypeOf ctrl Is RetroPanel Then
                            If _Conditions.RetroButtonHilight Then DirectCast(ctrl, RetroPanel).ButtonHilight = Color.FromArgb(255, ColorEditorManager1.Color)
                            If _Conditions.RetroButtonShadow Then DirectCast(ctrl, RetroPanel).ButtonShadow = Color.FromArgb(255, ColorEditorManager1.Color)
                        End If
                        If TypeOf ctrl Is Panel Then
                            ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                        End If
                    End If
                End If

                If TypeOf ctrl Is XenonGroupBox Then
                    If DirectCast(ctrl, XenonGroupBox).CustomColor Then Visual.FadeColor(ctrl, "backcolor", ctrl.BackColor, Color.FromArgb(255, ColorEditorManager1.Color), steps, delay)
                End If
                ctrl.Invalidate()

            ElseIf TypeOf ctrl Is RetroTextBox Then
                With TryCast(ctrl, RetroTextBox)
                    If _Conditions.RetroWindowText Then
                        ctrl.ForeColor = Color.FromArgb(ctrl.ForeColor.A, ColorEditorManager1.Color)
                    Else
                        ctrl.BackColor = Color.FromArgb(ctrl.BackColor.A, ColorEditorManager1.Color)
                    End If
                    ctrl.Invalidate()
                End With

            ElseIf TypeOf ctrl Is CursorControl Then

                With DirectCast(ctrl, CursorControl)
                    If _Conditions.CursorBack1 Then
                        .Prop_PrimaryColor1 = ColorEditorManager1.Color
                    ElseIf _Conditions.CursorBack2 Then
                        .Prop_PrimaryColor2 = ColorEditorManager1.Color

                    ElseIf _Conditions.CursorLine1 Then
                        .Prop_SecondaryColor1 = ColorEditorManager1.Color
                    ElseIf _Conditions.CursorLine2 Then
                        .Prop_SecondaryColor2 = ColorEditorManager1.Color

                    ElseIf _Conditions.CursorCircle1 Then
                        .Prop_LoadingCircleBack1 = ColorEditorManager1.Color
                    ElseIf _Conditions.CursorCircle2 Then
                        .Prop_LoadingCircleBack2 = ColorEditorManager1.Color

                    ElseIf _Conditions.CursorCircleHot1 Then
                        .Prop_LoadingCircleHot1 = ColorEditorManager1.Color
                    ElseIf _Conditions.CursorCircleHot2 Then
                        .Prop_LoadingCircleHot2 = ColorEditorManager1.Color
                    End If

                    .Invalidate()
                End With

            Else
                Try
                    Visual.FadeColor(ctrl, "backcolor", ctrl.BackColor, Color.FromArgb(255, ColorEditorManager1.Color), steps, delay)
                Catch
                    Try
                        ctrl.BackColor = Color.FromArgb(255, ColorEditorManager1.Color)
                    Catch

                    End Try
                End Try
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
            Label4.Text = My.Application.LanguageHelper.Extracting
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
    ReadOnly ColorThiefX As New ColorThiefDotNet.ColorThief

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If img IsNot Nothing Then
            For Each C As ColorThiefDotNet.QuantizedColor In ColorThiefX.GetPalette(img, XenonNumericUpDown1.Value, XenonNumericUpDown2.Value, XenonCheckBox1.Checked)
                ColorsList.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B))
            Next

            XenonCore.SetCtrlTxt(My.Application.LanguageHelper.Sorting, Label4)

        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ImgPaletteContainer.Controls.Clear()

        For Each C As Color In ColorsList
            Dim pnl As New XenonGroupBox With {.Size = New Drawing.Size(30, 25), .CustomColor = True}
            pnl.BackColor = Color.FromArgb(255, C)
            ImgPaletteContainer.Controls.Add(pnl)
            AddHandler pnl.Click, AddressOf Pnl_click
        Next

        ProgressBar1.Visible = False
        ColorsList.Clear()
        My.Application.AnimatorX.ShowSync(ImgPaletteContainer, True)
        My.Application.AnimatorX.ShowSync(XenonButton6, True)
    End Sub

    Private Sub Pnl_click(ByVal sender As Object, ByVal e As EventArgs)
        With CType(sender, XenonGroupBox)
            ColorEditorManager1.Color = .BackColor
            ColorEditor1.Color = .BackColor
            ColorGrid1.Color = .BackColor
            ColorWheel1.Color = .BackColor
        End With

        CHANGECOLORPREVIEW()
    End Sub

    Public Shared fs As IO.FileStream

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs)
        ColorWheel1.Visible = False
        ColorGrid1.Visible = False
    End Sub

    Private Sub XenonButton4_Click_1(sender As Object, e As EventArgs) Handles XenonButton4.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub XenonButton1_Click_1(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            ColorGrid1.Colors = ColorCollection.LoadPalette(OpenFileDialog2.FileName)
        End If
    End Sub


    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If OpenThemeDialog.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenThemeDialog.FileName
        End If
    End Sub

    Private Sub XenonButton5_Click_2(sender As Object, e As EventArgs) Handles XenonButton5.Click
        If IO.File.Exists(XenonTextBox1.Text) Then
            ThemePaletteContainer.Controls.Clear()

            Try
                For Each C As Color In CP.GetPaletteFromMSTheme(XenonTextBox1.Text)
                    Dim pnl As New XenonGroupBox With {.Size = New Drawing.Size(30, 25), .CustomColor = True}
                    pnl.BackColor = Color.FromArgb(255, C)
                    ThemePaletteContainer.Controls.Add(pnl)
                    AddHandler pnl.Click, AddressOf Pnl_click
                Next
            Catch
                MsgBox(My.Application.LanguageHelper.InvalidTheme, MsgBoxStyle.Critical + My.Application.MsgboxRt)
            End Try

        Else
            MsgBox(My.Application.LanguageHelper.ThemeNotExist, MsgBoxStyle.Critical + My.Application.MsgboxRt)
        End If
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        ThemePaletteContainer.Controls.Clear()

        Try
            For Each C As Color In CP.GetPaletteFromString(My.Resources.RetroThemesDB, XenonComboBox1.SelectedItem)
                Dim pnl As New XenonGroupBox With {.Size = New Drawing.Size(30, 25), .CustomColor = True}
                pnl.BackColor = Color.FromArgb(255, C)
                ThemePaletteContainer.Controls.Add(pnl)
                AddHandler pnl.Click, AddressOf Pnl_click
            Next
        Catch

        End Try
    End Sub

    Private Sub ScreenColorPicker1_ColorChanged(sender As Object, e As EventArgs) Handles ScreenColorPicker1.ColorChanged

    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class

Public Class Conditions

    Public Sub New()

    End Sub

    Public Property Win7 As Boolean = False
    Public Property Color1 As Boolean = False
    Public Property Color2 As Boolean = False
    Public Property BackColor1 As Boolean = False
    Public Property BackColor2 As Boolean = False
    Public Property Window_InactiveTitlebar As Boolean = False
    Public Property Window_ActiveTitlebar As Boolean = True
    Public Property AppUnderlineOnly As Boolean = False
    Public Property AppBackgroundOnly As Boolean = False
    Public Property StartColorOnly As Boolean = False
    Public Property StartSearchOnly As Boolean = False
    Public Property ActionCenterBtn As Boolean = False
    Public Property ActionCenterLink As Boolean = False
    Public Property RetroWindowColor1 As Boolean = False
    Public Property RetroWindowColor2 As Boolean = False
    Public Property RetroWindowForeColor As Boolean = False
    Public Property RetroWindowBorder As Boolean = False
    Public Property RetroWindowFrame As Boolean = False
    Public Property RetroButtonFace As Boolean = False
    Public Property RetroButtonDkShadow As Boolean = False
    Public Property RetroButtonShadow As Boolean = False
    Public Property RetroButtonHilight As Boolean = False
    Public Property RetroButtonLight As Boolean = False
    Public Property RetroButtonText As Boolean = False
    Public Property RetroAppWorkspace As Boolean = False
    Public Property RetroBackground As Boolean = False
    Public Property RetroWindowText As Boolean = False
    Public Property RetroHighlight17BitFixer As Boolean = False
    Public Property CursorBack1 As Boolean = False
    Public Property CursorBack2 As Boolean = False
    Public Property CursorLine1 As Boolean = False
    Public Property CursorLine2 As Boolean = False
    Public Property CursorCircle1 As Boolean = False
    Public Property CursorCircle2 As Boolean = False
    Public Property CursorCircleHot1 As Boolean = False
    Public Property CursorCircleHot2 As Boolean = False

End Class

