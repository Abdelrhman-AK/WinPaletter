using Ressy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

            if (System.IO.Path.GetFullPath(SourceFile).ToLower().StartsWith(SysPaths.Windows, StringComparison.OrdinalIgnoreCase))
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
                catch { } // Ignore deleting backup file if it fails

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

            using (System.IO.FileStream fileStream = System.IO.File.Create(BackupFile, 1, System.IO.FileOptions.None, accessControl))
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
            if (!NativeMethods.advapi.EnablePrivilege("SeTakeOwnershipPrivilege", false)) throw new Exception("Failed to get SeTakeOwnershipPrivilege");
            if (!NativeMethods.advapi.EnablePrivilege("SeSecurityPrivilege", false)) throw new Exception("Failed to get SeSecurityPrivilege");
            if (!NativeMethods.advapi.EnablePrivilege("SeRestorePrivilege", false)) throw new Exception("Failed to get SeRestorePrivilege");
            if (!NativeMethods.advapi.EnablePrivilege("SeBackupPrivilege", false)) throw new Exception("Failed to get SeBackupPrivilege");
        }
    }

    internal class Icons
    {
        public static Icon PEIconGroup_ToIcon(PortableExecutable PE, ResourceIdentifier iconGroupResourceIdentifier)
        {
            int structureSize = 14;
            byte[] iconBytes = PE.GetResource(iconGroupResourceIdentifier).Data.Skip(6).ToArray();

            List<IconInfo> icons = new();
            icons.Clear();

            for (int i = 0; i < iconBytes.Length; i += structureSize)
            {
                int iconWidth = iconBytes[i];                                   // Byte 0
                int iconHeight = iconBytes[i + 1];                              // Byte 1
                int iconIndex = BitConverter.ToInt16(iconBytes, i + 12);        // Byte 12 and 13
                int iconColors = iconBytes[i + 6];                              // Byte 6

                if (iconWidth == 0 && iconHeight == 0) continue;

                iconWidth = iconWidth == 0 ? 256 : iconWidth;
                iconHeight = iconHeight == 0 ? 256 : iconHeight;

                Ressy.Resource resource = PE.TryGetResource(new Ressy.ResourceIdentifier(Ressy.ResourceType.Icon, Ressy.ResourceName.FromCode(iconIndex)));
                if (resource is not null) icons.Add(new() { Width = iconWidth, Height = iconHeight, ColorCount = iconColors, Buffer = resource.Data });
            }

            using (MemoryStream stream = new())
            using (BinaryWriter writer = new(stream))
            {
                BytesToIcon(icons, writer);

                // Reset the stream position before reading the icon data
                stream.Seek(0, SeekOrigin.Begin);
                return new Icon(stream);
            }
        }

        public class IconInfo
        {
            public int Width;
            public int Height;
            public byte[] Buffer;
            public int ColorCount;

            public IconInfo() { }
        }

        public const int MaxIconWidth = 256;
        public const int MaxIconHeight = 256;

        private const ushort HeaderReserved = 0;
        private const ushort HeaderIconType = 1;
        private const byte HeaderLength = 6;

        private const byte EntryReserved = 0;
        private const byte EntryLength = 16;

        private const byte PngColorsInPalette = 0;
        private const ushort PngColorPlanes = 1;

        private static void BytesToIcon(IEnumerable<IconInfo> imageBuffers, BinaryWriter writer)
        {
            if (imageBuffers == null)
                throw new ArgumentNullException(nameof(imageBuffers));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            ThrowForInvalidImages(imageBuffers);

            IconInfo[] orderedBuffers = imageBuffers.OrderBy(b => b.Buffer.Length).ToArray();

            writer.Write(HeaderReserved);
            writer.Write(HeaderIconType);
            writer.Write((ushort)orderedBuffers.Length);

            Dictionary<uint, byte[]> buffers = new();
            uint lengthSum = 0;
            uint baseOffset = (uint)(HeaderLength + EntryLength * orderedBuffers.Length);

            for (int i = 0; i < orderedBuffers.Length; i++)
            {
                byte[] buffer = orderedBuffers[i].Buffer;
                byte width = (byte)orderedBuffers[i].Width;
                byte height = (byte)orderedBuffers[i].Height;
                byte colorCount = (byte)orderedBuffers[i].ColorCount;

                uint offset = (baseOffset + lengthSum);

                writer.Write(width);
                writer.Write(height);
                writer.Write(PngColorsInPalette);
                writer.Write(EntryReserved);
                writer.Write(PngColorPlanes);
                writer.Write((ushort)colorCount);
                writer.Write((uint)buffer.Length);
                writer.Write(offset);

                lengthSum += (uint)buffer.Length;
                buffers.Add(offset, buffer);
            }

            foreach (var kvp in buffers)
            {
                writer.BaseStream.Seek(kvp.Key, SeekOrigin.Begin);
                writer.Write(kvp.Value);
            }
        }

        private static void ThrowForInvalidImages(IEnumerable<IconInfo> imageBuffers)
        {
            foreach (var buffer in imageBuffers)
            {
                if (buffer.Buffer.Length == 0)
                {
                    throw new InvalidOperationException("Image buffer cannot be empty.");
                }
            }
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

        public static Icon GetIconFromIconGroup(PortableExecutable PE, ResourceIdentifier iconGroupResourceIdentifier)
        {
            return Icons.PEIconGroup_ToIcon(PE, iconGroupResourceIdentifier);
        }
    }
}