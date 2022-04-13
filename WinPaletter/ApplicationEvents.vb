Imports System.Reflection
Imports System.Runtime.InteropServices
Imports WinPaletter.XenonCore
Imports System
Imports Microsoft.Win32
Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Management
Imports System.Security.Principal

Namespace My
    Public Module WindowsVersions
        Public W11 As Boolean
        Public W10 As Boolean
        Public W8 As Boolean
        Public W_Aero As Boolean
    End Module

    Partial Friend Class MyApplication
#Region "Addons"
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

#End Region
        Public _Settings As XeSettings
        Public Wallpaper As Bitmap
        Public BackColor_Dark As Color = Color.FromArgb(24, 24, 26)
        Public BackColor_Light As Color = Color.FromArgb(235, 235, 235)
        Public WithEvents AnimatorX As AnimatorNS.Animator
        Public ExternalLink As Boolean = False
        Public ExternalLink_File As String = ""

        Public Function GetCurrentWallpaper() As Bitmap
            Try
                Dim rkWallPaper As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", False)
                Dim WallpaperPath As String = rkWallPaper.GetValue("Wallpaper").ToString()

                If IO.File.Exists(WallpaperPath) Then
                    Dim x As New IO.FileStream(WallpaperPath, IO.FileMode.OpenOrCreate, IO.FileAccess.Read)
                    Return Image.FromStream(x)
                    x.Close()
                    rkWallPaper.Close()
                Else
                    Dim bmp As Bitmap = New Bitmap(528, 297)
                    Dim g As Graphics = Graphics.FromImage(bmp)

                    With My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0")
                        g.Clear(Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2)))
                    End With

                    Return bmp
                    g.Dispose()
                    bmp.Dispose()
                End If
            Catch
                Return Nothing
            End Try
        End Function

        Public Const SHCNE_ASSOCCHANGED = &H8000000
        Public Const SHCNF_IDLIST = 0

        Function CreateFileAssociation(ByVal extension As String, ByVal className As String, ByVal description As String, ByVal exeProgram As String) As Boolean
            ' Extension is the extension to be registered (eg ".wpth"
            ' ClassName is the name of the associated class (eg "WinPaletter.ThemeFile")
            ' Description is the textual description (eg "WinPaletter ThemeFile"
            ' ExeProgram is the app that manages that extension (eg. Assembly.GetExecutingAssembly().Location)

            If extension.Substring(0, 1) <> "." Then extension = "." & extension
            Dim key1, key2, key3, key4 As RegistryKey

            Try
                ' create a value for this key that contains the classname
                key1 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
                key1.CreateSubKey(extension, True).SetValue("", className)
                ' create a new key for the Class name
                key2 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
                key2.CreateSubKey(className, True).SetValue("", description)
                ' associate the program to open the files with this extension
                key3 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
                key3.CreateSubKey(className & "\Shell\Open\Command", True).SetValue("", exeProgram & " ""%1""")
                ' associate file icon
                key4 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
                key4.CreateSubKey(className & "\DefaultIcon", True).SetValue("", exeProgram & If(className = "WinPaletter.ThemeFile", ",1", ",2"))

            Catch e As Exception
                Return False
            Finally
                If key1 IsNot Nothing Then key1.Close()
                If key2 IsNot Nothing Then key2.Close()
                If key3 IsNot Nothing Then key3.Close()
                If key4 IsNot Nothing Then key4.Close()
            End Try

            ' notify Windows that file associations have changed
            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, 0, 0)
            Return True
        End Function

        Function DeleteFileAssociation(ByVal extension As String, ByVal className As String) As Boolean
            Const SHCNE_ASSOCCHANGED = &H8000000
            Const SHCNF_IDLIST = 0
            If extension.Substring(0, 1) <> "." Then extension = "." & extension
            Dim key1, key2, key3, key4 As RegistryKey

            Try
                key1 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
                key1.DeleteSubKeyTree(extension, False)

                key2 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
                key2.DeleteSubKeyTree(className, False)

                'key3 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
                'key3.CreateSubKey(className & "\Shell\Open\Command", True).SetValue("", exeProgram & " ""%1""")

                'key4 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
                'key4.CreateSubKey(className & "\DefaultIcon", True).SetValue("", exeProgram & If(className = "WinPaletter.ThemeFile", ",1", ",2"))

            Catch e As Exception
                Return False
            Finally
                If key1 IsNot Nothing Then key1.Close()
                If key2 IsNot Nothing Then key2.Close()
                If key3 IsNot Nothing Then key3.Close()
                If key4 IsNot Nothing Then key4.Close()
            End Try

            ' notify Windows that file associations have changed
            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, 0, 0)
            Return True
        End Function

        Sub RegMon(KeyPath As String, valueName As String)
            Dim currentUser = WindowsIdentity.GetCurrent()
            Dim Base As String = String.Format("SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace("\", "\\"), valueName)
            Dim query = New WqlEventQuery(Base)
            watcher = New ManagementEventWatcher(query)
            watcher.Start()
        End Sub

        Dim WithEvents Watcher As ManagementEventWatcher

        Delegate Sub UpdateBKDelegate(ByVal [Image] As Image)
        Private Sub UpdateBK(ByVal [Image] As Image)
            MainFrm.pnl_preview.BackgroundImage = [Image]
            dragPreviewer.pnl_preview.BackgroundImage = [Image]
        End Sub

        Sub watcher_EventArrived(sender As Object, e As EventArrivedEventArgs) Handles watcher.EventArrived
            Wallpaper = ResizeImage(GetCurrentWallpaper(), 528, 297)
            Dim updateBkX As UpdateBKDelegate = AddressOf UpdateBK
            MainForm.Invoke(updateBkX, Wallpaper)
        End Sub


        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            Wallpaper = ResizeImage(My.Application.GetCurrentWallpaper(), 528, 297)

            RegMon("Control Panel\Desktop", "WallPaper")
            'RegMon("Control Panel\Colors", "Background")

            _Settings = New XeSettings(XeSettings.Mode.Registry)

            If _Settings.AutoAddExt Then
                CreateFileAssociation(".wpth", "WinPaletter.ThemeFile", "WinPaletter Theme File", """" & Assembly.GetExecutingAssembly().Location & """")
                CreateFileAssociation(".wpsf", "WinPaletter.SettingsFile", "WinPaletter Settings File", """" & Assembly.GetExecutingAssembly().Location & """")
            End If

            AnimatorX = New AnimatorNS.Animator With {.Interval = 1, .TimeStep = 0.07, .DefaultAnimation = AnimatorNS.Animation.Transparent, .AnimationType = AnimatorNS.AnimationType.Transparent}

            ExternalLink = False
            ExternalLink_File = ""

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
                        If My.Application._Settings.OpeningPreviewInApp_or_AppliesIt Then
                            ExternalLink = True
                            ExternalLink_File = arg
                        Else
                            Dim CPx As New CP(CP.Mode.File, arg)
                            CPx.Save(CP.SavingMode.Registry, arg)
                            RestartExplorer()
                            Process.GetCurrentProcess.Kill()
                        End If
                        '''''''
                    End If

                    If My.Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpsf" Then
                        SettingsX._External = True
                        SettingsX._File = arg
                        SettingsX.ShowDialog()
                        Process.GetCurrentProcess.Kill()
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
                        If My.Application._Settings.OpeningPreviewInApp_or_AppliesIt Then
                            If Not MainFrm.CP.GetHashCode = MainFrm.CP_Original.GetHashCode Then
                                Select Case MsgBox("Current Palette Changed. Do you want to save the palette as a theme file (for the program)?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
                                    Case MsgBoxResult.Yes
                                        If Not IO.File.Exists(MainFrm.SaveFileDialog1.FileName) Then
                                            If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                MainFrm.CP_Original = MainFrm.CP
                                                MainFrm.CP.Save(CP.SavingMode.File, MainFrm.SaveFileDialog1.FileName)
                                            Else
                                                Exit Sub
                                            End If
                                        Else
                                            MainFrm.CP_Original = MainFrm.CP
                                            MainFrm.CP.Save(CP.SavingMode.File, MainFrm.SaveFileDialog1.FileName)
                                        End If

                                    Case MsgBoxResult.Cancel
                                        Exit Sub
                                End Select
                            End If

                            MainFrm.CP = New CP(CP.Mode.File, arg)
                            MainFrm.OpenFileDialog1.FileName = arg
                            MainFrm.SaveFileDialog1.FileName = arg
                            MainFrm.ApplyCPValues(MainFrm.CP)
                            MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)

                        Else
                            MainFrm.CP = New CP(CP.Mode.File, arg)
                            MainFrm.OpenFileDialog1.FileName = arg
                            MainFrm.SaveFileDialog1.FileName = arg
                            MainFrm.ApplyCPValues(MainFrm.CP)
                            MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)
                            MainFrm.CP.Save(CP.SavingMode.Registry, arg)
                            RestartExplorer()
                            '''''''
                        End If
                    End If

                    If My.Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpsf" Then
                        SettingsX._External = True
                        SettingsX._File = arg
                        SettingsX.Show()
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
