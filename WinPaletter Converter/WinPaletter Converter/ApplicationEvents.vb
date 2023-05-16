Imports System.Reflection
Imports System.Security.Principal

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

        Private Function DomainCheck(sender As Object, e As System.ResolveEventArgs) As Assembly Handles Domain.AssemblyResolve
            Try : If e.Name.ToUpper.Contains("Newtonsoft.Json".ToUpper) Then Return Assembly.Load(Resources.Newtonsoft_Json)
            Catch : End Try

            Return Nothing
        End Function
    End Class
End Namespace
