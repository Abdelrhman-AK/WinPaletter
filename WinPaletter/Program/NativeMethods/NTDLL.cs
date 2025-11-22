using System;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    public static class NTDLL
    {
        /// <summary>
        /// Opens a handle to a section object in the Windows NT kernel namespace.
        /// </summary>
        /// <param name="SectionHandle">Outputs the handle to the opened section.</param>
        /// <param name="DesiredAccess">The access mask requested (e.g., READ_CONTROL or WRITE_DAC).</param>
        /// <param name="ObjectAttributes">A reference to an <see cref="OBJECT_ATTRIBUTES"/> structure describing the object.</param>
        /// <returns>NTSTATUS code indicating success or failure.</returns>
        [DllImport("ntdll.dll")]
        public static extern int NtOpenSection(out IntPtr SectionHandle, uint DesiredAccess, ref OBJECT_ATTRIBUTES ObjectAttributes);

        /// <summary>
        /// Sets the security descriptor on a kernel object.
        /// </summary>
        /// <param name="Handle">Handle to the object to modify.</param>
        /// <param name="SecurityInformation">
        /// Specifies which parts of the security descriptor are being applied. 
        /// Typically <see cref="DACL_SECURITY_INFORMATION"/>.
        /// </param>
        /// <param name="SecurityDescriptor">Pointer to the security descriptor in memory.</param>
        /// <returns>NTSTATUS code indicating success or failure.</returns>
        [DllImport("ntdll.dll")]
        public static extern int NtSetSecurityObject(IntPtr Handle, uint SecurityInformation, IntPtr SecurityDescriptor);

        /// <summary>
        /// Retrieves the security descriptor of a kernel object.
        /// </summary>
        /// <param name="Handle">Handle to the object.</param>
        /// <param name="SecurityInformation">
        /// Specifies which parts of the security descriptor to query. 
        /// Typically <see cref="DACL_SECURITY_INFORMATION"/>.
        /// </param>
        /// <param name="SecurityDescriptor">Outputs a pointer to the security descriptor.</param>
        /// <returns>NTSTATUS code indicating success or failure.</returns>
        [DllImport("ntdll.dll")]
        public static extern int NtQuerySecurityObject(IntPtr Handle, uint SecurityInformation, out IntPtr SecurityDescriptor);

        /// <summary>
        /// Closes an open handle to a kernel object.
        /// </summary>
        /// <param name="Handle">Handle to close.</param>
        /// <returns>NTSTATUS code indicating success or failure.</returns>
        [DllImport("ntdll.dll")]
        public static extern int NtClose(IntPtr Handle);

        /// <summary>
        /// Represents a Unicode string used by native Windows APIs.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct UNICODE_STRING
        {
            /// <summary>Length of the string in bytes (not including null terminator).</summary>
            public ushort Length;

            /// <summary>Maximum length of the string in bytes (including null terminator).</summary>
            public ushort MaximumLength;

            /// <summary>Pointer to the UTF-16 string buffer.</summary>
            public IntPtr Buffer;
        }

        /// <summary>
        /// Represents attributes required to open or create a kernel object.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct OBJECT_ATTRIBUTES
        {
            /// <summary>Size of this structure in bytes.</summary>
            public int Length;

            /// <summary>Handle to root directory, or <see cref="IntPtr.Zero"/>.</summary>
            public IntPtr RootDirectory;

            /// <summary>Pointer to a <see cref="UNICODE_STRING"/> representing the object name.</summary>
            public IntPtr ObjectName;

            /// <summary>Attributes (e.g., case-insensitive).</summary>
            public uint Attributes;

            /// <summary>Pointer to security descriptor (optional).</summary>
            public IntPtr SecurityDescriptor;

            /// <summary>Pointer to security quality of service structure (optional).</summary>
            public IntPtr SecurityQualityOfService;
        }

        /// <summary>
        /// Builds an <see cref="OBJECT_ATTRIBUTES"/> structure for a given NT object path.
        /// Allocates unmanaged memory for the <see cref="UNICODE_STRING"/> object name.
        /// </summary>
        /// <param name="path">The NT path of the object, e.g. \Sessions\1\Windows\ThemeSection</param>
        /// <returns>A fully initialized <see cref="OBJECT_ATTRIBUTES"/> structure.</returns>
        public static OBJECT_ATTRIBUTES BuildObjectAttributes(string path)
        {
            UNICODE_STRING unicode = new()
            {
                Buffer = Marshal.StringToHGlobalUni(path),
                Length = (ushort)(path.Length * 2),
                MaximumLength = (ushort)((path.Length * 2) + 2)
            };

            IntPtr namePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(UNICODE_STRING)));
            Marshal.StructureToPtr(unicode, namePtr, false);

            return new()
            {
                Length = Marshal.SizeOf(typeof(OBJECT_ATTRIBUTES)),
                ObjectName = namePtr
            };
        }

        /// <summary>Access right to read the security descriptor of an object.</summary>
        public const uint READ_CONTROL = 0x00020000;

        /// <summary>Access right to modify the discretionary access control list (DACL) of an object.</summary>
        public const uint WRITE_DAC = 0x00040000;

        /// <summary>Security information flag to indicate that the DACL should be read or set.</summary>
        public const uint DACL_SECURITY_INFORMATION = 0x00000004;

        [DllImport("ntdll.dll", CharSet = CharSet.Unicode)]
        public static extern int RtlGetVersion(ref OSVERSIONINFOEX lpVersionInformation);

        [StructLayout(LayoutKind.Sequential)]
        public struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
        }

        public static Version GetOSVersion()
        {
            OSVERSIONINFOEX v = new();
            v.dwOSVersionInfoSize = Marshal.SizeOf(v);
            RtlGetVersion(ref v);

            return new Version(v.dwMajorVersion, v.dwMinorVersion, v.dwBuildNumber);
        }
    }
}
