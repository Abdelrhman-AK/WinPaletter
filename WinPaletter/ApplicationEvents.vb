Imports System.Reflection
Imports System.Runtime.InteropServices
Imports WinPaletter.XenonCore
Imports System
Imports Microsoft.Win32
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My

    Public Module WindowsVersions
        Public W11 As Boolean
        Public W10 As Boolean
        Public W8 As Boolean
        Public W_Aero As Boolean
    End Module

    Partial Friend Class MyApplication
        Public _Settings As XeSettings

        Public Shared Event UserPreferenceChanged As Microsoft.Win32.UserPreferenceChangedEventHandler
        Public Wallpaper As Bitmap
        Public Font_CtrlBox As Font = New Font("Segoe MDL2 Assets", 6.5!, FontStyle.Regular)
        Public Font_Console As Font = New Font("JetBrains Mono Medium", 8, FontStyle.Regular)
        Public Font_Console_Bigger As Font = New Font("JetBrains Mono Medium", 10, FontStyle.Regular)
        Public BackColor_Dark As Color = Color.FromArgb(24, 24, 26)
        Public BackColor_Light As Color = Color.FromArgb(235, 235, 235)
        Public WithEvents AnimatorX As AnimatorNS.Animator
        Public ExternalLink As Boolean = False
        Public ExternalLink_File As String = ""

        Public Function GetCurrentWallpaper() As Bitmap
            Try
                Dim rkWallPaper As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", False)
                Dim WallpaperPath As String = rkWallPaper.GetValue("WallPaper").ToString()
                Dim x As New IO.FileStream(WallpaperPath, IO.FileMode.OpenOrCreate, IO.FileAccess.Read)
                Return Image.FromStream(x)
                x.Close()
                rkWallPaper.Close()
                My.Application.FlushMem()
            Catch
                Return Nothing
            End Try
        End Function

        Private Sub XenonMica_UserPreferenceChanged(sender As Object, e As Microsoft.Win32.UserPreferenceChangedEventArgs) Handles Me.UserPreferenceChanged
            If e.Category = e.Category.General Then
                Threading.Thread.Sleep(1000)
                Wallpaper = ResizeImage(GetCurrentWallpaper(), 528, 297)
                Global.WinPaletter.MainForm.pnl_preview.BackgroundImage = My.Application.Wallpaper
                Global.WinPaletter.dragPreviewer.pnl_preview.BackgroundImage = My.Application.Wallpaper
                '''''''''''''''''''''''''''''''''''MicaWall = GetCurrentWallpaper()
            End If
        End Sub

        <DllImport("KERNEL32.DLL", EntryPoint:=
      "SetProcessWorkingSetSize", SetLastError:=True,
      CallingConvention:=CallingConvention.StdCall)>
        Friend Shared Function SetProcessWorkingSetSize32Bit _
      (ByVal pProcess As IntPtr, ByVal dwMinimumWorkingSetSize _
      As Integer, ByVal dwMaximumWorkingSetSize As Integer) _
      As Boolean
        End Function

        <DllImport("KERNEL32.DLL", EntryPoint:=
      "SetProcessWorkingSetSize", SetLastError:=True,
      CallingConvention:=CallingConvention.StdCall)>
        Friend Shared Function SetProcessWorkingSetSize64Bit _
      (ByVal pProcess As IntPtr, ByVal dwMinimumWorkingSetSize _
      As Long, ByVal dwMaximumWorkingSetSize As Long) As Boolean
        End Function

        Public Sub FlushMem()
            GC.Collect()
            GC.WaitForPendingFinalizers()
            If Environment.OSVersion.Platform = PlatformID.Win32NT Then SetProcessWorkingSetSize32Bit(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1)
        End Sub

        <Runtime.InteropServices.DllImport("dwmapi.dll", PreserveSig:=False)>
        Public Shared Function DwmIsCompositionEnabled() As Boolean
        End Function
        Public Function AeroEnabled() As Boolean
            Return DwmIsCompositionEnabled()
        End Function

        <System.Runtime.InteropServices.DllImport("shell32.dll")> Shared Sub _
    SHChangeNotify(ByVal wEventId As Integer, ByVal uFlags As Integer,
    ByVal dwItem1 As Integer, ByVal dwItem2 As Integer)
        End Sub

        Function CreateFileAssociation(ByVal extension As String, ByVal className As String, ByVal description As String, ByVal exeProgram As String) As Boolean
            ' Extension is the extension to be registered (eg ".cad"
            ' ClassName is the name of the associated class (eg "CADDoc")
            ' Description is the textual description (eg "CAD Document"
            ' ExeProgram is the app that manages that extension (eg "c:\Cad\MyCad.exe")

            Const SHCNE_ASSOCCHANGED = &H8000000
            Const SHCNF_IDLIST = 0

            ' ensure that there is a leading dot
            If extension.Substring(0, 1) <> "." Then
                extension = "." & extension
            End If

            Dim key1, key2, key3, key4 As Microsoft.Win32.RegistryKey

            Try
                ' create a value for this key that contains the classname
                key1 = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(extension)
                key1.SetValue("", className)
                ' create a new key for the Class name
                key2 = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(className)
                key2.SetValue("", description)
                ' associate the program to open the files with this extension
                key3 = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(className & "\Shell\Open\Command")
                key3.SetValue("", exeProgram & " ""%1""")
                ' associate file icon
                key4 = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(className & "\DefaultIcon")
                key4.SetValue("", exeProgram & ",1")

            Catch e As Exception
                Return False
            Finally
                If Not key1 Is Nothing Then key1.Close()
                If Not key2 Is Nothing Then key2.Close()
                If Not key3 Is Nothing Then key3.Close()
                If Not key4 Is Nothing Then key4.Close()
            End Try

            ' notify Windows that file associations have changed
            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, 0, 0)
            Return True
        End Function

        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            _Settings = New XeSettings(XeSettings.Mode.Registry)

            If _Settings.AutoAddExt Then CreateFileAssociation(".wpth", "WinPaletter.ThemeFile", "WinPaletter Theme File", """" & System.Reflection.Assembly.GetExecutingAssembly().Location & """")

            AnimatorX = New AnimatorNS.Animator With {.Interval = 1, .TimeStep = 0.07, .DefaultAnimation = AnimatorNS.Animation.Transparent, .AnimationType = AnimatorNS.AnimationType.Transparent}

            ExternalLink = False
            ExternalLink_File = ""

            Wallpaper = ResizeImage(GetCurrentWallpaper(), 528, 297)
            AddHandler Microsoft.Win32.SystemEvents.UserPreferenceChanged, AddressOf XenonMica_UserPreferenceChanged

            Try
                W11 = My.Computer.Info.OSFullName.Contains("11")
            Catch
                W11 = False
            End Try

            Try
                W10 = My.Computer.Info.OSFullName.Contains("10")
            Catch
                W10 = False
            End Try

            Try
                W8 = My.Computer.Info.OSFullName.Contains("8")
            Catch
                W8 = False
            End Try

            Try
                W_Aero = AeroEnabled() And Not My.Computer.Info.OSFullName.Contains("8") And Not My.Computer.Info.OSFullName.Contains("10")
            Catch
                W_Aero = False
            End Try

            Try
                For x = 1 To Environment.GetCommandLineArgs.Count - 1
                    Dim arg As String = Environment.GetCommandLineArgs(x)
                    If My.Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpth" Then
                        ExternalLink = True
                        ExternalLink_File = arg
                        '''''''
                    End If
                Next
            Catch
            End Try
        End Sub

        Private Sub MyApplication_StartupNextInstance(sender As Object, e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            Try
                Dim arg As String = e.CommandLine(0)

                If arg = "" Then
                    ''MsgboxX.ShowMsg("Error", "You can't run double instance of Xenon Retro Studio.", MsgboxX.Icons.Error_, MsgboxX.Button.OK)
                    ''e.BringToForeground = True
                Else
                    If My.Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpth" Then
                        WinPaletter.MainForm.CP = New CP(CP.Mode.File, arg)
                        WinPaletter.MainForm.OpenFileDialog1.FileName = arg
                        WinPaletter.MainForm.SaveFileDialog1.FileName = arg
                        WinPaletter.MainForm.ApplyCPValues(WinPaletter.MainForm.CP)
                        WinPaletter.MainForm.ApplyLivePreviewFromCP(WinPaletter.MainForm.CP)
                        '''''''
                    End If
                End If
            Catch
                ''MsgboxX.ShowMsg("Error", "You can't run double instance of Xenon Retro Studio.", MsgboxX.Icons.Error_, MsgboxX.Button.OK)
                ''e.BringToForeground = True
            End Try
        End Sub

        Private WithEvents Domain As AppDomain = AppDomain.CurrentDomain

        Private Function DomainCheck(sender As Object, e As System.ResolveEventArgs) As System.Reflection.Assembly Handles Domain.AssemblyResolve
            If e.Name.ToUpper.Contains("Animator".ToUpper) Then Return Assembly.Load(My.Resources.Animator)
            If e.Name.ToUpper.Contains("Cyotek.Windows.Forms.ColorPicker".ToUpper) Then Return Assembly.Load(My.Resources.Cyotek_Windows_Forms_ColorPicker)
            If e.Name.ToUpper.Contains("ColorThief.Desktop.v46".ToUpper) Then Return Assembly.Load(My.Resources.ColorThief_Desktop_v46)
#Disable Warning BC42105
        End Function


#Enable Warning BC42105

    End Class
End Namespace
