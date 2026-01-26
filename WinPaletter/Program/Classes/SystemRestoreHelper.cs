using Serilog.Events;
using System.Management;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// A class that helps WinPaletter with System Restore Points creation
    /// </summary>
    public class SystemRestoreHelper
    {
        /// <summary>
        ///     The type of event. For more information, see <see cref="CreateRestorePoint"/>.
        /// </summary>
        public enum EventType
        {
            /// <summary>
            ///     A system change has begun. A subsequent nested call does not create a new restore
            ///     point.
            ///     <para>
            ///         Subsequent calls must use <see cref="EndNestedSystemChange"/>, not
            ///         <see cref="EndSystemChange"/>.
            ///     </para>
            /// </summary>
            BeginNestedSystemChange = 0x66,

            /// <summary>
            ///     A system change has begun.
            /// </summary>
            BeginSystemChange = 0x64,

            /// <summary>
            ///     A system change has ended.
            /// </summary>
            EndNestedSystemChange = 0x67,

            /// <summary>
            ///     A system change has ended.
            /// </summary>
            EndSystemChange = 0x65
        }

        /// <summary>
        ///     The type of restore point. For more information, see <see cref="CreateRestorePoint"/>.
        /// </summary>
        public enum RestorePointType
        {
            /// <summary>
            ///     An application has been installed.
            /// </summary>
            ApplicationInstall = 0x0,

            /// <summary>
            ///     An application has been uninstalled.
            /// </summary>
            ApplicationUninstall = 0x1,

            /// <summary>
            ///     An application needs to delete the restore point it created. For example, an
            ///     application would use this flag when a user cancels an installation. 
            /// </summary>
            CancelledOperation = 0xd,

            /// <summary>
            ///     A device driver has been installed.
            /// </summary>
            DeviceDriverInstall = 0xa,

            /// <summary>
            ///     An application has had features added or removed.
            /// </summary>
            ModifySettings = 0xc
        }

        /// <summary>
        /// Method to create a system restore point.
        /// </summary>
        /// <param name="description">The description of the restore point.</param>
        public static bool CreateRestorePoint(string description)
        {
            if (!Enabled)
            {
                if (MsgBox(Program.Localization.Strings.Messages.SysRestore_Msg0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, string.Format(Program.Localization.Strings.Messages.SysRestore_Msg1, Program.SystemPartition)) == DialogResult.Yes)
                {
                    SetSystemRestoreStatus(Program.SystemPartition, true);
                }
                else
                {
                    return false;
                }
            }

            bool result = false;

            // Reset frequency to 0 instead of 24 hours
            WriteReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", "SystemRestorePointCreationFrequency", 0);

            Program.Log?.Write(LogEventLevel.Information, $"Creating system restore point with description: {description}");

            // Start actions
            var mScope = new ManagementScope("\\\\localhost\\root\\default");
            var mPath = new ManagementPath("SystemRestore");
            var options = new ObjectGetOptions();
            using (var mClass = new ManagementClass(mScope, mPath, options))
            using (var parameters = mClass.GetMethodParameters("CreateRestorePoint"))
            {
                parameters["Description"] = description;
                parameters["EventType"] = (int)EventType.BeginSystemChange;
                parameters["RestorePointType"] = 7;
                mClass.InvokeMethod("CreateRestorePoint", parameters, null);
            }

            result = true;

            return result;
        }

        /// <summary>
        /// Detect if system restore is enabled or not.
        /// </summary>
        public static bool Enabled => ReadReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", "RPSessionInterval", 0) == 1;

        /// <summary>
        /// Sets the system restore status for a given drive.
        /// </summary>
        public static void SetSystemRestoreStatus(char driveLetter, bool enable)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Setting system restore status for drive {driveLetter} to {enable}");

            if (enable) { SrClient.EnableSR(driveLetter + ":"); } else { SrClient.DisableSR(driveLetter + ":"); }
        }
    }
}