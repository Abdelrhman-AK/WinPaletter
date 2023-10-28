using Ressy;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace WinPaletter
{

    public static class PE
    {

        private readonly static SecurityIdentifier identifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
        private readonly static NTAccount AdminAccount = (NTAccount)identifier.Translate(typeof(NTAccount));
        private readonly static FileSystemAccessRule AccessRule = new FileSystemAccessRule(AdminAccount, FileSystemRights.FullControl, AccessControlType.Allow);

        public static byte[] GetResource(string SourceFile, string ResourceType, int ID, ushort LangID = 1033)
        {
            var PE_File = new PortableExecutable(SourceFile);
            return PE_File.GetResource(new ResourceIdentifier(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID))).Data;
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
                            var PE_File = new PortableExecutable(SourceFile);
                            PE_File.SetResource(new ResourceIdentifier(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID)), NewRes);

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
                    Theme.Manager.AddNode(TreeView, string.Format("Replacing '{0}' resources", System.IO.Path.GetFileName(SourceFile)), "pe_patch");
                var PE_File = new PortableExecutable(SourceFile);
                PE_File.SetResource(new ResourceIdentifier(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID)), NewRes);
            }

        }

        private static bool CreateBackup(string SourceFile)
        {
            foreach (var backupFile in System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(SourceFile), System.IO.Path.GetFileNameWithoutExtension(SourceFile) + "*.bak"))
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
                string backupFile = System.IO.Path.GetDirectoryName(SourceFile) + @"\" + System.IO.Path.GetFileNameWithoutExtension(SourceFile) + Math.Abs(DateTime.Now.ToBinary()) + ".bak";

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
            var accessControl = System.IO.File.GetAccessControl(SourceFile);
            if (accessControl is null)
                return false;

            using (var fileStream = System.IO.File.Create(BackupFile, 1, System.IO.FileOptions.None, accessControl))
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
            var BackupAccessControl = System.IO.File.GetAccessControl(SourceFile);
            if (BackupAccessControl is null)
                return false;

            System.IO.File.SetAccessControl(SourceFile, BackupAccessControl);
            System.IO.File.Delete(BackupFile);
            return true;
        }

        public static void PreparePrivileges()
        {
            if (!Privileges.EnablePrivilege("SeTakeOwnershipPrivilege", false))
                throw new Exception("Failed to get SeTakeOwnershipPrivilege");
            if (!Privileges.EnablePrivilege("SeSecurityPrivilege", false))
                throw new Exception("Failed to get SeSecurityPrivilege");
            if (!Privileges.EnablePrivilege("SeRestorePrivilege", false))
                throw new Exception("Failed to get SeRestorePrivilege");
            if (!Privileges.EnablePrivilege("SeBackupPrivilege", false))
                throw new Exception("Failed to get SeBackupPrivilege");
        }

    }

    internal static class Privileges
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;

            public long Luid;

            public int Attr;
        }

        internal const int SE_PRIVILEGE_ENABLED = 2;

        internal const int SE_PRIVILEGE_DISABLED = 0;

        internal const int TOKEN_QUERY = 8;

        internal const int TOKEN_ADJUST_PRIVILEGES = 32;

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("dll")]
        private static extern int GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        public static bool EnablePrivilege(string privilege, bool disable)
        {
            long value = Process.GetCurrentProcess().Handle.ToInt32();
            var h = new IntPtr(value);
            var phtok = IntPtr.Zero;
            bool flag = OpenProcessToken(h, 40, ref phtok);
            TokPriv1Luid newst = default;
            newst.Count = 1;
            newst.Luid = 0L;
            newst.Attr = disable ? 0 : 2;
            flag = LookupPrivilegeValue(null, privilege, ref newst.Luid);
            return AdjustTokenPrivileges(phtok, disall: false, ref newst, 0, IntPtr.Zero, IntPtr.Zero);
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
                    using (var ms = new System.IO.MemoryStream(PE.GetResource(File, ResourceType, ResourceID)))
                    {
                        return (Bitmap)Image.FromStream(ms);
                    }
                }
                else
                {
                    return (Bitmap)Color.Black.ToBitmap(new Size(UnfoundW, UnfoundH));
                }
            }
            catch
            {
                return (Bitmap)Color.Black.ToBitmap(new Size(UnfoundW, UnfoundH));
            }
        }

    }
}