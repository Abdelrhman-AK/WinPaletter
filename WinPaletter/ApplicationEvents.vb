Imports System.ComponentModel
Imports System.Drawing.Text
Imports System.IO
Imports System.IO.Compression
Imports System.Management
Imports System.Net
Imports System.Reflection
Imports System.Security.Principal
Imports System.Threading
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Imports WinPaletter.PreviewHelpers
Imports WinPaletter.Reg_IO
Imports WinPaletter.XenonCore

Namespace My
    Module Env
        Public ReadOnly PATH_appData As String = IO.Directory.GetParent(Windows.Forms.Application.LocalUserAppDataPath).FullName
        Public ReadOnly PATH_Windows As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows).Replace("WINDOWS", "Windows")
        Public ReadOnly PATH_explorer As String = PATH_Windows & "\explorer.exe"
        Public ReadOnly PATH_ProgramFiles As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        Public ReadOnly PATH_System32 As String = PATH_Windows & "\System32"
        Public ReadOnly PATH_imageres As String = PATH_System32 & "\imageres.dll"
        Public ReadOnly PATH_UserProfile As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        Public ReadOnly PATH_TerminalJSON As String = PATH_UserProfile & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
        Public ReadOnly PATH_TerminalPreviewJSON As String = PATH_UserProfile & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
        Public ReadOnly PATH_PS86_reg As String = "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe"
        Public ReadOnly PATH_PS86_app As String = PATH_Windows & "\System32\WindowsPowerShell\v1.0"
        Public ReadOnly PATH_PS64_reg As String = "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe"
        Public ReadOnly PATH_PS64_app As String = PATH_Windows & "\SysWOW64\WindowsPowerShell\v1.0"
        Public ReadOnly PATH_StoreCache As String = PATH_appData & "\Store"
        Public ReadOnly PATH_ThemeResPackCache As String = PATH_appData & "\ThemeResPack_Cache"
        Public ReadOnly PATH_CursorsWP As String = PATH_appData & "\Cursors"
        Public ReadOnly AppVersion As String = My.Application.Info.Version.ToString

        Public ReadOnly _ignore As StringComparison = StringComparison.OrdinalIgnoreCase
        Public VS As String = PATH_appData & "\VisualStyles\Luna\luna.theme"
        Public resVS As VisualStylesRes

        Public ReadOnly DefaultAccent As Color = Color.FromArgb(0, 81, 210)
        Public ReadOnly DefaultBackColorDark As Color = Color.FromArgb(25, 25, 25)
        Public ReadOnly DefaultBackColorLight As Color = Color.FromArgb(230, 230, 230)

        ''' <summary>
        ''' Class represents colors for WinPaletter Controls (Styles)
        ''' </summary>
        Public Style As New WPStyle(DefaultAccent, DefaultBackColorDark, True)

        ''' <summary>
        ''' A boolean that represents if WinPaletter has started with a classic theme enabled (Loaded at application startup)
        ''' </summary>
        Public StartedWithClassicTheme As Boolean = False

        ''' <summary>
        ''' A boolean that represents if OS is Windows XP
        ''' </summary>
        Public ReadOnly WXP As Boolean = Environment.OSVersion.Version.Major = 5

        ''' <summary>
        ''' A boolean that represents if OS is Windows Vista
        ''' </summary>
        Public ReadOnly WVista As Boolean = Environment.OSVersion.Version.Major = 6 AndAlso Environment.OSVersion.Version.Minor = 0

        ''' <summary>
        ''' A boolean that represents if OS is Windows 7 or not
        ''' </summary>
        Public ReadOnly W7 As Boolean = Environment.OSVersion.Version.Major = 6 AndAlso Environment.OSVersion.Version.Minor = 1

        ''' <summary>
        ''' A boolean that represents if OS is Windows 8 or not
        ''' </summary>
        Public ReadOnly W8 As Boolean = Environment.OSVersion.Version.Major = 6 AndAlso Environment.OSVersion.Version.Minor = 2

        ''' <summary>
        ''' A boolean that represents if OS is Windows 8.1 or not
        ''' </summary>
        Public ReadOnly W81 As Boolean = Environment.OSVersion.Version.Major = 6 AndAlso Environment.OSVersion.Version.Minor = 3

        ''' <summary>
        ''' A boolean that represents if OS is Windows 10 or not
        ''' </summary>
        Public ReadOnly W10 As Boolean = Environment.OSVersion.Version.Major = 10 AndAlso Environment.OSVersion.Version.Minor = 0 AndAlso Environment.OSVersion.Version.Build < 22000

        ''' <summary>
        ''' A boolean that represents if OS is Windows 11 or not
        ''' </summary>
        Public ReadOnly W11 As Boolean = Environment.OSVersion.Version.Major = 10 AndAlso Environment.OSVersion.Version.Minor = 0 AndAlso Environment.OSVersion.Version.Build >= 22000

        ''' <summary>
        ''' A boolean that represents if OS is Windows 12 or not (For near future! :))
        ''' </summary>
        Public ReadOnly W12 As Boolean = Computer.Info.OSFullName.Contains("12")

        ''' <summary>
        ''' A boolean that represents if OS is Windows 10 (19H2=1909) or higher or not at all
        ''' </summary>
        Public ReadOnly W10_1909 As Boolean = (W12 OrElse W11 OrElse (W10 AndAlso GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", 0).ToString() >= 1909))

        ''' <summary>
        ''' A boolean that represents if OS is Windows 11 Build 22523 or higher or not at all
        ''' </summary>
        Public ReadOnly W11_22523 As Boolean = (W12 OrElse (W11 AndAlso GetReg("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", 0).ToString() >= 22523))

        ''' <summary>
        ''' A class that represents AnimatorNS
        ''' </summary>
        Public WithEvents Animator As AnimatorNS.Animator

        ''' <summary>
        ''' A class that represents WinPaletter's Settings
        ''' </summary>
        Public [Settings] As New XeSettings(XeSettings.Mode.Registry)

        ''' <summary>
        ''' A class that represents WinPaletter's Language Strings (Loaded at application startup)
        ''' </summary>
        Public Lang As New Localizer

        ''' <summary>
        ''' Current applied wallpaper
        ''' </summary>
        Public Wallpaper, Wallpaper_Unscaled As Bitmap

        ''' <summary>
        ''' List of exceptions thrown during theme applying
        ''' </summary>
        Public Saving_Exceptions As New List(Of Tuple(Of String, Exception))

        ''' <summary>
        ''' List of exceptions thrown during theme loading
        ''' </summary>
        Public Loading_Exceptions As New List(Of Tuple(Of String, Exception))

        ''' <summary>
        ''' Get if Application is started as administrator or not
        ''' </summary>
        Public ReadOnly isElevated As Boolean = New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator)

        ''' <summary>
        ''' ImageList for Notifications mini-icons (Loaded at application startup)
        ''' </summary>
        Public Notifications_IL As New ImageList With {.ImageSize = New Size(20, 20), .ColorDepth = ColorDepth.Depth32Bit}

        ''' <summary>
        ''' ImageList for Languages Nodes (Loaded at application startup)
        ''' </summary>
        Public Lang_IL As New ImageList With {.ImageSize = New Size(16, 16), .ColorDepth = ColorDepth.Depth32Bit}

        ''' <summary>
        ''' A class that contains info about ExplorerPatcher settings
        ''' </summary>
        Public EP As New ExplorerPatcher

        ''' <summary>
        ''' Variable responsible for the preview type on forms
        ''' </summary>
        Public PreviewStyle As WindowStyle = WindowStyle.W11

        ''' <summary>
        ''' Gets if WinPaletter's current version is designed as Beta or not
        ''' <br>Don't forget to make it <b>True</b> when you design a beta one</br>
        ''' </summary>
        Public ReadOnly IsBeta As Boolean = False

        ''' <summary>
        ''' CP is a short name for Color Palette (It was intentionally for Colors only in WinPaletter 1.0.0.0, not it include various parameters (not colors only)
        ''' </summary>
        Public CP, CP_Original, CP_FirstTime, CP_BeforeDrag As CP

        ''' <summary>
        ''' Used to make custom controls follow CP's font smoothing
        ''' </summary>
        Public RenderingHint As TextRenderingHint = TextRenderingHint.SystemDefault
    End Module

    Partial Friend Class MyApplication
#Region "   Invoking Region"
        Implements ISynchronizeInvoke

        Private ReadOnly _currentContext As SynchronizationContext = SynchronizationContext.Current
        Private ReadOnly _invokeLocker As New Object()

        Private ReadOnly Property ISynchronizeInvoke_InvokeRequired As Boolean Implements ISynchronizeInvoke.InvokeRequired
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        <Obsolete("This method Is Not supported!", True)>
        Private Function ISynchronizeInvoke_BeginInvoke(method As [Delegate], args() As Object) As IAsyncResult Implements ISynchronizeInvoke.BeginInvoke
            Throw New NotSupportedException("The method Or operation Is Not implemented.")
        End Function

        <Obsolete("This method Is Not supported!", True)>
        Private Function ISynchronizeInvoke_EndInvoke(result As IAsyncResult) As Object Implements ISynchronizeInvoke.EndInvoke
            Throw New NotSupportedException("The method Or operation Is Not implemented.")
        End Function

        Private Function Invoke(method As [Delegate], args() As Object) As Object Implements ISynchronizeInvoke.Invoke
            If method Is Nothing Then
                Throw New ArgumentNullException("method")
            End If

            SyncLock _invokeLocker
                Dim objectToGet As Object = Nothing
                Dim invoker As New SendOrPostCallback(Function(ByVal data As Object)
                                                          Return objectToGet = method.DynamicInvoke(args)
                                                      End Function)
                _currentContext.Send(invoker, method.Target)
                Return objectToGet
            End SyncLock
        End Function

        Public Function Invoke(ByVal method As [Delegate]) As Object
            Return Invoke(method, Nothing)
        End Function
#End Region

#Region "   Variables"
        Private WallMon_Watcher1, WallMon_Watcher2, WallMon_Watcher3, WallMon_Watcher4 As ManagementEventWatcher
        ReadOnly UpdateDarkModeInvoker As MethodInvoker = CType(Sub()
                                                                    FetchDarkMode()
                                                                    If [Settings].Appearance.AutoDarkMode Then ApplyStyle()
                                                                End Sub, MethodInvoker)
        Public ReadOnly UpdateWallpaperInvoker As MethodInvoker = CType(Sub()
                                                                            Dim Wall As Bitmap = FetchSuitableWallpaper(CP, PreviewStyle)
                                                                            MainFrm.pnl_preview.BackgroundImage = Wall
                                                                            MainFrm.pnl_preview_classic.BackgroundImage = Wall
                                                                            Metrics_Fonts.pnl_preview1.BackgroundImage = Wall
                                                                            Metrics_Fonts.pnl_preview2.BackgroundImage = Wall
                                                                            Metrics_Fonts.pnl_preview3.BackgroundImage = Wall
                                                                            Metrics_Fonts.pnl_preview4.BackgroundImage = Wall
                                                                            Metrics_Fonts.Classic_Preview1.BackgroundImage = Wall
                                                                            Metrics_Fonts.Classic_Preview3.BackgroundImage = Wall
                                                                            Metrics_Fonts.Classic_Preview4.BackgroundImage = Wall
                                                                            AltTabEditor.pnl_preview1.BackgroundImage = Wall
                                                                            AltTabEditor.Classic_Preview1.BackgroundImage = Wall
                                                                            MainFrm.pnl_preview.Invalidate()
                                                                            Metrics_Fonts.pnl_preview1.Invalidate()
                                                                            Metrics_Fonts.pnl_preview2.Invalidate()
                                                                            Metrics_Fonts.pnl_preview3.Invalidate()
                                                                            Metrics_Fonts.pnl_preview4.Invalidate()
                                                                            Metrics_Fonts.Classic_Preview1.Invalidate()
                                                                            Metrics_Fonts.Classic_Preview3.Invalidate()
                                                                            Metrics_Fonts.Classic_Preview4.Invalidate()
                                                                            AltTabEditor.pnl_preview1.Invalidate()
                                                                            AltTabEditor.Classic_Preview1.Invalidate()
                                                                        End Sub, MethodInvoker)
        Public ReadOnly processKiller As New Process With {.StartInfo = New ProcessStartInfo With {
                            .FileName = PATH_System32 & "\taskkill.exe",
                            .Verb = If(Not WXP, "runas", ""),
                            .Arguments = "/F /IM explorer.exe",
                            .WindowStyle = ProcessWindowStyle.Hidden,
                            .UseShellExecute = True}
                           }
        Public ReadOnly processExplorer As New Process With {.StartInfo = New ProcessStartInfo With {
                            .FileName = PATH_explorer,
                            .Arguments = "",
                            .Verb = If(Not W81 And Not W8 And Not WXP, "runas", ""),
                            .WindowStyle = ProcessWindowStyle.Normal,
                            .UseShellExecute = True}
                           }

        Public ExternalLink As Boolean = False
        Public ExternalLink_File As String = ""

        Public CopiedColor As Color = Nothing
        Public ColorEvent As MenuEvent = MenuEvent.None

        Public ConsoleFont As New Font("Lucida Console", 7.5)
        Public ConsoleFontMedium As New Font("Lucida Console", 9)
        Public ConsoleFontLarge As New Font("Lucida Console", 10)

        Public ExitAfterException As Boolean = False
        Public ShowWhatsNew As Boolean = False

        Public ArgsList As New List(Of String)
        Enum MenuEvent
            None
            Copy
            Cut
            Paste
            Override
            Delete
        End Enum
#End Region

#Region "   File Association And Uninstall"

        ''' <summary>
        ''' Associate WinPaletter Files Types in Registry
        ''' </summary>
        ''' <param name="extension">Extension is the file type to be registered (eg ".wpth")</param>
        ''' <param name="className">ClassName is the name of the associated class (eg "WinPaletter.ThemeFile")</param>
        ''' <param name="description">Textual description (eg "WinPaletter ThemeFile")</param>
        ''' <param name="exeProgram">ExeProgram is the app that manages that extension (eg. Assembly.GetExecutingAssembly().Location)</param>
        Sub CreateFileAssociation(ByVal extension As String, ByVal className As String, ByVal description As String, iconPath As String, ByVal exeProgram As String)

            If extension.Substring(0, 1) <> "." Then extension = "." & extension
            If exeProgram.Contains("""") Then exeProgram = exeProgram.Replace("""", "")
            exeProgram = String.Format("""{0}""", exeProgram)

            Dim mainKey, descriptionKey As RegistryKey
            mainKey = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
            descriptionKey = Registry.CurrentUser.OpenSubKey("Software", True).CreateSubKey("WinPaletter", True)

            Try
                mainKey.CreateSubKey(extension, True).SetValue("", className)
                mainKey.CreateSubKey(className, True).SetValue("", description)
                mainKey.CreateSubKey(className & "\Shell\Open", True).SetValue("Icon", exeProgram.Replace("""", "") & ", 0")
                mainKey.CreateSubKey(className & "\Shell\Open\Command", True).SetValue("", exeProgram & " ""%1""")

                If className.ToLower = "WinPaletter.ThemeFile".ToLower Then
                    mainKey.CreateSubKey(className & "\Shell\Edit In WinPaletter\Command", True).SetValue("", exeProgram & "  /edit:""%1""")
                    mainKey.CreateSubKey(className & "\Shell\Apply by WinPaletter\Command", True).SetValue("", exeProgram & "  /apply:""%1""")
                End If

                mainKey.CreateSubKey(className & "\DefaultIcon", True).SetValue("", iconPath)

                With descriptionKey
                    .SetValue("DisplayName", Application.Info.ProductName)
                    .SetValue("Publisher", Application.Info.CompanyName)
                    .SetValue("Version", Application.Info.Version.ToString)
                End With

            Catch e As Exception

            Finally
                If mainKey IsNot Nothing Then mainKey.Close()
                If descriptionKey IsNot Nothing Then descriptionKey.Close()
            End Try

            'Notify Windows that file associations have changed
            NativeMethods.Shell32.SHChangeNotify(NativeMethods.Shell32.SHCNE_ASSOCCHANGED, NativeMethods.Shell32.SHCNF_IDLIST, 0, 0)
        End Sub

        ''' <summary>
        ''' Removes WinPaletter Files Types Associate From Registry
        ''' </summary>
        ''' <param name="extension">Extension is the file type to be removed (eg ".wpth")</param>
        ''' <param name="className">ClassName is the name of the associated class to be removed (eg "WinPaletter.ThemeFile")</param>
        Sub DeleteFileAssociation(ByVal extension As String, ByVal className As String)

            If extension.Substring(0, 1) <> "." Then extension = "." & extension

            Dim mainKey, descriptionKey As RegistryKey
            mainKey = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
            descriptionKey = Registry.CurrentUser.OpenSubKey("Software\WinPaletter", True)

            Try
                mainKey.DeleteSubKeyTree(extension, False)
                mainKey.DeleteSubKeyTree(className, False)

                descriptionKey.DeleteValue("DisplayName", False)
                descriptionKey.DeleteValue("Publisher", False)
                descriptionKey.DeleteValue("Version", False)

            Catch e As Exception

            Finally
                If mainKey IsNot Nothing Then mainKey.Close()
                If descriptionKey IsNot Nothing Then descriptionKey.Close()
            End Try

            'Notify Windows that file associations have changed
            NativeMethods.Shell32.SHChangeNotify(NativeMethods.Shell32.SHCNE_ASSOCCHANGED, NativeMethods.Shell32.SHCNF_IDLIST, 0, 0)
        End Sub
        Sub CreateUninstaller()
            Dim guidText As String = Application.Info.ProductName
            Dim RegPath As String = "Software\Microsoft\Windows\CurrentVersion\Uninstall\" & guidText
            Dim exe As String = Assembly.GetExecutingAssembly().Location

            If Not IO.Directory.Exists(PATH_appData) Then IO.Directory.CreateDirectory(PATH_appData)
            IO.File.WriteAllBytes(PATH_appData & "\uninstall.ico", Resources.Icon_Uninstall.ToByteArray)

            If Registry.CurrentUser.OpenSubKey(RegPath, True) Is Nothing Then Registry.CurrentUser.CreateSubKey(RegPath, True)

            With Registry.CurrentUser.OpenSubKey(RegPath, True)
                .SetValue("DisplayName", "WinPaletter", RegistryValueKind.String)
                .SetValue("ApplicationVersion", Application.Info.Version.ToString, RegistryValueKind.String)
                .SetValue("DisplayVersion", Application.Info.Version.ToString, RegistryValueKind.String)
                .SetValue("Publisher", Application.Info.CompanyName, RegistryValueKind.String)
                .SetValue("DisplayIcon", PATH_appData & "\uninstall.ico", RegistryValueKind.String)
                .SetValue("URLInfoAbout", Resources.Link_Repository, RegistryValueKind.String)
                .SetValue("Contact", Resources.Link_Repository, RegistryValueKind.String)
                .SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"), RegistryValueKind.String)
                .SetValue("Comments", "This will help you delete WinPaletter and clean up its used data", RegistryValueKind.String)
                .SetValue("UninstallString", exe & " /uninstall", RegistryValueKind.String)
                .SetValue("QuietUninstallString", exe & " /uninstall-quiet", RegistryValueKind.String)
                .SetValue("InstallLocation", Application.Info.DirectoryPath, RegistryValueKind.String)
                .SetValue("NoModify", 1, RegistryValueKind.DWord)
                .SetValue("NoRepair", 1, RegistryValueKind.DWord)
                .SetValue("EstimatedSize", Computer.FileSystem.GetFileInfo(exe).Length / 1024, RegistryValueKind.DWord)
            End With
        End Sub
        Sub Uninstall_Quiet()
            DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile")
            DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile")
            DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack")

            Registry.CurrentUser.DeleteSubKeyTree("Software\WinPaletter", False)

            Try
                If Not My.WXP AndAlso IO.File.Exists(My.PATH_appData & "\WindowsStartup_Backup.wav") Then
                    ReplaceResource(My.PATH_imageres, "WAV", If(My.WVista, 5051, 5080), IO.File.ReadAllBytes(My.PATH_appData & "\WindowsStartup_Backup.wav"))
                End If
            Catch
            End Try

            If IO.Directory.Exists(PATH_appData) Then
                IO.Directory.Delete(PATH_appData, True)
                If Not My.WXP Then
                    CP.ResetCursorsToAero()
                    If My.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then CP.ResetCursorsToAero("HKEY_USERS\.DEFAULT")

                Else
                    CP.ResetCursorsToNone_XP()
                    If My.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs = XeSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite Then CP.ResetCursorsToNone_XP("HKEY_USERS\.DEFAULT")

                End If
            End If

            Dim guidText As String = Application.Info.ProductName
            Dim RegPath As String = "Software\Microsoft\Windows\CurrentVersion\Uninstall"
            Registry.CurrentUser.OpenSubKey(RegPath, True).DeleteSubKeyTree(guidText, False)

            Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
        End Sub
#End Region

#Region "   Wallpaper and User Preferences System"
        Private Sub FetchStockWallpaper(Size As Size)
            Using wall_New As New Bitmap(CType(GetWallpaper().Clone, Bitmap))
                Wallpaper_Unscaled = wall_New.Clone
                Wallpaper = wall_New.GetThumbnailImage(Computer.Screen.Bounds.Width, Computer.Screen.Bounds.Height, Nothing, IntPtr.Zero)
            End Using
        End Sub
        Public Function FetchSuitableWallpaper(CP As CP, PreviewConfig As WindowStyle) As Bitmap
            Using picbox As New PictureBox With {.Size = MainFrm.pnl_preview.Size, .BackColor = CP.Win32.Background}
                Dim Wall As Bitmap

                If Not CP.Wallpaper.Enabled Then
                    FetchStockWallpaper(picbox.Size)
                    Wall = Wallpaper
                Else
                    Dim condition0 As Boolean = PreviewConfig = WindowStyle.W11 And CP.WallpaperTone_W11.Enabled
                    Dim condition1 As Boolean = PreviewConfig = WindowStyle.W10 And CP.WallpaperTone_W10.Enabled
                    Dim condition2 As Boolean = PreviewConfig = WindowStyle.W81 And CP.WallpaperTone_W81.Enabled
                    Dim condition3 As Boolean = PreviewConfig = WindowStyle.W7 And CP.WallpaperTone_W7.Enabled
                    Dim condition4 As Boolean = PreviewConfig = WindowStyle.WVista And CP.WallpaperTone_WVista.Enabled
                    Dim condition5 As Boolean = PreviewConfig = WindowStyle.WXP And CP.WallpaperTone_WXP.Enabled
                    Dim condition As Boolean = condition0 OrElse condition1 OrElse condition2 OrElse condition3 OrElse condition4 OrElse condition5

                    If condition Then
                        Select Case PreviewConfig
                            Case WindowStyle.W11
                                Wall = GetTintedWallpaper(CP.WallpaperTone_W11)

                            Case WindowStyle.W10
                                Wall = GetTintedWallpaper(CP.WallpaperTone_W10)

                            Case WindowStyle.W81
                                Wall = GetTintedWallpaper(CP.WallpaperTone_W81)

                            Case WindowStyle.W7
                                Wall = GetTintedWallpaper(CP.WallpaperTone_W7)

                            Case WindowStyle.WVista
                                Wall = GetTintedWallpaper(CP.WallpaperTone_WVista)

                            Case WindowStyle.WXP
                                Wall = GetTintedWallpaper(CP.WallpaperTone_WXP)

                            Case Else
                                Wall = GetTintedWallpaper(CP.WallpaperTone_W11)

                        End Select
                    Else

                        If CP.Wallpaper.WallpaperType = CP.Structures.Wallpaper.WallpaperTypes.Picture Then
                            If IO.File.Exists(CP.Wallpaper.ImageFile) Then
                                Wall = Bitmap_Mgr.Load(CP.Wallpaper.ImageFile)
                            Else
                                FetchStockWallpaper(picbox.Size)
                                Wall = Wallpaper
                            End If

                        ElseIf CP.Wallpaper.WallpaperType = CP.Structures.Wallpaper.WallpaperTypes.SolidColor Then
                            Wall = Nothing

                        ElseIf CP.Wallpaper.WallpaperType = CP.Structures.Wallpaper.WallpaperTypes.SlideShow Then

                            If CP.Wallpaper.SlideShow_Folder_or_ImagesList Then
                                Dim ls As String() = Directory.EnumerateFiles(CP.Wallpaper.Wallpaper_Slideshow_ImagesRootPath, "*.*", SearchOption.TopDirectoryOnly).Where(Function(s)
                                                                                                                                                                               Return s.EndsWith(".bmp") _
                                                                                                                                                                                    OrElse s.EndsWith(".jpg") _
                                                                                                                                                                                    OrElse s.EndsWith(".png") _
                                                                                                                                                                                    OrElse s.EndsWith(".gif")
                                                                                                                                                                           End Function).ToArray
                                If ls.Count > 0 AndAlso IO.File.Exists(ls(0)) Then
                                    Wall = Bitmap_Mgr.Load(ls(0))

                                Else
                                    FetchStockWallpaper(picbox.Size)
                                    Wall = Wallpaper
                                End If

                            Else
                                If CP.Wallpaper.Wallpaper_Slideshow_Images.Count > 0 AndAlso IO.File.Exists(CP.Wallpaper.Wallpaper_Slideshow_Images(0)) Then
                                    Wall = Bitmap_Mgr.Load(CP.Wallpaper.Wallpaper_Slideshow_Images(0))
                                Else
                                    FetchStockWallpaper(picbox.Size)
                                    Wall = Wallpaper
                                End If
                            End If
                        Else
                            FetchStockWallpaper(picbox.Size)
                            Wall = Wallpaper
                        End If
                    End If
                End If

                If Wall IsNot Nothing Then

                    Dim ScaleW As Single = 1
                    Dim ScaleH As Single = 1

                    If Wall.Width > Screen.PrimaryScreen.Bounds.Size.Width Or Wall.Height > Screen.PrimaryScreen.Bounds.Size.Height Then
                        ScaleW = 1920 / picbox.Size.Width
                        ScaleH = 1080 / picbox.Size.Height
                    End If

                    Wall = Wall.Resize(Wall.Width / ScaleW, Wall.Height / ScaleH)

                    If CP.Wallpaper.WallpaperStyle = CP.Structures.Wallpaper.WallpaperStyles.Fill Then
                        picbox.SizeMode = PictureBoxSizeMode.CenterImage
                        Wall = DirectCast(Wall.Clone, Bitmap).FillScale(picbox.Size)

                    ElseIf CP.Wallpaper.WallpaperStyle = CP.Structures.Wallpaper.WallpaperStyles.Fit Then
                        picbox.SizeMode = PictureBoxSizeMode.Zoom

                    ElseIf CP.Wallpaper.WallpaperStyle = CP.Structures.Wallpaper.WallpaperStyles.Stretched Then
                        picbox.SizeMode = PictureBoxSizeMode.StretchImage

                    ElseIf CP.Wallpaper.WallpaperStyle = CP.Structures.Wallpaper.WallpaperStyles.Centered Then
                        picbox.SizeMode = PictureBoxSizeMode.CenterImage

                    ElseIf CP.Wallpaper.WallpaperStyle = CP.Structures.Wallpaper.WallpaperStyles.Tile Then
                        picbox.SizeMode = PictureBoxSizeMode.Normal
                        Wall = DirectCast(Wall.Clone, Bitmap).Tile(picbox.Size)

                    End If

                End If

                picbox.Image = Wall

                Return picbox.ToBitmap
            End Using
        End Function
        Public Function GetWallpaper() As Bitmap

            Dim WallpaperPath As String = GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "")
            Dim WallpaperType As Integer = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", 0)

            If IO.File.Exists(WallpaperPath) AndAlso (WallpaperType <> 1) Then
                Return New Bitmap(Bitmap_Mgr.Load(WallpaperPath).GetThumbnailImage(Computer.Screen.Bounds.Width, Computer.Screen.Bounds.Height, Nothing, IntPtr.Zero))
            Else
                Return GetReg("HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0").ToString.FromWin32RegToColor.ToBitmap(Computer.Screen.Bounds.Size)
            End If

        End Function
        Sub WallpaperType_Changed(sender As Object, e As EventArrivedEventArgs)
            Dim WallpaperType As Integer = GetReg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", 0)
            Dim S As New Stopwatch
            If WallpaperType <> 1 Then
                S.Reset()
                S.Start()
                Do Until IO.File.Exists(GetReg("HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "").ToString())
                    If S.ElapsedMilliseconds > 5000 Then Exit Do
                Loop
                S.Stop()
                Wallpaper_Changed()
            End If
        End Sub
        Sub Monitor()
            Dim currentUser = WindowsIdentity.GetCurrent()
            Dim KeyPath As String
            Dim valueName As String
            Dim Base As String

            KeyPath = "Control Panel\Desktop"
            valueName = "Wallpaper"
            Base = String.Format("SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace("\", "\\"), valueName)
            Dim query1 = New WqlEventQuery(Base)
            WallMon_Watcher1 = New ManagementEventWatcher(query1)

            KeyPath = "Control Panel\Colors"
            valueName = "Background"
            Base = String.Format("SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace("\", "\\"), valueName)
            Dim query2 = New WqlEventQuery(Base)
            WallMon_Watcher2 = New ManagementEventWatcher(query2)

            AddHandler WallMon_Watcher1.EventArrived, AddressOf Wallpaper_Changed
            WallMon_Watcher1.Start()

            AddHandler WallMon_Watcher2.EventArrived, AddressOf Wallpaper_Changed
            WallMon_Watcher2.Start()

            If My.W10 OrElse My.W11 Then
                KeyPath = "Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers"
                valueName = "BackgroundType"
                Base = String.Format("SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace("\", "\\"), valueName)
                Dim query3 = New WqlEventQuery(Base)
                WallMon_Watcher3 = New ManagementEventWatcher(query3)

                KeyPath = "Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"
                valueName = "AppsUseLightTheme"
                Base = String.Format("SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace("\", "\\"), valueName)
                Dim query4 = New WqlEventQuery(Base)
                WallMon_Watcher4 = New ManagementEventWatcher(query4)

                AddHandler WallMon_Watcher3.EventArrived, AddressOf WallpaperType_Changed
                WallMon_Watcher3.Start()

                AddHandler WallMon_Watcher4.EventArrived, AddressOf DarkMode_Changed
                WallMon_Watcher4.Start()

            Else
                AddHandler SystemEvents.UserPreferenceChanged, AddressOf OldWinPreferenceChanged
            End If

        End Sub
        Public Sub OldWinPreferenceChanged(ByVal sender As Object, ByVal e As UserPreferenceChangedEventArgs)
            If WXP AndAlso e.Category = UserPreferenceCategory.General Then
                Wallpaper_Changed()
            Else
                If e.Category = UserPreferenceCategory.Desktop Or e.Category = UserPreferenceCategory.Color Then Wallpaper_Changed()
            End If
        End Sub
        Sub DarkMode_Changed()
            Invoke(UpdateDarkModeInvoker)
        End Sub
        Sub Wallpaper_Changed()
            Invoke(UpdateWallpaperInvoker)
        End Sub
#End Region

#Region "   Application Startup and Shutdown Subs"
        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
            If Not My.WXP Then
                Try
                    WallMon_Watcher1.Stop()
                    WallMon_Watcher2.Stop()

                    If Not W7 And Not W81 And Not My.WVista Then
                        WallMon_Watcher3.Stop()
                        WallMon_Watcher4.Stop()
                    End If
                Catch
                End Try
            End If

            Try
                If IO.File.Exists("oldWinpaletter.trash") Then Kill("oldWinpaletter.trash")
                If IO.File.Exists("oldWinpaletter_2.trash") Then Kill("oldWinpaletter_2.trash")
            Catch : End Try

            RemoveHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf DomainCheck
            RemoveHandler AppDomain.CurrentDomain.UnhandledException, AddressOf Domain_UnhandledException
            RemoveHandler Windows.Forms.Application.ThreadException, AddressOf ThreadExceptionHandler

            Try
                RemoveHandler WallMon_Watcher1.EventArrived, AddressOf Wallpaper_Changed
                RemoveHandler WallMon_Watcher2.EventArrived, AddressOf Wallpaper_Changed
                RemoveHandler WallMon_Watcher3.EventArrived, AddressOf Wallpaper_Changed
                RemoveHandler WallMon_Watcher4.EventArrived, AddressOf Wallpaper_Changed
            Catch
            End Try

            RemoveHandler SystemEvents.UserPreferenceChanged, AddressOf OldWinPreferenceChanged
        End Sub

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup

            Try
                If IO.File.Exists("oldWinpaletter.trash") Then Kill("oldWinpaletter.trash")
                If IO.File.Exists("oldWinpaletter_2.trash") Then Kill("oldWinpaletter_2.trash")
            Catch : End Try

            Animator = New AnimatorNS.Animator With {.Interval = 1, .TimeStep = 0.07, .DefaultAnimation = AnimatorNS.Animation.Transparent, .AnimationType = AnimatorNS.AnimationType.Transparent}

            Try
                AddMemoryFont(Resources.JetBrainsMono_Medium)
                ConsoleFont = GetFont(0, 7.75)
                ConsoleFontMedium = GetFont(0, 9)
                ConsoleFontLarge = GetFont(0, 10)
            Catch
                ConsoleFont = New Font("Lucida Console", 7.5)
                ConsoleFontMedium = New Font("Lucida Console", 9)
                ConsoleFontLarge = New Font("Lucida Console", 10)
            End Try

            If Environment.GetCommandLineArgs.Count > 1 Then
                ArgsList.Clear()
                For x = 1 To Environment.GetCommandLineArgs.Count - 1
                    ArgsList.Add(Environment.GetCommandLineArgs(x))
                Next
            End If

            FetchDarkMode()
            ApplyStyle()

            For Each arg As String In ArgsList
                If arg.ToLower = "/exportlanguage" Then
                    Lang.ExportJSON(String.Format("language-en {0}.{1}.{2} {3}-{4}-{5}.json", Now.Hour, Now.Minute, Now.Second, Now.Day, Now.Month, Now.Year))
                    MsgBox(Lang.LngExported, MsgBoxStyle.Information)
                    Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
                    Exit For

                ElseIf arg.ToLower = "/uninstall" Then
                    Uninstall.ShowDialog()
                    Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
                    Exit For

                ElseIf arg.ToLower = "/uninstall-quiet" Then
                    Uninstall_Quiet()
                    Exit For

                ElseIf arg.StartsWith("/convert:", _ignore) Then
                    CMD_Convert(arg, True)

                ElseIf arg.StartsWith("/convert-list:", _ignore) Then
                    CMD_Convert_List(arg, True)

                End If
            Next

            If [Settings].Language.Enabled Then
                Try
                    Lang.LoadLanguageFromJSON([Settings].Language.File)
                Catch ex As Exception
                    BugReport.ThrowError(ex)
                End Try
            End If

            If Not [Settings].General.LicenseAccepted Then
                If LicenseForm.ShowDialog <> DialogResult.OK Then
                    Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
                End If
            End If

            ExternalLink = False
            ExternalLink_File = ""

            For Each arg As String In ArgsList
                If Not arg.StartsWith("/apply:", _ignore) And Not arg.StartsWith("/edit:", _ignore) And Not arg.StartsWith("/convert:", _ignore) And Not arg.StartsWith("/convert-list:", _ignore) Then

                    If Path.GetExtension(arg).ToLower = ".wpth" Then
                        If [Settings].FileTypeManagement.OpeningPreviewInApp_or_AppliesIt Then
                            ExternalLink = True
                            ExternalLink_File = arg
                        Else
                            Dim CPx As New CP(CP.CP_Type.File, arg)
                            CPx.Save(CP.CP_Type.Registry, arg)
                            If [Settings].ThemeApplyingBehavior.AutoRestartExplorer Then RestartExplorer()
                            Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
                        End If
                    End If

                    If Path.GetExtension(arg).ToLower = ".wpsf" Then
                        SettingsX._External = True
                        SettingsX._File = arg
                        SettingsX.ShowDialog()
                        Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
                    End If

                Else
                    If arg.StartsWith("/apply:", _ignore) Then
                        Dim File As String = arg.Remove(0, "/apply:".Count)
                        File = File.Replace("""", "")
                        If IO.File.Exists(File) Then
                            Dim CPx As New CP(CP.CP_Type.File, File)
                            CPx.Save(CP.CP_Type.Registry)
                            If [Settings].ThemeApplyingBehavior.AutoRestartExplorer Then RestartExplorer()
                            Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
                        End If

                    ElseIf arg.StartsWith("/edit:", _ignore) Then
                        Dim File As String = arg.Remove(0, "/edit:".Count)
                        File = File.Replace("""", "")
                        ExternalLink = True
                        ExternalLink_File = File
                    End If

                End If
            Next

            If Not My.WXP Then
                Try
                    Monitor()
                Catch ex As Exception
                    If MsgBox(My.Lang.MonitorIssue, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, My.Lang.MonitorIssue2 & vbCrLf & My.Lang.CP_RestoreCursorsErrorPressOK) = MsgBoxResult.Ok Then
                        BugReport.ThrowError(ex)
                    End If
                End Try
            Else
                AddHandler SystemEvents.UserPreferenceChanged, AddressOf OldWinPreferenceChanged
            End If

            Try
                If [Settings].FileTypeManagement.AutoAddExt Then
                    If Not IO.Directory.Exists(PATH_appData) Then IO.Directory.CreateDirectory(PATH_appData)

                    IO.File.WriteAllBytes(PATH_appData & "\fileextension.ico", Resources.fileextension.ToByteArray)
                    IO.File.WriteAllBytes(PATH_appData & "\settingsfile.ico", Resources.settingsfile.ToByteArray)
                    IO.File.WriteAllBytes(PATH_appData & "\themerespack.ico", Resources.ThemesResIcon.ToByteArray)

                    CreateFileAssociation(".wpth", "WinPaletter.ThemeFile", "WinPaletter Theme File", PATH_appData & "\fileextension.ico", Assembly.GetExecutingAssembly().Location)
                    CreateFileAssociation(".wpsf", "WinPaletter.SettingsFile", "WinPaletter Settings File", PATH_appData & "\settingsfile.ico", Assembly.GetExecutingAssembly().Location)
                    CreateFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack", "WinPaletter Theme Resources Pack", PATH_appData & "\themerespack.ico", Assembly.GetExecutingAssembly().Location)

                End If
            Catch
            End Try

            Notifications_IL.Images.Add("info", Resources.notify_info)
            Notifications_IL.Images.Add("apply", Resources.notify_applying)
            Notifications_IL.Images.Add("error", Resources.notify_error)
            Notifications_IL.Images.Add("warning", Resources.notify_warning)
            Notifications_IL.Images.Add("time", Resources.notify_time)
            Notifications_IL.Images.Add("success", Resources.notify_success)
            Notifications_IL.Images.Add("skip", Resources.notify_skip)
            Notifications_IL.Images.Add("admin", Resources.notify_administrator)

            Lang_IL.Images.Add("main", Resources.LangNode_Main)
            Lang_IL.Images.Add("value", Resources.LangNode_Value)
            Lang_IL.Images.Add("json", Resources.LangNode_JSON)

            Saving_Exceptions.Clear()
            Loading_Exceptions.Clear()

            If W7 Or WVista Or WXP Then
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
            End If

            'Detects if WinPaletter started with Classic Theme
            Dim vsFile As New Text.StringBuilder(260)
            Dim colorName As New Text.StringBuilder(260)
            Dim sizeName As New Text.StringBuilder(260)
            NativeMethods.UxTheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260)
            My.StartedWithClassicTheme = String.IsNullOrEmpty(vsFile.ToString)

            Try
                If Not IO.Directory.Exists(PATH_appData & "\VisualStyles\Luna") Then IO.Directory.CreateDirectory(PATH_appData & "\VisualStyles\Luna")
                IO.File.WriteAllBytes(PATH_appData & "\VisualStyles\Luna\Luna.zip", Resources.luna)
                Using s As New IO.FileStream(PATH_appData & "\VisualStyles\Luna\Luna.zip", IO.FileMode.Open, IO.FileAccess.Read)
                    Using z As New ZipArchive(s, ZipArchiveMode.Read)
                        For Each entry As ZipArchiveEntry In z.Entries
                            If entry.FullName.Contains("\") Then
                                Dim dest As String = Path.Combine(PATH_appData & "\VisualStyles\Luna", entry.FullName)
                                Dim dest_dir As String = dest.Replace("\" & dest.Split("\").Last, "")
                                If Not IO.Directory.Exists(dest_dir) Then IO.Directory.CreateDirectory(dest_dir)
                            End If
                            entry.ExtractToFile(Path.Combine(PATH_appData & "\VisualStyles\Luna", entry.FullName), True)
                        Next
                    End Using
                    s.Close()
                End Using
                IO.File.WriteAllText(PATH_appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", PATH_appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
            Catch ex As Exception
                BugReport.ThrowError(ex)
            End Try

            'Backup Windows Startup sound
            Try
                If Not My.WXP AndAlso Not IO.File.Exists(My.PATH_appData & "\WindowsStartup_Backup.wav") Then
                    Dim SoundBytes As Byte() = PE.GetResource(My.PATH_imageres, "WAVE", If(My.WVista, 5051, 5080))
                    IO.File.WriteAllBytes(My.PATH_appData & "\WindowsStartup_Backup.wav", SoundBytes)
                End If
            Catch ex As Exception
                BugReport.ThrowError(ex)
            End Try

#Region "WhatsNew"
            If Not [Settings].General.WhatsNewRecord.Contains(Application.Info.Version.ToString) Then
                '### Pop up WhatsNew
                ShowWhatsNew = True

                Dim ver As New List(Of String)
                ver.Clear()
                ver.Add(Application.Info.Version.ToString)

                For Each X As String In [Settings].General.WhatsNewRecord.ToArray()
                    ver.Add(X)
                Next

                ver = ver.DeDuplicate
                [Settings].General.WhatsNewRecord = ver.ToArray
                [Settings].General.Save()
            Else
                ShowWhatsNew = False
            End If
#End Region

            CreateUninstaller()

            If My.W11 Then PreviewStyle = WindowStyle.W11
            If My.W10 Then PreviewStyle = WindowStyle.W10
            If My.W81 Then PreviewStyle = WindowStyle.W81
            If My.W8 Then PreviewStyle = WindowStyle.W81
            If My.W7 Then PreviewStyle = WindowStyle.W7
            If My.WVista Then PreviewStyle = WindowStyle.WVista
            If My.WXP Then PreviewStyle = WindowStyle.WXP

            'Load CP
            If Not My.Application.ExternalLink Then
                My.CP = New CP(CP.CP_Type.Registry)
            Else
                My.CP = New CP(CP.CP_Type.File, My.Application.ExternalLink_File)
                MainFrm.OpenFileDialog1.FileName = My.Application.ExternalLink_File
                MainFrm.SaveFileDialog1.FileName = My.Application.ExternalLink_File
                My.Application.ExternalLink = False
                My.Application.ExternalLink_File = ""
            End If

            My.CP_Original = My.CP.Clone
            My.CP_FirstTime = My.CP.Clone
        End Sub

        Private Sub MyApplication_StartupNextInstance(sender As Object, e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            Try
                Dim arg As String = e.CommandLine(0)

                If arg = "" Then
                    e.BringToForeground = True
                Else
                    If arg.ToLower = "/exportlanguage".ToLower Then
                        MsgBox(Lang.LngShouldClose, MsgBoxStyle.Critical)

                    ElseIf arg.ToLower = "/uninstall" Then
                        Uninstall.ShowDialog()

                    ElseIf arg.ToLower = "/uninstall-quiet" Then
                        Uninstall_Quiet()

                    ElseIf arg.StartsWith("/convert:", _ignore) Then
                        CMD_Convert(arg, False)

                    ElseIf arg.StartsWith("/convert-list:", _ignore) Then
                        CMD_Convert_List(arg, False)

                    Else
                        If Not arg.StartsWith("/apply:", _ignore) And Not arg.StartsWith("/edit:", _ignore) Then

                            If Path.GetExtension(arg).ToLower = ".wpth" Then
                                If My.CP <> My.CP_Original Then
                                    If [Settings].ThemeApplyingBehavior.ShowSaveConfirmation Then
                                        Select Case ComplexSave.ShowDialog()
                                            Case DialogResult.Yes
                                                Dim r As String() = [Settings].General.ComplexSaveResult.Split(".")
                                                Dim r1 As String = r(0)
                                                Dim r2 As String = r(1)
                                                Select Case r1
                                                    Case 0              '' Save
                                                        If IO.File.Exists(MainFrm.SaveFileDialog1.FileName) Then
                                                            My.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                            My.CP_Original = My.CP.Clone
                                                        Else
                                                            If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                                My.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                                My.CP_Original = My.CP.Clone
                                                            Else
                                                                Exit Sub
                                                            End If
                                                        End If

                                                    Case 1              '' Save As
                                                        If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                            My.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                            My.CP_Original = My.CP.Clone
                                                        Else
                                                            Exit Sub
                                                        End If
                                                End Select

                                                Select Case r2
                                                    Case 1      '' Apply   ' Case 0= Don't Apply
                                                        MainFrm.Apply_Theme()
                                                End Select

                                            Case DialogResult.No


                                            Case DialogResult.Cancel
                                                Exit Sub
                                        End Select
                                    End If

                                End If

                                My.CP = New CP(CP.CP_Type.File, arg)
                                My.CP_Original = My.CP.Clone
                                MainFrm.OpenFileDialog1.FileName = arg
                                MainFrm.SaveFileDialog1.FileName = arg
                                MainFrm.ApplyCPValues(My.CP)
                                MainFrm.ApplyColorsToElements(My.CP)

                                If Not [Settings].FileTypeManagement.OpeningPreviewInApp_or_AppliesIt Then
                                    MainFrm.Apply_Theme()
                                End If
                            End If

                            If Path.GetExtension(arg).ToLower = ".wpsf" Then
                                SettingsX._External = True
                                SettingsX._File = arg
                                SettingsX.ShowDialog()
                            End If

                        Else
                            If arg.StartsWith("/apply:", _ignore) Then
                                Dim File As String = arg.Remove(0, "/apply:".Count)
                                File = File.Replace("""", "")
                                If IO.File.Exists(File) Then
                                    Dim CPx As New CP(CP.CP_Type.File, File)
                                    CPx.Save(CP.CP_Type.Registry)
                                    If [Settings].ThemeApplyingBehavior.AutoRestartExplorer Then RestartExplorer()
                                End If
                            End If

                            If arg.StartsWith("/edit:", _ignore) Then
                                Dim File As String = arg.Remove(0, "/edit:".Count)
                                File = File.Replace("""", "")

                                If My.CP <> My.CP_Original Then

                                    If [Settings].ThemeApplyingBehavior.ShowSaveConfirmation Then
                                        Select Case ComplexSave.ShowDialog()
                                            Case DialogResult.Yes
                                                Dim r As String() = [Settings].General.ComplexSaveResult.Split(".")
                                                Dim r1 As String = r(0)
                                                Dim r2 As String = r(1)
                                                Select Case r1
                                                    Case 0              '' Save
                                                        If IO.File.Exists(MainFrm.SaveFileDialog1.FileName) Then
                                                            My.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                            My.CP_Original = My.CP.Clone
                                                        Else
                                                            If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                                My.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                                My.CP_Original = My.CP.Clone
                                                            Else
                                                                Exit Sub
                                                            End If
                                                        End If

                                                    Case 1              '' Save As
                                                        If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                            My.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                            My.CP_Original = My.CP.Clone
                                                        Else
                                                            Exit Sub
                                                        End If
                                                End Select

                                                Select Case r2
                                                    Case 1      '' Apply   ' Case 0= Don't Apply
                                                        MainFrm.Apply_Theme()
                                                End Select

                                            Case DialogResult.No


                                            Case DialogResult.Cancel
                                                Exit Sub
                                        End Select
                                    End If

                                End If

                                My.CP = New CP(CP.CP_Type.File, File)
                                My.CP_Original = My.CP.Clone
                                MainFrm.OpenFileDialog1.FileName = File
                                MainFrm.SaveFileDialog1.FileName = File
                                MainFrm.ApplyCPValues(My.CP)
                                MainFrm.ApplyColorsToElements(My.CP)

                            End If
                        End If
                    End If
                End If
            Catch
                e.BringToForeground = True
            End Try
        End Sub

        Sub CMD_Convert(arg As String, KillProcessAfterConvert As Boolean)
            Try
                Dim arr As String() = arg.Remove(0, "/convert:".Count).Split("|")
                Dim Source As String = arr(0)
                Dim Destination As String = arr(1)
                Dim Compress As String = If(My.Settings.FileTypeManagement.CompressThemeFile, "1", "0")
                Dim OldWPTH As String = "0"
                If arr.Count = 3 Then Compress = arr(2)
                If arr.Count = 4 Then OldWPTH = arr(3)

                Dim _Convert As New Converter

                If IO.File.Exists(Source) AndAlso Not _Convert.FetchFile(Source) = Converter_CP.WP_Format.Error Then
                    _Convert.Convert(Source, Destination, Compress = "1", OldWPTH = "1")
                Else
                    MsgBox(My.Lang.Convert_Error_Phrasing, MsgBoxStyle.Critical, Source)
                End If

            Catch ex As Exception
                BugReport.ThrowError(ex)
            End Try

            If KillProcessAfterConvert Then
                Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
            End If
        End Sub

        Sub CMD_Convert_List(arg As String, KillProcessAfterConvert As Boolean)
            Try
                Dim source As String = arg.Remove(0, "/convert-list:".Count)
                Dim _Convert As New Converter

                If IO.File.Exists(source) Then

                    For Each File As String In IO.File.ReadAllLines(source)

                        Dim f As String
                        Dim compress As String = If(My.Settings.FileTypeManagement.CompressThemeFile, "1", "0")
                        Dim OldWPTH As String = "0"

                        If Not String.IsNullOrWhiteSpace(File) Then
                            If Not File.Contains("|") Then
                                f = File.Replace("""", "")
                            Else
                                Dim arr As String() = File.Split("|")
                                f = arr(0).Replace("""", "")
                                If arr.Count = 2 Then compress = arr(1)
                                If arr.Count = 3 Then compress = arr(2)
                            End If

                            Dim FI As New FileInfo(f)
                            Dim Name As String = Path.GetFileNameWithoutExtension(FI.Name)
                            Dim Dir As String = FI.FullName.Replace(FI.FullName.Split("\").Last, "WinPaletterConversion")
                            Dim SaveAs As String = Dir & "\" & Name & ".wpth"

                            If Not _Convert.FetchFile(f) = Converter_CP.WP_Format.Error Then
                                If Not IO.Directory.Exists(Dir) Then IO.Directory.CreateDirectory(Dir)
                                _Convert.Convert(f, SaveAs, compress = "1", OldWPTH = "1")
                            Else
                                MsgBox(My.Lang.Convert_Error_Phrasing, MsgBoxStyle.Critical, f)
                            End If
                        End If

                    Next

                End If

            Catch ex As Exception
                BugReport.ThrowError(ex)
            End Try

            If KillProcessAfterConvert Then
                Using Prc As Process = Process.GetCurrentProcess : Prc.Kill() : End Using
            End If
        End Sub

#End Region

#Region "   Domain (External Resources) and Exceptions Handling"
        Private Function DomainCheck(sender As Object, e As System.ResolveEventArgs) As Assembly
            Return GetAssemblyFromZIP(e.Name)
        End Function

        Function GetAssemblyFromZIP(Name As String) As Assembly
            Name = New AssemblyName(Name).Name

            If Name.StartsWith("WinPaletter.resources", My._ignore) Then Return Nothing

            Dim b As Byte() = Nothing

            Using ms As New MemoryStream(My.Resources.Assemblies)
                Using zip As New ZipArchive(ms)
                    If zip.Entries.Any(Function(entry) entry.Name.EndsWith(Name & ".dll", My._ignore)) Then
                        Using _as As New MemoryStream
                            zip.GetEntry(Name & ".dll").Open().CopyTo(_as)
                            _as.Seek(0, SeekOrigin.Begin)
                            b = _as.ToArray
                        End Using
                    End If
                End Using
            End Using

            If b IsNot Nothing Then
                Return Assembly.Load(b)
            Else
                Return Nothing
            End If

        End Function

        Sub ThreadExceptionHandler(ByVal sender As Object, ByVal e As ThreadExceptionEventArgs)
            BugReport.ThrowError(e.Exception)
        End Sub

        Private Sub SecondChanceExceptionHandler(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            e.ExitApplication = False
            BugReport.ThrowError(e.Exception)
        End Sub

        Private Sub Domain_UnhandledException(sender As Object, e As System.UnhandledExceptionEventArgs)
#If DEBUG Then
            If Not Debugger.IsAttached Then BugReport.ThrowError(CType(e.ExceptionObject, Exception), True)
#Else
            If Not Debugger.IsAttached Then Throw CType(e.ExceptionObject, Exception)
#End If
        End Sub
#End Region
    End Class

End Namespace