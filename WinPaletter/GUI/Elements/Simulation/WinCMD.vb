Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

Namespace UI.Simulation

    <Description("A simulated Windows Command Prompt/PS")>
    <DefaultEvent("Click")> Public Class WinCMD : Inherits ContainerControl
        Public Property CMD_ColorTable00 As Color
        Public Property CMD_ColorTable01 As Color
        Public Property CMD_ColorTable02 As Color
        Public Property CMD_ColorTable03 As Color
        Public Property CMD_ColorTable04 As Color
        Public Property CMD_ColorTable05 As Color
        Public Property CMD_ColorTable06 As Color
        Public Property CMD_ColorTable07 As Color
        Public Property CMD_ColorTable08 As Color
        Public Property CMD_ColorTable09 As Color
        Public Property CMD_ColorTable10 As Color
        Public Property CMD_ColorTable11 As Color
        Public Property CMD_ColorTable12 As Color
        Public Property CMD_ColorTable13 As Color
        Public Property CMD_ColorTable14 As Color
        Public Property CMD_ColorTable15 As Color
        Public Property CMD_ScreenColorsForeground As Integer = 7
        Public Property CMD_ScreenColorsBackground As Integer = 0
        Public Property CMD_PopupForeground As Integer = 15
        Public Property CMD_PopupBackground As Integer = 5
        Public Property PowerShell As Boolean = False
        Public Property Raster As Boolean = True
        Public Property RasterSize As Raster_Sizes = Raster_Sizes._8x12

        Public Property CustomTerminal As Boolean = False

        ReadOnly S1 As String = "(c) Microsoft Corporation. All rights reserved."
        ReadOnly S2 As String = My.PATH_System32 & ">"
        ReadOnly CV As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion"

        Enum Raster_Sizes
            _4x6
            _6x8
            _8x8
            _16x8
            _5x12
            _7x12
            _8x12
            _16x12
            _12x16
            _10x18
        End Enum

        Sub New()
            Text = ""
            DoubleBuffered = True
        End Sub

        Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
            Dim G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.AntiAlias
            G.TextRenderingHint = If(DesignMode, TextRenderingHint.ClearTypeGridFit, TextRenderingHint.SystemDefault)

            DoubleBuffered = True

            Dim Rect As New Rectangle(0, 0, Width - 1, Height - 1)
            Dim RectCMD As New Rectangle(Rect.X + 1, Rect.Y + 5, Rect.Width - 2, Rect.Height - 10)

            Dim pW0, pH0, pX0, pY0 As Integer
            pW0 = 240 * (Font.Size / 18)
            pH0 = 54 * (Font.Size / 18)
            pX0 = 5 * (Font.Size / 18)
            pY0 = 10 * (Font.Size / 18)

            Dim RectMiddle As New Rectangle(Rect.X + (Rect.Width - pW0) / 2, Rect.Y + (Rect.Height - pH0) / 2, pW0, pH0)
            Dim RectMiddleBorder As New Rectangle(RectMiddle.X + pX0, RectMiddle.Y + pY0, RectMiddle.Width - pX0 * 2, RectMiddle.Height - pY0 * 2)

            Dim FC, BK, PCF, PCB As Color
            Dim S As String

            Dim F As Font = Font

            If Not Raster Then
                If Not PowerShell Then
                    F = New Font(Font.Name, If(Font.SizeInPoints * 0.57 <= 0, 1, CSng(Font.Size * 0.57)), Font.Style)
                Else
                    F = New Font(Font.Name, If(Font.SizeInPoints * 0.57 <= 0, 1, CSng(Font.Size * 0.57)), Font.Style)
                End If
            End If

            Select Case CMD_ScreenColorsForeground
                Case 0
                    If CMD_ScreenColorsForeground = 0 And CMD_ScreenColorsBackground = 0 Then
                        FC = CMD_ColorTable07
                    Else
                        FC = CMD_ColorTable00
                    End If
                Case 1
                    FC = CMD_ColorTable01
                Case 2
                    FC = CMD_ColorTable02
                Case 3
                    FC = CMD_ColorTable03
                Case 4
                    FC = CMD_ColorTable04
                Case 5
                    FC = CMD_ColorTable05
                Case 6
                    FC = CMD_ColorTable06
                Case 7
                    FC = CMD_ColorTable07
                Case 8
                    FC = CMD_ColorTable08
                Case 9
                    FC = CMD_ColorTable09
                Case 10
                    FC = CMD_ColorTable10
                Case 11
                    FC = CMD_ColorTable11
                Case 12
                    FC = CMD_ColorTable12
                Case 13
                    FC = CMD_ColorTable13
                Case 14
                    FC = CMD_ColorTable14
                Case 15
                    FC = CMD_ColorTable15
            End Select

            Select Case CMD_ScreenColorsBackground
                Case 0
                    BK = CMD_ColorTable00
                Case 1
                    BK = CMD_ColorTable01
                Case 2
                    BK = CMD_ColorTable02
                Case 3
                    BK = CMD_ColorTable03
                Case 4
                    BK = CMD_ColorTable04
                Case 5
                    BK = CMD_ColorTable05
                Case 6
                    BK = CMD_ColorTable06
                Case 7
                    BK = CMD_ColorTable07
                Case 8
                    BK = CMD_ColorTable08
                Case 9
                    BK = CMD_ColorTable09
                Case 10
                    BK = CMD_ColorTable10
                Case 11
                    BK = CMD_ColorTable11
                Case 12
                    BK = CMD_ColorTable12
                Case 13
                    BK = CMD_ColorTable13
                Case 14
                    BK = CMD_ColorTable14
                Case 15
                    BK = CMD_ColorTable15
            End Select

            Select Case CMD_PopupForeground
                Case 0
                    PCF = CMD_ColorTable00
                Case 1
                    PCF = CMD_ColorTable01
                Case 2
                    PCF = CMD_ColorTable02
                Case 3
                    PCF = CMD_ColorTable03
                Case 4
                    PCF = CMD_ColorTable04
                Case 5
                    PCF = CMD_ColorTable05
                Case 6
                    PCF = CMD_ColorTable06
                Case 7
                    PCF = CMD_ColorTable07
                Case 8
                    PCF = CMD_ColorTable08
                Case 9
                    PCF = CMD_ColorTable09
                Case 10
                    PCF = CMD_ColorTable10
                Case 11
                    PCF = CMD_ColorTable11
                Case 12
                    PCF = CMD_ColorTable12
                Case 13
                    PCF = CMD_ColorTable13
                Case 14
                    PCF = CMD_ColorTable14
                Case 15
                    PCF = CMD_ColorTable15
            End Select

            Select Case CMD_PopupBackground
                Case 0
                    PCB = CMD_ColorTable00
                Case 1
                    PCB = CMD_ColorTable01
                Case 2
                    PCB = CMD_ColorTable02
                Case 3
                    PCB = CMD_ColorTable03
                Case 4
                    PCB = CMD_ColorTable04
                Case 5
                    PCB = CMD_ColorTable05
                Case 6
                    PCB = CMD_ColorTable06
                Case 7
                    PCB = CMD_ColorTable07
                Case 8
                    PCB = CMD_ColorTable08
                Case 9
                    PCB = CMD_ColorTable09
                Case 10
                    PCB = CMD_ColorTable10
                Case 11
                    PCB = CMD_ColorTable11
                Case 12
                    PCB = CMD_ColorTable12
                Case 13
                    PCB = CMD_ColorTable13
                Case 14
                    PCB = CMD_ColorTable14
                Case 15
                    PCB = CMD_ColorTable15
            End Select

            BackColor = BK
            G.Clear(BK)

            If Not CustomTerminal Then
                If Not PowerShell Then
                    Dim sx As String = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Replace("Microsoft Windows ", "")
                    sx = sx.Replace("S", "").Trim

                    Dim sy As String = "." & Microsoft.Win32.Registry.GetValue(CV, "UBR", 0).ToString
                    If sy = ".0" Then sy = ""

                    S = String.Format("Microsoft Windows [Version {0}{1}]", sx, sy) & vbCrLf & S1 & vbCrLf & vbCrLf & S2

                Else
                    S = "Windows PowerShell" & vbCrLf & S1 & vbCrLf & vbCrLf & "Install the latest PowerShell for new features and improvements! https://aka.ms/PSWindows" & vbCrLf & vbCrLf & "PS " & S2
                End If
            Else
                S = "This is just an preview to your custom terminal." & vbCrLf & vbCrLf & S2
            End If


            If Raster Then
                S &= vbCrLf & vbCrLf & "*Note: Raster Font will look different from the preview."
            End If

            If Not Raster Then
                Using br As New SolidBrush(FC) : G.DrawString(S, F, br, RectCMD.Location) : End Using

                Using br As New SolidBrush(PCB) : G.FillRectangle(br, RectMiddle) : End Using
                Using P As New Pen(PCF) : G.DrawRectangle(P, RectMiddleBorder) : End Using

                Using br As New SolidBrush(PCF) : G.DrawString("This is a pop-up", F, br, RectMiddleBorder, ContentAlignment.MiddleCenter.ToStringFormat) : End Using

            Else
                Dim i0, i1 As Bitmap
                Dim pW, pH, pX, pY As Integer

                Select Case RasterSize
                    Case Raster_Sizes._4x6
                        If Not PowerShell Then i0 = My.Resources.CMD_4x6 Else i0 = My.Resources.PS_4x6
                        i1 = My.Resources.CMD_4x6_P
                        pW = 120
                        pH = 18
                        pX = 2
                        pY = 3

                    Case Raster_Sizes._6x8
                        If Not PowerShell Then i0 = My.Resources.CMD_6x8 Else i0 = My.Resources.PS_6x8
                        i1 = My.Resources.CMD_6x8_P
                        pW = 180
                        pH = 24
                        pX = 3
                        pY = 4

                    Case Raster_Sizes._8x8
                        If Not PowerShell Then i0 = My.Resources.CMD_8x8 Else i0 = My.Resources.PS_8x8
                        i1 = My.Resources.CMD_8x8_P
                        pW = 240
                        pH = 24
                        pX = 4
                        pY = 4

                    Case Raster_Sizes._16x8
                        If Not PowerShell Then i0 = My.Resources.CMD_16x8 Else i0 = My.Resources.PS_16x8
                        i1 = My.Resources.CMD_16x8_P
                        pW = 480
                        pH = 24
                        pX = 8
                        pY = 4

                    Case Raster_Sizes._5x12
                        If Not PowerShell Then i0 = My.Resources.CMD_5x12 Else i0 = My.Resources.PS_5x12
                        i1 = My.Resources.CMD_5x12_P
                        pW = 150
                        pH = 36
                        pX = 3
                        pY = 6

                    Case Raster_Sizes._7x12
                        If Not PowerShell Then i0 = My.Resources.CMD_7x12 Else i0 = My.Resources.PS_7x12
                        i1 = My.Resources.CMD_7x12_P
                        pW = 210
                        pH = 36
                        pX = 4
                        pY = 6

                    Case Raster_Sizes._8x12
                        If Not PowerShell Then i0 = My.Resources.CMD_8x12 Else i0 = My.Resources.PS_8x12
                        i1 = My.Resources.CMD_8x12_P
                        pW = 240
                        pH = 36
                        pX = 4
                        pY = 6

                    Case Raster_Sizes._16x12
                        If Not PowerShell Then i0 = My.Resources.CMD_16x12 Else i0 = My.Resources.PS_16x12
                        i1 = My.Resources.CMD_16x12_P
                        pW = 480
                        pH = 36
                        pX = 8
                        pY = 6

                    Case Raster_Sizes._12x16
                        If Not PowerShell Then i0 = My.Resources.CMD_12x16 Else i0 = My.Resources.PS_12x16
                        i1 = My.Resources.CMD_12x16_P
                        pW = 360
                        pH = 48
                        pX = 6
                        pY = 8

                    Case Raster_Sizes._10x18
                        If Not PowerShell Then i0 = My.Resources.CMD_10x18 Else i0 = My.Resources.PS_10x18
                        i1 = My.Resources.CMD_10x18_P
                        pW = 300
                        pH = 54
                        pX = 8
                        pY = 9

                    Case Else
                        If Not PowerShell Then i0 = My.Resources.CMD_8x12 Else i0 = My.Resources.PS_8x12
                        i1 = My.Resources.CMD_8x12_P
                        pW = 240
                        pH = 36
                        pX = 4
                        pY = 6

                End Select

                G.DrawImage(i0.ReplaceColor(Color.FromArgb(204, 204, 204), FC), New Point(0, 1))

                RectMiddle = New Rectangle(Rect.X + (Rect.Width - pW) / 2, Rect.Y + (Rect.Height - 36) / 2, pW, pH)
                RectMiddleBorder = New Rectangle(RectMiddle.X + pX, RectMiddle.Y + pY, RectMiddle.Width - pX * 2, RectMiddle.Height - pY * 2)

                Using br As New SolidBrush(PCB) : G.FillRectangle(br, RectMiddle) : End Using
                Using P As New Pen(PCF) : G.DrawRectangle(P, RectMiddleBorder) : End Using


                G.DrawImage(i1.ReplaceColor(Color.FromArgb(204, 204, 204), PCF), New Point(RectMiddle.X + (RectMiddle.Width - i1.Width) / 2, RectMiddle.Y + (RectMiddle.Height - i1.Height) / 2))

            End If
        End Sub
    End Class

End Namespace