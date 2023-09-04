Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.AccessControl
Imports System.Security.Principal
Imports WinPaletter.NativeMethods.Kernel32
Imports WinPaletter.XenonCore

Public Module DLL_ResourcesManager

    Private ReadOnly identifier As New SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, Nothing)
    Private ReadOnly AdminAccount As NTAccount = CType(identifier.Translate(GetType(NTAccount)), NTAccount)
    Private ReadOnly AccessRule As New FileSystemAccessRule(AdminAccount, FileSystemRights.FullControl, AccessControlType.Allow)

#Region "Subs"

    Public Function GetResource(SourceFile As String, ResourceType As String, ID As Integer) As Byte()
        Dim hModule = LoadLibraryEx(SourceFile, IntPtr.Zero, 2UI)
        Dim _intPtr = FindResource(hModule, ID, ResourceType)

        If _intPtr = IntPtr.Zero Then
            FreeLibrary(hModule)
            Return Nothing
        End If

        Dim num = SizeofResource(hModule, _intPtr)
        If num = 0 Then Return Nothing

        Dim intPtr2 = LoadResource(hModule, _intPtr)
        If intPtr2 = IntPtr.Zero Then Return Nothing

        Dim array = New Byte(num - 1) {}
        Marshal.Copy(intPtr2, array, 0, num)
        FreeLibrary(hModule)
        Return array
    End Function

    Public Function ReplaceResource(SourceFile As String, ResourceType As String, ID As Integer, NewRes As Byte(), Optional LangID As UShort = 1033) As Boolean
        If My.isElevated Then

            Wow64DisableWow64FsRedirection(IntPtr.Zero)

            Dim tempFileName As String = Path.GetTempFileName()
            Dim success_0 As Boolean = False
            Dim success_EndUpdateResource As Boolean = False

            PreparePrivileges()                                             'To get authorized access to change PE file access/permissions

            If CreateBackup(SourceFile) Then                                'Makes a copy of EP file as a backup file

                If BackupPermissions(SourceFile, tempFileName) Then         'Source file rights have been backed up successfully

                    Dim hModule = LoadLibraryEx(SourceFile, IntPtr.Zero, 2UI)
                    Dim _intPtr = FindResource(hModule, ID, ResourceType)
                    FreeLibrary(hModule)

                    If _intPtr <> IntPtr.Zero Then                          'Target resource has been found

                        PreparePrivileges()                                 'To get authorized access to change resources for PE file
                        Dim intPtr_res = BeginUpdateResource(SourceFile, False)

                        If intPtr_res <> IntPtr.Zero Then                   'Updating resource is initiated successfully

                            success_0 = UpdateResource(intPtr_res, ResourceType, ID, LangID, NewRes, CUInt(NewRes.Length))

                            If success_0 Then
                                success_EndUpdateResource = EndUpdateResource(intPtr_res, False)
                            End If

                        End If

                    End If

                    RestorePermissions(SourceFile, tempFileName)

                End If
            End If

            If Not success_0 Or Not success_EndUpdateResource Then MsgBox(String.Format(My.Lang.CP_UpdateDLL_Error, SourceFile, Marshal.GetLastWin32Error), MsgBoxStyle.Critical)

            Wow64RevertWow64FsRedirection(IntPtr.Zero)

            Return success_0
        Else
            MsgBox(String.Format(My.Lang.CP_UpdateDLL_AsAdmin_Error0, SourceFile), MsgBoxStyle.Exclamation, My.Lang.CP_UpdateDLL_AsAdmin_Error1)
            Return False
        End If
    End Function

    Private Function CreateBackup(SourceFile) As Boolean
        For Each backupFile In Directory.GetFiles(IO.Path.GetDirectoryName(SourceFile), IO.Path.GetFileNameWithoutExtension(SourceFile) & "*.dll_bak")
            Try
                File.Delete(backupFile)
            Catch
            End Try
            PreparePrivileges()
        Next
        Dim result = True
        Try
            PreparePrivileges()
            Dim backupFile = Path.GetDirectoryName(SourceFile) & "\" & Path.GetFileNameWithoutExtension(SourceFile) & Date.Now.ToBinary & ".dll_bak"

            If Not My.isElevated Then
                Reg_IO.Takeown_File(SourceFile)
                Reg_IO.ICACLS(SourceFile)
            End If

            File.Move(SourceFile, backupFile)
            File.Copy(backupFile, SourceFile)
            Return result
        Catch
            Return False
        End Try
    End Function

    Public Function BackupPermissions(SourceFile As String, BackupFile As String) As Boolean
        Try
            Dim accessControl As FileSecurity = File.GetAccessControl(SourceFile)
            If accessControl Is Nothing Then Return False

            Using fileStream As FileStream = File.Create(BackupFile, 1, FileOptions.None, accessControl) : fileStream.Close() : End Using

            accessControl.SetOwner(AdminAccount) : File.SetAccessControl(SourceFile, accessControl)
            accessControl.AddAccessRule(AccessRule) : File.SetAccessControl(SourceFile, accessControl)

            Return True
        Catch ex As Exception
            BugReport.ThrowError(ex)
            Return False
        End Try
    End Function

    Public Function RestorePermissions(SourceFile As String, BackupFile As String) As Boolean
        Try
            Dim BackupAccessControl As FileSecurity = File.GetAccessControl(SourceFile)
            If BackupAccessControl Is Nothing Then Return False

            File.SetAccessControl(SourceFile, BackupAccessControl)
            IO.File.Delete(BackupFile)
            Return True
        Catch ex As Exception
            BugReport.ThrowError(ex)
            Return False
        End Try
    End Function

    Sub PreparePrivileges()
        If Not EnablePrivilege("SeTakeOwnershipPrivilege", False) Then Throw New Exception("Failed to get SeTakeOwnershipPrivilege")
        If Not EnablePrivilege("SeSecurityPrivilege", False) Then Throw New Exception("Failed to get SeSecurityPrivilege")
        If Not EnablePrivilege("SeRestorePrivilege", False) Then Throw New Exception("Failed to get SeRestorePrivilege")
        If Not EnablePrivilege("SeBackupPrivilege", False) Then Throw New Exception("Failed to get SeBackupPrivilege")
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