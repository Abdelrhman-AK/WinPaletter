using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.SrClient;

namespace WinPaletter
{
    /// <summary>
    /// Metadata for a restore point created by WinPaletter.
    /// </summary>
    public sealed class RestorePointInfo
    {
        /// <summary>
        /// The sequence number used by the OS to identify this restore point. Required to delete it.
        /// </summary>
        public uint SequenceNumber { get; set; }

        /// <summary>
        /// Full description string as stored by the OS, including the WinPaletter tag prefix.
        /// </summary>
        public string RawName { get; set; }

        /// <summary>
        /// RawName string with the WinPaletter tag prefix stripped off.
        /// </summary>
        public string Name => RawName != null && RawName.StartsWith(SystemRestoreHelper.Tag, StringComparison.OrdinalIgnoreCase)
            ? RawName.Substring(SystemRestoreHelper.Tag.Length)
            : RawName;

        /// <summary>
        /// When the restore point was created.
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// The restore point type reported by the OS.
        /// </summary>
        public RestorePointType RestorePointType { get; set; }

        /// <summary>
        /// The event type reported by the OS.
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// Returns a comprehensive string representation of all properties in the restore point.
        /// </summary>
        public override string ToString()
        {
            return $"RestorePointInfo [" +
                   $"\n  {nameof(SequenceNumber)}: {SequenceNumber}," +
                   $"\n  {nameof(Name)}: \"{Name}\"," +
                   $"\n  {nameof(RawName)}: \"{RawName}\"," +
                   $"\n  {nameof(CreationTime)}: {CreationTime:yyyy-MM-dd HH:mm:ss}," +
                   $"\n  {nameof(RestorePointType)}: {RestorePointType}," +
                   $"\n  {nameof(EventType)}: {EventType}," +
                   $"\n]";
        }
    }

    /// <summary>
    /// A class that helps WinPaletter with System Restore Points creation, enumeration and deletion.
    /// </summary>
    public static class SystemRestoreHelper
    {
        #region Constants

        /// <summary>
        /// Prefix used to tag every restore point WinPaletter creates, so it can later be told
        /// apart from restore points created by Windows Update, driver installers, etc.
        /// </summary>
        internal const string Tag = "WinPaletter - ";

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to create a system restore point.
        /// </summary>
        /// <param name="description">The description of the restore point. The WinPaletter tag is added automatically.</param>
        public static bool CreateRestorePoint(string description)
        {
            UI.WP.ProgressDialog dlg = null;
            bool result = false;
            bool completed = false;

            try
            {
                if (string.IsNullOrWhiteSpace(description))
                {
                    throw new ArgumentException("Restore point description cannot be empty.", nameof(description));
                }

                if (!User.Administrator)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Creating a restore point without administrative privileges; this will likely fail.");
                }

                // Ensure System Restore is enabled
                if (!EnsureSystemRestoreEnabled()) return false;

                string taggedDescription = Tag + description;

                dlg = new()
                {
                    Animation = UI.WP.AnimationResource.GetShellAnimation(UI.WP.ShellAnimation.FlyingPapers),
                    Text = Program.Localization.Strings.General.RestorePoint_DialogTitle,
                    Description = description,
                    ProgressBarStyle = UI.WP.ProgressBarStyle.MarqueeProgressBar,
                    ShowCancelButton = false,
                    MinimizeBox = false,
                    WindowTitle = Application.ProductName,
                };

                dlg.DoWork += (s, e) =>
                {
                    // Reset frequency to 0 instead of 24 hours
                    SetRestorePointFrequency(0);

                    Program.Log?.Write(LogEventLevel.Information, $"Creating system restore point with description: {taggedDescription}");

                    result = CreateRestorePointNative(taggedDescription);

                    if (result)
                    {
                        Program.Log?.Write(LogEventLevel.Information, "System restore point created successfully via native API");
                    }
                    else
                    {
                        Program.Log?.Write(LogEventLevel.Warning, "Native API failed, falling back to WMI");
                        result = CreateRestorePointWMI(taggedDescription);
                    }
                };

                dlg.RunWorkerCompleted += (s, e) =>
                {
                    if (e.Error != null)
                    {
                        Program.Log?.Write(LogEventLevel.Error, "Failed to create system restore point", e.Error);
                        result = false;
                    }
                    else if (result)
                    {
                        Program.Log?.Write(LogEventLevel.Information, "System restore point creation completed successfully");
                    }
                    else
                    {
                        Program.Log?.Write(LogEventLevel.Error, "Failed to create system restore point via any available driver");
                    }

                    completed = true;
                };

                // ShowDialog() only starts the shell dialog and the background worker, then returns
                // immediately - it does not block until the work is finished. Pump the message loop
                // here until RunWorkerCompleted has actually fired, otherwise we return the still-default result.
                dlg.ShowDialog();

                while (!completed)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(15);
                }

                return result;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Failed to create system restore point", ex);
                return false;
            }
            finally
            {
                dlg?.Dispose();
            }
        }

        /// <summary>
        /// Lists restore points that were created by WinPaletter (identified by tag), ordered from newest to oldest.
        /// Does not include restore points created by Windows Update, driver installs, or other software.
        /// </summary>
        public static List<RestorePointInfo> GetWinPaletterRestorePoints()
        {
            List<RestorePointInfo> list = [];

            try
            {
                long? shadowStorageUsed = TryGetSystemVolumeShadowStorageUsedBytes();

                using (ManagementObjectSearcher searcher = new(@"root\default", "SELECT * FROM SystemRestore"))
                {
                    foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                    {
                        string description = obj["Description"]?.ToString() ?? string.Empty;

                        if (!description.StartsWith(Tag, StringComparison.OrdinalIgnoreCase)) continue;

                        uint sequenceNumber = obj["SequenceNumber"] != null ? Convert.ToUInt32(obj["SequenceNumber"]) : 0;

                        DateTime creationTime = DateTime.MinValue;
                        string rawCreationTime = obj["CreationTime"]?.ToString();
                        if (!string.IsNullOrEmpty(rawCreationTime))
                        {
                            try
                            {
                                creationTime = ManagementDateTimeConverter.ToDateTime(rawCreationTime);
                            }
                            catch (Exception ex)
                            {
                                Program.Log?.Write(LogEventLevel.Warning, $"Could not parse creation time for restore point #{sequenceNumber}", ex);
                            }
                        }

                        int restorePointTypeRaw = obj["RestorePointType"] != null ? Convert.ToInt32(obj["RestorePointType"]) : -1;
                        int eventTypeRaw = obj["EventType"] != null ? Convert.ToInt32(obj["EventType"]) : -1;

                        RestorePointInfo info = new()
                        {
                            SequenceNumber = sequenceNumber,
                            RawName = description,
                            CreationTime = creationTime,
                            RestorePointType = (RestorePointType)restorePointTypeRaw,
                            EventType = (EventType)eventTypeRaw
                        };

                        list.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Failed to enumerate WinPaletter restore points", ex);
            }

            return [.. list.OrderByDescending(rp => rp.CreationTime)];
        }

        /// <summary>
        /// Gets the total shadow copy storage currently used on the system volume, in bytes. This is a
        /// volume-wide figure covering every restore point (WinPaletter's and everyone else's) plus any
        /// other VSS-based shadow copies on that volume - Windows does not expose per-restore-point disk
        /// usage through any documented API, so there is no way to attribute this total to an individual
        /// <see cref="RestorePointInfo"/>. Always null on Windows XP, since restore points there are not
        /// VSS-backed and this WMI class has nothing to report.
        /// </summary>
        public static long? GetSystemVolumeShadowStorageUsedBytes()
        {
            if (OS.WXP)
            {
                return null;
            }

            try
            {
                string systemDrive = Program.SystemPartition + ":\\";

                using (ManagementObjectSearcher searcher = new(@"root\cimv2", "SELECT * FROM Win32_ShadowStorage"))
                {
                    foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (obj["UsedSpace"] == null) continue;

                        try
                        {
                            using (ManagementObject volume = new(obj["Volume"]?.ToString() ?? string.Empty))
                            {
                                string deviceId = volume["DeviceID"]?.ToString();

                                // Prefer the entry that matches the system drive when the volume path resolves.
                                if (!string.IsNullOrEmpty(deviceId) && deviceId.IndexOf(systemDrive.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    return Convert.ToInt64(obj["UsedSpace"]);
                                }
                            }
                        }
                        catch (ManagementException)
                        {
                            // Volume path could not be resolved on this OS version; ignore and keep looking.
                        }
                    }

                    // No entry matched the system drive specifically (or the match couldn't be resolved); fall back to the first available shadow storage entry as a rough indicator.
                    foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (obj["UsedSpace"] != null)
                        {
                            return Convert.ToInt64(obj["UsedSpace"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Warning, "Could not read shadow storage usage", ex);
            }

            return null;
        }

        /// <summary>
        /// Deletes a single restore point by its sequence number.
        /// </summary>
        /// <param name="sequenceNumber">The sequence number of the restore point, as returned by <see cref="GetWinPaletterRestorePoints"/>.</param>
        public static bool DeleteRestorePoint(uint sequenceNumber)
        {
            try
            {
                if (!User.Administrator)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Deleting a restore point without administrative privileges; this will likely fail.");
                }

                Program.Log?.Write(LogEventLevel.Information, $"Deleting restore point #{sequenceNumber}");

                // SRRemoveRestorePoint returns 0 (ERROR_SUCCESS) on success.
                int result = SrClient.SRRemoveRestorePoint(sequenceNumber);

                if (result == 0)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Restore point #{sequenceNumber} deleted successfully");
                    return true;
                }

                Program.Log?.Write(LogEventLevel.Error, $"SRRemoveRestorePoint failed for #{sequenceNumber} with code {result}. " +
                    "Common causes: not running elevated, or the restore point was already merged/removed by the OS.");
                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Failed to delete restore point #{sequenceNumber}", ex);
                return false;
            }
        }

        /// <summary>
        /// Deletes every restore point created by WinPaletter (identified by tag). Does not touch restore points created by anything else.
        /// </summary>
        /// <returns>The count of restore points successfully deleted and the count that failed.</returns>
        public static (int Succeeded, int Failed) DeleteAllWinPaletterRestorePoints()
        {
            int succeeded = 0;
            int failed = 0;

            foreach (RestorePointInfo restorePoint in GetWinPaletterRestorePoints())
            {
                if (DeleteRestorePoint(restorePoint.SequenceNumber))
                {
                    succeeded++;
                }
                else
                {
                    failed++;
                }
            }

            Program.Log?.Write(LogEventLevel.Information, $"Deleted {succeeded} WinPaletter restore point(s), {failed} failed");

            return (succeeded, failed);
        }

        /// <summary>
        /// Gets a value indicating whether System Restore is enabled for the system drive.
        /// </summary>
        public static bool Enabled
        {
            get
            {
                try
                {
                    return IsSystemRestoreEnabledForDrive(Program.SystemPartition + ":");
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, "Failed to check System Restore enabled status", ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// Enables or disables System Restore for a given drive.
        /// </summary>
        public static void SetSystemRestoreStatus(char driveLetter, bool enable)
        {
            try
            {
                string drive = driveLetter + ":";
                Program.Log?.Write(LogEventLevel.Information, $"Setting system restore status for drive {drive} to {enable}");

                int result = enable ? SrClient.EnableSR(drive) : SrClient.DisableSR(drive);

                if (result != 0)
                {
                    throw new InvalidOperationException($"Failed to {(enable ? "enable" : "disable")} System Restore. Return code: {result}");
                }

                // Verify the change took effect
                if (IsSystemRestoreEnabledForDrive(drive) != enable)
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"System Restore status change may not have taken effect immediately");
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Failed to set system restore status", ex);
                throw;
            }
        }

        #endregion

        #region Private Implementation

        private static bool EnsureSystemRestoreEnabled()
        {
            if (Enabled)
                return true;

            DialogResult result = MsgBox(Program.Localization.Strings.Messages.SysRestore_Msg0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, string.Format(Program.Localization.Strings.Messages.SysRestore_Msg1, Program.SystemPartition));

            if (result == DialogResult.Yes)
            {
                SetSystemRestoreStatus(Program.SystemPartition, true);
                // Give it a moment to take effect
                System.Threading.Thread.Sleep(1000);
                return Enabled;
            }

            return false;
        }

        private static bool CreateRestorePointNative(string description)
        {
            try
            {
                // Begin the restore point
                RESTOREPOINTINFOW restorePointInfo = new()
                {
                    dwEventType = (int)EventType.BeginSystemChange,
                    dwRestorePtType = (int)RestorePointType.ApplicationInstall,
                    llSequenceNumber = 0,
                    szDescription = description
                };

                bool result = SrClient.SRSetRestorePointW(ref restorePointInfo, out STATEMGRSTATUS status);

                if (!result || status.nStatus != 0)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"Native API failed with status: {status.nStatus}");
                    return false;
                }

                // End the restore point
                restorePointInfo.dwEventType = (int)EventType.EndSystemChange;
                SrClient.SRSetRestorePointW(ref restorePointInfo, out _);

                return true;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Native API restore point creation failed", ex);
                return false;
            }
        }

        private static bool CreateRestorePointWMI(string description)
        {
            try
            {
                ManagementScope scope = new(@"\\.\root\default");
                scope.Connect();

                using (ManagementClass systemRestore = new(scope.Path.Path, "SystemRestore", new ObjectGetOptions()))
                {
                    ManagementBaseObject parameters = systemRestore.GetMethodParameters("CreateRestorePoint");
                    parameters["Description"] = description;
                    parameters["RestorePointType"] = (int)RestorePointType.ApplicationInstall;
                    parameters["EventType"] = (int)EventType.BeginSystemChange;

                    ManagementBaseObject outParams = systemRestore.InvokeMethod("CreateRestorePoint", parameters, null);

                    if (outParams != null && outParams.Properties["ReturnValue"] != null)
                    {
                        uint result = (uint)outParams.Properties["ReturnValue"].Value;
                        return result == 0;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "WMI restore point creation failed", ex);
                return false;
            }
        }

        private static bool IsSystemRestoreEnabledForDrive(string drive)
        {
            try
            {
                // Check global disable flag
                string disableSR = ReadReg<string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", "DisableSR");
                if (disableSR != null && disableSR.ToString() == "1")
                {
                    return false; // System Restore is globally disabled
                }

                // Check per-drive settings
                string driveLetter = drive.TrimEnd(':');
                string driveConfig = ReadReg<string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", driveLetter);

                // If the drive has a config value of 0, it's disabled
                if (driveConfig != null && driveConfig.ToString() == "0")
                {
                    return false;
                }

                // Also check RPSessionInterval as fallback
                string rpInterval = ReadReg<string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", "RPSessionInterval");
                if (rpInterval != null && rpInterval.ToString() == "1")
                {
                    // This indicates System Restore is generally enabled But we need to verify drive-specific settings
                    return driveConfig == null || driveConfig.ToString() != "0";
                }

                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Error checking System Restore status for drive {drive}", ex);
                return false;
            }
        }

        private static void SetRestorePointFrequency(int frequency)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore"))
                {
                    key?.SetValue("SystemRestorePointCreationFrequency", frequency, RegistryValueKind.DWord);
                }
                Program.Log?.Write(LogEventLevel.Information, $"Set restore point creation frequency to {frequency}");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Warning, "Failed to set restore point creation frequency", ex);
            }
        }

        /// <summary>
        /// Best-effort lookup of total shadow storage used on the target volume. This is a volume-wide figure (root\cimv2:Win32_ShadowStorage), not per-restore-point usage,
        /// because Windows does not expose the latter through any documented API. Always null on Windows XP: restore points there are not VSS-backed, so this WMI class has nothing
        /// to report.
        /// </summary>
        private static long? TryGetSystemVolumeShadowStorageUsedBytes()
        {
            if (OS.WXP)
            {
                return null;
            }

            try
            {
                string systemDrive = Program.SystemPartition + ":\\";

                using (ManagementObjectSearcher searcher = new(@"root\cimv2", "SELECT * FROM Win32_ShadowStorage"))
                {
                    foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (obj["UsedSpace"] == null) continue;

                        try
                        {
                            using (ManagementObject volume = new(obj["Volume"]?.ToString() ?? string.Empty))
                            {
                                string deviceId = volume["DeviceID"]?.ToString();

                                // Prefer the entry that matches the system drive when the volume path resolves.
                                if (!string.IsNullOrEmpty(deviceId) && deviceId.IndexOf(systemDrive.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    return Convert.ToInt64(obj["UsedSpace"]);
                                }
                            }
                        }
                        catch (ManagementException)
                        {
                            // Volume path could not be resolved on this OS version; ignore and keep looking.
                        }
                    }

                    // No entry matched the system drive specifically (or the match couldn't be resolved); fall back to the first available shadow storage entry as a rough indicator.
                    foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (obj["UsedSpace"] != null)
                        {
                            return Convert.ToInt64(obj["UsedSpace"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Warning, "Could not read shadow storage usage", ex);
            }

            return null;
        }

        #endregion
    }
}