Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.IO
Imports System.Net
Imports System.Runtime.InteropServices
Imports WinPaletter.CP

Public Class XenonCore

#Region "Backgroundworker Fixers"
    Public Shared Sub SetCtrlTxt(ByVal text As String, ByVal Ctrl As Control)
        Try
            If Ctrl.InvokeRequired Then
                Ctrl.Invoke(New setCtrlTxtInvoker(AddressOf SetCtrlTxt), text, Ctrl)
            Else
                Ctrl.Text = text
            End If
        Catch

        End Try
    End Sub
    Private Delegate Sub setCtrlTxtInvoker(ByVal text As String, ByVal Ctrl As Control)
#End Region

#Region "Misc"

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Shared Function SendMessageTimeout(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As UIntPtr, ByVal lParam As IntPtr, ByVal fuFlags As SendMessageTimeoutFlags, ByVal uTimeout As UInteger, <Out> ByRef lpdwResult As UIntPtr) As IntPtr
    End Function

    Shared WM_DWMCOLORIZATIONCOLORCHANGED As Integer = &H320
    Shared WM_DWMCOMPOSITIONCHANGED As Integer = &H31E
    Shared WM_THEMECHANGED As Integer = &H31A
    Shared WM_SYSCOLORCHANGE As Integer = &H15
    Shared WM_PALETTECHANGED As Integer = &H311
    Shared WM_WININICHANGE As UInteger = &H1A
    Shared WM_SETTINGCHANGE As UInteger = WM_WININICHANGE
    Shared HWND_MESSAGE As Int32 = -&H3
    Shared HWND_BROADCAST As IntPtr = New IntPtr(&HFFFF)
    Shared MSG_TIMEOUT As Integer = 5000
    Shared RESULT As UIntPtr

    <DllImport("dwmapi.dll", EntryPoint:="#131", PreserveSig:=False)>
    Private Shared Sub DwmSetColorizationParameters(ByRef parameters As DWM_COLORIZATION_PARAMS, ByVal unknown As Boolean)
    End Sub

    Private Structure DWM_COLORIZATION_PARAMS
        Public clrColor As UInteger
        Public clrAfterGlow As UInteger
        Public nIntensity As UInteger
        Public clrAfterGlowBalance As UInteger
        Public clrBlurBalance As UInteger
        Public clrGlassReflectionIntensity As UInteger
        Public fOpaque As Boolean
    End Structure

    Public Shared Sub RefreshDWM(CP As CP)
        Dim temp As New DWM_COLORIZATION_PARAMS
        temp.clrColor = CP.Aero_ColorizationColor.ToArgb
        temp.clrAfterGlow = CP.Aero_ColorizationAfterglow.ToArgb
        temp.nIntensity = CP.Aero_ColorizationColorBalance
        temp.clrAfterGlowBalance = CP.Aero_ColorizationAfterglowBalance
        temp.clrBlurBalance = CP.Aero_ColorizationBlurBalance
        temp.clrGlassReflectionIntensity = CP.Aero_ColorizationGlassReflectionIntensity
        temp.fOpaque = CP.Aero_Theme = AeroTheme.AeroOpaque
        DwmSetColorizationParameters(temp, False)
    End Sub

    Enum SendMessageTimeoutFlags As UInteger
        SMTO_NORMAL = &H0
        SMTO_BLOCK = &H1
        SMTO_ABORTIFHUNG = &H2
        SMTO_NOTIMEOUTIFNOTHUNG = &H8
    End Enum

    Declare Function SHChangeNotify Lib "Shell32.dll" (ByVal wEventID As Int32,
    ByVal uFlags As Int32, ByVal dwItem1 As Int32, ByVal dwItem2 As Int32) As Int32

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function LoadLibraryEx(ByVal lpFileName As String, ByVal hFile As IntPtr, ByVal dwFlags As UInteger) As IntPtr
    End Function
    <DllImport("Kernel32.dll", EntryPoint:="LockResource")>
    Private Shared Function LockResource(ByVal hGlobal As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll")>
    Private Shared Function FindResource(ByVal hModule As IntPtr, ByVal lpID As Integer, ByVal lpType As String) As IntPtr
    End Function
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function LoadResource(ByVal hModule As IntPtr, ByVal hResInfo As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function SizeofResource(ByVal hModule As IntPtr, ByVal hResInfo As IntPtr) As UInteger
    End Function
    Public Shared Sub RefreshRegisrty()
        Try : SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, 0, Marshal.PtrToStringAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        Try : SendMessageTimeout(HWND_BROADCAST, WM_SYSCOLORCHANGE, 0, Marshal.PtrToStringAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try

        'Try : SendMessageTimeout(HWND_BROADCAST, WM_DWMCOLORIZATIONCOLORCHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        'Try : SendMessageTimeout(HWND_BROADCAST, WM_DWMCOMPOSITIONCHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        'Try : SendMessageTimeout(HWND_BROADCAST, WM_THEMECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        'Try : SendMessageTimeout(HWND_BROADCAST, WM_SYSCOLORCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        'Try : SendMessageTimeout(HWND_BROADCAST, WM_PALETTECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        'Try : SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try

        'Try : SendMessageTimeout(HWND_BROADCAST, WM_DWMCOLORIZATIONCOLORCHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        'Try : SendMessageTimeout(HWND_BROADCAST, WM_DWMCOMPOSITIONCHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        'Try : SendMessageTimeout(HWND_BROADCAST, WM_THEMECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        'Try : SendMessageTimeout(HWND_BROADCAST, WM_SYSCOLORCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        'Try : SendMessageTimeout(HWND_BROADCAST, WM_PALETTECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
        'Try : SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    End Sub

    Public Shared Function BitmapFillScaler(ByVal Bitmap As Bitmap, Size As Size) As Bitmap
        Try
            Dim sourceWidth As Integer = Bitmap.Width
            Dim sourceHeight As Integer = Bitmap.Height
            Dim sourceX As Integer = 0
            Dim sourceY As Integer = 0
            Dim destX As Integer = 0
            Dim destY As Integer = 0
            Dim nPercent As Single = 0
            Dim nPercentW As Single = 0
            Dim nPercentH As Single = 0
            nPercentW = (CSng(Size.Width) / CSng(sourceWidth))
            nPercentH = (CSng(Size.Height) / CSng(sourceHeight))

            If nPercentH < nPercentW Then
                nPercent = nPercentH
                destX = System.Convert.ToInt16((Size.Width - (sourceWidth * nPercent)) / 2)
            Else
                nPercent = nPercentW
                destY = System.Convert.ToInt16((Size.Height - (sourceHeight * nPercent)) / 2)
            End If

            Dim destWidth As Integer = CInt((sourceWidth * nPercent))
            Dim destHeight As Integer = CInt((sourceHeight * nPercent))
            Dim bmPhoto As Bitmap = New Bitmap(Size.Width, Size.Height, PixelFormat.Format32bppArgb)
            bmPhoto.SetResolution(Bitmap.HorizontalResolution, Bitmap.VerticalResolution)
            Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic
            grPhoto.DrawImage(Bitmap, New Rectangle(0, 0, destWidth, destHeight))
            grPhoto.Dispose()
            Dim bm As Bitmap = bmPhoto.Clone(New Rectangle(0, 0, destWidth, destHeight), PixelFormat.Format32bppArgb)
            Dim f As Single

            If nPercentH < nPercentW Then
                f = Size.Width - bm.Width
                bm = ResizeImage(bm, Size.Width, Size.Height + f)
                bm = bm.Clone(New Rectangle(0, 1 / 3 * f, Size.Width, Size.Height), PixelFormat.Format32bppArgb)
            Else
                f = Size.Height - bm.Height
                bm = ResizeImage(bm, Size.Width, Size.Height + f)
                bm = bm.Clone(New Rectangle(1 / 3 * f, 0, Size.Width, Size.Height), PixelFormat.Format32bppArgb)
            End If


            Return bm
        Catch
            Return Bitmap
        End Try
    End Function
    Public Shared Sub RestartExplorer()
        With My.Application
            Try
                .processKiller.Start()
                .processKiller.WaitForExit()
                .processExplorer.Start()
            Catch
            End Try
        End With
    End Sub
    Public Shared Function LoadFromDLL(File As String, ResourceID As Integer, Optional ResourceType As String = "IMAGE") As Bitmap
        Try
            If IO.File.Exists(File) Then
                Dim hMod As IntPtr = LoadLibraryEx(File, IntPtr.Zero, &H2)
                Dim hRes As IntPtr = FindResource(hMod, ResourceID, ResourceType)
                Dim size As UInteger = SizeofResource(hMod, hRes)
                Dim pt As IntPtr = LoadResource(hMod, hRes)
                Dim bPtr As Byte() = New Byte(size - 1) {}
                Marshal.Copy(pt, bPtr, 0, CInt(size))
                Return Image.FromStream(New MemoryStream(bPtr))
            Else
                Return ColorToBitmap(Color.Black, My.Computer.Screen.Bounds.Size)
            End If
        Catch
            Return ColorToBitmap(Color.Black, My.Computer.Screen.Bounds.Size)
        End Try

    End Function
    Public Shared Function GetControlImage(ByVal ctl As Control) As Bitmap
        Dim bm As New Bitmap(ctl.Width, ctl.Height)
        ctl.DrawToBitmap(bm, New Rectangle(0, 0, ctl.Width, ctl.Height))
        Return bm
    End Function
    Public Shared Function ColorToBitmap([Color] As Color, [Size] As Size)
        Dim b As New Bitmap([Size].Width, [Size].Height)
        Dim g As Graphics = Graphics.FromImage(b)
        g.Clear([Color])
        g.Save()
        Return b
        g.Dispose()
        b.Dispose()
    End Function
    Public Shared Function GetAverageColor(ByVal [Image] As Bitmap) As Color
        Try
            Dim bmp As Bitmap = [Image]
            Dim totalR As Integer = 0
            Dim totalG As Integer = 0
            Dim totalB As Integer = 0

            Try
                If bmp IsNot Nothing Then
                    For x As Integer = 0 To bmp.Width - 1
                        For y As Integer = 0 To bmp.Height - 1
                            Dim pixel As Color = bmp.GetPixel(x, y)
                            totalR += pixel.R
                            totalG += pixel.G
                            totalB += pixel.B
                        Next
                    Next
                End If
            Catch

            End Try

            If bmp IsNot Nothing Then
                Dim totalPixels As Integer = bmp.Height * bmp.Width
                Dim averageR As Integer = totalR \ totalPixels
                Dim averageg As Integer = totalG \ totalPixels
                Dim averageb As Integer = totalB \ totalPixels
                Return Color.FromArgb(averageR, averageg, averageb)
            Else
                Return Color.FromArgb(80, 80, 80)
            End If
        Catch

        End Try
    End Function
    Public Shared Function CCB(ByVal color As Color, ByVal correctionFactor As Single) As Color
        Dim red As Single = CSng(color.R)
        Dim green As Single = CSng(color.G)
        Dim blue As Single = CSng(color.B)

        If correctionFactor < 0 Then
            correctionFactor = 1 + correctionFactor
            red *= correctionFactor
            green *= correctionFactor
            blue *= correctionFactor
        Else
            red = (255 - red) * correctionFactor + red
            green = (255 - green) * correctionFactor + green
            blue = (255 - blue) * correctionFactor + blue
        End If
        Try
            Return Color.FromArgb(color.A, CInt(red), CInt(green), CInt(blue))
        Catch
        End Try
    End Function
    Public Shared Function BlurBitmap(ByRef image As Image, Optional ByVal BlurForce As Integer = 2) As Bitmap
        Dim g As Graphics = Graphics.FromImage(image)

        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Dim att As New ImageAttributes
        Dim m As New ColorMatrix With {.Matrix33 = 0.4F}
        att.SetColorMatrix(m)

        BlurForce += 1

        For x = -BlurForce To BlurForce Step 0.5
            g.DrawImage(image, New Rectangle(x, 0, image.Width - 1, image.Height - 1), 0, 0, image.Width - 1, image.Height - 1, GraphicsUnit.Pixel, att)
        Next

        For y = -BlurForce To BlurForce Step 0.5
            g.DrawImage(image, New Rectangle(0, y, image.Width - 1, image.Height - 1), 0, 0, image.Width - 1, image.Height - 1, GraphicsUnit.Pixel, att)
        Next

        Return image
        att.Dispose()
        g.Dispose()
    End Function
    Public Shared Function FadeBitmap(ByVal bmp As Bitmap, ByVal opacity As Single) As Bitmap
        Try
            Dim bmp2 As New Bitmap(bmp.Width, bmp.Height, Imaging.PixelFormat.Format32bppArgb)
            opacity = Math.Max(0, Math.Min(opacity, 1.0F))
            Using ia As New Imaging.ImageAttributes
                Dim cm As New Imaging.ColorMatrix With {.Matrix33 = opacity}
                ia.SetColorMatrix(cm)
                Dim destpoints() As PointF = {New Point(0, 0), New Point(bmp.Width, 0), New Point(0, bmp.Height)}
                Using g As Graphics = Graphics.FromImage(bmp2)
                    g.DrawImage(bmp, destpoints,
              New RectangleF(Point.Empty, bmp.Size), GraphicsUnit.Pixel, ia)
                    g.Dispose()
                End Using
            End Using
            Return bmp2
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Function NoiseBitmap(bmp As Bitmap, NoiseMode As LogonUI7_NoiseMode, opacity As Single) As Bitmap
        Try
            Dim g As Graphics = Graphics.FromImage(bmp)
            Dim br As TextureBrush
            If NoiseMode = LogonUI7_NoiseMode.Acrylic Then br = New TextureBrush(FadeBitmap(My.Resources.GaussianBlur, opacity))
            If NoiseMode = LogonUI7_NoiseMode.Aero Then br = New TextureBrush(FadeBitmap(My.Resources.AeroGlass, opacity))
            g.FillRectangle(br, New Rectangle(0, 0, bmp.Width, bmp.Height))
            g.Save()
            Return bmp
            g.Dispose()
            bmp.Dispose()
        Catch
            Return Nothing
        End Try
    End Function
    Public Shared Function InvertColor(ByVal [Color] As Color) As Color
        Return Color.FromArgb([Color].A, 255 - [Color].R, 255 - [Color].G, 255 - [Color].B)
    End Function
    Public Shared Function IsColorDark(ByVal [Color] As Color) As Boolean
        Return Not ([Color].R * 0.2126 + [Color].G * 0.7152 + [Color].B * 0.0722 > 255 / 2)
    End Function
    Public Shared Function GetDarkMode() As Boolean
        Dim i As Long

        If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
            Return True
        Else
            Try
                If My.Application._Settings.Appearance_Auto Then
                    i = CLng(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", Nothing))
                    If i = 1 Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return My.Application._Settings.Appearance_Dark
                End If
            Catch
                Return True
            End Try
        End If
    End Function
    Public Shared Function IsNetAvaliable() As Boolean
        If My.Computer.Network.IsAvailable Then
            Try
                Using client = New WebClient()
                    Using stream = client.OpenRead("https://www.github.com")
                        Return True
                    End Using
                End Using
            Catch
                Return False
            End Try
        Else
            Return False
        End If
    End Function
#End Region

#Region " ResizeImage "

    Public Overloads Shared Function ResizeImage(bmSource As Drawing.Bitmap, TargetWidth As Int32, TargetHeight As Int32) As Drawing.Bitmap
        Dim bmDest As New Drawing.Bitmap(TargetWidth, TargetHeight, Drawing.Imaging.PixelFormat.Format32bppArgb)

        Dim nSourceAspectRatio = bmSource.Width / bmSource.Height
        Dim nDestAspectRatio = bmDest.Width / bmDest.Height

        Dim NewX = 0
        Dim NewY = 0

        Using grDest = Drawing.Graphics.FromImage(bmDest)
            With grDest
                .CompositingQuality = Drawing.Drawing2D.CompositingQuality.HighQuality
                .InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                .PixelOffsetMode = Drawing.Drawing2D.PixelOffsetMode.HighQuality
                .SmoothingMode = Drawing.Drawing2D.SmoothingMode.AntiAlias
                .CompositingMode = Drawing.Drawing2D.CompositingMode.SourceOver
                .DrawImage(bmSource, 0, 0, TargetWidth, TargetHeight)
            End With
        End Using

        Return bmDest
    End Function
#End Region

#Region "Dark\Light Mode"
    Public Shared Sub ApplyDarkMode(Optional ByVal [Form] As Form = Nothing)
        Dim DarkMode As Boolean = GetDarkMode()


        If Form Is Nothing Then
            '####################### For Form1
            Try
                EnumControls(MainFrm, DarkMode)
                MainFrm.Invalidate()
                MainFrm.Refresh()
            Catch

            End Try

            '####################### For all open forms
            Try
                For Each OFORM In Application.OpenForms
                    Select Case DarkMode
                        Case True
                            If TryCast(OFORM, Form).BackColor <> My.Application.BackColor_Dark Then
                                TryCast(OFORM, Form).BackColor = My.Application.BackColor_Dark
                                TryCast(OFORM, Form).Invalidate()
                                TryCast(OFORM, Form).Refresh()
                            End If
                        Case False
                            If TryCast(OFORM, Form).BackColor <> My.Application.BackColor_Light Then
                                TryCast(OFORM, Form).BackColor = My.Application.BackColor_Light
                                TryCast(OFORM, Form).Invalidate()
                                TryCast(OFORM, Form).Refresh()
                            End If
                    End Select
                    EnumControls(TryCast(OFORM, Form), DarkMode)
                    'Adjust_Form(DarkMode, TryCast(OFORM, Form))
                Next OFORM
            Catch

            End Try
        Else
            '####################### For Selected [Form]
            Select Case DarkMode
                Case True
                    If TryCast([Form], Form).BackColor <> My.Application.BackColor_Dark Then
                        TryCast([Form], Form).BackColor = My.Application.BackColor_Dark
                        TryCast([Form], Form).Invalidate()
                        TryCast([Form], Form).Refresh()
                    End If
                Case False
                    If TryCast([Form], Form).BackColor <> My.Application.BackColor_Light Then
                        TryCast([Form], Form).BackColor = My.Application.BackColor_Light
                        TryCast([Form], Form).Invalidate()
                        TryCast([Form], Form).Refresh()
                    End If
            End Select
            EnumControls(TryCast([Form], Form), DarkMode)
            [Form].Refresh()
            'Adjust_Form(DarkMode, [Form])
        End If

        MainFrm.ContextMenuStrip1.BackColor = If(DarkMode, Color.FromArgb(35, 35, 35), Color.FromArgb(250, 250, 250))
        MainFrm.ContextMenuStrip1.ForeColor = If(DarkMode, Color.White, Color.Black)

        For Each it As ToolStripItem In MainFrm.ContextMenuStrip1.Items
            it.ForeColor = If(DarkMode, Color.White, Color.Black)
        Next

        'MainFrm.status_lbl.BackColor = If(DarkMode, Color.FromArgb(55, 55, 55), Color.FromArgb(200, 200, 200))
        MainFrm.status_lbl.ForeColor = If(DarkMode, Color.White, Color.Black)

    End Sub
    Public Shared Sub EnumControls(ByVal ctrl As Control, ByVal DarkMode As Boolean)

        Dim b As Boolean = False
        If TypeOf ctrl Is RetroButton Then b = True
        If TypeOf ctrl Is RetroCheckBox Then b = True
        If TypeOf ctrl Is RetroGroupBox Then b = True
        If TypeOf ctrl Is RetroLabel Then b = True
        If TypeOf ctrl Is RetroPanel Then b = True
        If TypeOf ctrl Is RetroPanelRaised Then b = True
        If TypeOf ctrl Is RetroRadioButton Then b = True
        If TypeOf ctrl Is RetroSeparatorH Then b = True
        If TypeOf ctrl Is RetroSeparatorV Then b = True
        If TypeOf ctrl Is RetroTextBox Then b = True
        If TypeOf ctrl Is RetroWindow Then b = True

        If Not b Then
            Select Case DarkMode
                Case True
                    If ctrl.ForeColor = Color.Black Then ctrl.ForeColor = Color.White
                Case False
                    If ctrl.ForeColor = Color.White Then ctrl.ForeColor = Color.Black
            End Select
        End If


        If TypeOf ctrl Is XenonGroupBox Then
            If TryCast(ctrl, XenonGroupBox).CustomColor Then
                TryCast(ctrl, XenonGroupBox).LineColor = CCB(ctrl.BackColor, If(IsColorDark(ctrl.BackColor), 0.1, -0.1))
            Else
                TryCast(ctrl, XenonGroupBox).BackColor = CCB(GetParentColor(ctrl), If(IsColorDark(GetParentColor(ctrl)), 0.05, -0.05))
                TryCast(ctrl, XenonGroupBox).LineColor = CCB(GetParentColor(ctrl), If(IsColorDark(GetParentColor(ctrl)), 0.1, -0.1))
            End If
        End If

        If TypeOf ctrl Is XenonButton Then
            ctrl.BackColor = CCB(GetParentColor(ctrl), If(IsColorDark(GetParentColor(ctrl)), 0.04, -0.04))
            ctrl.ForeColor = If(DarkMode, Color.White, Color.Black)
        End If

        If TypeOf ctrl Is RichTextBox Then
            ctrl.BackColor = ctrl.Parent.BackColor
            ctrl.ForeColor = If(DarkMode, Color.White, Color.Black)
        End If

        If TypeOf ctrl Is LinkLabel Then
            DirectCast(ctrl, LinkLabel).LinkColor = If(DarkMode, Color.White, Color.Black)
        End If

        If TypeOf ctrl Is DataGridView Then
            Dim ColumnBack As Color
            Dim Fore As Color
            Dim CellBack As Color

            Select Case DarkMode
                Case True
                    ColumnBack = Color.FromArgb(60, 60, 60)
                    Fore = Color.White
                    CellBack = ctrl.Parent.BackColor
                Case False
                    ColumnBack = Color.FromArgb(150, 150, 150)
                    Fore = Color.Black
                    CellBack = ctrl.Parent.BackColor
            End Select

            For Each Row As DataGridViewRow In TryCast(ctrl, DataGridView).Rows
                Row.DefaultCellStyle.ForeColor = Fore
            Next

            TryCast(ctrl, DataGridView).ColumnHeadersDefaultCellStyle.BackColor = ColumnBack
            TryCast(ctrl, DataGridView).ColumnHeadersDefaultCellStyle.ForeColor = Fore
            TryCast(ctrl, DataGridView).BackColor = ctrl.Parent.BackColor
            TryCast(ctrl, DataGridView).BackgroundColor = ctrl.Parent.BackColor
            TryCast(ctrl, DataGridView).DefaultCellStyle.BackColor = CellBack
            TryCast(ctrl, DataGridView).ForeColor = Fore
            TryCast(ctrl, DataGridView).DefaultCellStyle.ForeColor = Fore
            TryCast(ctrl, DataGridView).RowTemplate.DefaultCellStyle.ForeColor = Fore

            ctrl.Invalidate()
            ctrl.Refresh()
        End If

        If TypeOf ctrl Is XenonCheckBox Then TryCast(ctrl, XenonCheckBox).ColorPalette = New XenonColorPalette(ctrl)
        If TypeOf ctrl Is XenonRadioButton Then TryCast(ctrl, XenonRadioButton).ColorPalette = New XenonColorPalette(ctrl)
        If TypeOf ctrl Is XenonComboBox Then TryCast(ctrl, XenonComboBox).ColorPalette = New XenonColorPalette(ctrl)
        If TypeOf ctrl Is XenonTextBox Then TryCast(ctrl, XenonTextBox).ColorPalette = New XenonColorPalette(ctrl)
        If TypeOf ctrl Is XenonToggle Then TryCast(ctrl, XenonToggle).ColorPalette = New XenonColorPalette(ctrl)

        If TypeOf ctrl Is TreeView Then
            With TryCast(ctrl, TreeView)
                .BackColor = ctrl.Parent.BackColor
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With
        End If

        If TypeOf ctrl Is TrackBar Then TryCast(ctrl, TrackBar).BackColor = ctrl.Parent.BackColor
        If TypeOf ctrl Is CheckedListBox Then TryCast(ctrl, CheckedListBox).BackColor = ctrl.Parent.BackColor
        If TypeOf ctrl Is ListBox Then TryCast(ctrl, ListBox).BackColor = ctrl.Parent.BackColor

        If ctrl.HasChildren Then
            For Each c As Control In ctrl.Controls
                c.Invalidate()
                c.Refresh()
                EnumControls(c, DarkMode)
            Next
        End If

        ctrl.Invalidate()
        ctrl.Refresh()
    End Sub
#End Region

#Region "String Stream"
    Public Shared Sub CList_FromStr(ByVal [List] As List(Of String), ByVal [String] As String)
        [List].Clear()
        Using Reader As New System.IO.StringReader([String])
            While Reader.Peek >= 0
                [List].Add(Reader.ReadLine)
            End While
            Reader.Close()
            Reader.Dispose()
        End Using
    End Sub
    Public Shared Function CStr_FromList(ByVal [List] As List(Of String)) As String
        Return String.Join(vbCrLf, [List].ToArray)
    End Function
#End Region

End Class

Public Class Acrylism

#Region "Fluent"

    Friend Structure WindowCompositionAttributeData
        Public Attribute As WindowCompositionAttribute
        Public Data As IntPtr
        Public SizeOfData As Integer
    End Structure

    Friend Enum WindowCompositionAttribute
        WCA_ACCENT_POLICY = 19
    End Enum

    Friend Enum AccentState
        ACCENT_DISABLED = 0
        ACCENT_ENABLE_GRADIENT = 1
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2
        ACCENT_ENABLE_BLURBEHIND = 3
        ACCENT_ENABLE_TRANSPARANT = 6
        ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
    End Enum

    <StructLayout(LayoutKind.Sequential)>
    Friend Structure AccentPolicy
        Public AccentState As AccentState
        Public AccentFlags As Integer
        Public GradientColor As Integer
        Public AnimationId As Integer
    End Structure

    Friend Declare Function SetWindowCompositionAttribute Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef data As WindowCompositionAttributeData) As Integer
    Private Declare Auto Function FindWindow Lib "user32.dll" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr

#End Region

    Public Shared Sub EnableBlur(ByVal Handle As IntPtr, Optional ByVal Border As Boolean = True)

        Dim accent = New AccentPolicy With {.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND}
        If Border Then accent.AccentFlags = &H20 Or &H40 Or &H80 Or &H100
        Dim accentStructSize = Marshal.SizeOf(accent)
        Dim accentPtr = Marshal.AllocHGlobal(accentStructSize)
        Marshal.StructureToPtr(accent, accentPtr, False)

        Dim Data = New WindowCompositionAttributeData With {
                .Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                .SizeOfData = accentStructSize,
                .Data = accentPtr
            }

        SetWindowCompositionAttribute(Handle, Data)

        Marshal.FreeHGlobal(accentPtr)
    End Sub
End Class

Public Class Visual
    '' FILENAME:        Visual.vb
    '' NAMESPACE:       PI.Common
    '' APPLICATION:     N/A
    '' CREATED BY:      Luke Berg
    '' CREATED:         10-02-06
    '' REVISED BY:      _revisedby_
    '' REVISED:         _revised_
    '' DESCRIPTION:     A common module of visual functions.

    Private Shared colorsFading As New Dictionary(Of String, BackgroundWorker) 'Keeps track of any backgroundworkers already fading colors
    Private Shared backgroundWorkers As New Dictionary(Of BackgroundWorker, ColorFaderInformation) 'Associate each background worker with information it needs

    ' The delegate of a method that will be called when the color finishes fading
    Public Delegate Sub DoneFading(ByVal container As Object, ByVal colorProperty As String)

    ''' <summary>
    '''  Fades a color property from one color to another
    ''' </summary>
    ''' <param name="container">The object that contains the color property</param>
    ''' <param name="colorProperty">The name of the color property to change</param>
    ''' <param name="startColor">The color to start the fade with</param>
    ''' <param name="endColor">The color to end the fade with</param>
    ''' <param name="steps">The number of steps to take to fade from the start color to the end color</param>
    ''' <param name="delay">The delay in milliseconds between each step in the fade.</param>
    ''' <param name="callback">A function to be called when the fade completes</param>
    ''' <remarks></remarks>
    Public Shared Sub FadeColor(ByVal container As Object, ByVal colorProperty As String, ByVal startColor As Color, ByVal endColor As Color, ByVal steps As Integer, ByVal delay As Integer, Optional ByVal callback As DoneFading = Nothing)
        Dim colorSteps(0) As ColorStep
        colorSteps(0) = New ColorStep(endColor, steps)
        FadeColor(container, colorProperty, startColor, colorSteps, delay, callback)
    End Sub

    ''' <summary>
    '''  Fades a color property from one color to another, and then to yet another
    ''' </summary>
    ''' <param name="container">The object that contains the color property</param>
    ''' <param name="colorProperty">The name of the color property to change</param>
    ''' <param name="startColor">The color to start the fade with</param>
    ''' <param name="middleColor">The color to fade to first</param>
    ''' <param name="middleSteps">The number of steps to take in fading to the first color</param>
    ''' <param name="endcolor">The last color to fade to</param>
    ''' <param name="endSteps">The number of steps to take in fading to the last color</param>
    ''' <param name="delay">The delay between each step in the fade</param>
    ''' <param name="callback">A function that will be called after the fading has completed</param>
    ''' <remarks></remarks>
    Public Shared Sub FadeColor(ByVal container As Object, ByVal colorProperty As String, ByVal startColor As Color, ByVal middleColor As Color, ByVal middleSteps As Integer, ByVal endcolor As Color, ByVal endSteps As Integer, ByVal delay As Integer, Optional ByVal callback As DoneFading = Nothing)
        Dim colorSteps(1) As ColorStep
        colorSteps(0) = New ColorStep(middleColor, middleSteps)
        colorSteps(1) = New ColorStep(endcolor, endSteps)
        FadeColor(container, colorProperty, startColor, colorSteps, delay, callback)
    End Sub

    ''' <summary>
    '''  Fades a color property to various colors
    ''' </summary>
    ''' <param name="container">The object that contains the color property</param>
    ''' <param name="colorProperty">The name of the color property to change</param>
    ''' <param name="startColor">The color to start the fade with</param>
    ''' <param name="colorSteps">A list of steps in fading the color - an enumerable list of colors and the steps to get to that color</param>
    ''' <param name="delay">The delay between each step in fading the color</param>
    ''' <param name="callBack">A method to call when the fading has completed</param>
    ''' <remarks></remarks>
    Public Shared Sub FadeColor(ByVal container As Object, ByVal colorProperty As String, ByVal startColor As Color, ByVal colorSteps As IEnumerable(Of ColorStep), ByVal delay As Integer, Optional ByVal callBack As DoneFading = Nothing)

        Dim colorFader As BackgroundWorker

        ' Stores all the parameter information into a class that the background worker will access
        Dim colorFaderInfo As New ColorFaderInformation(container, colorProperty, startColor, colorSteps, delay, callBack)

        ' Checks if the color is already in the process of fading.
#Disable Warning BC42030
        If colorsFading.TryGetValue(GenerateHashCode(container, colorProperty), colorFader) Then
#Enable Warning BC42030

            ' Cancels the backgroundWorkers process and sets a flag indicating that it should restart itself with
            ' the new information.
            colorFader.CancelAsync()
            colorFaderInfo.Rerun = True
            backgroundWorkers(colorFader) = colorFaderInfo
        Else

            ' Creates a new backgroundWorker and adds handlers to all its events
            colorFader = New BackgroundWorker()
            AddHandler colorFader.DoWork, AddressOf BackgroundWorker_DoWork
            AddHandler colorFader.ProgressChanged, AddressOf BackgroundWorker_ProgressChanged
            AddHandler colorFader.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted
            colorFader.WorkerReportsProgress = True
            colorFader.WorkerSupportsCancellation = True

            backgroundWorkers.Add(colorFader, colorFaderInfo)
            colorsFading.Add(GenerateHashCode(container, colorProperty), colorFader)

        End If

        ' Starts the backgroundWorker beginning the fade
        If Not colorFader.IsBusy() Then
            colorFader.RunWorkerAsync(colorFaderInfo)
        End If
    End Sub

    ''' <summary>
    '''  The work that the background worker does in fading the color
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Shared Sub BackgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Dim info As ColorFaderInformation = CType(e.Argument, ColorFaderInformation)
        ' These are calculated with each iteration (step) and used to set the color
        ' when the background worker reports its progress.

        Dim curA As Double
        Dim curR As Double
        Dim curG As Double
        Dim curB As Double

        Dim startStepColor As Color = info.StartColor
        Dim endStepColor As Color

        For Each colorStep As ColorStep In info.Colors

            endStepColor = colorStep.Color

            ' Gets the amount to change each color part per step

            Dim aStep As Double = (CType(endStepColor.A, Double) - startStepColor.A) / colorStep.Steps
            Dim rStep As Double = (CType(endStepColor.R, Double) - startStepColor.R) / colorStep.Steps
            Dim gStep As Double = (CType(endStepColor.G, Double) - startStepColor.G) / colorStep.Steps
            Dim bStep As Double = (CType(endStepColor.B, Double) - startStepColor.B) / colorStep.Steps

            ' the red, green and blue parts of the current color
            curA = startStepColor.A
            curR = startStepColor.R
            curG = startStepColor.G
            curB = startStepColor.B

            ' loop through, and fade
            For i As Integer = 1 To colorStep.Steps
                curA += aStep
                curR += rStep
                curB += bStep
                curG += gStep

                Try : CType(sender, BackgroundWorker).ReportProgress(0, Color.FromArgb(CInt(curA), CInt(curR), CInt(curG), CInt(curB))) : Catch : End Try

                System.Threading.Thread.Sleep(info.Delay)

                If CType(sender, BackgroundWorker).CancellationPending Then
                    e.Cancel = True
                    Exit For
                End If
            Next

            startStepColor = endStepColor

        Next

    End Sub

    ''' <summary>
    '''  Calls to this method are marshalled back to the original thread, so here is where we actually change the color.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Shared Sub BackgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        Dim info As ColorFaderInformation
#Disable Warning BC42030
        If backgroundWorkers.TryGetValue(CType(sender, BackgroundWorker), info) Then
#Enable Warning BC42030
            Dim currentColor As Color = CType(e.UserState, Color)
            Try
                CallByName(info.Container, info.ColorProperty, CallType.Let, currentColor)
            Catch
            End Try
        End If
    End Sub

    ''' <summary>
    '''  This is raised when the background method completes.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Shared Sub BackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        Dim info As ColorFaderInformation = Nothing

        If backgroundWorkers.TryGetValue(CType(sender, BackgroundWorker), info) Then

            If Not e.Cancelled Then

                If info.CallBack IsNot Nothing Then
                    info.CallBack.Invoke(info.Container, info.ColorProperty)
                End If

                backgroundWorkers.Remove(CType(sender, BackgroundWorker))
                colorsFading.Remove(GenerateHashCode(info.Container, info.ColorProperty))
            Else

                If info.Rerun Then

                    info.Rerun = False
                    CType(sender, BackgroundWorker).RunWorkerAsync(info)

                End If

            End If

        End If

    End Sub

    ''' <summary>
    '''  Generates a hashcode for an object and its color that are in the process of fading
    ''' </summary>
    ''' <param name="container">The object whose color property needs to be faded</param>
    ''' <param name="colorProperty">The string name of the property to fade</param>
    ''' <returns>A unique string representing the object and it's color property</returns>
    ''' <remarks></remarks>
    Private Shared Function GenerateHashCode(ByVal container As Object, ByVal colorProperty As String) As String
        Return container.GetHashCode() & colorProperty
    End Function

    ''' <summary>
    '''  A simple class for storing information a backgroundWorker needs to perform the fading.
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ColorFaderInformation

        Public CallBack As DoneFading
        Public Container As Object
        Public ColorProperty As String
        Public StartColor As Color
        Public Colors As IEnumerable(Of ColorStep)
        Public Delay As Integer
        Public Rerun As Boolean

        Public Sub New(ByVal container As Object, ByVal colorProperty As String, ByVal startColor As Color, ByVal colorSteps As IEnumerable(Of ColorStep), ByVal delay As Integer, Optional ByVal callBack As DoneFading = Nothing)
            Me.Container = container
            Me.ColorProperty = colorProperty
            Me.StartColor = startColor
            Me.Colors = colorSteps
            Me.Delay = delay
            Me.CallBack = callBack
            Me.Rerun = False
        End Sub

    End Class

    ''' <summary>
    '''  A simple class needed to represent a single step in the fading process
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ColorStep

        Public Color As Color
        Public Steps As Integer

        Public Sub New(ByVal color As Color, ByVal steps As Integer)
            Me.Color = color
            Me.Steps = steps
        End Sub

    End Structure

End Class