Imports System.Security.AccessControl
Imports Microsoft.Win32
Imports Newtonsoft.Json.Linq
Imports WinPaletter.NativeMethods

''' <summary>
''' Class contains custom Registry and IO functions
''' </summary>
Public Class Reg_IO
    Private Enum Reg_scope
        HKEY_CURRENT_USER
        HKEY_LOCAL_MACHINE
        HKEY_USERS
        HKEY_CLASSES_ROOT
        HKEY_CURRENT_CONFIG
    End Enum

    Shared Sub EditReg([TreeView] As TreeView, KeyName As String, ValueName As String, Value As Object, Optional RegType As RegistryValueKind = RegistryValueKind.DWord)
        EditReg(KeyName, ValueName, Value, RegType, [TreeView])
    End Sub

    Shared Sub EditReg(KeyName As String, ValueName As String, Value As Object, Optional RegType As RegistryValueKind = RegistryValueKind.DWord, Optional [TreeView] As TreeView = Nothing)
        Dim R As RegistryKey = Nothing

        If KeyName.StartsWith("Computer\", My._ignore) Then KeyName = KeyName.Remove(0, "Computer\".Count)

        Dim KeyName_BeforeModification As String = KeyName

        If RegType = RegistryValueKind.String And Value Is Nothing Then Value = ""

        Dim scope As Reg_scope

        If KeyName.StartsWith("HKEY_CURRENT_USER", My._ignore) Then
            scope = Reg_scope.HKEY_CURRENT_USER
            KeyName = KeyName.Remove(0, "HKEY_CURRENT_USER\".Count)

        ElseIf KeyName.StartsWith("HKEY_USERS", My._ignore) Then
            scope = Reg_scope.HKEY_USERS
            KeyName = KeyName.Remove(0, "HKEY_USERS\".Count)

        ElseIf KeyName.StartsWith("HKEY_LOCAL_MACHINE", My._ignore) Then
            scope = Reg_scope.HKEY_LOCAL_MACHINE
            KeyName = KeyName.Remove(0, "HKEY_LOCAL_MACHINE\".Count)

        ElseIf KeyName.StartsWith("HKEY_CLASSES_ROOT", My._ignore) Then
            scope = Reg_scope.HKEY_CLASSES_ROOT
            KeyName = KeyName.Remove(0, "HKEY_CLASSES_ROOT\".Count)

        ElseIf KeyName.StartsWith("HKEY_CURRENT_CONFIG", My._ignore) Then
            scope = Reg_scope.HKEY_CURRENT_CONFIG
            KeyName = KeyName.Remove(0, "HKEY_CURRENT_CONFIG\".Count)
        End If

        Select Case scope
            Case Reg_scope.HKEY_CURRENT_USER
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32)
                If R.OpenSubKey(KeyName, RegistryKeyPermissionCheck.ReadWriteSubTree) Is Nothing Then R.CreateSubKey(KeyName, True)

            Case Reg_scope.HKEY_CURRENT_CONFIG
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry32)
                If R.OpenSubKey(KeyName, RegistryKeyPermissionCheck.ReadWriteSubTree) Is Nothing Then R.CreateSubKey(KeyName, True)

            Case Reg_scope.HKEY_CLASSES_ROOT
                R = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32)
                If R.OpenSubKey(KeyName, RegistryKeyPermissionCheck.ReadWriteSubTree) Is Nothing Then R.CreateSubKey(KeyName, True)

            Case Reg_scope.HKEY_LOCAL_MACHINE
                R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, If(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Default))
                If My.isElevated Then
                    If R.OpenSubKey(KeyName, RegistryKeyPermissionCheck.ReadWriteSubTree) Is Nothing Then R.CreateSubKey(KeyName, True)
                End If

            Case Reg_scope.HKEY_USERS
                R = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32)
                If My.isElevated Then
                    If R.OpenSubKey(KeyName, RegistryKeyPermissionCheck.ReadWriteSubTree) Is Nothing Then R.CreateSubKey(KeyName, True)
                End If

        End Select

        'Skips setting to registry if the values are the same
        Try
            Dim ToCheck As Object = GetReg(KeyName_BeforeModification, ValueName, Nothing)
            Dim CheckBy As Object = Value
            Dim Skip As Boolean = False

            If ToCheck IsNot Nothing Then
                If ToCheck.GetType.IsArray Then
                    Skip = Enumerable.SequenceEqual(ToCheck, CheckBy)

                ElseIf TypeOf ToCheck Is Integer AndAlso CheckBy IsNot Nothing AndAlso TypeOf CheckBy Is Boolean Then
                    Skip = ToCheck = CType(CheckBy, Boolean).ToInteger

                ElseIf CheckBy IsNot Nothing Then
                    Skip = ToCheck = CheckBy

                End If

                If Skip Then
                    AddVerboseItem([TreeView], True, KeyName_BeforeModification, ValueName, Value, RegType)
                    Exit Sub
                End If
            End If
        Catch
        End Try

        Try
            If (My.isElevated AndAlso (scope = Reg_scope.HKEY_LOCAL_MACHINE OrElse scope = Reg_scope.HKEY_USERS)) OrElse (Not scope = Reg_scope.HKEY_LOCAL_MACHINE And Not scope = Reg_scope.HKEY_USERS) Then
                R.OpenSubKey(KeyName, RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue(ValueName, Value, RegType)
                AddVerboseItem([TreeView], False, KeyName_BeforeModification, ValueName, Value, RegType)
            Else
                If scope = Reg_scope.HKEY_LOCAL_MACHINE Then
                    EditReg_CMD("HKEY_LOCAL_MACHINE\" & KeyName, ValueName, Value, RegType)
                ElseIf scope = Reg_scope.HKEY_USERS Then
                    EditReg_CMD("HKEY_USERS\" & KeyName, ValueName, Value, RegType)
                End If
                AddVerboseItem([TreeView], False, KeyName_BeforeModification, ValueName, Value, RegType)
            End If
        Catch ex As Exception
            If My.Settings.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Detailed Then
                Dim v0 As String = ValueName
                Dim v1 As String
                If TypeOf Value Is Boolean Then
                    v1 = CBool(Value).ToInteger
                ElseIf TypeOf Value Is Byte() Then
                    v1 = String.Join(" ", CType(Value, Byte()))
                Else
                    v1 = Convert.ToString(Value)
                End If
                If String.IsNullOrWhiteSpace(v0) Then v0 = "(default)"
                If String.IsNullOrWhiteSpace(v1) Then v1 = "null"
                Dim v2 As String = ex.Message & " - " & String.Format(My.Lang.Verbose_RegScheme, KeyName_BeforeModification, v0, v1, RegType.ToString)
                CP.AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, v2), "error")
                My.Saving_Exceptions.Add(New Tuple(Of String, Exception)(v2, ex))
            Else
                CP.AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, ex.Message), "error")
                My.Saving_Exceptions.Add(New Tuple(Of String, Exception)(ex.Message, ex))
            End If
        End Try

        Try
            If R IsNot Nothing Then
                R.Flush()
                R.Close()
                R.Dispose()
            End If
        Catch
        End Try

    End Sub

    Private Shared Sub AddVerboseItem([TreeView] As TreeView, Skipped As Boolean, KeyName As String, ValueName As String, Value As Object, RegType As RegistryValueKind)
        If My.Settings.ThemeLog.VerboseLevel = WPSettings.Structures.ThemeLog.VerboseLevels.Detailed AndAlso [TreeView] IsNot Nothing Then
            Dim v0 As String = ValueName
            Dim v1 As String
            Dim v2 As String
            Dim v3 As String
            If TypeOf Value Is Boolean Then
                v1 = CBool(Value).ToInteger
            ElseIf TypeOf Value Is Byte() Then
                v1 = String.Join(" ", CType(Value, Byte()))
            Else
                v1 = Convert.ToString(Value)
            End If
            If String.IsNullOrWhiteSpace(v0) Then v0 = "(default)"
            If String.IsNullOrWhiteSpace(v1) Then v1 = "null"
            If Not Skipped Then
                v2 = String.Format(My.Lang.Verbose_RegScheme, KeyName, v0, v1, RegType.ToString)
                v3 = "reg_add"
            Else
                If Not My.Settings.ThemeLog.ShowSkippedItemsOnDetailedVerbose Then Exit Sub
                v2 = String.Format(My.Lang.Verbose_RegSkipped, String.Format(My.Lang.Verbose_RegScheme, KeyName, v0, v1, RegType.ToString))
                v3 = "reg_skip"
            End If
            CP.AddNode([TreeView], v2, v3)
        End If
    End Sub

    Shared Sub EditReg_CMD(ByVal RegistryKeyPath As String, ByVal ValueName As String, ByVal Value As Object, Optional RegType As RegistryValueKind = RegistryValueKind.DWord)
        Dim regTemplate As String

        Dim _Value As String
        If RegistryKeyPath.StartsWith("HKEY_LOCAL_MACHINE", My._ignore) Then RegistryKeyPath = "HKLM" & RegistryKeyPath.Remove(0, "HKEY_LOCAL_MACHINE".Count)
        If RegistryKeyPath.StartsWith("HKEY_CURRENT_USER", My._ignore) Then RegistryKeyPath = "HKCU" & RegistryKeyPath.Remove(0, "HKEY_CURRENT_USER".Count)
        If RegistryKeyPath.StartsWith("HKEY_USERS", My._ignore) Then RegistryKeyPath = "HKU" & RegistryKeyPath.Remove(0, "HKEY_USERS".Count)
        If RegistryKeyPath.StartsWith("HKEY_CLASSES_ROOT", My._ignore) Then RegistryKeyPath = "HKCR" & RegistryKeyPath.Remove(0, "HKEY_CLASSES_ROOT".Count)
        If RegistryKeyPath.StartsWith("HKEY_CURRENT_CONFIG", My._ignore) Then RegistryKeyPath = "HKCC" & RegistryKeyPath.Remove(0, "HKEY_CURRENT_CONFIG".Count)

        '/v = Value Name
        '/t = Registry Value Type
        '/d = Value
        '/f = Disable prompt
        If Value IsNot Nothing Then
            Select Case RegType
                Case RegistryValueKind.String
                    regTemplate = "add ""{0}"" /v ""{1}"" /t REG_SZ /d ""{2}"" /f"
                    _Value = Value.ToString

                Case RegistryValueKind.DWord
                    regTemplate = "add ""{0}"" /v ""{1}"" /t REG_DWORD /d {2} /f"
                    _Value = CInt(Value).DWORD

                Case RegistryValueKind.QWord
                    regTemplate = "add ""{0}"" /v ""{1}"" /t REG_QWORD /d {2} /f"
                    _Value = CInt(Value).QWORD

                Case RegistryValueKind.Binary
                    regTemplate = "add ""{0}"" /v ""{1}"" /t REG_BINARY /d {2} /f"
                    _Value = BitConverter.ToString(Value).Replace("-", "") 'BitConverter.ToString(Value).Replace("-", ",")

                Case RegistryValueKind.ExpandString
                    regTemplate = "add ""{0}"" /v ""{1}"" /t REG_EXPAND_SZ /d ""{2}"" /f"
                    _Value = Value.ToString

                Case RegistryValueKind.MultiString
                    regTemplate = "add ""{0}"" /v ""{1}"" /t REG_MULTI_SZ /d ""{2}"" /f"
                    _Value = Value.ToString.Replace(vbCrLf, "\0") & "\0\0"
                    'A sequence of null-terminated strings, terminated by an empty string (\0). The following is an example: String1\0String2\0String3\0LastString\0\0. The first \0 terminates the first string, the second-from-last \0 terminates the last string, and the final \0 terminates the sequence. Note that the final terminator must be factored into the length of the string.

                Case RegistryValueKind.None
                    regTemplate = "add ""{0}"" /v ""{1}"" /t REG_NONE /d ""{2}"" /f"
                    _Value = Value.ToString

                Case Else
                    regTemplate = "add ""{0}"" /v ""{1}"" /d ""{2}"" /f"
                    _Value = Value.ToString

            End Select

        Else
            regTemplate = "add ""{0}"" /v ""{1}"" /d ""{2}"" /f"
            _Value = ""

        End If

        If _Value.Contains("%") Then _Value = _Value.Replace("%", "^%")

        Using process As New Process With {.StartInfo = New ProcessStartInfo With {
           .FileName = "reg",
           .Verb = If(My.WXP AndAlso My.isElevated, "", "runas"),
           .Arguments = String.Format(regTemplate, RegistryKeyPath, ValueName, _Value),
           .WindowStyle = ProcessWindowStyle.Hidden,
           .CreateNoWindow = True,
           .UseShellExecute = True
        }}

            process.Start()
            process.WaitForExit()
        End Using
    End Sub
    Shared Function GetReg(KeyName As String, ValueName As String, DefaultValue As Object, Optional RaiseExceptions As Boolean = False, Optional IfNothingReturnDefaultValue As Boolean = True) As Object
        Dim Result As Object = Nothing
        Dim R As RegistryKey = Nothing

        If KeyName.StartsWith("Computer\", My._ignore) Then KeyName = KeyName.Remove(0, "Computer\".Count)

        If KeyName.StartsWith("HKEY_CURRENT_USER", My._ignore) Then
            KeyName = KeyName.Remove(0, "HKEY_CURRENT_USER\".Count)
            R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32)

        ElseIf KeyName.StartsWith("HKEY_USERS", My._ignore) Then
            KeyName = KeyName.Remove(0, "HKEY_USERS\".Count)
            R = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32)

        ElseIf KeyName.StartsWith("HKEY_LOCAL_MACHINE", My._ignore) Then
            KeyName = KeyName.Remove(0, "HKEY_LOCAL_MACHINE\".Count)
            R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, If(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Default))

        ElseIf KeyName.StartsWith("HKEY_CLASSES_ROOT", My._ignore) Then
            KeyName = KeyName.Remove(0, "HKEY_CLASSES_ROOT\".Count)
            R = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32)

        ElseIf KeyName.StartsWith("HKEY_CURRENT_CONFIG", My._ignore) Then
            KeyName = KeyName.Remove(0, "HKEY_CURRENT_CONFIG\".Count)
            R = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry32)

        End If

        Try
            If R.OpenSubKey(KeyName, False, RegistryRights.ReadKey) IsNot Nothing Then Result = R.OpenSubKey(KeyName, False, RegistryRights.ReadKey).GetValue(ValueName, DefaultValue)
            Try
                If R IsNot Nothing Then
                    R.Flush()
                    R.Close()
                    R.Dispose()
                End If
            Catch
            End Try
            Return If(IfNothingReturnDefaultValue AndAlso Result Is Nothing, DefaultValue, Result)
        Catch ex As Exception
            My.Loading_Exceptions.Add(New Tuple(Of String, Exception)(KeyName & " : " & ValueName, ex))
            If RaiseExceptions Then BugReport.ThrowError(ex)
            Try
                If R IsNot Nothing Then
                    R.Flush()
                    R.Close()
                    R.Dispose()
                End If
            Catch
            End Try
            Return DefaultValue
        End Try

    End Function
    Shared Sub DelReg_AdministratorDeflector(ByVal RegistryKeyPath As String, ByVal ValueName As String)
        Dim regTemplate As String
        If RegistryKeyPath.StartsWith("HKEY_LOCAL_MACHINE", My._ignore) Then RegistryKeyPath = "HKLM" & RegistryKeyPath.Remove(0, "HKEY_LOCAL_MACHINE".Count)
        If RegistryKeyPath.StartsWith("HKEY_CURRENT_USER", My._ignore) Then RegistryKeyPath = "HKCU" & RegistryKeyPath.Remove(0, "HKEY_CURRENT_USER".Count)
        If RegistryKeyPath.StartsWith("HKEY_USERS", My._ignore) Then RegistryKeyPath = "HKU" & RegistryKeyPath.Remove(0, "HKEY_USERS".Count)
        If RegistryKeyPath.StartsWith("HKEY_CLASSES_ROOT", My._ignore) Then RegistryKeyPath = "HKCR" & RegistryKeyPath.Remove(0, "HKEY_CLASSES_ROOT".Count)
        If RegistryKeyPath.StartsWith("HKEY_CURRENT_CONFIG", My._ignore) Then RegistryKeyPath = "HKCC" & RegistryKeyPath.Remove(0, "HKEY_CURRENT_CONFIG".Count)

        '/f = Disable prompt
        regTemplate = "delete ""{0}\{1}"" /f"

        Using process As New Process With {.StartInfo = New ProcessStartInfo With {
           .FileName = "reg",
           .Verb = If(My.WXP AndAlso My.isElevated, "", "runas"),
           .Arguments = String.Format(regTemplate, RegistryKeyPath, ValueName),
           .WindowStyle = ProcessWindowStyle.Hidden,
           .CreateNoWindow = True,
           .UseShellExecute = True
        }}

            process.Start()
            process.WaitForExit()
        End Using
    End Sub

    Shared Sub SFC(Optional File As String = "", Optional IfNotExist_DoScannow As Boolean = False)
        If My.WXP Then Exit Sub

        Kernel32.Wow64DisableWow64FsRedirection(IntPtr.Zero)

        Using process As New Process With {.StartInfo = New ProcessStartInfo With {
           .FileName = My.PATH_System32 & "\sfc.exe",
           .Verb = "runas",
           .WindowStyle = ProcessWindowStyle.Hidden,
           .CreateNoWindow = True,
           .UseShellExecute = True
        }}

            If IO.File.Exists(File) Then
                process.StartInfo.Arguments = "/SCANFILE=""" & File & """"
            Else
                If IfNotExist_DoScannow Then
                    process.StartInfo.Arguments = "/Scannow"
                Else
                    Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
                    Exit Sub
                End If
            End If

            process.Start()
            process.WaitForExit()
        End Using

        Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero)
    End Sub

    Shared Sub Takeown_File(File As String, Optional AsAdministrator As Boolean = False)
        If IO.File.Exists(File) Then
            Using process As New Process With {.StartInfo = New ProcessStartInfo With {
                    .FileName = My.PATH_System32 & "\takeown.exe",
                    .Verb = If(My.WXP, "", "runas"),
                    .Arguments = String.Format("/f ""{0}""", File, If(AsAdministrator, " /a", "")),
                    .WindowStyle = ProcessWindowStyle.Hidden,
                    .CreateNoWindow = True,
                    .UseShellExecute = True
                 }}


                process.Start()
                process.WaitForExit()
            End Using

            Try
                Dim fSecurity As FileSecurity = IO.File.GetAccessControl(File)
                fSecurity.AddAccessRule(New FileSystemAccessRule(Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow))
                IO.File.SetAccessControl(File, fSecurity)
            Catch
            End Try

        End If
    End Sub

    Shared Sub ICACLS(File As String, Optional AsAdministrator As Boolean = False)
        If IO.File.Exists(File) Then
            Using process As New Process With {.StartInfo = New ProcessStartInfo With {
                    .FileName = My.PATH_System32 & "\ICACLS.exe",
                    .Verb = If(My.WXP, "", "runas"),
                    .Arguments = String.Format("""{0}"" /grant {1}:F", File, If(AsAdministrator, "administrators", "%username%")),
                    .WindowStyle = ProcessWindowStyle.Hidden,
                    .CreateNoWindow = True,
                    .UseShellExecute = True
                 }}

                process.Start()
                process.WaitForExit()
            End Using

            Try
                Dim fSecurity As FileSecurity = IO.File.GetAccessControl(File)
                fSecurity.AddAccessRule(New FileSystemAccessRule(Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow))
                IO.File.SetAccessControl(File, fSecurity)
            Catch
            End Try
        End If
    End Sub
    Shared Sub Move_File(source As String, destination As String)
        If IO.File.Exists(source) Then
            Using process As New Process With {.StartInfo = New ProcessStartInfo With {
                    .FileName = "cmd",
                    .Verb = If(My.WXP AndAlso My.isElevated, "", "runas"),
                    .Arguments = String.Format("/c move ""{0}"" ""{1}""", source, destination),
                    .WindowStyle = ProcessWindowStyle.Hidden,
                    .CreateNoWindow = True,
                    .UseShellExecute = True
                 }}

                process.Start()
                process.WaitForExit()
            End Using

        End If
    End Sub
End Class

