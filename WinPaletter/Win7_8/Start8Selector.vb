Imports System.Runtime.InteropServices
Imports WinPaletter.XenonCore
Public Class Start8Selector
    Dim imageres As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\imageres.dll"

    Private Sub Start8Selector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        LoadImagesFromDLL()

        Select Case MainFrm.CP.Windows8.Start
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
        img1.Image = ResizeImage(My.Application.WinRes.MetroStart_1, 64, 64)
        img2.Image = ResizeImage(My.Application.WinRes.MetroStart_2, 64, 64)
        img3.Image = ResizeImage(My.Application.WinRes.MetroStart_3, 64, 64)
        img4.Image = ResizeImage(My.Application.WinRes.MetroStart_4, 64, 64)
        img5.Image = ResizeImage(My.Application.WinRes.MetroStart_5, 64, 64)
        img6.Image = ResizeImage(My.Application.WinRes.MetroStart_6, 64, 64)
        img7.Image = ResizeImage(My.Application.WinRes.MetroStart_7, 64, 64)
        img8.Image = ResizeImage(My.Application.WinRes.MetroStart_8, 64, 64)
        img9.Image = ResizeImage(My.Application.WinRes.MetroStart_9, 64, 64)
        img10.Image = ResizeImage(My.Application.WinRes.MetroStart_10, 64, 64)
        img11.Image = ResizeImage(My.Application.WinRes.MetroStart_11, 64, 64)
        img12.Image = ResizeImage(My.Application.WinRes.MetroStart_12, 64, 64)
        img13.Image = ResizeImage(My.Application.WinRes.MetroStart_13, 64, 64)
        img14.Image = ResizeImage(My.Application.WinRes.MetroStart_14, 64, 64)
        img15.Image = ResizeImage(My.Application.WinRes.MetroStart_15, 64, 64)
        img16.Image = ResizeImage(My.Application.WinRes.MetroStart_16, 64, 64)
        img17.Image = ResizeImage(My.Application.WinRes.MetroStart_17, 64, 64)
        img18.Image = ResizeImage(My.Application.WinRes.MetroStart_18, 64, 64)
        img19.Image = ColorToBitmap(MainFrm.CP.Windows8.PersonalColors_Background, New Size(64, 64))
        img20.Image = ResizeImage(My.Application.GetCurrentWallpaper, 64, 64)
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        If img1.Checked Then MainFrm.CP.Windows8.Start = 1
        If img2.Checked Then MainFrm.CP.Windows8.Start = 2
        If img3.Checked Then MainFrm.CP.Windows8.Start = 3
        If img4.Checked Then MainFrm.CP.Windows8.Start = 4
        If img5.Checked Then MainFrm.CP.Windows8.Start = 5
        If img6.Checked Then MainFrm.CP.Windows8.Start = 6
        If img7.Checked Then MainFrm.CP.Windows8.Start = 7
        If img8.Checked Then MainFrm.CP.Windows8.Start = 8
        If img9.Checked Then MainFrm.CP.Windows8.Start = 9
        If img10.Checked Then MainFrm.CP.Windows8.Start = 10
        If img11.Checked Then MainFrm.CP.Windows8.Start = 11
        If img12.Checked Then MainFrm.CP.Windows8.Start = 12
        If img13.Checked Then MainFrm.CP.Windows8.Start = 13
        If img14.Checked Then MainFrm.CP.Windows8.Start = 14
        If img15.Checked Then MainFrm.CP.Windows8.Start = 15
        If img16.Checked Then MainFrm.CP.Windows8.Start = 16
        If img17.Checked Then MainFrm.CP.Windows8.Start = 17
        If img18.Checked Then MainFrm.CP.Windows8.Start = 18
        If img19.Checked Then MainFrm.CP.Windows8.Start = 19
        If img20.Checked Then MainFrm.CP.Windows8.Start = 20
        Me.Close()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub
End Class