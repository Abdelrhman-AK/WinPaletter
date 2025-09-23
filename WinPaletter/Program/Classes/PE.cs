using Ressy;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// Class to handle Portable Executable (PE) files.
    /// </summary>
    public static class PE
    {
        /// <summary>
        /// Class to handle icons in Portable Executable (PE) files.
        /// </summary>
        private static class Icons
        {
            /// <summary>
            /// Convert an icon group in a Portable Executable (PE) File to an <see cref="Icon"/>.
            /// </summary>
            /// <param name="PE"></param>
            /// <param name="iconGroupResourceIdentifier"></param>
            /// <returns></returns>
            public static Icon PEIconGroup_ToIcon(PortableExecutable PE, ResourceIdentifier iconGroupResourceIdentifier)
            {
                if (PE is null) throw new ArgumentNullException(nameof(PE));
                if (iconGroupResourceIdentifier is null) throw new ArgumentNullException(nameof(iconGroupResourceIdentifier));

                if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Extracting icon group '{iconGroupResourceIdentifier}' from PE file '{PE.FilePath}'.");

                int structureSize = 14;
                byte[] iconBytes = [.. PE.GetResource(iconGroupResourceIdentifier).Data.Skip(6)];

                List<IconInfo> icons = [];
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

                    Resource resource = PE.TryGetResource(new(Ressy.ResourceType.Icon, ResourceName.FromCode(iconIndex)));
                    if (resource is not null) icons.Add(new() { Width = iconWidth, Height = iconHeight, ColorCount = iconColors, Buffer = resource.Data });
                }

                using (MemoryStream stream = new())
                using (BinaryWriter writer = new(stream))
                {
                    BytesToIcon(icons, writer);

                    // Reset the stream position before reading the icon data
                    stream.Seek(0, SeekOrigin.Begin);

                    if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Successfully extracted {icons.Count} icons from PE file '{PE.FilePath}'.");

                    return new Icon(stream);
                }
            }

            /// <summary>
            /// IconInfo class to store icon information.
            /// </summary>
            public class IconInfo
            {
                public int Width;
                public int Height;
                public byte[] Buffer;
                public int ColorCount;

                public IconInfo() { }
            }

            /// <summary>
            /// Maximum icon width.
            /// </summary>
            public const int MaxIconWidth = 256;

            /// <summary>
            /// Maximum icon height.
            /// </summary>
            public const int MaxIconHeight = 256;

            private const ushort HeaderReserved = 0;
            private const ushort HeaderIconType = 1;
            private const byte HeaderLength = 6;

            private const byte EntryReserved = 0;
            private const byte EntryLength = 16;

            private const byte PngColorsInPalette = 0;
            private const ushort PngColorPlanes = 1;

            /// <summary>
            /// Convert a collection of icon buffers to an icon.
            /// </summary>
            /// <param name="imageBuffers"></param>
            /// <param name="writer"></param>
            /// <exception cref="ArgumentNullException"></exception>
            private static void BytesToIcon(IEnumerable<IconInfo> imageBuffers, BinaryWriter writer)
            {
                // Validate the input
                if (imageBuffers == null)
                    throw new ArgumentNullException(nameof(imageBuffers));
                if (writer == null)
                    throw new ArgumentNullException(nameof(writer));

                // Throw an exception if any of the image buffers are invalid
                ThrowForInvalidImages(imageBuffers);

                // Order the buffers by size
                IconInfo[] orderedBuffers = [.. imageBuffers.OrderBy(b => b.Buffer.Length)];

                // Write the icon header
                writer.Write(HeaderReserved);
                writer.Write(HeaderIconType);
                writer.Write((ushort)orderedBuffers.Length);

                // Write the icon entries
                Dictionary<uint, byte[]> buffers = [];
                uint lengthSum = 0;
                uint baseOffset = (uint)(HeaderLength + EntryLength * orderedBuffers.Length);

                // Write the icon entries
                for (int i = 0; i < orderedBuffers.Length; i++)
                {
                    byte[] buffer = orderedBuffers[i].Buffer;
                    byte width = (byte)orderedBuffers[i].Width;
                    byte height = (byte)orderedBuffers[i].Height;
                    byte colorCount = (byte)orderedBuffers[i].ColorCount;

                    uint offset = baseOffset + lengthSum;

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

                // Write the icon data
                foreach (KeyValuePair<uint, byte[]> kvp in buffers)
                {
                    writer.BaseStream.Seek(kvp.Key, SeekOrigin.Begin);
                    writer.Write(kvp.Value);
                }
            }

            /// <summary>
            /// Throw an exception if any of the image buffers are invalid
            /// </summary>
            /// <param name="imageBuffers"></param>
            /// <exception cref="InvalidOperationException"></exception>
            private static void ThrowForInvalidImages(IEnumerable<IconInfo> imageBuffers)
            {
                foreach (IconInfo buffer in imageBuffers)
                {
                    if (buffer.Buffer.Length == 0)
                    {
                        throw new InvalidOperationException("Image buffer cannot be empty.");
                    }
                }
            }
        }

        /// <summary>
        /// Security Identifier for the Builtin Administrators group.
        /// </summary>
        private readonly static SecurityIdentifier identifier = new(WellKnownSidType.BuiltinAdministratorsSid, null);

        /// <summary>
        /// NT Account for the Builtin Administrators group.
        /// </summary>
        private readonly static NTAccount AdminAccount = (NTAccount)identifier.Translate(typeof(NTAccount));

        /// <summary>
        /// Access rule for the Builtin Administrators group.
        /// </summary>
        private readonly static FileSystemAccessRule AccessRule = new(AdminAccount, FileSystemRights.FullControl, AccessControlType.Allow);

        /// <summary>
        /// Get a resource from a Portable Executable (PE) File as a byte array.
        /// </summary>
        /// <param name="SourceFile"></param>
        /// <param name="ResourceType"></param>
        /// <param name="ID"></param>
        /// <param name="LangID"></param>
        /// <returns></returns>
        public static byte[] GetResource(string SourceFile, string ResourceType, int ID, ushort LangID = 1033)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Getting resource '{ResourceType}' with ID '{ID}' from PE file '{SourceFile}'.");
            PortableExecutable PE_File = new(SourceFile);
            return PE_File.GetResource(new(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID))).Data;
        }

        /// <summary>
        /// Replace a resource in a Portable Executable (PE) File by a byte array.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="SourceFile"></param>
        /// <param name="ResourceType"></param>
        /// <param name="ID"></param>
        /// <param name="NewRes"></param>
        /// <param name="LangID"></param>
        public static void ReplaceResource(TreeView treeView, string SourceFile, string ResourceType, int ID, byte[] NewRes, ushort LangID = 1033)
        {
            ReplaceResource(SourceFile, ResourceType, ID, NewRes, LangID, treeView);
        }

        /// <summary>
        /// Replace a resource in a Portable Executable (PE) File by a byte array.
        /// </summary>
        /// <param name="SourceFile"></param>
        /// <param name="ResourceType"></param>
        /// <param name="ID"></param>
        /// <param name="NewRes"></param>
        /// <param name="LangID"></param>
        /// <param name="treeView"></param>
        public static void ReplaceResource(string SourceFile, string ResourceType, int ID, byte[] NewRes, ushort LangID = 1033, TreeView treeView = null)
        {
            if (Path.GetFullPath(SourceFile).ToLower().StartsWith(SysPaths.Windows, StringComparison.OrdinalIgnoreCase))
            {
                if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Replacing resource '{ResourceType}' with ID '{ID}' in PE file '{SourceFile}'.");

                // It is a system PE File that needs rights/permissions modification.

                if (Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert && Program.Settings.ThemeApplyingBehavior.PE_ModifyByDefault || !Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert && Forms.PE_Warning.NotifyAction(SourceFile, ResourceType, ID, LangID) == DialogResult.OK)
                {
                    string TempFile = Path.GetTempFileName();

                    if (treeView is not null)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.PE_GettingAccess, Path.GetFileName(SourceFile)), "admin");
                    PreparePrivileges();                                     // To get authorized access to change PE File access/permissions

                    if (treeView is not null)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.PE_CreateBackup, Path.GetFileName(SourceFile)), "pe_backup");
                    if (CreateBackup(SourceFile))                        // Makes a copy of EP File as a backup File
                    {

                        if (treeView is not null)
                            ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.PE_GetBackupPermissions, Path.GetFileName(SourceFile)), "pe_backup");
                        if (BackupPermissions(SourceFile, TempFile))     // Source File rights have been backed up successfully
                        {

                            if (treeView is not null)
                                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.PE_GetAccessToChangeResources, Path.GetFileName(SourceFile)), "admin");
                            PreparePrivileges();                             // To get authorized access to change resources for PE File

                            if (treeView is not null)
                                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.PE_PatchingPE, Path.GetFileName(SourceFile)), "pe_patch");
                            PortableExecutable PE_File = new(SourceFile);
                            PE_File.SetResource(new(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID)), NewRes);
                            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Resource '{ResourceType}' with ID '{ID}' has been replaced in PE file '{SourceFile}'.");

                            if (treeView is not null)
                                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.PE_RestoringPermissions, Path.GetFileName(SourceFile)), "pe_restore");
                            RestorePermissions(SourceFile, TempFile);        // Restore source File rights

                        }
                    }

                }
            }

            else
            {
                // It isn't in system directory and can be modified without changing rights/permissions.
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, $"Replacing '{Path.GetFileName(SourceFile)}' resources", "pe_patch");
                PortableExecutable PE_File = new(SourceFile);
                PE_File.SetResource(new(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID)), NewRes);
                if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Resource '{ResourceType}' with ID '{ID}' has been replaced in PE file '{SourceFile}'.");
            }

        }

        /// <summary>
        /// Create a backup of a Portable Executable (PE) File.
        /// </summary>
        /// <param name="SourceFile"></param>
        /// <returns></returns>
        private static bool CreateBackup(string SourceFile)
        {
            foreach (string backupFile in Directory.GetFiles(Path.GetDirectoryName(SourceFile), $"{Path.GetFileNameWithoutExtension(SourceFile)}*.bak"))
            {
                try
                {
                    if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Deleting old PE backup file `{backupFile}`");
                    File.Delete(backupFile);
                }
                catch { } // Ignore deleting backup File if it fails

                PreparePrivileges();
            }
            bool result = true;
            try
            {
                PreparePrivileges();
                string backupFile = $@"{Path.GetDirectoryName(SourceFile)}\{Path.GetFileNameWithoutExtension(SourceFile)}{Math.Abs(DateTime.Now.ToBinary())}.bak";

                File.Move(SourceFile, backupFile);
                File.Copy(backupFile, SourceFile);

                if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"A backup to PE file has been created as `{backupFile}`");
                return result;
            }
            catch
            {
                if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Error, $"Couldn't backup PE file `{SourceFile}");
                return false;
            }
        }

        /// <summary>
        /// Backup permissions of a Portable Executable (PE) File.
        /// </summary>
        /// <param name="SourceFile"></param>
        /// <param name="BackupFile"></param>
        /// <returns></returns>
        private static bool BackupPermissions(string SourceFile, string BackupFile)
        {
            FileSecurity accessControl = File.GetAccessControl(SourceFile);
            if (accessControl is null)
                return false;

            using (FileStream fileStream = File.Create(BackupFile, 1, FileOptions.None, accessControl))
            {
                fileStream.Close();
            }

            accessControl.SetOwner(AdminAccount);
            accessControl.AddAccessRule(AccessRule);
            File.SetAccessControl(SourceFile, accessControl);

            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Permissions of PE file `{SourceFile}` have been backed up to `{BackupFile}`");

            return true;
        }

        /// <summary>
        /// Restore permissions of a Portable Executable (PE) File.
        /// </summary>
        /// <param name="SourceFile"></param>
        /// <param name="BackupFile"></param>
        /// <returns></returns>
        private static bool RestorePermissions(string SourceFile, string BackupFile)
        {
            FileSecurity BackupAccessControl = File.GetAccessControl(SourceFile);
            if (BackupAccessControl is null)
                return false;

            File.SetAccessControl(SourceFile, BackupAccessControl);
            File.Delete(BackupFile);

            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Permissions of PE file `{SourceFile}` have been restored from `{BackupFile}`");

            return true;
        }

        /// <summary>
        /// Prepare privileges for a Portable Executable (PE) File.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private static void PreparePrivileges()
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, "Preparing privileges for PE file modification.");
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, "Enabling SeTakeOwnershipPrivilege, SeSecurityPrivilege, SeRestorePrivilege, and SeBackupPrivilege.");

            if (!ADVAPI.EnablePrivilege("SeTakeOwnershipPrivilege", false)) throw new Exception("Failed to get SeTakeOwnershipPrivilege");
            if (!ADVAPI.EnablePrivilege("SeSecurityPrivilege", false)) throw new Exception("Failed to get SeSecurityPrivilege");
            if (!ADVAPI.EnablePrivilege("SeRestorePrivilege", false)) throw new Exception("Failed to get SeRestorePrivilege");
            if (!ADVAPI.EnablePrivilege("SeBackupPrivilege", false)) throw new Exception("Failed to get SeBackupPrivilege");
        }

        /// <summary>
        /// Get a PNG image as <see cref="Bitmap"/> from a Portable Executable (PE) File.
        /// </summary>
        /// <param name="File"></param>
        /// <param name="ResourceID"></param>
        /// <param name="ResourceType"></param>
        /// <param name="UnfoundW"></param>
        /// <param name="UnfoundH"></param>
        /// <returns></returns>
        public static Bitmap GetPNG(string File, int ResourceID, string ResourceType = "IMAGE", int UnfoundW = 50, int UnfoundH = 50)
        {
            try
            {
                if (System.IO.File.Exists(File))
                {
                    using (MemoryStream ms = new(PE.GetResource(File, ResourceType, ResourceID)))
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

        /// <summary>
        /// Get an <see cref="Icon"/> from a <see cref="PortableExecutable"/> instance.
        /// </summary>
        /// <param name="PE"></param>
        /// <param name="iconGroupResourceIdentifier"></param>
        /// <returns></returns>
        public static Icon GetIcon(PortableExecutable PE, ResourceIdentifier iconGroupResourceIdentifier)
        {
            return Icons.PEIconGroup_ToIcon(PE, iconGroupResourceIdentifier);
        }

        /// <summary>
        /// Get an <see cref="Icon"/> from a Portable Executable (PE) File, provided by its icon group index.
        /// <br></br>
        /// <br></br>- If the icon group index is positive, the icon group is extracted by index.
        /// <br></br>- If the icon group index is negative, the icon group is extracted by resource name.
        /// </summary>
        /// <param name="dllPath"></param>
        /// <param name="iconIndex"></param>
        /// <returns></returns>
        public static Icon GetIcon(string dllPath, int iconIndex = 0)
        {
            if (!File.Exists(dllPath)) return null;

            IntPtr[] largeIcons = new IntPtr[1];
            IntPtr[] smallIcons = new IntPtr[1];

            uint result;

            if (iconIndex >= 0)
            {
                // Positive index, extract icon by index
                result = Shell32.ExtractIconEx(dllPath, iconIndex, largeIcons, smallIcons, 1);
            }
            else
            {
                // Negative index, extract icon by resource name
                result = Shell32.PrivateExtractIcons(dllPath, iconIndex, 32, 32, largeIcons, null, 1, 0);
            }

            if (result > 0)
            {
                IntPtr iconHandle = largeIcons[0]; // Use smallIcons[0] for small icons

                Icon extractedIcon = Icon.FromHandle(iconHandle).Clone() as Icon;

                // Clean up the icon handle
                Shell32.DestroyIcon(iconHandle);

                return extractedIcon;
            }

            return null;
        }

        /// <summary>
        /// Get the number of icon groups in a Portable Executable (PE) File.
        /// </summary>
        /// <param name="dllPath"></param>
        /// <returns></returns>
        public static int GetIconGroupCount(string dllPath)
        {
            if (!File.Exists(dllPath)) return 0;

            int count = 0;

            IntPtr hModule = Kernel32.LoadLibrary(dllPath);
            if (hModule == IntPtr.Zero)
            {
                int error = Marshal.GetLastWin32Error();
                return 0;
            }

            try
            {
                Kernel32.EnumResourceNames(hModule, (IntPtr)(uint)14 /*RT_GROUP_ICON*/, (h, t, name, l) =>
                {
                    count++;
                    return true; // Continue enumeration
                }, IntPtr.Zero);
            }
            finally
            {
                Kernel32.FreeLibrary(hModule);
            }

            return count;
        }
    }
}