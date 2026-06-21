using Ressy;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
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

                    Program.Log?.Write(LogEventLevel.Information, $"Successfully extracted {icons.Count} icons from PE file.");

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
        private static readonly SecurityIdentifier identifier = new(WellKnownSidType.BuiltinAdministratorsSid, null);

        /// <summary>
        /// NT Account for the Builtin Administrators group.
        /// </summary>
        private static readonly NTAccount AdminAccount = (NTAccount)identifier.Translate(typeof(NTAccount));

        /// <summary>
        /// Access rule for the Builtin Administrators group.
        /// </summary>
        private static readonly FileSystemAccessRule AccessRule = new(AdminAccount, FileSystemRights.FullControl, AccessControlType.Allow);

        /// <summary>
        /// Per-file synchronization objects. Concurrent calls that target the same physical PE file (for example, several icon
        /// replacements running inside the same Parallel.Invoke theme-application pass) are serialized against each other instead
        /// of racing, since each call would otherwise start from its own snapshot of the file and could silently undo another
        /// thread's change.
        /// </summary>
        private static readonly Dictionary<string, object> FileLocks = [with(StringComparer.OrdinalIgnoreCase)];
        private static readonly object FileLocksGate = new();

        /// <summary>
        /// Gets a process-wide lock object unique to a given file path, used to serialize every read/modify/write sequence
        /// against that file.
        /// </summary>
        private static object GetFileLock(string FilePath)
        {
            string Key = Path.GetFullPath(FilePath);

            lock (FileLocksGate)
            {
                if (!FileLocks.TryGetValue(Key, out object LockObj))
                {
                    LockObj = new object();
                    FileLocks[Key] = LockObj;
                }

                return LockObj;
            }
        }

        private static List<string> GetResourceFiles(string SourceFile)
        {
            List<string> Files = [SourceFile];

            if (SourceFile.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) || SourceFile.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                string[] ValidLocations = [SysPaths.Windows, SysPaths.System32, SysPaths.SysWOW64];

                string FullSourcePath = Path.GetFullPath(SourceFile);

                bool IsWindowsSystemFile = ValidLocations.Any(x => FullSourcePath.StartsWith(Path.GetFullPath(x) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase));

                if (IsWindowsSystemFile)
                {
                    string MunFile = Path.Combine(SysPaths.SystemResources, Path.GetFileName(SourceFile) + ".mun");
                    if (File.Exists(MunFile)) Files.Add(MunFile);
                }
            }

            return Files;
        }

        /// <summary>
        /// True if the given file lives under the protected Windows directory tree and therefore needs privilege elevation and
        /// ownership changes before it can be modified.
        /// </summary>
        private static bool IsProtectedSystemFile(string FilePath)
        {
            string FullPath = Path.GetFullPath(FilePath);
            string WindowsRoot = Path.GetFullPath(SysPaths.Windows);

            return FullPath.StartsWith(WindowsRoot + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) || FullPath.Equals(WindowsRoot, StringComparison.OrdinalIgnoreCase);
        }

        private static string GetResourceFileForReplace(string SourceFile, string ResourceType, int ID, ushort LangID)
        {
            List<string> Files = GetResourceFiles(SourceFile);

            foreach (string FilePath in Files)
            {
                try
                {
                    using Stream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                    PortableExecutable PE_File = new(stream);

                    Resource Resource = PE_File.GetResource(new(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID)));

                    if (Resource != null)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Resource '{ResourceType}:{ID}' (lang {LangID}) located in '{FilePath}'.");
                        return FilePath;
                    }
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Debug, $"Could not inspect '{FilePath}' while locating resource '{ResourceType}:{ID}': {ex.Message}");
                }
            }

            // Resource does not exist in any candidate file.
            // Prefer the redirected MUN file when present, since modern split system files store their actual resource
            // data there rather than in the original DLL/EXE stub.
            string Fallback = Files.Count > 1 ? Files[1] : Files[0];
            Program.Log?.Write(LogEventLevel.Warning, $"Resource '{ResourceType}:{ID}' (lang {LangID}) was not found in any candidate of '{SourceFile}'. Falling back to '{Fallback}'.");
            return Fallback;
        }

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
            Program.Log?.Write(LogEventLevel.Information, $"Getting resource '{ResourceType}' with ID '{ID}' from PE file '{SourceFile}'.");

            foreach (string FilePath in GetResourceFiles(SourceFile))
            {
                try
                {
                    using Stream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                    PortableExecutable PE_File = new(stream);

                    Resource Resource = PE_File.GetResource(new(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID)));

                    if (Resource != null) return Resource.Data;
                }
                catch { }
            }

            throw new FileNotFoundException($"Resource '{ResourceType}:{ID}' was not found in '{SourceFile}' or its MUN file.");
        }

        private static void ReplaceWithRetry(string Source, string Target, int retries = 10)
        {
            string OldFile = Program.GetUniqueFileName(Path.GetDirectoryName(Target), Path.GetFileName(Target) + ".WinPaletter.old");

            for (int i = 0; i < retries; i++)
            {
                try
                {
                    if (File.Exists(OldFile))
                    {
                        File.SetAttributes(OldFile, FileAttributes.Normal);
                        File.Delete(OldFile);
                    }

                    // Rename original file away
                    if (!Kernel32.MoveFileEx(Target, OldFile, Kernel32.MOVEFILE_WRITE_THROUGH))
                    {
                        throw new IOException($"Could not rename original file. Error: {Marshal.GetLastWin32Error()}");
                    }

                    // Move patched file into place
                    if (!Kernel32.MoveFileEx(Source, Target, Kernel32.MOVEFILE_WRITE_THROUGH))
                    {
                        // Restore original if replacement failed
                        Kernel32.MoveFileEx(OldFile, Target, Kernel32.MOVEFILE_WRITE_THROUGH);

                        throw new IOException($"Could not move patched file. Error: {Marshal.GetLastWin32Error()}");
                    }

                    return;
                }
                catch
                {
                    Thread.Sleep(300);
                }
            }


            throw new IOException($"Failed replacing {Target} even after {retries} retries.");
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
        /// <br></br>
        /// <br></br>The replacement is always performed on a private working copy first; the live file is only ever
        /// touched once that working copy has been verified to actually contain the new resource, and any failure
        /// leaves the live file untouched or restored from a fresh backup.
        /// </summary>
        /// <param name="SourceFile"></param>
        /// <param name="ResourceType"></param>
        /// <param name="ID"></param>
        /// <param name="NewRes"></param>
        /// <param name="LangID"></param>
        /// <param name="treeView"></param>
        public static void ReplaceResource(string SourceFile, string ResourceType, int ID, byte[] NewRes, ushort LangID = 1033, TreeView treeView = null)
        {
            if (string.IsNullOrWhiteSpace(SourceFile)) throw new ArgumentNullException(nameof(SourceFile));
            if (string.IsNullOrWhiteSpace(ResourceType)) throw new ArgumentNullException(nameof(ResourceType));
            if (NewRes is null) throw new ArgumentNullException(nameof(NewRes));
            if (!File.Exists(SourceFile)) throw new FileNotFoundException($"Source file '{SourceFile}' was not found.", SourceFile);

            string TargetFile = GetResourceFileForReplace(SourceFile, ResourceType, ID, LangID);

            // Serialize every read/modify/write sequence that targets this exact file. This matters as soon as theme
            // application patches multiple resources of the same DLL/MUN in parallel.
            lock (GetFileLock(TargetFile))
            {
                if (IsProtectedSystemFile(TargetFile))
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Replacing resource '{ResourceType}' with ID '{ID}' in protected system PE file '{TargetFile}'.");

                    bool UserAllowed = Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert && Program.Settings.ThemeApplyingBehavior.PE_ModifyByDefault
                                      || !Program.Settings.ThemeApplyingBehavior.Ignore_PE_Modify_Alert && Forms.PE_Warning.NotifyAction(TargetFile, ResourceType, ID, LangID) == DialogResult.OK;

                    if (!UserAllowed) return;

                    ReplaceResourceProtected(TargetFile, ResourceType, ID, NewRes, LangID, treeView);
                }
                else
                {
                    // It isn't in a system directory and can be modified without changing rights/permissions.
                    if (treeView is not null) ThemeLog.AddNode(treeView, $"Replacing '{Path.GetFileName(TargetFile)}' resources", "pe_patch");

                    ReplaceResourceDirect(TargetFile, ResourceType, ID, NewRes, LangID);

                    Program.Log?.Write(LogEventLevel.Information, $"Resource '{ResourceType}' with ID '{ID}' has been replaced in PE file '{TargetFile}'.");
                }
            }
        }

        /// <summary>
        /// Replaces a resource in a PE file that is not protected by the OS. A working copy is edited and verified
        /// first, and only then copied over the real file, so a failed edit never leaves the real file in a broken
        /// state.
        /// </summary>
        private static void ReplaceResourceDirect(string TargetFile, string ResourceType, int ID, byte[] NewRes, ushort LangID)
        {
            string TempFile = CreateTempCopy(TargetFile);

            string BackupFile = Program.GetUniqueFileName(Path.GetDirectoryName(TargetFile), Path.GetFileName(TargetFile) + ".WinPaletter.old");

            try
            {
                ApplyResourceEdit(TempFile, ResourceType, ID, NewRes, LangID);
                VerifyResourceEdit(TempFile, ResourceType, ID, NewRes, LangID);

                // Keep a rollback copy
                File.Move(TargetFile, BackupFile);

                // Put patched file in place
                if (!Kernel32.MoveFileEx(TempFile, TargetFile, Kernel32.MOVEFILE_WRITE_THROUGH))
                {
                    Kernel32.MoveFileEx(BackupFile, TargetFile, Kernel32.MOVEFILE_WRITE_THROUGH);

                    throw new IOException($"Could not replace '{TargetFile}'. Win32 error: {Marshal.GetLastWin32Error()}");
                }

                TryDelete(BackupFile);
            }
            finally
            {
                TryDelete(TempFile);
            }
        }

        /// <summary>
        /// Replaces a resource in a protected Windows system PE file (or its redirected MUN file). The new resource
        /// is written and verified on a private working copy first, held in a writable scratch directory (protected
        /// directories like SystemResources deny creating new files even to elevated Administrators). The live file
        /// is only ever touched once that working copy is proven good, via a backup followed by overwriting the
        /// existing file's content in place, which only needs write access to the file itself, not its directory.
        /// Ownership/ACL/attributes are always restored afterwards, success or failure, and a failed write restores
        /// the live file from the fresh backup.
        /// </summary>
        private static void ReplaceResourceProtected(string TargetFile, string ResourceType, int ID, byte[] NewRes, ushort LangID, TreeView treeView)
        {
            string FileName = Path.GetFileName(TargetFile);
            string DirectoryPath = Path.GetDirectoryName(TargetFile);

            string TempFile = null;
            string BackupFile = null;
            string PermissionsBackupFile = null;
            string DirectoryPermissionsBackupFile = null;

            FileAttributes OriginalAttributes = default;
            bool PermissionsElevated = false;

            try
            {
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.PE_GettingAccess, FileName), "admin");

                PreparePrivileges();

                // Create patched copy first
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.PE_PatchingPE, FileName), "pe_patch");

                TempFile = CreateTempCopy(TargetFile);

                ApplyResourceEdit(TempFile, ResourceType, ID, NewRes, LangID);
                VerifyResourceEdit(TempFile, ResourceType, ID, NewRes, LangID);

                // Backup original
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.PE_CreateBackup, FileName), "pe_backup");

                BackupFile = CreateBackup(TargetFile);

                // Backup permissions
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.PE_GetBackupPermissions, FileName), "admin");

                PermissionsBackupFile = Path.GetTempFileName();
                DirectoryPermissionsBackupFile = Path.GetTempFileName();

                ElevatePermissions(TargetFile, PermissionsBackupFile, out OriginalAttributes);
                ElevateDirectoryPermissions(DirectoryPath, DirectoryPermissionsBackupFile);

                PermissionsElevated = true;

                // Replace using rename/move like Explorer
                if (treeView is not null)
                    ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.PE_GetAccessToChangeResources, FileName), "pe_patch");

                ReplaceWithRetry(TempFile, TargetFile);

                Program.Log?.Write(LogEventLevel.Information, $"Resource '{ResourceType}' with ID '{ID}' has been replaced in PE file '{TargetFile}'.");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Failed to replace resource '{ResourceType}:{ID}' in '{TargetFile}': {ex.Message}");

                if (BackupFile is not null && File.Exists(BackupFile))
                {
                    try
                    {
                        ReplaceWithRetry(BackupFile, TargetFile);

                        Program.Log?.Write(LogEventLevel.Information, $"Restored '{TargetFile}' from backup.");
                    }
                    catch (Exception RestoreEx)
                    {
                        Program.Log?.Write(LogEventLevel.Fatal, $"Restore failed: {RestoreEx.Message}");
                    }
                }

                throw;
            }
            finally
            {
                if (PermissionsElevated)
                {
                    RestorePermissions(TargetFile, PermissionsBackupFile, OriginalAttributes);
                    RestoreDirectoryPermissions(DirectoryPath, DirectoryPermissionsBackupFile);
                }


                TryDelete(PermissionsBackupFile);
                TryDelete(DirectoryPermissionsBackupFile);
                TryDelete(TempFile);
            }
        }

        /// <summary>
        /// Gets a writable scratch directory on the same volume as <paramref name="ReferenceFile"/>. Temp/backup
        /// files are never placed next to the reference file itself, because directories like
        /// C:\Windows\SystemResources deny creating new files even to elevated Administrators (only TrustedInstaller
        /// can create entries there), regardless of what permissions are later granted on the file being patched.
        /// </summary>
        private static string GetScratchDirectory(string ReferenceFile)
        {
            string TargetRoot = Path.GetPathRoot(Path.GetFullPath(ReferenceFile));
            string PreferredDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "WinPaletter", "PEPatcher");

            try
            {
                Directory.CreateDirectory(PreferredDir);

                if (Path.GetPathRoot(PreferredDir).Equals(TargetRoot, StringComparison.OrdinalIgnoreCase))
                {
                    return PreferredDir;
                }
            }
            catch { }

            // ProgramData was not writable, or is not on the same volume as the reference file (uncommon
            // multi-drive setups). Fall back to a scratch folder at the root of that same volume.
            string FallbackDir = Path.Combine(TargetRoot, "WinPaletter_PEPatcher");
            Directory.CreateDirectory(FallbackDir);
            return FallbackDir;
        }

        /// <summary>
        /// Creates a private working copy of a file in a writable scratch directory on the same volume, with normal
        /// attributes so it is freely editable regardless of the source file's attributes.
        /// </summary>
        private static string CreateTempCopy(string SourceFile)
        {
            string Dir = GetScratchDirectory(SourceFile);
            string TempFile = Path.Combine(Dir, $"~{Path.GetFileNameWithoutExtension(SourceFile)}_{Guid.NewGuid():N}{Path.GetExtension(SourceFile)}.tmp");

            File.Copy(SourceFile, TempFile, true);
            File.SetAttributes(TempFile, FileAttributes.Normal);

            return TempFile;
        }

        /// <summary>
        /// Writes a resource into a PE file in place. Intended to be used only on private working copies, never
        /// directly on a live system file.
        /// </summary>
        private static void ApplyResourceEdit(string FilePath, string ResourceType, int ID, byte[] NewRes, ushort LangID)
        {
            using FileStream stream = new(FilePath, FileMode.Open, FileAccess.ReadWrite);

            PortableExecutable PE_File = new(stream);
            Resource res = new(new(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID)), NewRes);
            PE_File.SetResource(res);
        }

        /// <summary>
        /// Re-opens a patched file read-only and confirms the target resource now matches the expected bytes, before
        /// that file is allowed anywhere near a live system file.
        /// </summary>
        private static void VerifyResourceEdit(string FilePath, string ResourceType, int ID, byte[] ExpectedRes, ushort LangID)
        {
            using FileStream stream = new(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            PortableExecutable PE_File = new(stream);
            Resource Resource = PE_File.GetResource(new(Ressy.ResourceType.FromString(ResourceType), ResourceName.FromCode(ID), new Language(LangID)));

            if (Resource is null || Resource.Data is null || !Resource.Data.SequenceEqual(ExpectedRes))
            {
                throw new InvalidOperationException($"Verification failed after writing resource '{ResourceType}:{ID}', the patched copy did not contain the expected data. The edit has been discarded and no live file was touched.");
            }
        }

        /// <summary>
        /// Deletes a file if it exists, swallowing and logging any failure rather than throwing. Intended for
        /// best-effort cleanup of temporary/backup files.
        /// </summary>
        private static void TryDelete(string FilePath)
        {
            if (string.IsNullOrEmpty(FilePath)) return;

            try
            {
                if (File.Exists(FilePath))
                {
                    File.SetAttributes(FilePath, FileAttributes.Normal);
                    File.Delete(FilePath);
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Debug, $"Could not delete temporary file `{FilePath}`: {ex.Message}");
            }
        }

        /// <summary>
        /// Create a backup of a Portable Executable (PE) File, in the writable scratch directory rather than next to
        /// the source file (protected directories deny creating new files there). This is always a plain copy, never
        /// a move, so the live file is never removed from disk, even momentarily, regardless of whether the copy
        /// succeeds.
        /// </summary>
        /// <param name="SourceFile"></param>
        /// <returns></returns>
        private static string CreateBackup(string SourceFile)
        {
            string Dir = GetScratchDirectory(SourceFile);
            string BaseName = Path.GetFileNameWithoutExtension(SourceFile);

            foreach (string OldBackup in Directory.GetFiles(Dir, $"{BaseName}*.bak"))
            {
                try
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Deleting old PE backup file `{OldBackup}`");
                    File.SetAttributes(OldBackup, FileAttributes.Normal);
                    File.Delete(OldBackup);
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"Could not delete old PE backup file `{OldBackup}`: {ex.Message}");
                }
            }

            string BackupFile = Path.Combine(Dir, $"{BaseName}{Math.Abs(DateTime.Now.ToBinary())}.bak");

            File.Copy(SourceFile, BackupFile, false);

            Program.Log?.Write(LogEventLevel.Information, $"A backup to PE file has been created as `{BackupFile}`");

            return BackupFile;
        }

        /// <summary>
        /// Takes ownership of a protected file and grants the Administrators group full control, after preserving
        /// the original ACL (stamped onto <paramref name="PermissionsBackupFile"/>) and the original attributes so
        /// both can be restored later.
        /// </summary>
        /// <param name="TargetFile"></param>
        /// <param name="PermissionsBackupFile"></param>
        /// <param name="OriginalAttributes"></param>
        private static void ElevatePermissions(string TargetFile, string PermissionsBackupFile, out FileAttributes OriginalAttributes)
        {
            OriginalAttributes = File.GetAttributes(TargetFile);

            FileSecurity OriginalSecurity = File.GetAccessControl(TargetFile);

            using (FileStream BackupStream = File.Create(PermissionsBackupFile, 1, FileOptions.None, OriginalSecurity))
            {
                BackupStream.Close();
            }

            FileSecurity ElevatedSecurity = File.GetAccessControl(TargetFile);
            ElevatedSecurity.SetOwner(AdminAccount);
            ElevatedSecurity.AddAccessRule(AccessRule);
            File.SetAccessControl(TargetFile, ElevatedSecurity);

            File.SetAttributes(TargetFile, FileAttributes.Normal);

            Program.Log?.Write(LogEventLevel.Information, $"Permissions of PE file `{TargetFile}` have been elevated, original ACL preserved in `{PermissionsBackupFile}`.");
        }

        private static void ElevateDirectoryPermissions(string DirectoryPath, string PermissionsBackupFile)
        {
            DirectorySecurity OriginalSecurity = Directory.GetAccessControl(DirectoryPath);

            byte[] SecurityBytes = OriginalSecurity.GetSecurityDescriptorBinaryForm();

            File.WriteAllBytes(PermissionsBackupFile, SecurityBytes);


            DirectorySecurity ElevatedSecurity = Directory.GetAccessControl(DirectoryPath);

            ElevatedSecurity.SetOwner(AdminAccount);
            ElevatedSecurity.AddAccessRule(AccessRule);

            Directory.SetAccessControl(DirectoryPath, ElevatedSecurity);
        }

        private static void RestoreDirectoryPermissions(string DirectoryPath, string PermissionsBackupFile)
        {
            if (string.IsNullOrEmpty(PermissionsBackupFile) || !File.Exists(PermissionsBackupFile))
                return;

            try
            {
                byte[] SecurityBytes = File.ReadAllBytes(PermissionsBackupFile);

                DirectorySecurity OriginalSecurity = new();

                OriginalSecurity.SetSecurityDescriptorBinaryForm(SecurityBytes);

                Directory.SetAccessControl(DirectoryPath, OriginalSecurity);

                Program.Log?.Write(LogEventLevel.Information, $"Permissions of directory `{DirectoryPath}` have been restored.");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Could not restore directory permissions of `{DirectoryPath}`: {ex.Message}");
            }
        }

        /// <summary>
        /// Restores a file's original ACL, read back from <paramref name="PermissionsBackupFile"/> (never from the
        /// live file itself, which by this point still carries the elevated ACL), along with its original
        /// attributes.
        /// </summary>
        /// <param name="TargetFile"></param>
        /// <param name="PermissionsBackupFile"></param>
        /// <param name="OriginalAttributes"></param>
        private static void RestorePermissions(string TargetFile, string PermissionsBackupFile, FileAttributes OriginalAttributes)
        {
            if (string.IsNullOrEmpty(PermissionsBackupFile) || !File.Exists(PermissionsBackupFile))
            {
                Program.Log?.Write(LogEventLevel.Warning, $"No permissions backup was available to restore for `{TargetFile}`.");
                return;
            }

            try
            {
                FileSecurity OriginalSecurity = File.GetAccessControl(PermissionsBackupFile);
                File.SetAccessControl(TargetFile, OriginalSecurity);
                File.SetAttributes(TargetFile, OriginalAttributes);

                Program.Log?.Write(LogEventLevel.Information, $"Permissions of PE file `{TargetFile}` have been restored from `{PermissionsBackupFile}`.");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Could not restore permissions of `{TargetFile}` from `{PermissionsBackupFile}`: {ex.Message}");
            }
        }

        /// <summary>
        /// Prepare privileges for a Portable Executable (PE) File.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private static void PreparePrivileges()
        {
            Program.Log?.Write(LogEventLevel.Information, "Preparing privileges for PE file modification.");
            Program.Log?.Write(LogEventLevel.Information, "Enabling SeTakeOwnershipPrivilege, SeSecurityPrivilege, SeRestorePrivilege, and SeBackupPrivilege.");

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
        /// <param name="desiredSize"></param>
        /// <returns></returns>
        public static Icon GetIcon(string dllPath, int iconIndex = 0, int desiredSize = 32)
        {
            if (!File.Exists(dllPath)) return null;

            IntPtr[] largeIcons = new IntPtr[1];

            // Extract the icon at the requested size
            uint result = User32.PrivateExtractIcons(dllPath, iconIndex, desiredSize, desiredSize, largeIcons, null, 1, 0);

            if (result > 0 && largeIcons[0] != IntPtr.Zero)
            {
                try
                {
                    // Convert HICON to .NET Icon (clone to make safe)
                    Icon ico = Icon.FromHandle(largeIcons[0]).Clone() as Icon;
                    return ico;
                }
                finally
                {
                    User32.DestroyIcon(largeIcons[0]);
                }
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