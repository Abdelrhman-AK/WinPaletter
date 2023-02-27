Imports System.ComponentModel
Imports System.Management
Imports System.Net
Imports System.Reflection
Imports System.Security.Principal
Imports System.Threading
Imports Microsoft.Win32
Imports WinPaletter.XenonCore
Imports Microsoft.VisualBasic.ApplicationServices
Imports System.Runtime.InteropServices
Imports System.IO.Compression

Namespace My
    Module Env

        Public ReadOnly PATH_Windows As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows).Replace("WINDOWS", "Windows")
        Public ReadOnly PATH_System32 As String = PATH_Windows & "\System32"
        Public ReadOnly PATH_imageres As String = PATH_System32 & "\imageres.dll"
        Public ReadOnly PATH_Windows_UI_Immersive_dll As String = PATH_System32 & "\Windows.UI.Immersive.dll"
        Public ReadOnly PATH_UserProfile As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        Public ReadOnly PATH_TerminalJSON As String = PATH_UserProfile & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
        Public ReadOnly PATH_TerminalPreviewJSON As String = PATH_UserProfile & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"

        Public ReadOnly _strIgnore As StringComparison = StringComparison.OrdinalIgnoreCase
        Public VS As String = My.Application.appData & "\VisualStyles\Luna\luna.theme"
        Public resVS As VisualStylesRes
        Public LunaRes As New Luna(Luna.ColorStyles.Blue)

        ''' <summary>
        ''' Boolean Represents if OS is Windows XP
        ''' </summary>
        Public ReadOnly WXP As Boolean = Computer.Info.OSFullName.ToLower.Contains("xp")

        ''' <summary>
        ''' Boolean Represents if OS is Windows Vista
        ''' </summary>
        Public ReadOnly WVista As Boolean = Computer.Info.OSFullName.ToLower.Contains("vista")

        ''' <summary>
        ''' Boolean Represents if OS is Windows 7 or not
        ''' </summary>
        Public ReadOnly W7 As Boolean = Computer.Info.OSFullName.Contains("7")

        ''' <summary>
        ''' Boolean Represents if OS is Windows 8/8.1 or not
        ''' </summary>
        Public ReadOnly W8 As Boolean = Computer.Info.OSFullName.Contains("8")

        ''' <summary>
        ''' Boolean Represents if OS is Windows 10 or not
        ''' </summary>
        Public ReadOnly W10 As Boolean = Computer.Info.OSFullName.Contains("10")

        ''' <summary>
        ''' Boolean Represents if OS is Windows 11 or not
        ''' </summary>
        Public ReadOnly W11 As Boolean = Computer.Info.OSFullName.Contains("11")

        ''' <summary>
        ''' Boolean Represents if OS is Windows 10 (19H2=1909) and Higher or not
        ''' </summary>
        Public ReadOnly W10_1909 As Boolean = (W11 Or (W10 AndAlso Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", 0).ToString() >= 1909))

        ''' <summary>
        ''' Boolean Represents if OS is Windows 11 Build 22523 and Higher or not
        ''' </summary>
        Public ReadOnly W11_22523 As Boolean = (W11 AndAlso Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", 0).ToString() >= 22523)

        ''' <summary>
        ''' Class Represents AnimatorNS
        ''' </summary>
        Public WithEvents Animator As AnimatorNS.Animator

        ''' <summary>
        ''' Class Represents WinPaletter's Settings
        ''' </summary>
        Public [Settings] As New XeSettings(XeSettings.Mode.Registry)

        ''' <summary>
        ''' Class Represents WinPaletter's Language Strings (Loaded at application startup)
        ''' </summary>
        Public Lang As New Localizer

        ''' <summary>
        ''' Current Applied Wallpaper
        ''' </summary>
        Public Wallpaper As Bitmap

        ''' <summary>
        ''' List of exceptions thrown during theme applying
        ''' </summary>
        Public Saving_Exceptions As New List(Of Tuple(Of String, Exception))

        ''' <summary>
        ''' Class Represents Resources (PNGs, Icons, ...) from Windows extracted from System DLLs (Loaded at application startup)
        ''' </summary>
        Public WinRes As WinResources

        ''' <summary>
        ''' List of installed fonts' names (Loaded at application startup)
        ''' </summary>
        Public FontsList As New List(Of String)

        ''' <summary>
        ''' List of installed monospaced fonts' names (Loaded at application startup)
        ''' </summary>
        Public FontsFixedList As New List(Of String)

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

        <Obsolete("This method is not supported!", True)>
        Private Function ISynchronizeInvoke_BeginInvoke(method As [Delegate], args() As Object) As IAsyncResult Implements ISynchronizeInvoke.BeginInvoke
            Throw New NotSupportedException("The method or operation is not implemented.")
        End Function

        <Obsolete("This method is not supported!", True)>
        Private Function ISynchronizeInvoke_EndInvoke(result As IAsyncResult) As Object Implements ISynchronizeInvoke.EndInvoke
            Throw New NotSupportedException("The method or operation is not implemented.")
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
        Private WithEvents Domain As AppDomain = AppDomain.CurrentDomain
        Private WallMon_Watcher1, WallMon_Watcher2, WallMon_Watcher3, WallMon_Watcher4 As ManagementEventWatcher
        ReadOnly UpdateDarkModeInvoker As MethodInvoker = CType(Sub()
                                                                    If [Settings].Appearance_Auto Then ApplyDarkMode()
                                                                End Sub, MethodInvoker)
        Public ReadOnly UpdateWallpaperInvoker As MethodInvoker = CType(Sub()
                                                                            Dim Wall As Bitmap
                                                                            Try
                                                                                If MainFrm.CP IsNot Nothing Then
                                                                                    Select Case MainFrm.PreviewConfig
                                                                                        Case MainFrm.WinVer.W11
                                                                                            If MainFrm.CP.WallpaperTone_W11.Enabled Then Wall = MainFrm.GetTintedWallpaper(MainFrm.CP.WallpaperTone_W11) Else Wall = Wallpaper

                                                                                        Case MainFrm.WinVer.W10
                                                                                            If MainFrm.CP.WallpaperTone_W10.Enabled Then Wall = MainFrm.GetTintedWallpaper(MainFrm.CP.WallpaperTone_W10) Else Wall = Wallpaper

                                                                                        Case MainFrm.WinVer.W8
                                                                                            If MainFrm.CP.WallpaperTone_W8.Enabled Then Wall = MainFrm.GetTintedWallpaper(MainFrm.CP.WallpaperTone_W8) Else Wall = Wallpaper

                                                                                        Case MainFrm.WinVer.W7
                                                                                            If MainFrm.CP.WallpaperTone_W7.Enabled Then Wall = MainFrm.GetTintedWallpaper(MainFrm.CP.WallpaperTone_W7) Else Wall = Wallpaper

                                                                                        Case MainFrm.WinVer.WVista
                                                                                            If MainFrm.CP.WallpaperTone_WVista.Enabled Then Wall = MainFrm.GetTintedWallpaper(MainFrm.CP.WallpaperTone_WVista) Else Wall = Wallpaper

                                                                                        Case MainFrm.WinVer.WXP
                                                                                            If MainFrm.CP.WallpaperTone_WXP.Enabled Then Wall = MainFrm.GetTintedWallpaper(MainFrm.CP.WallpaperTone_WXP) Else Wall = Wallpaper
                                                                                    End Select
                                                                                Else
                                                                                    Wall = Wallpaper
                                                                                End If
                                                                            Catch
                                                                                Wall = Wallpaper
                                                                            End Try
                                                                            MainFrm.pnl_preview.BackgroundImage = Wall
                                                                            MainFrm.pnl_preview_classic.BackgroundImage = Wall
                                                                            DragPreviewer.pnl_preview.BackgroundImage = Wall
                                                                            DragPreviewer.pnl_preview_classic.BackgroundImage = Wall
                                                                            Metrics_Fonts.pnl_preview1.BackgroundImage = Wall
                                                                            Metrics_Fonts.pnl_preview2.BackgroundImage = Wall
                                                                            Metrics_Fonts.pnl_preview3.BackgroundImage = Wall
                                                                            Metrics_Fonts.pnl_preview4.BackgroundImage = Wall
                                                                            Metrics_Fonts.Classic_Preview1.BackgroundImage = Wall
                                                                            Metrics_Fonts.Classic_Preview3.BackgroundImage = Wall
                                                                            Metrics_Fonts.Classic_Preview4.BackgroundImage = Wall
                                                                            MainFrm.pnl_preview.Invalidate()
                                                                            DragPreviewer.pnl_preview.Invalidate()
                                                                            DragPreviewer.pnl_preview_classic.Invalidate()
                                                                            Metrics_Fonts.pnl_preview1.Invalidate()
                                                                            Metrics_Fonts.pnl_preview2.Invalidate()
                                                                            Metrics_Fonts.pnl_preview3.Invalidate()
                                                                            Metrics_Fonts.pnl_preview4.Invalidate()
                                                                            Metrics_Fonts.Classic_Preview1.Invalidate()
                                                                            Metrics_Fonts.Classic_Preview3.Invalidate()
                                                                            Metrics_Fonts.Classic_Preview4.Invalidate()
                                                                        End Sub, MethodInvoker)

        Public explorerPath As String = String.Format("{0}\{1}", Environment.GetEnvironmentVariable("WINDIR"), "explorer.exe")
        Public appData As String = IO.Directory.GetParent(Windows.Forms.Application.LocalUserAppDataPath).FullName
        Public curPath As String = appData & "\Cursors"
        Public ReadOnly processKiller As New Process With {.StartInfo = New ProcessStartInfo With {
                            .FileName = Environment.GetEnvironmentVariable("WINDIR") & "\System32\taskkill.exe",
                            .Verb = If(Not WXP, "runas", ""),
                            .Arguments = "/F /IM explorer.exe",
                            .WindowStyle = ProcessWindowStyle.Hidden,
                            .UseShellExecute = True}
                           }
        Public ReadOnly processExplorer As New Process With {.StartInfo = New ProcessStartInfo With {
                            .FileName = explorerPath,
                            .Arguments = "",
                            .Verb = If(Not W8 And Not WXP, "runas", ""),
                            .WindowStyle = ProcessWindowStyle.Normal,
                            .UseShellExecute = True}
                           }

        Public ExternalLink As Boolean = False
        Public ExternalLink_File As String = ""

        Public CopiedColor As Color = Nothing
        Public ColorEvent As MenuEvent = MenuEvent.None

        Public ConsoleFont As New Font("Lucida Console", 7.5)
        Public ConsoleFontDef As New Font("Lucida Console", 7.5, FontStyle.Bold)
        Public ConsoleFontLarge As New Font("Lucida Console", 10)
        Public ConsoleFontMedium As New Font("Lucida Console", 9)

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

#Region "   File Association and Uninstall"

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
                    mainKey.CreateSubKey(className & "\Shell\Edit in WinPaletter\Command", True).SetValue("", exeProgram & "  /edit:""%1""")
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

            If Not IO.Directory.Exists(appData) Then IO.Directory.CreateDirectory(appData)
            IO.File.WriteAllBytes(appData & "\uninstall.ico", Resources.Icon_Uninstall.ToByteArray)

            If Registry.CurrentUser.OpenSubKey(RegPath, True) Is Nothing Then Registry.CurrentUser.CreateSubKey(RegPath, True)

            With Registry.CurrentUser.OpenSubKey(RegPath, True)
                .SetValue("DisplayName", "WinPaletter", RegistryValueKind.String)
                .SetValue("ApplicationVersion", Application.Info.Version.ToString, RegistryValueKind.String)
                .SetValue("DisplayVersion", Application.Info.Version.ToString, RegistryValueKind.String)
                .SetValue("Publisher", Application.Info.CompanyName, RegistryValueKind.String)
                .SetValue("DisplayIcon", appData & "\uninstall.ico", RegistryValueKind.String)
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
            Registry.CurrentUser.DeleteSubKeyTree("Software\WinPaletter", False)
            If IO.Directory.Exists(Application.appData) Then
                IO.Directory.Delete(Application.appData, True)
                If Not My.WXP Then
                    CP.ResetCursorsToAero()
                Else
                    CP.ResetCursorsToNone_XP()
                End If
            End If

            Dim guidText As String = Application.Info.ProductName
            Dim RegPath As String = "Software\Microsoft\Windows\CurrentVersion\Uninstall"
            Registry.CurrentUser.OpenSubKey(RegPath, True).DeleteSubKeyTree(guidText, False)

            Process.GetCurrentProcess.Kill()
        End Sub
#End Region

#Region "   Wallpaper and User Preferences System"
        Public Function GetWallpaper() As Bitmap
            'Gets Wallpaper Path
            Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
            Dim WallpaperPath As String = R1.GetValue("Wallpaper").ToString()
            If R1 IsNot Nothing Then R1.Close()

            'Gets Wallpaper Type (Valid only for Windows 10\11)
            Dim WallpaperType As Integer = 0
            If Not W7 And Not W8 And Not WVista And Not WXP Then
                Try
                    Dim R2 As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", True)
                    If R2.GetValue("BackgroundType", Nothing) Is Nothing Then R2.SetValue("BackgroundType", 0, RegistryValueKind.DWord)
                    WallpaperType = R2.GetValue("BackgroundType")
                    If R2 IsNot Nothing Then R2.Close()
                Catch
                End Try
            End If

            Dim img As Bitmap

            If IO.File.Exists(WallpaperPath) And WallpaperType = 0 Then
                Dim x As New IO.FileStream(WallpaperPath, IO.FileMode.Open, IO.FileAccess.Read)
                img = New Bitmap(Image.FromStream(x))
                x.Close()
            Else
                With Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0")
                    img = Color.FromArgb(255, .ToString.Split(" ")(0), .ToString.Split(" ")(1), .ToString.Split(" ")(2)).ToBitmap(New Size(528, 297))
                End With
            End If

            Return img
        End Function
        Sub WallpaperType_Changed(sender As Object, e As EventArrivedEventArgs)

            Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", True)
            If R1.GetValue("BackgroundType", Nothing) Is Nothing Then R1.SetValue("BackgroundType", 0, RegistryValueKind.DWord)

            Dim R2 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
            Dim S As New Stopwatch

            If R1.GetValue("BackgroundType") = 0 Then
                S.Reset()
                S.Start()

                Do Until IO.File.Exists(R2.GetValue("Wallpaper").ToString())
                    If S.ElapsedMilliseconds > 5000 Then Exit Do
                Loop

                S.Stop()

                Wallpaper_Changed()
            End If

            If R1 IsNot Nothing Then R1.Close()
            If R2 IsNot Nothing Then R2.Close()
        End Sub
        Sub Monitor()
            Dim currentUser = WindowsIdentity.GetCurrent()
            Dim KeyPath As String
            Dim valueName As String
            Dim Base As String

            If Not My.WXP Then
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
            End If

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
            Try
                If e.Category = UserPreferenceCategory.Desktop Or e.Category = UserPreferenceCategory.Color Then Wallpaper_Changed()
            Catch
            End Try
        End Sub
        Sub DarkMode_Changed()
            Invoke(UpdateDarkModeInvoker)
        End Sub
        Sub Wallpaper_Changed()
            Wallpaper = GetWallpaper().Resize(528, 297)
            Invoke(UpdateWallpaperInvoker)
        End Sub
#End Region

#Region "   Application Startup and Shutdown Subs"
        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
            If Not My.WXP Then
                Try
                    WallMon_Watcher1.Stop()
                    WallMon_Watcher2.Stop()

                    If Not W7 And Not W8 And Not My.WVista Then
                        WallMon_Watcher3.Stop()
                        WallMon_Watcher4.Stop()
                    End If
                Catch
                End Try
            End If

            Try : If IO.File.Exists("oldWinpaletter.trash") Then Kill("oldWinpaletter.trash")
            Catch : End Try
        End Sub

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            AddHandler Windows.Forms.Application.ThreadException, AddressOf MyThreadExceptionHandler

            Try : If IO.File.Exists("oldWinpaletter.trash") Then Kill("oldWinpaletter.trash")
            Catch : End Try

            Animator = New AnimatorNS.Animator With {.Interval = 1, .TimeStep = 0.07, .DefaultAnimation = AnimatorNS.Animation.Transparent, .AnimationType = AnimatorNS.AnimationType.Transparent}

            Try
                AddMemoryFont(Resources.JetBrainsMono_Regular)
                ConsoleFont = GetFont(0, 7.5)
                ConsoleFontDef = GetFont(0, 7.5, FontStyle.Underline)
                ConsoleFontLarge = GetFont(0, 10)
                ConsoleFontMedium = GetFont(0, 9)
            Catch
                ConsoleFont = New Font("Lucida Console", 7.5)
                ConsoleFontDef = New Font("Lucida Console", 7.5, FontStyle.Bold)
                ConsoleFontLarge = New Font("Lucida Console", 10)
                ConsoleFontMedium = New Font("Lucida Console", 9)
            End Try

            If Environment.GetCommandLineArgs.Count > 1 Then
                ArgsList.Clear()
                For x = 1 To Environment.GetCommandLineArgs.Count - 1
                    ArgsList.Add(Environment.GetCommandLineArgs(x))
                Next
            End If

            FontsList = FontFamily.Families.[Select](Function(f) f.Name).ToList()

            'Old method of loading fixed fonts reused due to 2 issues:
            '1) A reported GitHub issue
            '2) Windows Vista issue with Windows.Media.Fonts
            Dim B As New Bitmap(30, 30)
            Dim G As Graphics = Graphics.FromImage(B)
            FontsFixedList.Clear()
            FontsFixedList = NativeMethods.GDI32.GetFixedWidthFonts(G).[Select](Function(f) f.Name).ToList()
            B.Dispose()
            G.Dispose()

            ApplyDarkMode()

            For Each arg As String In ArgsList
                If arg.ToLower = "/exportlanguage" Then
                    Lang.ExportJSON(String.Format("language-en {0}.{1}.{2} {3}-{4}-{5}.wplng", Now.Hour, Now.Minute, Now.Second, Now.Day, Now.Month, Now.Year))
                    Debug.WriteLine(Lang.LngExported)
                    Console.WriteLine(Lang.LngExported)
                    MsgBox(Lang.LngExported, MsgBoxStyle.Information)
                    Process.GetCurrentProcess.Kill()
                    Exit For
                End If

                If arg.ToLower = "/uninstall" Then
                    Uninstall.ShowDialog()
                    Process.GetCurrentProcess.Kill()
                    Exit For
                End If

                If arg.ToLower = "/uninstall-quiet" Then
                    Uninstall_Quiet()
                    Exit For
                End If
            Next

            If [Settings].Language Then
                Try
                    Lang.LoadLanguageFromJSON([Settings].Language_File)
                Catch ex As Exception
                    MsgBox("There is an error occured during loading language.", MsgBoxStyle.Critical, ex.Message, My.Lang.CollapseNote, My.Lang.ExpandNote, ex.StackTrace)
                End Try
            End If

            ExternalLink = False
            ExternalLink_File = ""

            If Not [Settings].LicenseAccepted Then
                If LicenseForm.ShowDialog <> DialogResult.OK Then Process.GetCurrentProcess.Kill()
            End If

            For Each arg As String In ArgsList

                If Not arg.ToLower.StartsWith("/apply:") And Not arg.ToLower.StartsWith("/edit:") Then

                    If Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpth" Then
                        If [Settings].OpeningPreviewInApp_or_AppliesIt Then
                            ExternalLink = True
                            ExternalLink_File = arg
                        Else
                            Dim CPx As New CP(CP.CP_Type.File, arg)
                            CPx.Save(CP.CP_Type.Registry, arg)
                            If [Settings].AutoRestartExplorer Then RestartExplorer()
                            Process.GetCurrentProcess.Kill()
                        End If
                    End If

                    If Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpsf" Then
                        SettingsX._External = True
                        SettingsX._File = arg
                        SettingsX.ShowDialog()
                        Process.GetCurrentProcess.Kill()
                    End If

                Else
                    If arg.ToLower.StartsWith("/apply:") Then
                        Dim File As String = arg.Remove(0, "/apply:".Count)
                        File = File.Replace("""", "")
                        If IO.File.Exists(File) Then
                            Dim CPx As New CP(CP.CP_Type.File, File)
                            CPx.Save(CP.CP_Type.Registry)
                            If [Settings].AutoRestartExplorer Then RestartExplorer()
                            Process.GetCurrentProcess.Kill()
                        End If
                    End If

                    If arg.ToLower.StartsWith("/edit:") Then
                        Dim File As String = arg.Remove(0, "/edit:".Count)
                        File = File.Replace("""", "")
                        ExternalLink = True
                        ExternalLink_File = File
                    End If

                End If
            Next

            Wallpaper = GetWallpaper().Resize(528, 297)

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
                If [Settings].AutoAddExt Then
                    If Not IO.Directory.Exists(appData) Then IO.Directory.CreateDirectory(appData)

                    IO.File.WriteAllBytes(appData & "\fileextension.ico", Resources.fileextension.ToByteArray)
                    IO.File.WriteAllBytes(appData & "\settingsfile.ico", Resources.settingsfile.ToByteArray)

                    CreateFileAssociation(".wpth", "WinPaletter.ThemeFile", "WinPaletter Theme File", appData & "\fileextension.ico", Assembly.GetExecutingAssembly().Location)
                    CreateFileAssociation(".wpsf", "WinPaletter.SettingsFile", "WinPaletter Settings File", appData & "\settingsfile.ico", Assembly.GetExecutingAssembly().Location)
                End If
            Catch
            End Try

            Notifications_IL.Images.Add("info", Resources.notify_info)
            Notifications_IL.Images.Add("error", Resources.notify_error)
            Notifications_IL.Images.Add("warning", Resources.notify_warning)
            Notifications_IL.Images.Add("time", Resources.notify_time)
            Notifications_IL.Images.Add("success", Resources.notify_success)
            Notifications_IL.Images.Add("skip", Resources.notify_skip)
            Notifications_IL.Images.Add("admin", Resources.notify_administrator)

            Lang_IL.Images.Add("main", Resources.LangNode_Main)
            Lang_IL.Images.Add("value", Resources.LangNode_Value)
            Lang_IL.Images.Add("json", Resources.LangNode_JSON)

            Try : WinRes = New WinResources : Catch : End Try

            Saving_Exceptions.Clear()

            If W7 Or WVista Or WXP Then ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Try
                If Not IO.Directory.Exists(appData & "\VisualStyles\Luna") Then IO.Directory.CreateDirectory(appData & "\VisualStyles\Luna")
                IO.File.WriteAllBytes(appData & "\VisualStyles\Luna\Luna.zip", Resources.luna)
                Dim s As New IO.FileStream(appData & "\VisualStyles\Luna\Luna.zip", IO.FileMode.Open, IO.FileAccess.Read)
                Dim z As New ZipArchive(s, ZipArchiveMode.Read)
                z.ExtractToDirectory(appData & "\VisualStyles\Luna", True)
                z.Dispose()
                s.Close()
                s.Dispose()
                IO.File.WriteAllText(appData & "\VisualStyles\Luna\luna.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", appData & "\VisualStyles\Luna\luna.msstyles", vbCrLf))
            Catch ex As Exception
                BugReport.ThrowError(ex)
            End Try

#Region "WhatsNew"
            If Not [Settings].WhatsNewRecord.Contains(Application.Info.Version.ToString) Then
                '### Pop up WhatsNew
                ShowWhatsNew = True

                Dim ver As New List(Of String)
                ver.Clear()
                ver.Add(Application.Info.Version.ToString)

                For Each X As String In [Settings].WhatsNewRecord.ToArray()
                    ver.Add(X)
                Next

                ver = ver.DeDuplicate
                [Settings].WhatsNewRecord = ver.ToArray
                [Settings].Save(XeSettings.Mode.Registry)

                CreateUninstaller()
            Else
                ShowWhatsNew = False
            End If
#End Region
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
                    Else
                        If Not arg.ToLower.StartsWith("/apply:") And Not arg.ToLower.StartsWith("/edit:") Then

                            If Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpth" Then
                                If MainFrm.CP <> MainFrm.CP_Original Then
                                    If [Settings].ShowSaveConfirmation Then
                                        Select Case ComplexSave.ShowDialog()
                                            Case DialogResult.Yes
                                                Dim r As String() = [Settings].ComplexSaveResult.Split(".")
                                                Dim r1 As String = r(0)
                                                Dim r2 As String = r(1)
                                                Select Case r1
                                                    Case 0              '' Save
                                                        If IO.File.Exists(MainFrm.SaveFileDialog1.FileName) Then
                                                            MainFrm.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                            MainFrm.CP_Original = MainFrm.CP.Clone
                                                        Else
                                                            If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                                MainFrm.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                                MainFrm.CP_Original = MainFrm.CP.Clone
                                                            Else
                                                                Exit Sub
                                                            End If
                                                        End If

                                                    Case 1              '' Save As
                                                        If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                            MainFrm.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                            MainFrm.CP_Original = MainFrm.CP.Clone
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

                                MainFrm.CP = New CP(CP.CP_Type.File, arg)
                                MainFrm.CP_Original = MainFrm.CP.Clone
                                MainFrm.OpenFileDialog1.FileName = arg
                                MainFrm.SaveFileDialog1.FileName = arg
                                MainFrm.ApplyCPValues(MainFrm.CP)
                                MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)

                                If Not [Settings].OpeningPreviewInApp_or_AppliesIt Then
                                    MainFrm.Apply_Theme()
                                End If
                            End If

                            If Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpsf" Then
                                SettingsX._External = True
                                SettingsX._File = arg
                                SettingsX.ShowDialog()
                            End If

                        Else
                            If arg.ToLower.StartsWith("/apply:") Then
                                Dim File As String = arg.Remove(0, "/apply:".Count)
                                File = File.Replace("""", "")
                                If IO.File.Exists(File) Then
                                    Dim CPx As New CP(CP.CP_Type.File, File)
                                    CPx.Save(CP.CP_Type.Registry)
                                    If [Settings].AutoRestartExplorer Then RestartExplorer()
                                End If
                            End If

                            If arg.ToLower.StartsWith("/edit:") Then
                                Dim File As String = arg.Remove(0, "/edit:".Count)
                                File = File.Replace("""", "")

                                If MainFrm.CP <> MainFrm.CP_Original Then

                                    If [Settings].ShowSaveConfirmation Then
                                        Select Case ComplexSave.ShowDialog()
                                            Case DialogResult.Yes
                                                Dim r As String() = [Settings].ComplexSaveResult.Split(".")
                                                Dim r1 As String = r(0)
                                                Dim r2 As String = r(1)
                                                Select Case r1
                                                    Case 0              '' Save
                                                        If IO.File.Exists(MainFrm.SaveFileDialog1.FileName) Then
                                                            MainFrm.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                            MainFrm.CP_Original = MainFrm.CP.Clone
                                                        Else
                                                            If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                                MainFrm.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                                MainFrm.CP_Original = MainFrm.CP.Clone
                                                            Else
                                                                Exit Sub
                                                            End If
                                                        End If

                                                    Case 1              '' Save As
                                                        If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                            MainFrm.CP.Save(CP.CP_Type.File, MainFrm.SaveFileDialog1.FileName)
                                                            MainFrm.CP_Original = MainFrm.CP.Clone
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

                                MainFrm.CP = New CP(CP.CP_Type.File, File)
                                MainFrm.CP_Original = MainFrm.CP.Clone
                                MainFrm.OpenFileDialog1.FileName = File
                                MainFrm.SaveFileDialog1.FileName = File
                                MainFrm.ApplyCPValues(MainFrm.CP)
                                MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)

                            End If
                        End If
                    End If
                End If
            Catch
                e.BringToForeground = True
            End Try
        End Sub
#End Region

#Region "   Domain (External Resources) and Exceptions Handling"
        Private Function DomainCheck(sender As Object, e As System.ResolveEventArgs) As Assembly Handles Domain.AssemblyResolve
            Try : If e.Name.ToUpper.Contains("Animator".ToUpper) Then Return Assembly.Load(Resources.Animator)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("Cyotek.Windows.Forms.ColorPicker".ToUpper) Then Return Assembly.Load(Resources.Cyotek_Windows_Forms_ColorPicker)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("ColorThief.Desktop.v46".ToUpper) Then Return Assembly.Load(Resources.ColorThief_Desktop_v46)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("AnimCur".ToUpper) Then Return Assembly.Load(Resources.AnimCur)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("Newtonsoft.Json".ToUpper) Then Return Assembly.Load(Resources.Newtonsoft_Json)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("Ookii.Dialogs.WinForms".ToUpper) Then Return Assembly.Load(Resources.Ookii_Dialogs_WinForms)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("System.Buffers".ToUpper) Then Return Assembly.Load(Resources.System_Buffers)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("System.Memory".ToUpper) Then Return Assembly.Load(Resources.System_Memory)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("System.Numerics.Vectors".ToUpper) Then Return Assembly.Load(Resources.System_Numerics_Vectors)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("System.Resources.Extensions".ToUpper) Then Return Assembly.Load(Resources.System_Resources_Extensions)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("System.Runtime.CompilerServices.Unsafe".ToUpper) Then Return Assembly.Load(Resources.System_Runtime_CompilerServices_Unsafe)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("Devcorp.Controls.VisualStyles".ToUpper) Then Return Assembly.Load(Resources.Devcorp_Controls_VisualStyles)
            Catch : End Try

        End Function

        Sub MyThreadExceptionHandler(ByVal sender As Object, ByVal e As ThreadExceptionEventArgs)
            BugReport.ThrowError(e.Exception)
            If ExitAfterException Then Process.GetCurrentProcess.Kill()
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            BugReport.ThrowError(e.Exception)
            e.ExitApplication = ExitAfterException
            If ExitAfterException Then Process.GetCurrentProcess.Kill()
        End Sub

        Private Sub Domain_UnhandledException(sender As Object, e As System.UnhandledExceptionEventArgs) Handles Domain.UnhandledException
            Throw DirectCast(e.ExceptionObject, Exception)
            If ExitAfterException Then Process.GetCurrentProcess.Kill()
        End Sub

#End Region
    End Class

End Namespace