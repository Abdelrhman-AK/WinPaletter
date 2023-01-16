Imports System.ComponentModel
Imports System.Management
Imports System.Reflection
Imports System.Security.Principal
Imports System.Threading
Imports AnimatorNS
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Namespace My
    Module GlobalVariables
        ''' <summary>
        ''' Boolean Represents if OS is Windows 11 or not
        ''' </summary>
        Public ReadOnly W11 As Boolean = Computer.Info.OSFullName.Contains("11")

        ''' <summary>
        ''' Boolean Represents if OS is Windows 10 or not
        ''' </summary>
        Public ReadOnly W10 As Boolean = Computer.Info.OSFullName.Contains("10")

        ''' <summary>
        ''' Boolean Represents if OS is Windows 8/8.1 or not
        ''' </summary>
        Public ReadOnly W8 As Boolean = Computer.Info.OSFullName.Contains("8")

        ''' <summary>
        ''' Boolean Represents if OS is Windows 7 or not
        ''' </summary>
        Public ReadOnly W7 As Boolean = Computer.Info.OSFullName.Contains("7")

        ''' <summary>
        ''' Boolean Represents if OS is Windows 10 (19H2=1909) and Higher or not
        ''' </summary>
        Public ReadOnly W10_1909 As Boolean = (W11 Or (W10 And Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", 0).ToString() >= 1909))

        ''' <summary>
        ''' Class Represents AnimatorNS
        ''' </summary>
        Public WithEvents [AnimatorNS] As Animator

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
        ''' ImageList for Changelogs form (Loaded at application startup)
        ''' </summary>
        Public Changelog_IL As New ImageList With {.ImageSize = New Size(24, 24), .ColorDepth = ColorDepth.Depth32Bit}

        ''' <summary>
        ''' ImageList for Notifications mini-icons (Loaded at application startup)
        ''' </summary>
        Public Notifications_IL As New ImageList With {.ImageSize = New Size(20, 20), .ColorDepth = ColorDepth.Depth32Bit}

        ''' <summary>
        ''' ImageList for Languages Nodes (Loaded at application startup)
        ''' </summary>
        Public Lang_IL As New ImageList With {.ImageSize = New Size(16, 16), .ColorDepth = ColorDepth.Depth32Bit}

        Delegate Function MsgBoxDelegate([OriginalMsgBoxStyle] As MsgBoxStyle) As MsgBoxStyle
        ''' <summary>
        ''' Function to return MsgboxStyle with Arabic layout (Right-to-left) or without (According to Class Lang)
        ''' </summary>
        Public MsgboxRt As MsgBoxDelegate = Function([OriginalMsgBoxStyle] As MsgBoxStyle) [OriginalMsgBoxStyle] + If(Lang.RightToLeft, MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, 0)
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
        ReadOnly UpdateWallpaperInvoker As MethodInvoker = CType(Sub()
                                                                     MainFrm.pnl_preview.BackgroundImage = Wallpaper
                                                                     dragPreviewer.pnl_preview.BackgroundImage = Wallpaper
                                                                     Metrics_Fonts.pnl_preview1.BackgroundImage = Wallpaper
                                                                     Metrics_Fonts.pnl_preview2.BackgroundImage = Wallpaper
                                                                     Metrics_Fonts.pnl_preview3.BackgroundImage = Wallpaper
                                                                     Metrics_Fonts.pnl_preview4.BackgroundImage = Wallpaper
                                                                     MainFrm.pnl_preview.Invalidate()
                                                                     dragPreviewer.pnl_preview.Invalidate()
                                                                     Metrics_Fonts.pnl_preview1.Invalidate()
                                                                     Metrics_Fonts.pnl_preview2.Invalidate()
                                                                     Metrics_Fonts.pnl_preview3.Invalidate()
                                                                     Metrics_Fonts.pnl_preview4.Invalidate()
                                                                 End Sub, MethodInvoker)

        Public explorerPath As String = String.Format("{0}\{1}", Environment.GetEnvironmentVariable("WINDIR"), "explorer.exe")
        Public appData As String = IO.Directory.GetParent(Windows.Forms.Application.LocalUserAppDataPath).FullName
        Public curPath As String = appData & "\Cursors"
        Public ReadOnly processKiller As New Process With {.StartInfo = New ProcessStartInfo With {
                            .FileName = Environment.GetEnvironmentVariable("WINDIR") & "\System32\taskkill.exe",
                            .Verb = "runas",
                            .Arguments = "/F /IM explorer.exe",
                            .WindowStyle = ProcessWindowStyle.Hidden,
                            .UseShellExecute = True}
                           }
        Public ReadOnly processExplorer As New Process With {.StartInfo = New ProcessStartInfo With {
                            .FileName = explorerPath,
                            .Arguments = "",
                            .Verb = If(Not W8, "runas", ""),
                            .WindowStyle = ProcessWindowStyle.Normal,
                            .UseShellExecute = True}
                           }

        Public BackColor_Dark As Color = Color.FromArgb(25, 25, 25) 'FromArgb(24, 24, 26)
        Public BackColor_Light As Color = Color.FromArgb(230, 230, 230) 'FromArgb(235, 235, 235)

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
            Dim file As New IO.FileStream(appData & "\uninstall.ico", IO.FileMode.OpenOrCreate)
            Resources.Icon_Uninstall.Save(file)
            file.Close()

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
                CP.ResetCursorsToAero()
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
            If Not W7 And Not W8 Then
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
                img = Image.FromStream(x)
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

            If Not W7 And Not W8 Then
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
                AddHandler SystemEvents.UserPreferenceChanged, AddressOf Win7_8_UserPreferenceChanged
            End If

        End Sub
        Public Sub Win7_8_UserPreferenceChanged(ByVal sender As Object, ByVal e As UserPreferenceChangedEventArgs)
            If e.Category = UserPreferenceCategory.Desktop Then Wallpaper_Changed()
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
            Try
                WallMon_Watcher1.Stop()
                WallMon_Watcher2.Stop()

                If Not W7 And Not W8 Then
                    WallMon_Watcher3.Stop()
                    WallMon_Watcher4.Stop()
                End If
            Catch
            End Try

            Try : If IO.File.Exists("oldWinpaletter.trash") Then Kill("oldWinpaletter.trash")
            Catch : End Try
        End Sub

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            Try : If IO.File.Exists("oldWinpaletter.trash") Then Kill("oldWinpaletter.trash")
            Catch : End Try

            [AnimatorNS] = New Animator With {.Interval = 1, .TimeStep = 0.07, .DefaultAnimation = Animation.Transparent, .AnimationType = AnimationType.Transparent}
            AddHandler Windows.Forms.Application.ThreadException, AddressOf MyThreadExceptionHandler

            Try
                MemoryFonts.AddMemoryFont(Resources.JetBrainsMono_Regular)
                ConsoleFont = MemoryFonts.GetFont(0, 7.5)
                ConsoleFontDef = MemoryFonts.GetFont(0, 7.5, FontStyle.Underline)
                ConsoleFontLarge = MemoryFonts.GetFont(0, 10)
                ConsoleFontMedium = MemoryFonts.GetFont(0, 9)
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

            FontsList.Clear()
            FontsFixedList.Clear()
            For Each [font] As FontFamily In FontFamily.Families
                FontsList.Add([font].Name)
            Next
            For Each xx In Windows.Media.Fonts.SystemTypefaces.GroupBy(Function(x) x.FontFamily.ToString()).[Select](Function(grp) grp.First()).Where(Function(x) New Windows.Media.FormattedText("Hl", Globalization.CultureInfo.InvariantCulture, Windows.FlowDirection.LeftToRight, x, 10, Windows.Media.Brushes.Black).Width = New Windows.Media.FormattedText("HH", Globalization.CultureInfo.InvariantCulture, System.Windows.FlowDirection.LeftToRight, x, 10, Windows.Media.Brushes.Black).Width).ToList()
                FontsFixedList.Add(xx.FontFamily.Source.Split("#")(0))
            Next

            ApplyDarkMode()

            For Each arg As String In ArgsList
                If arg.ToLower = "/exportlanguage" Then
                    Lang.ExportJSON(String.Format("language-en {0}.{1}.{2} {3}-{4}-{5}.wplng", Now.Hour, Now.Minute, Now.Second, Now.Day, Now.Month, Now.Year))
                    Debug.WriteLine(Lang.LngExported)
                    Console.WriteLine(Lang.LngExported)
                    MsgBox(Lang.LngExported, MsgboxRt(MsgBoxStyle.Information))
                    Process.GetCurrentProcess.Kill()
                    Exit For
                End If

                If arg.ToLower = "/uninstall" Then
                    Uninstall.ShowDialog()
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
                    MsgBox("There is an error occured during loading language." & vbCrLf & vbCrLf & ex.Message & vbCrLf & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical)
                End Try
            Else
                Lang.LoadInternal()
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
                            Dim CPx As New CP(CP.Mode.File, arg)
                            CPx.Save(CP.Mode.Registry, arg)
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
                            Dim CPx As New CP(CP.Mode.File, File)
                            CPx.Save(CP.Mode.Registry)
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

            Monitor()

            Try
                If [Settings].AutoAddExt Then
                    If Not IO.Directory.Exists(appData) Then IO.Directory.CreateDirectory(appData)

                    Dim file As New IO.FileStream(appData & "\fileextension.ico", IO.FileMode.OpenOrCreate)
                    Resources.fileextension.Save(file)
                    file.Close()

                    file = New IO.FileStream(appData & "\settingsfile.ico", IO.FileMode.OpenOrCreate)
                    Resources.settingsfile.Save(file)
                    file.Close()

                    CreateFileAssociation(".wpth", "WinPaletter.ThemeFile", "WinPaletter Theme File", appData & "\fileextension.ico", Assembly.GetExecutingAssembly().Location)
                    CreateFileAssociation(".wpsf", "WinPaletter.SettingsFile", "WinPaletter Settings File", appData & "\settingsfile.ico", Assembly.GetExecutingAssembly().Location)
                End If
            Catch
            End Try

            Changelog_IL.Images.Add("Stable", Resources.CL_Stable)
            Changelog_IL.Images.Add("Beta", Resources.CL_Beta)
            Changelog_IL.Images.Add("Add", Resources.CL_add)
            Changelog_IL.Images.Add("Removed", Resources.CL_Removed)
            Changelog_IL.Images.Add("BugFix", Resources.CL_BugFix)
            Changelog_IL.Images.Add("New", Resources.CL_New)
            Changelog_IL.Images.Add("Channel", Resources.CL_channel)
            Changelog_IL.Images.Add("Error", Resources.CL_Error)
            Changelog_IL.Images.Add("Date", Resources.CL_Date)

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
                        MsgBox(Lang.LngShouldClose, MsgboxRt(MsgBoxStyle.Critical))

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
                                                            MainFrm.CP.Save(CP.Mode.File, MainFrm.SaveFileDialog1.FileName)
                                                            MainFrm.CP_Original = MainFrm.CP.Clone
                                                        Else
                                                            If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                                MainFrm.CP.Save(CP.Mode.File, MainFrm.SaveFileDialog1.FileName)
                                                                MainFrm.CP_Original = MainFrm.CP.Clone
                                                            Else
                                                                Exit Sub
                                                            End If
                                                        End If

                                                    Case 1              '' Save As
                                                        If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                            MainFrm.CP.Save(CP.Mode.File, MainFrm.SaveFileDialog1.FileName)
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

                                MainFrm.CP = New CP(CP.Mode.File, arg)
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
                                    Dim CPx As New CP(CP.Mode.File, File)
                                    CPx.Save(CP.Mode.Registry)
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
                                                            MainFrm.CP.Save(CP.Mode.File, MainFrm.SaveFileDialog1.FileName)
                                                            MainFrm.CP_Original = MainFrm.CP.Clone
                                                        Else
                                                            If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                                MainFrm.CP.Save(CP.Mode.File, MainFrm.SaveFileDialog1.FileName)
                                                                MainFrm.CP_Original = MainFrm.CP.Clone
                                                            Else
                                                                Exit Sub
                                                            End If
                                                        End If

                                                    Case 1              '' Save As
                                                        If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                            MainFrm.CP.Save(CP.Mode.File, MainFrm.SaveFileDialog1.FileName)
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

                                MainFrm.CP = New CP(CP.Mode.File, File)
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
        Private Function DomainCheck(sender As Object, e As System.ResolveEventArgs) As System.Reflection.Assembly Handles Domain.AssemblyResolve

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