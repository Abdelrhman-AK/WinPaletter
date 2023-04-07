Imports WinPaletter.CMD
Imports WinPaletter.XenonCore
Public Class Store_ConsolesPreview
    Public CP As CP

    Private Sub Store_ConsolesPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        ApplyCMDPreview(XenonCMD1, CP.CommandPrompt, False)
        ApplyCMDPreview(XenonCMD2, CP.PowerShellx86, True)
        ApplyCMDPreview(XenonCMD3, CP.PowerShellx64, True)
    End Sub

    Sub ApplyCMDPreview(XenonCMD As XenonCMD, [Console] As CP.Structures.Console, PS As Boolean)
        XenonCMD.CMD_ColorTable00 = [Console].ColorTable00
        XenonCMD.CMD_ColorTable01 = [Console].ColorTable01
        XenonCMD.CMD_ColorTable02 = [Console].ColorTable02
        XenonCMD.CMD_ColorTable03 = [Console].ColorTable03
        XenonCMD.CMD_ColorTable04 = [Console].ColorTable04
        XenonCMD.CMD_ColorTable05 = [Console].ColorTable05
        XenonCMD.CMD_ColorTable06 = [Console].ColorTable06
        XenonCMD.CMD_ColorTable07 = [Console].ColorTable07
        XenonCMD.CMD_ColorTable08 = [Console].ColorTable08
        XenonCMD.CMD_ColorTable09 = [Console].ColorTable09
        XenonCMD.CMD_ColorTable10 = [Console].ColorTable10
        XenonCMD.CMD_ColorTable11 = [Console].ColorTable11
        XenonCMD.CMD_ColorTable12 = [Console].ColorTable12
        XenonCMD.CMD_ColorTable13 = [Console].ColorTable13
        XenonCMD.CMD_ColorTable14 = [Console].ColorTable14
        XenonCMD.CMD_ColorTable15 = [Console].ColorTable15
        XenonCMD.CMD_PopupForeground = [Console].PopupForeground
        XenonCMD.CMD_PopupBackground = [Console].PopupBackground
        XenonCMD.CMD_ScreenColorsForeground = [Console].ScreenColorsForeground
        XenonCMD.CMD_ScreenColorsBackground = [Console].ScreenColorsBackground

        If Not [Console].FontRaster Then
            With Font.FromLogFont(New LogFont With {.lfFaceName = [Console].FaceName, .lfWeight = [Console].FontWeight})
                XenonCMD.Font = New Font(.FontFamily, CInt([Console].FontSize / 65536), .Style)
            End With
        End If

        XenonCMD.PowerShell = PS
        XenonCMD.Raster = [Console].FontRaster
        Select Case [Console].FontSize
            Case 393220
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._4x6

            Case 524294
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._6x8


            Case 524296
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._8x8

            Case 524304
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._16x8

            Case 786437
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._5x12

            Case 786439
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._7x12

            Case 0
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._8x12

            Case 786448
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._16x12

            Case 1048588
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._12x16

            Case 1179658
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._10x18

            Case Else
                XenonCMD.RasterSize = XenonCMD.Raster_Sizes._8x12

        End Select

        XenonCMD.Refresh()
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Close()
    End Sub
End Class