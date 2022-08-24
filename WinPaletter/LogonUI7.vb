Imports System.IO
Imports WinPaletter.XenonCore
Public Class LogonUI7
    Private _Shown As Boolean = False
    Dim imageres As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\imageres.dll"
    Dim b As Bitmap

    Private Sub LogonUI7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        _Shown = False
        LoadFromCP(MainFrm.CP)
        'ApplyPreview()
    End Sub

    Private Sub LogonUI7_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        _Shown = True
    End Sub

    Sub LoadFromCP(CP As CP)
        XenonToggle1.Checked = CP.LogonUI7_Enabled

        Select Case CP.LogonUI7_Mode
            Case CP.LogonUI7_Modes.Default_
                XenonRadioButton1.Checked = True

            Case CP.LogonUI7_Modes.Wallpaper
                XenonRadioButton2.Checked = True

            Case CP.LogonUI7_Modes.CustomImage
                XenonRadioButton4.Checked = True

            Case CP.LogonUI7_Modes.SolidColor
                XenonRadioButton3.Checked = True

        End Select

        XenonTextBox1.Text = CP.LogonUI7_ImagePath
        color_pick.BackColor = CP.LogonUI7_Color

        XenonCheckBox8.Checked = CP.LogonUI7_Effect_Grayscale
        XenonCheckBox7.Checked = CP.LogonUI7_Effect_Blur
        XenonCheckBox6.Checked = CP.LogonUI7_Effect_Noise

        XenonNumericUpDown1.Value = CP.LogonUI7_Effect_Blur_Intensity
        XenonNumericUpDown2.Value = CP.LogonUI7_Effect_Noise_Intensity

        Select Case CP.LogonUI7_Effect_Noise_Mode
            Case CP.LogonUI7_NoiseMode.Acrylic
                XenonComboBox1.SelectedIndex = 0

            Case CP.LogonUI7_NoiseMode.Aero
                XenonComboBox1.SelectedIndex = 1

        End Select

    End Sub

    Sub ApplyPreview()
        Cursor = Cursors.AppStarting

        If XenonRadioButton1.Checked Then b = LoadFromDLL(imageres, 5038)
        If XenonRadioButton2.Checked Then b = My.Application.GetCurrentWallpaper
        If XenonRadioButton3.Checked Then b = ColorToBitmap(color_pick.BackColor, My.Computer.Screen.Bounds.Size)

        If XenonRadioButton4.Checked Then
            If IO.File.Exists(XenonTextBox1.Text) Then
                b = Image.FromStream(New FileStream(XenonTextBox1.Text, IO.FileMode.Open, IO.FileAccess.Read))
            Else
                b = ColorToBitmap(Color.Black, My.Computer.Screen.Bounds.Size)
            End If
        End If

        If XenonCheckBox8.Checked Then b = Grayscale(b)
        If XenonCheckBox7.Checked Then b = BlurBitmap(b, XenonNumericUpDown1.Value)

        If XenonCheckBox6.Checked Then
            Select Case XenonComboBox1.SelectedIndex
                Case 0
                    b = NoiseBitmap(b, CP.LogonUI7_NoiseMode.Acrylic, XenonNumericUpDown2.Value / 100)
                Case 1
                    b = NoiseBitmap(b, CP.LogonUI7_NoiseMode.Aero, XenonNumericUpDown2.Value / 100)
            End Select
        End If

        b = BitmapFillScaler(b, My.Computer.Screen.Bounds.Size)
        pnl_preview.BackgroundImage = b

        Cursor = Cursors.Default
    End Sub

    Private Sub XenonRadioButton1_CheckedChanged(sender As Object) Handles XenonRadioButton1.CheckedChanged, XenonRadioButton2.CheckedChanged,
                                                                           XenonRadioButton3.CheckedChanged, XenonRadioButton4.CheckedChanged,
                                                                           XenonCheckBox8.CheckedChanged, XenonCheckBox7.CheckedChanged, XenonCheckBox6.CheckedChanged

        If _Shown And sender.checked Then ApplyPreview()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click

    End Sub
End Class