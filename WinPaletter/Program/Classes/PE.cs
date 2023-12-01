using Ressy;
using System;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace WinPaletter
{

    public static class PE
    {

        private readonly static SecurityIdentifier identifier = new(WellKnownSidType.BuiltinAdministratorsSid, null);
        private readonly static NTAccount AdminAccount = (NTAccount)identifier.Translate(typeof(NTAccount));
        private readonly static FileSystemAccessRule AccessRule = new(AdminAccount, FileSystemRights.FullControl, AccessControlType.Allow);

        public static byte[] GetResource(string SourceFile, string ResourceType, int ID, ushort LangID = 1033)
        {
            PortableExecutable PE_File = new(SourceFile);
            return PE_File.GetResource(new(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID))).Data;
        }

        public static void ReplaceResource(TreeView TreeView, string SourceFile, string ResourceType, int ID, byte[] NewRes, ushort LangID = 1033)
        {
            ReplaceResource(SourceFile, ResourceType, ID, NewRes, LangID, TreeView);
        }

        public static void ReplaceResource(string SourceFile, string ResourceType, int ID, byte[] NewRes, ushort LangID = 1033, TreeView TreeView = null)
        {

            if (System.IO.Path.GetFullPath(SourceFile).ToLower().StartsWith(PathsExt.Windows, StringComparison.OrdinalIgnoreCase))
            {
                // It is a system PE file that needs rights/permissions modification.

                if (Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert && Program.Settings.ThemeApplyingBehavior.PE_ModifyByDefault || !Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert && Forms.PE_Warning.NotifyAction(SourceFile, ResourceType, ID, LangID) == DialogResult.OK)
                {

                    string TempFile = System.IO.Path.GetTempFileName();

                    if (TreeView is not null)
                        Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_PE_GettingAccess, System.IO.Path.GetFileName(SourceFile)), "admin");
                    PreparePrivileges();                                     // To get authorized access to change PE file access/permissions

                    if (TreeView is not null)
                        Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_PE_CreateBackup, System.IO.Path.GetFileName(SourceFile)), "pe_backup");
                    if (CreateBackup(SourceFile))                        // Makes a copy of EP file as a backup file
                    {

                        if (TreeView is not null)
                            Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_PE_GetBackupPermissions, System.IO.Path.GetFileName(SourceFile)), "pe_backup");
                        if (BackupPermissions(SourceFile, TempFile))     // Source file rights have been backed up successfully
                        {

                            if (TreeView is not null)
                                Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_PE_GetAccessToChangeResources, System.IO.Path.GetFileName(SourceFile)), "admin");
                            PreparePrivileges();                             // To get authorized access to change resources for PE file

                            if (TreeView is not null)
                                Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_PE_PatchingPE, System.IO.Path.GetFileName(SourceFile)), "pe_patch");
                            PortableExecutable PE_File = new(SourceFile);
                            PE_File.SetResource(new(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID)), NewRes);

                            if (TreeView is not null)
                                Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_PE_RestoringPermissions, System.IO.Path.GetFileName(SourceFile)), "pe_restore");
                            RestorePermissions(SourceFile, TempFile);        // Restore source file rights

                        }
                    }

                }
            }

            else
            {
                // It isn't in system directory and can be modified without changing rights/permissions.
                if (TreeView is not null)
                    Theme.Manager.AddNode(TreeView, $"Replacing '{System.IO.Path.GetFileName(SourceFile)}' resources", "pe_patch");
                PortableExecutable PE_File = new(SourceFile);
                PE_File.SetResource(new(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID)), NewRes);
            }

        }

        private static bool CreateBackup(string SourceFile)
        {
            foreach (string backupFile in System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(SourceFile), $"{System.IO.Path.GetFileNameWithoutExtension(SourceFile)}*.bak"))
            {
                try
                {
                    System.IO.File.Delete(backupFile);
                }
                catch
                {
                }
                PreparePrivileges();
            }
            bool result = true;
            try
            {
                PreparePrivileges();
                string backupFile = $@"{System.IO.Path.GetDirectoryName(SourceFile)}\{System.IO.Path.GetFileNameWithoutExtension(SourceFile)}{Math.Abs(DateTime.Now.ToBinary())}.bak";

                System.IO.File.Move(SourceFile, backupFile);
                System.IO.File.Copy(backupFile, SourceFile);
                return result;
            }
            catch
            {
                return false;
            }
        }

        public static bool BackupPermissions(string SourceFile, string BackupFile)
        {
            FileSecurity accessControl = System.IO.File.GetAccessControl(SourceFile);
            if (accessControl is null)
                return false;

            using (FileStream fileStream = System.IO.File.Create(BackupFile, 1, System.IO.FileOptions.None, accessControl))
            {
                fileStream.Close();
            }

            accessControl.SetOwner(AdminAccount);
            accessControl.AddAccessRule(AccessRule);
            System.IO.File.SetAccessControl(SourceFile, accessControl);

            return true;
        }

        public static bool RestorePermissions(string SourceFile, string BackupFile)
        {
            FileSecurity BackupAccessControl = System.IO.File.GetAccessControl(SourceFile);
            if (BackupAccessControl is null)
                return false;

            System.IO.File.SetAccessControl(SourceFile, BackupAccessControl);
            System.IO.File.Delete(BackupFile);
            return true;
        }

        public static void PreparePrivileges()
        {
            if (!NativeMethods.advapi.EnablePrivilege("SeTakeOwnershipPrivilege", false))
                throw new Exception("Failed to get SeTakeOwnershipPrivilege");
            if (!NativeMethods.advapi.EnablePrivilege("SeSecurityPrivilege", false))
                throw new Exception("Failed to get SeSecurityPrivilege");
            if (!NativeMethods.advapi.EnablePrivilege("SeRestorePrivilege", false))
                throw new Exception("Failed to get SeRestorePrivilege");
            if (!NativeMethods.advapi.EnablePrivilege("SeBackupPrivilege", false))
                throw new Exception("Failed to get SeBackupPrivilege");
        }
    }

    public static class PE_Functions
    {
        public static Bitmap GetPNGFromDLL(string File, int ResourceID, string ResourceType = "IMAGE", int UnfoundW = 50, int UnfoundH = 50)
        {
            try
            {
                if (System.IO.File.Exists(File))
                {
                    using (System.IO.MemoryStream ms = new(PE.GetResource(File, ResourceType, ResourceID)))
                    {
                        return (Bitmap)Image.FromStream(ms);
                    }
                }
                else
                {
                    return Color.Black.ToBitmap(new Size(UnfoundW, UnfoundH));
                }
            }
            catch
            {
                return Color.Black.ToBitmap(new Size(UnfoundW, UnfoundH));
            }
        }

    }
}