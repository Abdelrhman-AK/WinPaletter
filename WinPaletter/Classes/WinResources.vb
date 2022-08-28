Imports WinPaletter.XenonCore

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

    Private imageres As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\system32\imageres.dll"

    Sub New()
        MetroStart_1 = LoadFromDLL(imageres, 22001, "PNG")
        MetroStart_2 = LoadFromDLL(imageres, 21601, "PNG")
        MetroStart_3 = LoadFromDLL(imageres, 22101, "PNG")
        MetroStart_4 = LoadFromDLL(imageres, 21901, "PNG")
        MetroStart_5 = LoadFromDLL(imageres, 21501, "PNG")
        MetroStart_6 = LoadFromDLL(imageres, 21701, "PNG")
        MetroStart_7 = LoadFromDLL(imageres, 21301, "PNG")
        MetroStart_8 = LoadFromDLL(imageres, 20701, "PNG")
        MetroStart_9 = LoadFromDLL(imageres, 21201, "PNG")
        MetroStart_10 = LoadFromDLL(imageres, 20501, "PNG")
        MetroStart_11 = LoadFromDLL(imageres, 21001, "PNG")
        MetroStart_12 = LoadFromDLL(imageres, 20801, "PNG")
        MetroStart_13 = LoadFromDLL(imageres, 20601, "PNG")
        MetroStart_14 = LoadFromDLL(imageres, 20401, "PNG")
        MetroStart_15 = LoadFromDLL(imageres, 21401, "PNG")
        MetroStart_16 = LoadFromDLL(imageres, 21101, "PNG")
        MetroStart_17 = LoadFromDLL(imageres, 20301, "PNG")
        MetroStart_18 = LoadFromDLL(imageres, 20201, "PNG")
    End Sub
End Class
