Imports System.Drawing.Imaging
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.Threading
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
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function PostMessage(ByVal hWnd As IntPtr,
        <MarshalAs(UnmanagedType.U4)> ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean

    End Function
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr

    End Function
    Const WM_USER As Integer = &H400
    Public Shared Sub RestartExplorer()
        If My.Application._Settings.AutoRestartExplorer Then
            Dim ptr = FindWindow("Shell_TrayWnd", Nothing)
            PostMessage(ptr, WM_USER + 436, CType(0, IntPtr), CType(0, IntPtr))

            Do
                ptr = FindWindow("Shell_TrayWnd", Nothing)

                If ptr.ToInt32() = 0 Then
                    Exit Do
                End If

                Thread.Sleep(1000)
            Loop While True


            Try : Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\explorer.exe").WaitForInputIdle() : Catch : End Try
        End If
    End Sub
    Public Shared Function GetControlImage(ByVal ctl As Control) As Bitmap
        Dim bm As New Bitmap(ctl.Width, ctl.Height)
        ctl.DrawToBitmap(bm, New Rectangle(0, 0, ctl.Width, ctl.Height))
        Return bm
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
            i = CLng(Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", Nothing))
            If i = 1 Then
                Return False
            Else
                Return True
            End If
        End If
    End Function
    Public Shared Function IsNetAvaliable() As Boolean
        If My.Computer.Network.IsAvailable Then
            Try
                Using client = New WebClient()
                    Using stream = client.OpenRead("http://www.google.com")
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
    Public Overloads Shared Function ResizeImage(SourceImage As Drawing.Image, TargetWidth As Int32, TargetHeight As Int32) As Drawing.Bitmap
        Dim bmSource = New Drawing.Bitmap(SourceImage)

        Return ResizeImage(bmSource, TargetWidth, TargetHeight)
    End Function

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
    End Sub


    Public Shared Sub EnumControls(ByVal ctrl As Control, ByVal DarkMode As Boolean)

        Select Case DarkMode
            Case True
                If ctrl.ForeColor = Color.Black Then ctrl.ForeColor = Color.White
            Case False
                If ctrl.ForeColor = Color.White Then ctrl.ForeColor = Color.Black
        End Select

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