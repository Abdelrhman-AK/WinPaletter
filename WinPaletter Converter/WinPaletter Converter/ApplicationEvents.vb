Imports System.Reflection
Imports System.Security.Principal
Imports System.Threading
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32

Namespace My
    Module Env
        Public ReadOnly PATH_Windows As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows).Replace("WINDOWS", "Windows")
        Public ReadOnly PATH_ProgramFiles As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
        Public ReadOnly PATH_System32 As String = PATH_Windows & "\System32"
        Public ReadOnly PATH_imageres As String = PATH_System32 & "\imageres.dll"
        Public ReadOnly PATH_Windows_UI_Immersive_dll As String = PATH_System32 & "\Windows.UI.Immersive.dll"
        Public ReadOnly PATH_UserProfile As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        Public ReadOnly PATH_TerminalJSON As String = PATH_UserProfile & "\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json"
        Public ReadOnly PATH_TerminalPreviewJSON As String = PATH_UserProfile & "\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json"
        Public ReadOnly PATH_PS86_reg As String = "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe"
        Public ReadOnly PATH_PS86_app As String = PATH_Windows & "\System32\WindowsPowerShell\v1.0"
        Public ReadOnly PATH_PS64_reg As String = "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe"
        Public ReadOnly PATH_PS64_app As String = PATH_Windows & "\SysWOW64\WindowsPowerShell\v1.0"
        Public ReadOnly PATH_StoreCache As String = Application.appData & "\Store"

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
        Public ReadOnly W10_1909 As Boolean = (W11 OrElse (W10 AndAlso Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", 0).ToString() >= 1909))

        ''' <summary>
        ''' Boolean Represents if OS is Windows 11 Build 22523 and Higher or not
        ''' </summary>
        Public ReadOnly W11_22523 As Boolean = (W11 AndAlso Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", 0).ToString() >= 22523)

        ''' <summary>
        ''' Get if Application is started as administrator or not
        ''' </summary>
        Public ReadOnly isElevated As Boolean = New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator)
    End Module

    Partial Friend Class MyApplication
#Region "   Variables"
        Private WithEvents Domain As AppDomain = AppDomain.CurrentDomain
        Public explorerPath As String = String.Format("{0}\{1}", Environment.GetEnvironmentVariable("WINDIR"), "explorer.exe")
        Public appData As String = IO.Directory.GetParent(Windows.Forms.Application.LocalUserAppDataPath).FullName
        Public curPath As String = appData & "\Cursors"
#End Region

#Region "   Application Startup and Shutdown Subs"
        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
            RemoveHandler Windows.Forms.Application.ThreadException, AddressOf MyThreadExceptionHandler

        End Sub

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            AddHandler Windows.Forms.Application.ThreadException, AddressOf MyThreadExceptionHandler

        End Sub
#End Region

#Region "   Domain (External Resources) and Exceptions Handling"
        Private Function DomainCheck(sender As Object, e As System.ResolveEventArgs) As Assembly Handles Domain.AssemblyResolve
            Try : If e.Name.ToUpper.Contains("Newtonsoft.Json".ToUpper) Then Return Assembly.Load(Resources.Newtonsoft_Json)
            Catch : End Try

            Return Nothing
        End Function

        Sub MyThreadExceptionHandler(ByVal sender As Object, ByVal e As ThreadExceptionEventArgs)
            Throw e.Exception
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Throw e.Exception
        End Sub

        Private Sub Domain_UnhandledException(sender As Object, e As System.UnhandledExceptionEventArgs) Handles Domain.UnhandledException
            Throw DirectCast(e.ExceptionObject, Exception)
        End Sub

#End Region

    End Class
End Namespace
