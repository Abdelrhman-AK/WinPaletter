Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.AccessControl
Imports System.Security.Principal
Imports WinPaletter.NativeMethods.Kernel32

Public Module DLL_ResourcesManager

    Private ReadOnly identifier As New SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, Nothing)
    Private ReadOnly AdmAccount As NTAccount = CType(identifier.Translate(GetType(NTAccount)), NTAccount)
    Private ReadOnly fsRule As New FileSystemAccessRule(AdmAccount, FileSystemRights.FullControl, AccessControlType.Allow)

#Region "Subs"

    Public Function GetResource(SourceFile As String, ResourceType As String, ID As Integer) As Byte()
        Wow64DisableWow64FsRedirection(IntPtr.Zero)
        Dim hModule = LoadLibraryEx(SourceFile, IntPtr.Zero, 2UI)
        Dim _intPtr = FindResource(hModule, ID, ResourceType)
        If _intPtr = IntPtr.Zero Then
            FreeLibrary(hModule)
            Return Nothing
        End If
        Dim num = SizeofResource(hModule, _intPtr)
        Dim intPtr2 = LoadResource(hModule, _intPtr)
        If num = 0 Then
            Return Nothing
        End If
        If intPtr2 = IntPtr.Zero Then
            Return Nothing
        End If
        Dim array = New Byte(num - 1) {}
        Marshal.Copy(intPtr2, array, 0, num)
        FreeLibrary(hModule)
        Wow64RevertWow64FsRedirection(IntPtr.Zero)
        Return array
    End Function

    Public Function ReplaceResource(SourceFile As String, ResourceType As String, ID As Integer, NewRes As Byte(), Optional LangID As UShort = 1033) As Boolean
        Wow64DisableWow64FsRedirection(IntPtr.Zero)
        Prepare()

        Dim tempFileName As String = Path.GetTempFileName()
        Dim flag As Boolean

        If Not BackupRights(SourceFile, tempFileName) Then
            Return False
        End If
        If PrepareFileCopy(SourceFile) Then
            Dim hModule = LoadLibraryEx(SourceFile, System.IntPtr.Zero, 2UI)
            Dim _intPtr = FindResource(hModule, ID, ResourceType)
            FreeLibrary(hModule)
            If _intPtr = IntPtr.Zero Then
                Return False
            End If
            Dim intPtr2 = BeginUpdateResource(SourceFile, bDeleteExistingResources:=False)
            If intPtr2 = IntPtr.Zero Then
                Return False
            End If

            flag = UpdateResource(intPtr2, ResourceType, ID, LangID, NewRes, CUInt(NewRes.Length))

            Prepare()
            If Not EndUpdateResource(intPtr2, Not flag) Then
                Throw New Exception("EndUpdateResource Failed:" & Marshal.GetLastWin32Error())
            End If
        Else
            flag = False
        End If

        RestoreRights(SourceFile, tempFileName)
        Wow64RevertWow64FsRedirection(IntPtr.Zero)
        Return flag
    End Function

    Private Function PrepareFileCopy(SourceFile) As Boolean
        Dim files = Directory.GetFiles(Path.GetDirectoryName(SourceFile), Path.GetFileNameWithoutExtension(SourceFile) & "*.ssc")
        For Each path In files
            Try
                File.Delete(path)
            Catch
            End Try
            Prepare()
        Next
        Dim result = True
        Try
            Prepare()
            Dim text = Path.GetDirectoryName(SourceFile) & "\" & Path.GetFileNameWithoutExtension(SourceFile) & Date.Now.Day & Date.Now.Month & Date.Now.Year & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & ".ssc"
            File.Move(SourceFile, text)
            File.Copy(text, SourceFile)
            Return result
        Catch
            Return False
        End Try
    End Function

    Public Function BackupRights(SourceFile As String, BackupFile As String) As Boolean
        Try
            Reg_IO.Takeown_File(SourceFile)
            Dim accessControl = File.GetAccessControl(SourceFile)
            If accessControl Is Nothing Then
                Return False
            End If
            Dim fileStream = File.Create(BackupFile, 1, FileOptions.None, accessControl)
            fileStream.Close()
            fileStream.Dispose()
            accessControl.SetOwner(AdmAccount)
            File.SetAccessControl(SourceFile, accessControl)
            accessControl.AddAccessRule(fsRule)
            File.SetAccessControl(SourceFile, accessControl)
            Return True
        Catch ex As Exception
            BugReport.ThrowError(ex)
            Return False
        End Try
    End Function

    Public Function RestoreRights(SourceFile As String, BackupFile As String) As Boolean
        Wow64DisableWow64FsRedirection(IntPtr.Zero)
        Dim fileInfo As New FileInfo(BackupFile)
        Dim accessControl As FileSecurity = fileInfo.GetAccessControl()
        If accessControl Is Nothing Then
            Wow64RevertWow64FsRedirection(IntPtr.Zero)
            Return False
        End If
        accessControl.SetAccessRuleProtection(isProtected:=True, preserveInheritance:=True)
        Dim owner = accessControl.GetOwner(GetType(NTAccount))
        Dim fileInfo2 As New FileInfo(SourceFile)
        fileInfo2.SetAccessControl(accessControl)
        accessControl.SetOwner(owner)
        fileInfo2.SetAccessControl(accessControl)
        File.Delete(BackupFile)
        Wow64RevertWow64FsRedirection(IntPtr.Zero)
        Return True
    End Function

    Public Sub Prepare()
        If Not EnablePrivilege("SeTakeOwnershipPrivilege", disable:=False) Then
            Throw New Exception("Failed to get SeTakeOwnershipPrivilege")
        End If
        If Not EnablePrivilege("SeSecurityPrivilege", disable:=False) Then
            Throw New Exception("Failed to get SeSecurityPrivilege")
        End If
        If Not EnablePrivilege("SeRestorePrivilege", disable:=False) Then
            Throw New Exception("Failed to get SeRestorePrivilege")
        End If
        If Not EnablePrivilege("SeBackupPrivilege", disable:=False) Then
            Throw New Exception("Failed to get SeBackupPrivilege")
        End If
    End Sub
#End Region

End Module

Friend Module Privileges
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Friend Structure TokPriv1Luid
        Public Count As Integer

        Public Luid As Long

        Public Attr As Integer
    End Structure

    Friend Const SE_PRIVILEGE_ENABLED As Integer = 2

    Friend Const SE_PRIVILEGE_DISABLED As Integer = 0

    Friend Const TOKEN_QUERY As Integer = 8

    Friend Const TOKEN_ADJUST_PRIVILEGES As Integer = 32

    <DllImport("advapi32.dll", ExactSpelling:=True, SetLastError:=True)>
    Private Function AdjustTokenPrivileges(ByVal htok As IntPtr, ByVal disall As Boolean, ByRef newst As TokPriv1Luid, ByVal len As Integer, ByVal prev As IntPtr, ByVal relen As IntPtr) As Boolean
    End Function

    <DllImport("dll")>
    Private Function GetCurrentProcess() As Integer
    End Function

    <DllImport("advapi32.dll", ExactSpelling:=True, SetLastError:=True)>
    Private Function OpenProcessToken(ByVal h As IntPtr, ByVal acc As Integer, ByRef phtok As IntPtr) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)>
    Private Function LookupPrivilegeValue(ByVal host As String, ByVal name As String, ByRef pluid As Long) As Boolean
    End Function

    Public Function EnablePrivilege(ByVal privilege As String, ByVal disable As Boolean) As Boolean
        Dim value As Long = Process.GetCurrentProcess().Handle.ToInt32()
        Dim h As New IntPtr(value)
        Dim phtok = IntPtr.Zero
        Dim flag = OpenProcessToken(h, 40, phtok)
        Dim newst As TokPriv1Luid = Nothing
        newst.Count = 1
        newst.Luid = 0L
        If disable Then
            newst.Attr = 0
        Else
            newst.Attr = 2
        End If
        flag = LookupPrivilegeValue(Nothing, privilege, newst.Luid)
        Return AdjustTokenPrivileges(phtok, disall:=False, newst, 0, IntPtr.Zero, IntPtr.Zero)
    End Function
End Module

Public Module Resources_Functions
    Public Function GetImageFromDLL(File As String, ResourceID As Integer, Optional ResourceType As String = "IMAGE", Optional UnfoundW As Integer = 50, Optional UnfoundH As Integer = 50) As Bitmap
        Try
            If IO.File.Exists(File) Then
                Using ms As New MemoryStream(GetResource(File, ResourceType, ResourceID))
                    Return Bitmap.FromStream(ms)
                End Using
            Else
                Return Color.Black.ToBitmap(New Size(UnfoundW, UnfoundH))
            End If
        Catch
            Return Color.Black.ToBitmap(New Size(UnfoundW, UnfoundH))
        End Try
    End Function

End Module