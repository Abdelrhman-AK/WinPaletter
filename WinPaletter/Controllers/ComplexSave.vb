Imports WinPaletter.XenonCore

Public Class ComplexSave
    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub ComplexSave_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim i1 As Integer = 0
        Dim i2 As Integer = 0

        If XenonRadioImage1.Checked Then i1 = 0
        If XenonRadioImage2.Checked Then i1 = 1
        If XenonRadioImage3.Checked Then i1 = 2

        If XenonRadioImage6.Checked Then i2 = 1
        If XenonRadioImage5.Checked Then i2 = 2
        If XenonRadioImage7.Checked Then i2 = 3
        If XenonRadioImage4.Checked Then i2 = 0

        My.Settings.General.ComplexSaveResult = i1 & "." & i2
        My.Settings.ThemeApplyingBehavior.ShowSaveConfirmation = XenonCheckBox2.Checked
        My.Settings.General.Save()
        My.Settings.ThemeApplyingBehavior.Save()
    End Sub

    Private Sub ComplexSave_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLanguage
        ApplyDarkMode(Me)

        Dim c As Color = PictureBox1.Image.AverageColor

        XenonAnimatedBox1.Color = c
        XenonAnimatedBox1.Color1 = c
        XenonAnimatedBox1.Color2 = c

        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)

        Dim r As String() = My.Settings.General.ComplexSaveResult.Split(".")
        Dim r1 As String = r(0)
        Dim r2 As String = r(1)

        If My.W11 Then
            XenonRadioImage7.Image = My.Resources.Native11.Resize(20, 20)

        ElseIf My.W10 Then
            XenonRadioImage7.Image = My.Resources.Native10.Resize(20, 20)

        ElseIf My.W8 Then
            XenonRadioImage7.Image = My.Resources.Native8.Resize(20, 20)

        ElseIf My.W7 Then
            XenonRadioImage7.Image = My.Resources.Native7.Resize(20, 20)

        ElseIf My.WVista Then
            XenonRadioImage7.Image = My.Resources.NativeVista.Resize(20, 20)

        ElseIf My.WXP Then
            XenonRadioImage7.Image = My.Resources.NativeXP.Resize(20, 20)

        Else
            XenonRadioImage7.Image = My.Resources.Native11.Resize(20, 20)
        End If


        If r1 = 0 Then
            XenonRadioImage1.Checked = True
        ElseIf r1 = 1 Then
            XenonRadioImage2.Checked = True
        ElseIf r1 = 2 Then
            XenonRadioImage3.Checked = True
        Else
            XenonRadioImage3.Checked = True
        End If

        If r2 = 0 Then
            XenonRadioImage4.Checked = True
        ElseIf r2 = 1 Then
            XenonRadioImage6.Checked = True
        ElseIf r2 = 2 Then
            XenonRadioImage5.Checked = True
        ElseIf r2 = 3 Then
            XenonRadioImage7.Checked = True
        Else
            XenonRadioImage6.Checked = True
        End If

        XenonCheckBox2.Checked = My.Settings.ThemeApplyingBehavior.ShowSaveConfirmation

        Me.DialogResult = DialogResult.None
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        My.Settings.ThemeApplyingBehavior.ShowSaveConfirmation = XenonCheckBox2.Checked
        My.Settings.ThemeApplyingBehavior.Save()
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Public Function GetResponse(SaveFileDialog As System.Windows.Forms.SaveFileDialog, Apply_Theme_Sub As MethodInvoker, Apply_FirstTheme_Sub As MethodInvoker, Apply_DefaultWin_Sub As MethodInvoker) As Boolean
        If My.CP <> My.CP_Original AndAlso My.Settings.ThemeApplyingBehavior.ShowSaveConfirmation Then
            XenonGroupBox2.Enabled = Apply_Theme_Sub IsNot Nothing Or Apply_FirstTheme_Sub IsNot Nothing Or Apply_DefaultWin_Sub IsNot Nothing

            Select Case ShowDialog()
                Case DialogResult.Yes

                    Dim r As String() = My.Settings.General.ComplexSaveResult.Split(".")
                    Dim r1 As String = r(0)
                    Dim r2 As String = r(1)

                    Select Case r1
                        Case 0              '' Save
                            If IO.File.Exists(SaveFileDialog.FileName) Then
                                My.CP.Save(CP.CP_Type.File, SaveFileDialog.FileName)
                                My.CP_Original = My.CP.Clone
                            Else
                                If SaveFileDialog.ShowDialog = DialogResult.OK Then
                                    My.CP.Save(CP.CP_Type.File, SaveFileDialog.FileName)
                                    My.CP_Original = My.CP.Clone
                                Else
                                    Return False
                                End If
                            End If
                        Case 1              '' Save As
                            If SaveFileDialog.ShowDialog = DialogResult.OK Then
                                My.CP.Save(CP.CP_Type.File, SaveFileDialog.FileName)
                                My.CP_Original = My.CP.Clone
                            Else
                                Return False
                            End If
                    End Select

                    Select Case r2
                        Case 1
                            If Apply_Theme_Sub IsNot Nothing Then Apply_Theme_Sub()

                        Case 2
                            If Apply_FirstTheme_Sub IsNot Nothing Then Apply_FirstTheme_Sub()

                        Case 3
                            If Apply_DefaultWin_Sub IsNot Nothing Then Apply_DefaultWin_Sub()

                    End Select

                Case DialogResult.No

                Case DialogResult.Cancel
                    Return False
            End Select

            Return True
        Else
            Return False
        End If

    End Function
End Class