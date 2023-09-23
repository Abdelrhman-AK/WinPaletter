﻿Public Class Start8Selector
    Private Sub Start8Selector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Icon = MainFrm.Icon

        LoadLanguage
        ApplyStyle(Me)
        LoadImagesFromDLL()

        Select Case My.CP.Windows81.Start
            Case 1
                img1.Checked = True
            Case 2
                img2.Checked = True
            Case 3
                img3.Checked = True
            Case 4
                img4.Checked = True
            Case 5
                img5.Checked = True
            Case 6
                img6.Checked = True
            Case 7
                img7.Checked = True
            Case 8
                img8.Checked = True
            Case 9
                img9.Checked = True
            Case 10
                img10.Checked = True
            Case 11
                img11.Checked = True
            Case 12
                img12.Checked = True
            Case 13
                img13.Checked = True
            Case 14
                img14.Checked = True
            Case 15
                img15.Checked = True
            Case 16
                img16.Checked = True
            Case 17
                img17.Checked = True
            Case 18
                img18.Checked = True
            Case 19
                img19.Checked = True
            Case 20
                img20.Checked = True
            Case Else
                img1.Checked = True
        End Select
    End Sub

    Sub LoadImagesFromDLL()
        img19.Image = My.CP.Windows81.PersonalColors_Background.ToBitmap(New Size(64, 64))
        img20.Image = My.Wallpaper.Resize(64, 64)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If img1.Checked Then My.CP.Windows81.Start = 1
        If img2.Checked Then My.CP.Windows81.Start = 2
        If img3.Checked Then My.CP.Windows81.Start = 3
        If img4.Checked Then My.CP.Windows81.Start = 4
        If img5.Checked Then My.CP.Windows81.Start = 5
        If img6.Checked Then My.CP.Windows81.Start = 6
        If img7.Checked Then My.CP.Windows81.Start = 7
        If img8.Checked Then My.CP.Windows81.Start = 8
        If img9.Checked Then My.CP.Windows81.Start = 9
        If img10.Checked Then My.CP.Windows81.Start = 10
        If img11.Checked Then My.CP.Windows81.Start = 11
        If img12.Checked Then My.CP.Windows81.Start = 12
        If img13.Checked Then My.CP.Windows81.Start = 13
        If img14.Checked Then My.CP.Windows81.Start = 14
        If img15.Checked Then My.CP.Windows81.Start = 15
        If img16.Checked Then My.CP.Windows81.Start = 16
        If img17.Checked Then My.CP.Windows81.Start = 17
        If img18.Checked Then My.CP.Windows81.Start = 18
        If img19.Checked Then My.CP.Windows81.Start = 19
        If img20.Checked Then My.CP.Windows81.Start = 20
        Me.Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub
End Class