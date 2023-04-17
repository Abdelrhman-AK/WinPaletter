Public Class WinResources
    Public Property MetroStart_1 As Bitmap
    Public Property MetroStart_2 As Bitmap
    Public Property MetroStart_3 As Bitmap
    Public Property MetroStart_4 As Bitmap
    Public Property MetroStart_5 As Bitmap
    Public Property MetroStart_6 As Bitmap
    Public Property MetroStart_7 As Bitmap
    Public Property MetroStart_8 As Bitmap
    Public Property MetroStart_9 As Bitmap
    Public Property MetroStart_10 As Bitmap
    Public Property MetroStart_11 As Bitmap
    Public Property MetroStart_12 As Bitmap
    Public Property MetroStart_13 As Bitmap
    Public Property MetroStart_14 As Bitmap
    Public Property MetroStart_15 As Bitmap
    Public Property MetroStart_16 As Bitmap
    Public Property MetroStart_17 As Bitmap
    Public Property MetroStart_18 As Bitmap

    Public Property MetroLock_0 As Bitmap
    Public Property MetroLock_1 As Bitmap
    Public Property MetroLock_2 As Bitmap
    Public Property MetroLock_3 As Bitmap
    Public Property MetroLock_4 As Bitmap
    Public Property MetroLock_5 As Bitmap

    Sub New()
        If My.W8 Then
            MetroStart_1 = GetImageFromDLL(My.PATH_imageres, 22001, "PNG")
            MetroStart_2 = GetImageFromDLL(My.PATH_imageres, 21601, "PNG")
            MetroStart_3 = GetImageFromDLL(My.PATH_imageres, 22101, "PNG")
            MetroStart_4 = GetImageFromDLL(My.PATH_imageres, 21901, "PNG")
            MetroStart_5 = GetImageFromDLL(My.PATH_imageres, 21501, "PNG")
            MetroStart_6 = GetImageFromDLL(My.PATH_imageres, 21701, "PNG")
            MetroStart_7 = GetImageFromDLL(My.PATH_imageres, 21301, "PNG")
            MetroStart_8 = GetImageFromDLL(My.PATH_imageres, 20701, "PNG")
            MetroStart_9 = GetImageFromDLL(My.PATH_imageres, 21201, "PNG")
            MetroStart_10 = GetImageFromDLL(My.PATH_imageres, 20501, "PNG")
            MetroStart_11 = GetImageFromDLL(My.PATH_imageres, 21001, "PNG")
            MetroStart_12 = GetImageFromDLL(My.PATH_imageres, 20801, "PNG")
            MetroStart_13 = GetImageFromDLL(My.PATH_imageres, 20601, "PNG")
            MetroStart_14 = GetImageFromDLL(My.PATH_imageres, 20401, "PNG")
            MetroStart_15 = GetImageFromDLL(My.PATH_imageres, 21401, "PNG")
            MetroStart_16 = GetImageFromDLL(My.PATH_imageres, 21101, "PNG")
            MetroStart_17 = GetImageFromDLL(My.PATH_imageres, 20301, "PNG")
            MetroStart_18 = GetImageFromDLL(My.PATH_imageres, 20201, "PNG")
            MetroLock_0 = GetImageFromDLL(My.PATH_Windows_UI_Immersive_dll, 39169)
            MetroLock_1 = GetImageFromDLL(My.PATH_Windows_UI_Immersive_dll, 39172)
            MetroLock_2 = GetImageFromDLL(My.PATH_Windows_UI_Immersive_dll, 39175)
            MetroLock_3 = GetImageFromDLL(My.PATH_Windows_UI_Immersive_dll, 39178)
            MetroLock_4 = GetImageFromDLL(My.PATH_Windows_UI_Immersive_dll, 39181)
            MetroLock_5 = GetImageFromDLL(My.PATH_Windows_UI_Immersive_dll, 39184)
        Else

            MetroStart_1 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_2 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_3 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_4 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_5 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_6 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_7 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_8 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_9 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_10 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_11 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_12 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_13 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_14 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_15 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_16 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_17 = Color.Black.ToBitmap(New Size(50, 50))
            MetroStart_18 = Color.Black.ToBitmap(New Size(50, 50))
            MetroLock_0 = Color.Black.ToBitmap(New Size(50, 50))
            MetroLock_1 = Color.Black.ToBitmap(New Size(50, 50))
            MetroLock_2 = Color.Black.ToBitmap(New Size(50, 50))
            MetroLock_3 = Color.Black.ToBitmap(New Size(50, 50))
            MetroLock_4 = Color.Black.ToBitmap(New Size(50, 50))
            MetroLock_5 = Color.Black.ToBitmap(New Size(50, 50))
        End If
    End Sub
End Class
