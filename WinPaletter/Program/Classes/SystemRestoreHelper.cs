using Ookii.Dialogs.WinForms;
using Serilog.Events;
using System;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using Microsoft.Win32;
using static WinPaletter.NativeMethods.SrClient;

namespace WinPaletter
{
    /// <summary>
    /// A class that helps WinPaletter with System Restore Points creation
    /// </summary>
    public static class SystemRestoreHelper
    {
        #region Public Methods

        /// <summary>
        /// Method to create a system restore point.
        /// </summary>
        /// <param name="description">The description of the restore point.</param>
        public static bool CreateRestorePoint(string description)
        {
            ProgressDialog dlg = null;

            try
            {
                // Ensure System Restore is enabled
                if (!EnsureSystemRestoreEnabled())
                    return false;

                if (!OS.WXP)
                {
                    dlg = new()
                    {
                        Animation = AnimationResource.GetShellAnimation(ShellAnimation.FlyingPapers),
                        Text = Program.Localization.Strings.General.RestorePoint_DialogTitle,
                        Description = description,
                        ProgressBarStyle = Ookii.Dialogs.WinForms.ProgressBarStyle.MarqueeProgressBar,
                        ShowCancelButton = false,
                        MinimizeBox = false,
                        WindowTitle = Application.ProductName,
                    };
                    dlg.Show();
                }

                // Reset frequency to 0 instead of 24 hours
                SetRestorePointFrequency(0);

                Program.Log?.Write(LogEventLevel.Information, $"Creating system restore point with description: {description}");

                // Try native API first (works on all supported Windows versions)
                if (CreateRestorePointNative(description))
                {
                    Program.Log?.Write(LogEventLevel.Information, "System restore point created successfully via native API");
                    return true;
                }

                // Fallback to WMI
                Program.Log?.Write(LogEventLevel.Warning, "Native API failed, falling back to WMI");
                return CreateRestorePointWMI(description);
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

            DialogResult result = MsgBox(
                Program.Localization.Strings.Messages.SysRestore_Msg0,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                string.Format(Program.Localization.Strings.Messages.SysRestore_Msg1, Program.SystemPartition)
            );

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
                // Method 1: Check via WMI - Query the protection status
                using (ManagementObjectSearcher searcher = new(@"root\default", "SELECT * FROM SystemRestore"))
                {
                    foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                    {
                        // Check if this object is for our drive and if protection is enabled
                        string objDrive = obj["Drive"]?.ToString();
                        if (string.Equals(objDrive, drive, StringComparison.OrdinalIgnoreCase))
                        {
                            // If we found the drive in SystemRestore, it's enabled
                            // The presence of the object itself indicates it's being monitored
                            return true;
                        }
                    }
                }

                // Method 2: Check via registry - More reliable for enabled/disabled status

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
                    // This indicates System Restore is generally enabled
                    // But we need to verify drive-specific settings
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

        #endregion
    }
}