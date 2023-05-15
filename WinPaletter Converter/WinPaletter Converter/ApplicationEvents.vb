Imports System.Reflection
Imports System.Security.Principal
Imports System.Threading
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    Module Env
        Public ReadOnly PATH_Windows As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows).Replace("WINDOWS", "Windows")

        ''' <summary>
        ''' Boolean Represents if OS is Windows 11 or not
        ''' </summary>
        Public ReadOnly W11 As Boolean = Computer.Info.OSFullName.Contains("11")

        ''' <summary>
        ''' Get if Application is started as administrator or not
        ''' </summary>
        Public ReadOnly isElevated As Boolean = New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator)
    End Module

    Partial Friend Class MyApplication
        Private WithEvents Domain As AppDomain = AppDomain.CurrentDomain
        Public appData As String = IO.Directory.GetParent(Windows.Forms.Application.LocalUserAppDataPath).FullName

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
