Imports System.Management
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Security.Principal
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Imports WinPaletter.XenonCore

Namespace My
    Public Module WindowsVersions
        Public W11 As Boolean
        Public W10 As Boolean
        Public W8 As Boolean
        Public W7 As Boolean
        Public W10_1909 As Boolean
    End Module
    Partial Friend Class MyApplication

#Region "Variables"
        Public _Settings As XeSettings
        Public Wallpaper As Bitmap
        Public BackColor_Dark As Color = Color.FromArgb(24, 24, 26)
        Public BackColor_Light As Color = Color.FromArgb(235, 235, 235)
        Public WithEvents AnimatorX As AnimatorNS.Animator
        Public ExternalLink As Boolean = False
        Public ExternalLink_File As String = ""
        Public ChangeLogImgLst As New ImageList
        Public ComplexSaveResult As String = "2.0"
        Dim WallMon_Watcher1, WallMon_Watcher2, WallMon_Watcher3, WallMon_Watcher4 As ManagementEventWatcher
        Public ShowChangelog As Boolean = False
        Public explorerPath As String = String.Format("{0}\{1}", Environment.GetEnvironmentVariable("WINDIR"), "explorer.exe")
        Public processKiller As New Process
        Public processExplorer As New Process
        Public AeroKiller As New Process
        Public AeroStarter As New Process
        Public LanguageHelper As New Localizer
        Public allForms As List(Of String)
        Public appData As String = IO.Directory.GetParent(System.Windows.Forms.Application.LocalUserAppDataPath).FullName
        Public curPath As String = appData & "\Cursors"
        Public WinRes As WinResources

        Public CopiedColor As Color = Nothing
        Public ColorEvent As MenuEvent = MenuEvent.None

        Public ConsoleFont As New Font("Lucida Console", 7.5)
        Public ConsoleFontDef As New Font("Lucida Console", 7.5, FontStyle.Bold)
        Public ConsoleFontLarge As New Font("Lucida Console", 10)

        Public FontsList As New List(Of String)
        Public FontsFixedList As New List(Of String)

        Public ReadOnly isElevated As Boolean = New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator)

        Enum MenuEvent
            None
            Copy
            Cut
            Paste
            Override
            Delete
        End Enum
#End Region

#Region "File Association"
        <System.Runtime.InteropServices.DllImport("shell32.dll")> Shared Sub SHChangeNotify(ByVal wEventId As Integer, ByVal uFlags As Integer, ByVal dwItem1 As Integer, ByVal dwItem2 As Integer)
        End Sub

        Public Const SHCNE_ASSOCCHANGED = &H8000000
        Public Const SHCNF_IDLIST = 0

        Function CreateFileAssociation(ByVal extension As String, ByVal className As String, ByVal description As String, ByVal exeProgram As String) As Boolean
            ' Extension is the extension to be registered (eg ".wpth"
            ' ClassName is the name of the associated class (eg "WinPaletter.ThemeFile")
            ' Description is the textual description (eg "WinPaletter ThemeFile"
            ' ExeProgram is the app that manages that extension (eg. Assembly.GetExecutingAssembly().Location)

            If extension.Substring(0, 1) <> "." Then extension = "." & extension

            Dim key1, key2, key3, key4, key5 As RegistryKey
            key1 = Nothing
            key2 = Nothing
            key3 = Nothing
            key4 = Nothing
            key5 = Nothing

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
                key3.CreateSubKey(className & "\Shell\Edit in WinPaletter\Command", True).SetValue("", exeProgram & "  /edit:""%1""")
                key3.CreateSubKey(className & "\Shell\Apply by WinPaletter\Command", True).SetValue("", exeProgram & "  /apply:""%1""")

                key3.OpenSubKey(className & "\Shell\Open", True).SetValue("Icon", exeProgram.Replace("""", "") & ", 0")

                ' associate file icon
                key4 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)

                If Not IO.Directory.Exists(appData) Then IO.Directory.CreateDirectory(appData)

                key4.CreateSubKey(className & "\DefaultIcon", True).SetValue("", If(className = "WinPaletter.ThemeFile", appData & "\fileextension.ico", appData & "\settingsfile.ico"))

                key5 = Registry.CurrentUser.OpenSubKey("Software", True)
                With key5.CreateSubKey("WinPaletter", True)
                    .SetValue("DisplayName", My.Application.Info.ProductName)
                    .SetValue("Publisher", My.Application.Info.CompanyName)
                    .SetValue("Version", My.Application.Info.Version.ToString)
                End With

            Catch e As Exception
                Return False
            Finally
                If key1 IsNot Nothing Then key1.Close()
                If key2 IsNot Nothing Then key2.Close()
                If key3 IsNot Nothing Then key3.Close()
                If key4 IsNot Nothing Then key4.Close()
                If key5 IsNot Nothing Then key5.Close()
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
            key1 = Nothing
            key2 = Nothing
            key3 = Nothing
            key4 = Nothing

            Try
                key1 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
                key1.DeleteSubKeyTree(extension, False)

                key2 = Registry.CurrentUser.OpenSubKey("Software\Classes", True)
                key2.DeleteSubKeyTree(className, False)

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
#End Region

#Region "Wallpaper Change Detector"
        Sub Monitor()
            Dim currentUser = WindowsIdentity.GetCurrent()
            Dim KeyPath As String
            Dim valueName As String
            Dim Base As String

            Try
                KeyPath = "Control Panel\Desktop"
                valueName = "Wallpaper"
                Base = String.Format("SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace("\", "\\"), valueName)
                Dim query1 = New WqlEventQuery(Base)
                WallMon_Watcher1 = New ManagementEventWatcher(query1)
            Catch
            End Try


            Try
                KeyPath = "Control Panel\Colors"
                valueName = "Background"
                Base = String.Format("SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace("\", "\\"), valueName)
                Dim query2 = New WqlEventQuery(Base)
                WallMon_Watcher2 = New ManagementEventWatcher(query2)
            Catch
            End Try


            Try
                If Not My.W7 And Not My.W8 Then
                    KeyPath = "Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers"
                    valueName = "BackgroundType"
                    Base = String.Format("SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace("\", "\\"), valueName)
                    Dim query3 = New WqlEventQuery(Base)
                    WallMon_Watcher3 = New ManagementEventWatcher(query3)
                End If
            Catch
            End Try

            Try
                If Not My.W7 And Not My.W8 Then
                    KeyPath = "Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"
                    valueName = "AppsUseLightTheme"
                    Base = String.Format("SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace("\", "\\"), valueName)
                    Dim query4 = New WqlEventQuery(Base)
                    WallMon_Watcher4 = New ManagementEventWatcher(query4)
                End If
            Catch
            End Try

            Try : AddHandler WallMon_Watcher1.EventArrived, AddressOf Wallpaper_Changed : Catch : End Try
            Try : AddHandler WallMon_Watcher2.EventArrived, AddressOf Wallpaper_Changed : Catch : End Try

            If Not My.W7 And Not My.W8 Then
                Try : AddHandler WallMon_Watcher3.EventArrived, AddressOf WallpaperType_Changed : Catch : End Try
                Try : AddHandler WallMon_Watcher4.EventArrived, AddressOf DarkMode_Changed : Catch : End Try
            End If

            Try : WallMon_Watcher1.Start() : Catch : End Try
            Try : WallMon_Watcher2.Start() : Catch : End Try

            If Not My.W7 And Not My.W8 Then
                Try : WallMon_Watcher3.Start() : Catch : End Try
                Try : WallMon_Watcher4.Start() : Catch : End Try
            End If

        End Sub

        Sub DarkMode_Changed()
            Dim UpdateDarkModeX As UpdateDarkModeDelegate = AddressOf UpdateDarkMode
            MainForm.Invoke(UpdateDarkModeX)
        End Sub

        Private Sub UpdateDarkMode()
            If My.Application._Settings.Appearance_Auto Then
                ApplyDarkMode()
            End If
        End Sub
        Delegate Sub UpdateDarkModeDelegate()

        Sub Wallpaper_Changed(sender As Object, e As EventArrivedEventArgs)
            Try
                Wallpaper = ResizeImage(GetCurrentWallpaper(), 528, 297)
                Dim updateBkX As UpdateBKDelegate = AddressOf UpdateBK
                MainForm.Invoke(updateBkX, Wallpaper)
            Catch
            End Try
        End Sub

        Private Sub UpdateBK(ByVal [Image] As Image)
            MainFrm.pnl_preview.BackgroundImage = [Image]
            dragPreviewer.pnl_preview.BackgroundImage = [Image]
            MainFrm.pnl_preview.Invalidate()
            dragPreviewer.pnl_preview.Invalidate()
        End Sub
        Delegate Sub UpdateBKDelegate(ByVal [Image] As Image)

        Sub WallpaperType_Changed(sender As Object, e As EventArrivedEventArgs)
            Dim R1 As RegistryKey
            If Not My.W7 And Not My.W8 Then
                R1 = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", False)
                If R1.GetValue("BackgroundType", Nothing) Is Nothing Then R1.SetValue("BackgroundType", 0, RegistryValueKind.DWord)
            End If


            Dim R2 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", False)
            Dim S As New Stopwatch

            If Not My.W7 And Not My.W8 Then
                If R1.GetValue("BackgroundType") = 0 Then
                    S.Reset()
                    S.Start()

                    Do Until IO.File.Exists(R2.GetValue("Wallpaper").ToString())
                        If S.ElapsedMilliseconds > 5000 Then Exit Do
                    Loop

                    S.Stop()

                    Wallpaper = ResizeImage(GetCurrentWallpaper(), 528, 297)
                    Dim updateBkX As UpdateBKDelegate = AddressOf UpdateBK
                    MainForm.Invoke(updateBkX, Wallpaper)

                End If
            End If


            R1.Close()
            R2.Close()
        End Sub
#End Region
        Public Function GetCurrentWallpaper() As Bitmap
            If My.W7 Then
                Try : WallMon_Watcher1.Stop() : Catch : End Try
                Try : WallMon_Watcher2.Stop() : Catch : End Try
            End If


            Dim R1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", False)
            Dim R2 As RegistryKey

            If Not My.W7 And Not My.W8 Then
                R2 = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", False)
                If R2.GetValue("BackgroundType", 0) Is Nothing Then R1.SetValue("BackgroundType", 0, RegistryValueKind.DWord)
            End If

            Dim WallpaperPath As String = R1.GetValue("Wallpaper").ToString()
            Dim WallpaperType As Integer

            If Not My.W7 And Not My.W8 Then WallpaperType = R2.GetValue("BackgroundType")

            If IO.File.Exists(WallpaperPath) And WallpaperType = 0 Then
                Dim x As New IO.FileStream(WallpaperPath, IO.FileMode.Open, IO.FileAccess.Read)
                Return Image.FromStream(x)
                x.Close()
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

            R1.Close()
            R2.Close()

            If My.W7 Then
                Try : WallMon_Watcher1.Start() : Catch : End Try
                Try : WallMon_Watcher2.Start() : Catch : End Try
            End If
        End Function

        Sub DetectOS()
            W11 = My.Computer.Info.OSFullName.Contains("11")
            W10 = My.Computer.Info.OSFullName.Contains("10")
            W8 = My.Computer.Info.OSFullName.Contains("8")
            W7 = My.Computer.Info.OSFullName.Contains("7")
            W10_1909 = W11 Or (W10 And Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", 0).ToString() >= 1909)
        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
            WallMon_Watcher1.Stop()
            WallMon_Watcher2.Stop()

            If Not My.W7 And Not My.W8 Then
                WallMon_Watcher3.Stop()
                WallMon_Watcher4.Stop()
            End If

            Try : If IO.File.Exists("oldWinpaletter.trash") Then Kill("oldWinpaletter.trash")
            Catch : End Try
        End Sub

        Function RemoveDuplicate(ByVal TheList As List(Of String)) As List(Of String)
            Dim Result As New List(Of String)

            Dim Exist As Boolean = False
            For Each ElementString As String In TheList
                Exist = False
                For Each ElementStringInResult As String In Result
                    If ElementString = ElementStringInResult Then
                        Exist = True
                        Exit For
                    End If
                Next
                If Not Exist Then
                    Result.Add(ElementString)
                End If
            Next

            Return Result
        End Function

        Public Function MsgboxRt() As MsgBoxStyle
            Return If(LanguageHelper.RightToLeft, MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, 0)
        End Function

        Public Sub AdjustFonts()
            Dim f As String = "Segoe UI"

            If My.W11 And Not My.Application.LanguageHelper.RightToLeft Then
                f = "Segoe UI Variable Display"

                With MainFrm.Label1 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With MainFrm.Label10 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With MainFrm.Label17 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With MainFrm.themename_lbl : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With MainFrm.author_lbl : .Font = New Font(f, .Font.Size, .Font.Style) : End With

                With ColorPickerDlg.Label1 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With ColorPickerDlg.Label2 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With ColorPickerDlg.Label3 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With ColorPickerDlg.Label5 : .Font = New Font(f, .Font.Size, .Font.Style) : End With

                With About.Label17 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With About.Label4 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With About.Label3 : .Font = New Font(f, .Font.Size, .Font.Style) : End With

                With ComplexSave.Label1 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With ComplexSave.Label2 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With ComplexSave.Label17 : .Font = New Font(f, .Font.Size, .Font.Style) : End With

                With SettingsX.Label17 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With SettingsX.Label1 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With SettingsX.Label2 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With SettingsX.Label3 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With SettingsX.Label5 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With SettingsX.Label6 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With SettingsX.Label7 : .Font = New Font(f, .Font.Size, .Font.Style) : End With

                With Whatsnew.Label2 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With Whatsnew.Label1 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With Whatsnew.Label4 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With Whatsnew.Label13 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
                With Whatsnew.Label7 : .Font = New Font(f, .Font.Size, .Font.Style) : End With
            End If

        End Sub

        Public Function GetFormFromName(Name As String) As Form
            If Name.ToLower = "About".ToLower Then Return About
            If Name.ToLower = "Changelog".ToLower Then Return Changelog
            If Name.ToLower = "ColorPickerDlg".ToLower Then Return ColorPickerDlg
            If Name.ToLower = "ComplexSave".ToLower Then Return ComplexSave
            If Name.ToLower = "dragPreviewer".ToLower Then Return dragPreviewer
            If Name.ToLower = "EditInfo".ToLower Then Return EditInfo
            If Name.ToLower = "LogonUI".ToLower Then Return LogonUI
            If Name.ToLower = "MainFrm".ToLower Then Return MainFrm
            If Name.ToLower = "Whatsnew".ToLower Then Return Whatsnew
            If Name.ToLower = "Updates".ToLower Then Return Updates
            If Name.ToLower = "Win32UI".ToLower Then Return Win32UI
            If Name.ToLower = "SettingsX".ToLower Then Return SettingsX
            If Name.ToLower = "CursorsStudio".ToLower Then Return CursorsStudio
            If Name.ToLower = "ApplyingTheme".ToLower Then Return ApplyingTheme
            If Name.ToLower = "LogonUI7".ToLower Then Return LogonUI7
            If Name.ToLower = "LogonUI8Colors".ToLower Then Return LogonUI8Colors
            If Name.ToLower = "LogonUI8_Pics".ToLower Then Return LogonUI8_Pics
            If Name.ToLower = "Start8Selector".ToLower Then Return Start8Selector
            If Name.ToLower = "cmd".ToLower Then Return cmd
            If Name.ToLower = "ExternalTerminal".ToLower Then Return ExternalTerminal
            If Name.ToLower = "NewExtTerminal".ToLower Then Return NewExtTerminal
            If Name.ToLower = "TerminalInfo".ToLower Then Return TerminalInfo
            If Name.ToLower = "TerminalsDashboard".ToLower Then Return TerminalsDashboard
            If Name.ToLower = "WindowsTerminal".ToLower Then Return WindowsTerminal
            If Name.ToLower = "WindowsTerminalDecide".ToLower Then Return WindowsTerminalDecide
            If Name.ToLower = "WindowsTerminalCopycat".ToLower Then Return WindowsTerminalCopycat
            If Name.ToLower = "LicenseForm".ToLower Then Return LicenseForm
        End Function

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup

            Try
                MemoryFonts.AddMemoryFont(My.Resources.JetBrainsMono_Regular)
                ConsoleFont = MemoryFonts.GetFont(0, 7.5)
                ConsoleFontDef = MemoryFonts.GetFont(0, 7.5, FontStyle.Underline)
                ConsoleFontLarge = MemoryFonts.GetFont(0, 10)

            Catch
                ConsoleFont = New Font("Lucida Console", 7.5)
                ConsoleFontDef = New Font("Lucida Console", 7.5, FontStyle.Bold)
                ConsoleFontLarge = New Font("Lucida Console", 10)

            End Try

            FontsList.Clear()
            FontsFixedList.Clear()
            For Each [font] As FontFamily In FontFamily.Families
                FontsList.Add([font].Name)
            Next
            Dim B As New Bitmap(30, 30)
            Dim G As Graphics = Graphics.FromImage(B)
            For Each [font] As FontFamily In NativeMethods.GDI32.GetFixedWidthFonts(G)
                FontsFixedList.Add([font].Name)
            Next
            B.Dispose()
            G.Dispose()

            _Settings = New XeSettings(XeSettings.Mode.Registry)
            allForms = New List(Of String) From {
                        "About",
                        "Changelog",
                        "ColorPickerDlg",
                        "ComplexSave",
                        "dragPreviewer",
                        "EditInfo",
                        "LogonUI",
                        "MainFrm",
                        "Whatsnew",
                        "Updates",
                        "Win32UI",
                        "SettingsX",
                        "CursorsStudio",
                        "ApplyingTheme",
                        "LogonUI7",
                        "LogonUI8Colors",
                        "LogonUI8_Pics",
                        "Start8Selector",
                        "cmd",
                        "ExternalTerminal",
                        "NewExtTerminal",
                        "TerminalInfo",
                        "TerminalsDashboard",
                        "WindowsTerminal",
                        "WindowsTerminalDecide",
                        "WindowsTerminalCopycat",
                        "LicenseForm"
                        }

            Try
                For x = 1 To Environment.GetCommandLineArgs.Count - 1
                    Dim arg As String = Environment.GetCommandLineArgs(x)
                    If arg.ToLower = "/exportlanguage" Then
                        LanguageHelper.ExportNativeLang(String.Format("language-en {0}.{1}.{2} {3}-{4}-{5}.wplng", Now.Hour, Now.Minute, Now.Second, Now.Day, Now.Month, Now.Year))
                        MsgBox(LanguageHelper.LngExported, MsgBoxStyle.Information + MsgboxRt())
                        Process.GetCurrentProcess.Kill()
                    End If
                Next
            Catch
            End Try

            If My.Application._Settings.Language Then
                My.Application.LanguageHelper.LoadLanguageFromFile(My.Application._Settings.Language_File)
            Else
                My.Application.LanguageHelper.LoadInternal()
            End If

            Dim ProcessKillerInfo As New ProcessStartInfo With {
                .FileName = Environment.GetEnvironmentVariable("WINDIR") & "\System32\taskkill.exe",
                .Verb = "runas",
                .Arguments = "/F /IM explorer.exe",
                .WindowStyle = ProcessWindowStyle.Hidden,
                .UseShellExecute = True
            }
            Dim processExplorerInfo As New ProcessStartInfo With {
.FileName = explorerPath,
.Arguments = "",
.WindowStyle = ProcessWindowStyle.Normal,
.UseShellExecute = True
}
            If Not My.W8 Then processExplorerInfo.Verb = "runas"
            processKiller.StartInfo = ProcessKillerInfo
            processExplorer.StartInfo = processExplorerInfo

            DetectOS()

            Try : If IO.File.Exists("oldWinpaletter.trash") Then Kill("oldWinpaletter.trash")
            Catch : End Try

            Wallpaper = ResizeImage(My.Application.GetCurrentWallpaper(), 528, 297)
            Monitor()
            ApplyDarkMode()

            ExternalLink = False
            ExternalLink_File = ""
            ComplexSaveResult = "2.0"  '' 2 = Don't save,  0 = Don't Apply

            If Not _Settings.LicenseAccepted Then
                If LicenseForm.ShowDialog <> DialogResult.OK Then Process.GetCurrentProcess.Kill()
            End If

            Try
                For x = 1 To Environment.GetCommandLineArgs.Count - 1
                    Dim arg As String = Environment.GetCommandLineArgs(x)

                    Try
                        If arg.ToLower.StartsWith("/apply:") Then
                            Dim File As String = arg.Remove(0, "/apply:".Count)
                            File = File.Replace("""", "")
                            If IO.File.Exists(File) Then
                                Dim CPx As New CP(CP.Mode.File, File)
                                CPx.Save(CP.SavingMode.Registry)
                                If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                            End If
                            Process.GetCurrentProcess.Kill()
                        End If
                    Catch
                    End Try

                    Try
                        If arg.ToLower.StartsWith("/edit:") Then
                            Dim File As String = arg.Remove(0, "/edit:".Count)
                            File = File.Replace("""", "")
                            ExternalLink = True
                            ExternalLink_File = File
                        End If
                    Catch
                    End Try

                    Try
                        If My.Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpth" Then
                            If My.Application._Settings.OpeningPreviewInApp_or_AppliesIt Then
                                ExternalLink = True
                                ExternalLink_File = arg
                            Else
                                Dim CPx As New CP(CP.Mode.File, arg)
                                CPx.Save(CP.SavingMode.Registry, arg)
                                If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                                Process.GetCurrentProcess.Kill()
                            End If
                        End If
                    Catch
                    End Try

                    Try
                        If My.Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpsf" Then
                            SettingsX._External = True
                            SettingsX._File = arg
                            SettingsX.ShowDialog()
                            Process.GetCurrentProcess.Kill()
                        End If
                    Catch
                    End Try
                Next
            Catch
            End Try

            Try
                If _Settings.AutoAddExt Then
                    If Not IO.Directory.Exists(appData) Then IO.Directory.CreateDirectory(appData)

                    If _Settings.AutoAddExt Then
                        If Not IO.Directory.Exists(appData) Then IO.Directory.CreateDirectory(appData)

                        Dim file As System.IO.FileStream = New System.IO.FileStream(appData & "\fileextension.ico", System.IO.FileMode.OpenOrCreate)
                        My.Resources.fileextension.Save(file)
                        file.Close()

                        file = New System.IO.FileStream(appData & "\settingsfile.ico", System.IO.FileMode.OpenOrCreate)
                        My.Resources.settingsfile.Save(file)
                        file.Close()

                        CreateFileAssociation(".wpth", "WinPaletter.ThemeFile", "WinPaletter Theme File", """" & Assembly.GetExecutingAssembly().Location & """")
                        CreateFileAssociation(".wpsf", "WinPaletter.SettingsFile", "WinPaletter Settings File", """" & Assembly.GetExecutingAssembly().Location & """")
                    End If
                End If
            Catch
            End Try

            AnimatorX = New AnimatorNS.Animator With {.Interval = 1, .TimeStep = 0.07, .DefaultAnimation = AnimatorNS.Animation.Transparent, .AnimationType = AnimatorNS.AnimationType.Transparent}

            CP.PopulateThemeToListbox(Win32UI.XenonComboBox1)
            CP.PopulateThemeToListbox(ColorPickerDlg.XenonComboBox1)

            ChangeLogImgLst.ColorDepth = ColorDepth.Depth32Bit
            ChangeLogImgLst.ImageSize = New Size(24, 24)
            ChangeLogImgLst.Images.Add("Stable", My.Resources.CL_Stable)
            ChangeLogImgLst.Images.Add("Beta", My.Resources.CL_Beta)
            ChangeLogImgLst.Images.Add("Add", My.Resources.CL_add)
            ChangeLogImgLst.Images.Add("Removed", My.Resources.CL_Removed)
            ChangeLogImgLst.Images.Add("BugFix", My.Resources.CL_BugFix)
            ChangeLogImgLst.Images.Add("New", My.Resources.CL_New)
            ChangeLogImgLst.Images.Add("Channel", My.Resources.CL_channel)
            ChangeLogImgLst.Images.Add("Error", My.Resources.CL_Error)
            ChangeLogImgLst.Images.Add("Date", My.Resources.CL_Date)


            Try : WinRes = New WinResources : Catch : End Try

#Region "WhatsNew"
            If Not _Settings.WhatsNewRecord.ToArray.Contains(My.Application.Info.Version.ToString) Then
                '### Pop up WhatsNew
                Whatsnew.ShowDialog()
                Dim ver As New List(Of String)
                ver.Clear()
                ver.Add(My.Application.Info.Version.ToString)

                For Each X As String In _Settings.WhatsNewRecord.ToArray()
                    ver.Add(X)
                Next

                ver = RemoveDuplicate(ver)
                _Settings.WhatsNewRecord = ver.ToArray
                _Settings.Save(XeSettings.Mode.Registry)
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
                        MsgBox(LanguageHelper.LngShouldClose, MsgBoxStyle.Critical + MsgboxRt())
                    Else

                        Try
                            If arg.ToLower.StartsWith("/apply:") Then
                                Dim File As String = arg.Remove(0, "/apply:".Count)
                                File = File.Replace("""", "")
                                If IO.File.Exists(File) Then
                                    Dim CPx As New CP(CP.Mode.File, File)
                                    CPx.Save(CP.SavingMode.Registry)
                                    If My.Application._Settings.AutoRestartExplorer Then RestartExplorer()
                                End If
                            End If
                        Catch
                        End Try

                        Try
                            If arg.ToLower.StartsWith("/edit:") Then
                                Dim File As String = arg.Remove(0, "/edit:".Count)
                                File = File.Replace("""", "")

                                If Not MainFrm.CP.Equals(MainFrm.CP_Original) Then
                                    Select Case ComplexSave.ShowDialog()
                                        Case DialogResult.Yes
                                            Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                                            Dim r1 As String = r(0)
                                            Dim r2 As String = r(1)
                                            Select Case r1
                                                Case 0              '' Save
                                                    If IO.File.Exists(MainFrm.SaveFileDialog1.FileName) Then
                                                        MainFrm.CP.Save(CP.SavingMode.File, MainFrm.SaveFileDialog1.FileName)
                                                        MainFrm.CP_Original = MainFrm.CP
                                                    Else
                                                        If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                            MainFrm.CP.Save(CP.SavingMode.File, MainFrm.SaveFileDialog1.FileName)
                                                            MainFrm.CP_Original = MainFrm.CP
                                                        Else
                                                            Exit Sub
                                                        End If
                                                    End If

                                                Case 1              '' Save As
                                                    If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                        MainFrm.CP.Save(CP.SavingMode.File, MainFrm.SaveFileDialog1.FileName)
                                                        MainFrm.CP_Original = MainFrm.CP
                                                    Else
                                                        Exit Sub
                                                    End If
                                            End Select

                                            Select Case r2
                                                Case 1      '' Apply   ' Case 0= Don't Apply
                                                    MainFrm.CP.Save(CP.SavingMode.Registry)
                                                    RestartExplorer()
                                            End Select

                                        Case DialogResult.No


                                        Case DialogResult.Cancel
                                            Exit Sub
                                    End Select


                                End If

                                MainFrm.CP = New CP(CP.Mode.File, File)
                                MainFrm.CP_Original = New CP(CP.Mode.File, File)
                                MainFrm.OpenFileDialog1.FileName = File
                                MainFrm.SaveFileDialog1.FileName = File
                                MainFrm.ApplyCPValues(MainFrm.CP)
                                MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)

                            End If
                        Catch
                        End Try

                        If My.Computer.FileSystem.GetFileInfo(arg).Extension.ToLower = ".wpth" Then
                            If My.Application._Settings.OpeningPreviewInApp_or_AppliesIt Then
                                If Not MainFrm.CP.Equals(MainFrm.CP_Original) Then
                                    Select Case ComplexSave.ShowDialog()
                                        Case DialogResult.Yes
                                            Dim r As String() = My.Application.ComplexSaveResult.Split(".")
                                            Dim r1 As String = r(0)
                                            Dim r2 As String = r(1)
                                            Select Case r1
                                                Case 0              '' Save
                                                    If IO.File.Exists(MainFrm.SaveFileDialog1.FileName) Then
                                                        MainFrm.CP.Save(CP.SavingMode.File, MainFrm.SaveFileDialog1.FileName)
                                                        MainFrm.CP_Original = MainFrm.CP
                                                    Else
                                                        If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                            MainFrm.CP.Save(CP.SavingMode.File, MainFrm.SaveFileDialog1.FileName)
                                                            MainFrm.CP_Original = MainFrm.CP
                                                        Else
                                                            Exit Sub
                                                        End If
                                                    End If

                                                Case 1              '' Save As
                                                    If MainFrm.SaveFileDialog1.ShowDialog = DialogResult.OK Then
                                                        MainFrm.CP.Save(CP.SavingMode.File, MainFrm.SaveFileDialog1.FileName)
                                                        MainFrm.CP_Original = MainFrm.CP
                                                    Else
                                                        Exit Sub
                                                    End If
                                            End Select

                                            Select Case r2
                                                Case 1      '' Apply   ' Case 0= Don't Apply
                                                    MainFrm.CP.Save(CP.SavingMode.Registry)
                                                    RestartExplorer()
                                            End Select

                                        Case DialogResult.No


                                        Case DialogResult.Cancel
                                            Exit Sub
                                    End Select


                                End If

                                MainFrm.CP = New CP(CP.Mode.File, arg)
                                MainFrm.CP_Original = New CP(CP.Mode.File, arg)
                                MainFrm.OpenFileDialog1.FileName = arg
                                MainFrm.SaveFileDialog1.FileName = arg
                                MainFrm.ApplyCPValues(MainFrm.CP)
                                MainFrm.ApplyLivePreviewFromCP(MainFrm.CP)

                            Else
                                MainFrm.CP = New CP(CP.Mode.File, arg)
                                MainFrm.CP_Original = New CP(CP.Mode.File, arg)
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
                            SettingsX.ShowDialog()
                            '''''''
                        End If

                    End If
                End If
            Catch
                e.BringToForeground = True
            End Try
        End Sub

        Private WithEvents Domain As AppDomain = AppDomain.CurrentDomain

        Private Function DomainCheck(sender As Object, e As System.ResolveEventArgs) As System.Reflection.Assembly Handles Domain.AssemblyResolve

            Try : If e.Name.ToUpper.Contains("Animator".ToUpper) Then Return Assembly.Load(My.Resources.Animator)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("Cyotek.Windows.Forms.ColorPicker".ToUpper) Then Return Assembly.Load(My.Resources.Cyotek_Windows_Forms_ColorPicker)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("ColorThief.Desktop.v46".ToUpper) Then Return Assembly.Load(My.Resources.ColorThief_Desktop_v46)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("AnimCur".ToUpper) Then Return Assembly.Load(My.Resources.AnimCur)
            Catch : End Try

            Try : If e.Name.ToUpper.Contains("Newtonsoft.Json".ToUpper) Then Return Assembly.Load(My.Resources.Newtonsoft_Json)
            Catch : End Try

        End Function

        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            '#If DEBUG Then
            Throw e.Exception
            'MsgBox(e.Exception.Message & vbCrLf & vbCrLf & e.Exception.StackTrace, MsgBoxStyle.Critical + MsgboxRt())
            '#End If
        End Sub


    End Class
End Namespace