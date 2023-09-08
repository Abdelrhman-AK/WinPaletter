Imports System.Runtime.InteropServices
Imports System.Security.AccessControl
Imports System.Security.Principal
Imports Ressy

Public Module PE

    Private ReadOnly identifier As New SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, Nothing)
    Private ReadOnly AdminAccount As NTAccount = CType(identifier.Translate(GetType(NTAccount)), NTAccount)
    Private ReadOnly AccessRule As New FileSystemAccessRule(AdminAccount, FileSystemRights.FullControl, AccessControlType.Allow)

    Public Function GetResource(SourceFile As String, ResourceType As String, ID As Integer, Optional LangID As UShort = 1033) As Byte()
        Dim PE_File As New PortableExecutable(SourceFile)
        Return PE_File.GetResource(New ResourceIdentifier(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), New Language(LangID))).Data
    End Function

    Public Sub ReplaceResource(SourceFile As String, ResourceType As String, ID As Integer, NewRes As Byte(), Optional LangID As UShort = 1033)

        If IO.Path.GetFullPath(SourceFile).ToLower.StartsWith(My.PATH_Windows, My._ignore) Then
            'It is a system PE file that needs rights/permissions modification.

            If (My.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert AndAlso My.Settings.ThemeApplyingBehavior.PE_ModifyByDefault) OrElse (Not My.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert AndAlso PE_Warning.NotifyAction(SourceFile, ResourceType, ID, LangID) = DialogResult.OK) Then

                Dim TempFile As String = IO.Path.GetTempFileName

                PreparePrivileges()                                     'To get authorized access to change PE file access/permissions

                If CreateBackup(SourceFile) Then                        'Makes a copy of EP file as a backup file

                    If BackupPermissions(SourceFile, TempFile) Then     'Source file rights have been backed up successfully

                        PreparePrivileges()                             'To get authorized access to change resources for PE file

                        Dim PE_File As New PortableExecutable(SourceFile)
                        PE_File.SetResource(New ResourceIdentifier(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), New Language(LangID)), NewRes)

                        RestorePermissions(SourceFile, TempFile)        'Restore source file rights

                    End If
                End If

            End If

        Else
            'It isn't in system directory and can be modified without changing rights/permissions.

            Dim PE_File As New PortableExecutable(SourceFile)
            PE_File.SetResource(New ResourceIdentifier(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), New Language(LangID)), NewRes)
        End If

    End Sub

    Private Function CreateBackup(SourceFile) As Boolean
        For Each backupFile In IO.Directory.GetFiles(IO.Path.GetDirectoryName(SourceFile), IO.Path.GetFileNameWithoutExtension(SourceFile) & "*.bak")
            Try
                IO.File.Delete(backupFile)
            Catch
            End Try
            PreparePrivileges()
        Next
        Dim result = True
        Try
            PreparePrivileges()
            Dim backupFile = IO.Path.GetDirectoryName(SourceFile) & "\" & IO.Path.GetFileNameWithoutExtension(SourceFile) & Math.Abs(Date.Now.ToBinary) & ".bak"

            IO.File.Move(SourceFile, backupFile)
            IO.File.Copy(backupFile, SourceFile)
            Return result
        Catch
            Return False
        End Try
    End Function

    Public Function BackupPermissions(SourceFile As String, BackupFile As String) As Boolean
        Dim accessControl As FileSecurity = IO.File.GetAccessControl(SourceFile)
        If accessControl Is Nothing Then Return False

        Using fileStream As IO.FileStream = IO.File.Create(BackupFile, 1, IO.FileOptions.None, accessControl) : fileStream.Close() : End Using

        accessControl.SetOwner(AdminAccount)
        accessControl.AddAccessRule(AccessRule)
        IO.File.SetAccessControl(SourceFile, accessControl)

        Return True
    End Function

    Public Function RestorePermissions(SourceFile As String, BackupFile As String) As Boolean
        Dim BackupAccessControl As FileSecurity = IO.File.GetAccessControl(SourceFile)
        If BackupAccessControl Is Nothing Then Return False

        IO.File.SetAccessControl(SourceFile, BackupAccessControl)
        IO.File.Delete(BackupFile)
        Return True
    End Function

    Sub PreparePrivileges()
        If Not EnablePrivilege("SeTakeOwnershipPrivilege", False) Then Throw New Exception("Failed to get SeTakeOwnershipPrivilege")
        If Not EnablePrivilege("SeSecurityPrivilege", False) Then Throw New Exception("Failed to get SeSecurityPrivilege")
        If Not EnablePrivilege("SeRestorePrivilege", False) Then Throw New Exception("Failed to get SeRestorePrivilege")
        If Not EnablePrivilege("SeBackupPrivilege", False) Then Throw New Exception("Failed to get SeBackupPrivilege")
    End Sub

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

Public Module PE_Functions
    Public Function GetPNGFromDLL(File As String, ResourceID As Integer, Optional ResourceType As String = "IMAGE", Optional UnfoundW As Integer = 50, Optional UnfoundH As Integer = 50) As Bitmap
        Try
            If IO.File.Exists(File) Then
                Using ms As New IO.MemoryStream(GetResource(File, ResourceType, ResourceID))
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