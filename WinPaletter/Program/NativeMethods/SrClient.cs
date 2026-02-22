using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides class implementation for interacting with the System Restore API.
    /// </summary>
    public static class SrClient
    {
        /// <summary>
        /// The SRSetRestorePoint function specifies the beginning and the ending of a set of changes so that System Restore can create a restore point.
        /// </summary>
        /// <param name="pRestorePtSpec">A pointer to a RESTOREPOINTINFOW structure that specifies the restore point.</param>
        /// <param name="pSMgrStatus">A pointer to a STATEMGRSTATUS structure that receives the status information.</param>
        /// <returns>If the function succeeds, the return value is TRUE. Otherwise, it is FALSE.</returns>
        [DllImport("SrClient.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SRSetRestorePointW(ref RESTOREPOINTINFOW pRestorePtSpec, out STATEMGRSTATUS pSMgrStatus);

        /// <summary>
        /// Disable System Restore on the specified drive.
        /// </summary>
        /// <param name="Drive">The drive path (e.g., "C:")</param>
        /// <returns>Returns 0 if successful, non-zero if failed.</returns>
        [DllImport("SrClient.dll", SetLastError = true)]
        public static extern int DisableSR([MarshalAs(UnmanagedType.LPWStr)] string Drive);

        /// <summary>
        /// Enable System Restore on the specified drive.
        /// </summary>
        /// <param name="Drive">The drive path (e.g., "C:")</param>
        /// <returns>Returns 0 if successful, non-zero if failed.</returns>
        [DllImport("SrClient.dll", SetLastError = true)]
        public static extern int EnableSR([MarshalAs(UnmanagedType.LPWStr)] string Drive);

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
        /// Contains status information used by the SRSetRestorePoint function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct STATEMGRSTATUS
        {
            /// <summary>
            /// The status code. If this member is 0, the function succeeded. 
            /// Any other value indicates an error.
            /// </summary>
            public int nStatus;

            /// <summary>
            /// The sequence number of the restore point.
            /// </summary>
            public long llSequenceNumber;
        }

        /// <summary>
        /// Contains information used by the SRSetRestorePoint function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct RESTOREPOINTINFOW
        {
            /// <summary>
            /// The type of event. Use constants from <see cref="RestoreEventType"/>.
            /// </summary>
            public int dwEventType;

            /// <summary>
            /// The type of restore point. Use constants from <see cref="RestorePointType"/>.
            /// </summary>
            public int dwRestorePtType;

            /// <summary>
            /// The sequence number of the restore point. Set to 0 for new restore points.
            /// </summary>
            public long llSequenceNumber;

            /// <summary>
            /// The description of the restore point. Maximum length is 256 characters.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDescription;
        }
    }
}