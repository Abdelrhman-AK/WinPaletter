using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading;
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
        public static bool CreateRestorePoint(string description, bool waitForFlushing = false)
        {
            UI.WP.ProgressDialog dlg = null;
            bool result = false;

            try
            {
                Program.Log?.Debug($"CreateRestorePoint called with description: \"{description}\", waitForFlushing: {waitForFlushing}, OS.WXP: {OS.WXP}");

                if (string.IsNullOrWhiteSpace(description))
                {
                    throw new ArgumentException("Restore point description cannot be empty.", nameof(description));
                }

                if (!User.Administrator)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Creating a restore point without administrative privileges; this will likely fail.");
                }

                // Ensure System Restore is enabled
                if (!EnsureSystemRestoreEnabled())
                {
                    Program.Log?.Debug("EnsureSystemRestoreEnabled returned false; aborting restore point creation.");
                    return false;
                }

                string taggedDescription = Tag + description;

                dlg = new()
                {
                    Animation = UI.WP.AnimationResource.GetShellAnimation(UI.WP.ShellAnimation.FlyingPapers),
                    Text = Program.Localization.Strings.General.RestorePoint_DialogTitle,
                    Description = $"{Program.Localization.Strings.General.Name}: {description}",
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

                    if (OS.WXP)
                    {
                        // Windows XP's srclient.dll only exports the ANSI SRSetRestorePointA entry point.
                        // The unicode SRSetRestorePointW export used by CreateRestorePointNative() was
                        // introduced in Windows Vista, so calling it on XP would just throw and waste a
                        // P/Invoke attempt before falling back anyway. Go straight to the WMI path on XP.
                        Program.Log?.Debug("OS.WXP is true; skipping CreateRestorePointNative (SRSetRestorePointW is not guaranteed present pre-Vista) and using WMI directly.");

                        // Even after the settle delay in EnsureSystemRestoreEnabled(), the SR service's
                        // restore-point database can still occasionally not be ready yet on XP, which
                        // surfaces as WMI error 1065 (ERROR_DATABASE_DOES_NOT_EXIST). Retry a few times
                        // with a short delay before giving up, rather than failing on the first attempt.
                        const int maxXpAttempts = 5;
                        for (int attempt = 1; attempt <= maxXpAttempts; attempt++)
                        {
                            result = CreateRestorePointWMI(taggedDescription);
                            if (result)
                            {
                                Program.Log?.Debug($"XP WMI restore point creation succeeded on attempt {attempt}/{maxXpAttempts}");
                                break;
                            }

                            Program.Log?.Debug($"XP WMI restore point creation attempt {attempt}/{maxXpAttempts} failed (likely SR database not ready yet, error 1065)");
                            if (attempt < maxXpAttempts) Thread.Sleep(1000);
                        }
                    }
                    else
                    {
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
                    }

                    Program.Log?.Debug($"CreateRestorePoint DoWork finished with result: {result}");
                };

                dlg.RunWorkerCompleted += (s, e) =>
                {
                    if (e.Error != null)
                    {
                        Program.Log?.Write(LogEventLevel.Error, "Failed to create system restore point", e.Error);
                        Program.Log?.Debug("RunWorkerCompleted received a non-null Error", e.Error);
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
                };

                dlg.ShowDialog();

                // Waits for seconds so that a reloaded information can get newly created point
                if (waitForFlushing) Thread.Sleep(500);

                Program.Log?.Debug($"CreateRestorePoint returning: {result}");
                return result;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Failed to create system restore point", ex);
                Program.Log?.Debug("Exception thrown in CreateRestorePoint", ex);
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
                Program.Log?.Debug("GetWinPaletterRestorePoints called");

                long? shadowStorageUsed = TryGetSystemVolumeShadowStorageUsedBytes();
                Program.Log?.Debug($"System volume shadow storage currently in use: {(shadowStorageUsed.HasValue ? shadowStorageUsed.Value.ToString() : "null")} bytes");

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
                                Program.Log?.Debug($"Failed to parse CreationTime \"{rawCreationTime}\" for restore point #{sequenceNumber}", ex);
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

                Program.Log?.Debug($"GetWinPaletterRestorePoints found {list.Count} WinPaletter-tagged restore point(s)");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Failed to enumerate WinPaletter restore points", ex);
                Program.Log?.Debug("Exception thrown in GetWinPaletterRestorePoints", ex);
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
            return TryGetSystemVolumeShadowStorageUsedBytes();
        }

        /// <summary>
        /// Deletes a single restore point by its sequence number.
        /// </summary>
        /// <param name="sequenceNumber">The sequence number of the restore point, as returned by <see cref="GetWinPaletterRestorePoints"/>.</param>
        public static bool DeleteRestorePoint(uint sequenceNumber, bool waitForFlushing)
        {
            try
            {
                Program.Log?.Debug($"DeleteRestorePoint called for #{sequenceNumber}, waitForFlushing: {waitForFlushing}, OS.WXP: {OS.WXP}");

                if (!User.Administrator)
                {
                    Program.Log?.Write(LogEventLevel.Warning, "Deleting a restore point without administrative privileges; this will likely fail.");
                }

                Program.Log?.Write(LogEventLevel.Information, $"Deleting restore point #{sequenceNumber}");

                int result;

                if (OS.WXP)
                {
                    // SRRemoveRestorePoint is documented as supported since Windows XP, but in practice
                    // it has been externally reported to return ERROR_SUCCESS on XP without actually
                    // removing the restore point (e.g. the "System Restore Explorer" tool author found
                    // the same thing and had to restrict restore-point deletion to Vista and later for
                    // this reason). The documented alternative for removing a restore point is to call
                    // SRSetRestorePoint with RestorePointType = CANCELLED_OPERATION (13), EventType =
                    // END_SYSTEM_CHANGE, and llSequenceNumber set to the point's sequence number.
                    Program.Log?.Debug($"OS.WXP is true; SRRemoveRestorePoint is unreliable on XP, using SRSetRestorePointW(CANCELLED_OPERATION) instead for #{sequenceNumber}");
                    result = DeleteRestorePointNativeXP(sequenceNumber);
                }
                else
                {
                    // SRRemoveRestorePoint returns 0 (ERROR_SUCCESS) on success.
                    result = SrClient.SRRemoveRestorePoint(sequenceNumber);
                }

                Program.Log?.Debug($"Restore point removal call returned code {result} for #{sequenceNumber}");

                if (OS.WXP && result == 0)
                {
                    // Neither removal path above can be fully trusted on XP, so confirm the restore
                    // point is actually gone before reporting success - this is exactly what was
                    // reported as broken (a success result with the restore point still present).
                    bool stillExists = RestorePointStillExists(sequenceNumber);
                    Program.Log?.Debug($"DeleteRestorePoint (XP): post-deletion verification for #{sequenceNumber}, still exists: {stillExists}");

                    if (stillExists)
                    {
                        Program.Log?.Write(LogEventLevel.Warning, $"Restore point #{sequenceNumber} removal reported success but the restore point is still present on disk (known Windows XP limitation). Reporting failure instead of a false success.");
                        result = -1;
                    }
                }

                if (result == 0)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Restore point #{sequenceNumber} deleted successfully");
                    if (waitForFlushing) Thread.Sleep(500);
                    return true;
                }

                Program.Log?.Write(LogEventLevel.Error, $"Restore point removal failed for #{sequenceNumber} with code {result}. " +
                    "Common causes: not running elevated, the restore point was already merged/removed by the OS, or (on Windows XP) this is a known limitation where restore points cannot be reliably deleted individually.");
                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Failed to delete restore point #{sequenceNumber}", ex);
                Program.Log?.Debug($"Exception thrown in DeleteRestorePoint for #{sequenceNumber}", ex);
                return false;
            }
        }

        /// <summary>
        /// Deletes every restore point created by WinPaletter (identified by tag). Does not touch restore points created by anything else.
        /// </summary>
        /// <returns>The count of restore points successfully deleted and the count that failed.</returns>
        public static (int Succeeded, int Failed) DeleteAllWinPaletterRestorePoints(bool waitForFlushing)
        {
            Program.Log?.Debug("DeleteAllWinPaletterRestorePoints called");

            int succeeded = 0;
            int failed = 0;

            foreach (RestorePointInfo restorePoint in GetWinPaletterRestorePoints())
            {
                if (DeleteRestorePoint(restorePoint.SequenceNumber, false))
                {
                    succeeded++;
                }
                else
                {
                    failed++;
                    Program.Log?.Debug($"DeleteAllWinPaletterRestorePoints: failed to delete #{restorePoint.SequenceNumber} (\"{restorePoint.Name}\")");
                }
            }

            Program.Log?.Write(LogEventLevel.Information, $"Deleted {succeeded} WinPaletter restore point(s), {failed} failed");
            if (waitForFlushing) Thread.Sleep(500);
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
                    bool enabled = IsSystemRestoreEnabledForDrive(Program.SystemPartition + ":");
                    Program.Log?.Debug($"SystemRestoreHelper.Enabled evaluated to {enabled} for drive {Program.SystemPartition}:");
                    return enabled;
                }
                catch (Exception ex)
                {
                    Program.Log?.Write(LogEventLevel.Error, "Failed to check System Restore enabled status", ex);
                    Program.Log?.Debug("Exception thrown in Enabled getter", ex);
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

                // Windows XP's EnableSR/DisableSR exports (unlike SRSetRestorePointW/SRRemoveRestorePoint)
                // do a strict path-style match on the drive string and require a trailing backslash, e.g.
                // "C:\" rather than "C:". Passing the bare "C:" form on XP returns error 15
                // (ERROR_INVALID_DRIVE) even though the drive is valid. Vista and later accept "C:" fine,
                // so only XP needs the adjusted string; the plain "drive" value is still used everywhere
                // else (logging, verification) so registry lookups in IsSystemRestoreEnabledForDrive keep
                // working unchanged.
                string srClientDrive = OS.WXP ? driveLetter + @":\" : drive;

                Program.Log?.Write(LogEventLevel.Information, $"Setting system restore status for drive {drive} to {enable}");
                Program.Log?.Debug($"SetSystemRestoreStatus called for drive {drive} (SrClient drive string: \"{srClientDrive}\"), enable: {enable}, OS.WXP: {OS.WXP}");

                int result = enable ? SrClient.EnableSR(srClientDrive) : SrClient.DisableSR(srClientDrive);
                Program.Log?.Debug($"{(enable ? "EnableSR" : "DisableSR")} returned code {result} for drive string \"{srClientDrive}\"");

                // Windows XP's EnableSR/DisableSR start or stop the "srservice" system service under the
                // hood, and the underlying service-control call fails if the service is already in the
                // requested state:
                //   1056 = ERROR_SERVICE_ALREADY_RUNNING -> EnableSR called while SR is already enabled
                //   1062 = ERROR_SERVICE_NOT_ACTIVE       -> DisableSR called while SR is already disabled
                // Neither is really a failure - the drive already ends up in the state the caller asked
                // for - so on XP these two codes are treated as success rather than thrown as errors.
                // Vista and later don't drive srservice this way and aren't affected.
                if (OS.WXP && ((enable && result == 1056) || (!enable && result == 1062)))
                {
                    Program.Log?.Debug($"{(enable ? "EnableSR" : "DisableSR")} returned {result} on XP ({(enable ? "ERROR_SERVICE_ALREADY_RUNNING" : "ERROR_SERVICE_NOT_ACTIVE")}); System Restore is already in the requested state, treating as success.");
                    result = 0;
                }

                if (result != 0)
                {
                    throw new InvalidOperationException($"Failed to {(enable ? "enable" : "disable")} System Restore. Return code: {result}");
                }

                // Verify the change took effect
                if (IsSystemRestoreEnabledForDrive(drive) != enable)
                {
                    Program.Log?.Write(LogEventLevel.Warning, $"System Restore status change may not have taken effect immediately");
                    Program.Log?.Debug($"SetSystemRestoreStatus: post-change verification mismatch for drive {drive}, expected {enable}");
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Failed to set system restore status", ex);
                Program.Log?.Debug($"Exception thrown in SetSystemRestoreStatus for drive {driveLetter}:", ex);
                throw;
            }
        }

        #endregion

        #region Private Implementation

        private static bool EnsureSystemRestoreEnabled()
        {
            Program.Log?.Debug("EnsureSystemRestoreEnabled called");

            if (Enabled)
            {
                Program.Log?.Debug("EnsureSystemRestoreEnabled: already enabled");
                return true;
            }

            DialogResult result = MsgBox(Program.Localization.Strings.Messages.SysRestore_Msg0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, string.Format(Program.Localization.Strings.Messages.SysRestore_Msg1, Program.SystemPartition));

            Program.Log?.Debug($"EnsureSystemRestoreEnabled: user prompted, response: {result}");

            if (result == DialogResult.Yes)
            {
                SetSystemRestoreStatus(Program.SystemPartition, true);

                // Give it a moment to take effect. Windows XP needs noticeably longer than Vista+ here:
                // after (re-)enabling System Restore, the SR service has to finish rebuilding its
                // restore-point database on disk before CreateRestorePoint will succeed. 1 second isn't
                // reliably enough on XP and a premature call fails with WMI error 1065
                // (ERROR_DATABASE_DOES_NOT_EXIST). Vista and later initialize fast enough that 1 second
                // is unchanged.
                int settleDelayMs = OS.WXP ? 5000 : 1000;
                Program.Log?.Debug($"EnsureSystemRestoreEnabled: waiting {settleDelayMs}ms for SR service to settle (OS.WXP: {OS.WXP})");
                System.Threading.Thread.Sleep(settleDelayMs);

                bool nowEnabled = Enabled;
                Program.Log?.Debug($"EnsureSystemRestoreEnabled: after enabling, status is {nowEnabled}");
                return nowEnabled;
            }

            return false;
        }

        private static bool CreateRestorePointNative(string description)
        {
            try
            {
                Program.Log?.Debug($"CreateRestorePointNative called (Vista and later only) with description: \"{description}\"");

                // Begin the restore point
                RESTOREPOINTINFOW restorePointInfo = new()
                {
                    dwEventType = (int)EventType.BeginSystemChange,
                    dwRestorePtType = (int)RestorePointType.ApplicationInstall,
                    llSequenceNumber = 0,
                    szDescription = description
                };

                bool result = SrClient.SRSetRestorePointW(ref restorePointInfo, out STATEMGRSTATUS status);
                Program.Log?.Debug($"SRSetRestorePointW (BeginSystemChange) returned {result}, status.nStatus: {status.nStatus}");

                if (!result || status.nStatus != 0)
                {
                    Program.Log?.Write(LogEventLevel.Error, $"Native API failed with status: {status.nStatus}");
                    return false;
                }

                // End the restore point
                restorePointInfo.dwEventType = (int)EventType.EndSystemChange;
                SrClient.SRSetRestorePointW(ref restorePointInfo, out _);
                Program.Log?.Debug("SRSetRestorePointW (EndSystemChange) call completed");

                return true;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Native API restore point creation failed", ex);
                Program.Log?.Debug("Exception thrown in CreateRestorePointNative", ex);
                return false;
            }
        }

        /// <summary>
        /// Windows XP-only removal path. SRRemoveRestorePoint is unreliable on XP (see the comment in
        /// <see cref="DeleteRestorePoint"/>), so this uses the documented alternative: calling
        /// SRSetRestorePoint with RestorePointType = CANCELLED_OPERATION (13) and EventType =
        /// END_SYSTEM_CHANGE for the target sequence number. Returns 0 (ERROR_SUCCESS-equivalent) on
        /// apparent success, matching the convention used by SRRemoveRestorePoint's return value, or a
        /// non-zero/negative code on failure. The caller still verifies the point is actually gone
        /// afterward, since this path isn't guaranteed either.
        /// </summary>
        private static int DeleteRestorePointNativeXP(uint sequenceNumber)
        {
            try
            {
                Program.Log?.Debug($"DeleteRestorePointNativeXP called for #{sequenceNumber}");

                RESTOREPOINTINFOW restorePointInfo = new()
                {
                    dwEventType = (int)EventType.EndSystemChange,
                    dwRestorePtType = 13, // CANCELLED_OPERATION - not exposed as a named RestorePointType member
                    llSequenceNumber = sequenceNumber,
                    szDescription = string.Empty
                };

                bool ok = SrClient.SRSetRestorePointW(ref restorePointInfo, out STATEMGRSTATUS status);
                Program.Log?.Debug($"DeleteRestorePointNativeXP: SRSetRestorePointW(CANCELLED_OPERATION) returned {ok}, status.nStatus: {status.nStatus} for #{sequenceNumber}");

                if (ok && status.nStatus == 0)
                {
                    return 0;
                }

                return status.nStatus != 0 ? status.nStatus : -1;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"XP native restore point cancellation failed for #{sequenceNumber}", ex);
                Program.Log?.Debug($"Exception thrown in DeleteRestorePointNativeXP for #{sequenceNumber}", ex);
                return -1;
            }
        }

        /// <summary>
        /// Checks whether a restore point with the given sequence number is still present in the
        /// SystemRestore WMI store. Used on Windows XP to confirm a removal actually took effect, since
        /// neither native removal path can be fully trusted there to report accurate results.
        /// </summary>
        private static bool RestorePointStillExists(uint sequenceNumber)
        {
            try
            {
                Program.Log?.Debug($"RestorePointStillExists called for #{sequenceNumber}");

                using (ManagementObjectSearcher searcher = new(@"root\default", $"SELECT SequenceNumber FROM SystemRestore WHERE SequenceNumber = {sequenceNumber}"))
                {
                    foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                    {
                        Program.Log?.Debug($"RestorePointStillExists: #{sequenceNumber} is still present");
                        return true;
                    }
                }

                Program.Log?.Debug($"RestorePointStillExists: #{sequenceNumber} was not found, deletion confirmed");
                return false;
            }
            catch (Exception ex)
            {
                // If we can't verify one way or the other, don't risk falsely reporting a successful
                // deletion - assume it's still there.
                Program.Log?.Write(LogEventLevel.Warning, $"Could not verify whether restore point #{sequenceNumber} still exists", ex);
                Program.Log?.Debug($"Exception thrown in RestorePointStillExists for #{sequenceNumber}, assuming still present to be safe", ex);
                return true;
            }
        }

        private static bool CreateRestorePointWMI(string description)
        {
            try
            {
                Program.Log?.Debug($"CreateRestorePointWMI called with description: \"{description}\"");

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
                        Program.Log?.Debug($"WMI SystemRestore.CreateRestorePoint ReturnValue: {result}");
                        return result == 0;
                    }
                }

                Program.Log?.Debug("CreateRestorePointWMI: InvokeMethod returned no usable ReturnValue");
                return false;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "WMI restore point creation failed", ex);
                Program.Log?.Debug("Exception thrown in CreateRestorePointWMI", ex);
                return false;
            }
        }

        private static bool IsSystemRestoreEnabledForDrive(string drive)
        {
            try
            {
                Program.Log?.Debug($"IsSystemRestoreEnabledForDrive called for drive: {drive}");

                // Check global disable flag
                string disableSR = ReadReg<string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", "DisableSR");
                Program.Log?.Debug($"IsSystemRestoreEnabledForDrive: DisableSR = \"{disableSR ?? "null"}\"");
                if (disableSR != null && disableSR.ToString() == "1")
                {
                    return false; // System Restore is globally disabled
                }

                if (OS.WXP)
                {
                    // On Windows XP, DisableSR is the only reliable registry-based signal for enablement.
                    // The per-drive value this method checks further down isn't populated on XP the way
                    // it's assumed here (drive monitoring state on XP is tracked via the
                    // SystemRestoreConfig WMI class, not a plain per-drive-letter DWORD under this key),
                    // and RPSessionInterval is a restore-point creation frequency in seconds, not a 0/1
                    // "enabled" flag. Relying on either of those below was making this method always
                    // report "disabled" on XP, even immediately after a successful EnableSR call. Trust
                    // DisableSR alone on XP; Vista and later still use the full check below unchanged.
                    bool xpEnabled = disableSR == null || disableSR.ToString() != "1";
                    Program.Log?.Debug($"IsSystemRestoreEnabledForDrive: OS.WXP is true, trusting DisableSR alone, result: {xpEnabled}");
                    return xpEnabled;
                }

                // Check per-drive settings
                string driveLetter = drive.TrimEnd(':');
                string driveConfig = ReadReg<string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", driveLetter);
                Program.Log?.Debug($"IsSystemRestoreEnabledForDrive: {driveLetter} = \"{driveConfig ?? "null"}\"");

                // If the drive has a config value of 0, it's disabled
                if (driveConfig != null && driveConfig.ToString() == "0")
                {
                    return false;
                }

                // Also check RPSessionInterval as fallback
                string rpInterval = ReadReg<string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", "RPSessionInterval");
                Program.Log?.Debug($"IsSystemRestoreEnabledForDrive: RPSessionInterval = \"{rpInterval ?? "null"}\"");
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
                Program.Log?.Debug($"Exception thrown in IsSystemRestoreEnabledForDrive for drive {drive}", ex);
                return false;
            }
        }

        private static void SetRestorePointFrequency(int frequency)
        {
            try
            {
                Program.Log?.Debug($"SetRestorePointFrequency called with frequency: {frequency}");

                using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore"))
                {
                    key?.SetValue("SystemRestorePointCreationFrequency", frequency, RegistryValueKind.DWord);
                }
                Program.Log?.Write(LogEventLevel.Information, $"Set restore point creation frequency to {frequency}");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Warning, "Failed to set restore point creation frequency", ex);
                Program.Log?.Debug("Exception thrown in SetRestorePointFrequency", ex);
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
                Program.Log?.Debug("TryGetSystemVolumeShadowStorageUsedBytes: OS.WXP is true, returning null (Win32_ShadowStorage is a VSS construct, not applicable to XP restore points).");
                return null;
            }

            try
            {
                Program.Log?.Debug("TryGetSystemVolumeShadowStorageUsedBytes: querying root\\cimv2:Win32_ShadowStorage");

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
                                    long matched = Convert.ToInt64(obj["UsedSpace"]);
                                    Program.Log?.Debug($"TryGetSystemVolumeShadowStorageUsedBytes: matched system drive {systemDrive}, UsedSpace: {matched} bytes");
                                    return matched;
                                }
                            }
                        }
                        catch (ManagementException ex)
                        {
                            // Volume path could not be resolved on this OS version; ignore and keep looking.
                            Program.Log?.Debug("TryGetSystemVolumeShadowStorageUsedBytes: could not resolve a Volume reference, continuing to next entry", ex);
                        }
                    }

                    // No entry matched the system drive specifically (or the match couldn't be resolved); fall back to the first available shadow storage entry as a rough indicator.
                    foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                    {
                        if (obj["UsedSpace"] != null)
                        {
                            long fallback = Convert.ToInt64(obj["UsedSpace"]);
                            Program.Log?.Debug($"TryGetSystemVolumeShadowStorageUsedBytes: no drive-specific match, falling back to first entry, UsedSpace: {fallback} bytes");
                            return fallback;
                        }
                    }
                }

                Program.Log?.Debug("TryGetSystemVolumeShadowStorageUsedBytes: no Win32_ShadowStorage entries with UsedSpace found, returning null");
            }
            catch (ManagementException ex)
            {
                Program.Log?.Debug("WMI initialization or query failed safely while getting shadow storage usage");
                Program.Log?.Debug("ManagementException thrown in TryGetSystemVolumeShadowStorageUsedBytes", ex);
            }
            catch (Exception ex)
            {
                Program.Log?.Debug("Could not read shadow storage usage");
                Program.Log?.Debug("Exception thrown in TryGetSystemVolumeShadowStorageUsedBytes", ex);
            }

            return null;
        }

        #endregion
    }
}