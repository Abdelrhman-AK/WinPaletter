// ============================================================================
// Classic Theme
// ============================================================================
// Original Project: ClassicThemeTray
// Author: spitfirex86
// Description: Provides tools to enable/disable Windows classic theme for console windows
// License: MIT
// Source: https://github.com/spitfirex86/ClassicThemeTray/blob/master/ClassicThemeTray/Program.cs
// ============================================================================

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinPaletter
{
    /// <summary>
    /// Provides methods to enable or disable the classic Windows theme for the console windows
    /// by manipulating NT theme section security.
    /// </summary>
    public static class ClassicTheme
    {
        /// <summary>
        /// SDDL string that grants full access to the theme section for the system and denies it to interactive users.
        /// Used to enable classic theme restrictions.
        /// </summary>
        private const string AllowAccessSddl = "O:BAG:SYD:(A;;CCLCRC;;;IU)(A;;CCDCLCSWRPSDRCWDWO;;;SY)";

        /// <summary>
        /// SDDL string that denies discretionary access to the theme section for interactive users.
        /// Used to disable modern theme modifications and enforce classic theme behavior.
        /// </summary>
        private const string DenyAccessSddl = "O:BAG:SYD:(A;;RC;;;IU)(A;;DCSWRPSDRCWDWO;;;SY)";

        /// <summary>
        /// Returns the NT object path of the theme section for the current session.
        /// Example: "\Sessions\1\Windows\ThemeSection"
        /// </summary>
        private static string ThemeSectionPath => $@"\Sessions\{Process.GetCurrentProcess().SessionId}\Windows\ThemeSection";

        /// <summary>
        /// Gets a value indicating whether the classic theme security is currently enabled
        /// (i.e., access is denied to the interactive user).
        /// </summary>
        public static bool Enabled => GetSectionSecurity() == DenyAccessSddl;

        /// <summary>
        /// Enables the classic theme by setting the theme section security to deny access for the interactive user.
        /// </summary>
        public static void Enable() => SetSectionSecurity(DenyAccessSddl);

        /// <summary>
        /// Disables the classic theme by restoring full access to the theme section for interactive users.
        /// </summary>
        public static void Disable() => SetSectionSecurity(AllowAccessSddl);

        /// <summary>
        /// Sets the security descriptor of the NT theme section to the specified SDDL string.
        /// Allocates unmanaged memory for the security descriptor, which is automatically freed.
        /// </summary>
        /// <param name="sddl">
        /// The Security Descriptor Definition Language (SDDL) string representing the desired security settings.
        /// Typically <see cref="AllowAccessSddl"/> or <see cref="DenyAccessSddl"/>.
        /// </param>
        private static void SetSectionSecurity(string sddl)
        {
            using (SafeSecurityDescriptor mem = new(sddl))
            using (SafeObjectAttributes obj = new(ThemeSectionPath))
            {
                NativeMethods.NTDLL.NtOpenSection(out IntPtr handle, NativeMethods.NTDLL.WRITE_DAC, ref obj.Obj);
                NativeMethods.NTDLL.NtSetSecurityObject(handle, NativeMethods.NTDLL.DACL_SECURITY_INFORMATION, mem.SecurityDescriptor);
                NativeMethods.NTDLL.NtClose(handle);
            }
        }

        /// <summary>
        /// Retrieves the current security descriptor of the NT theme section as an SDDL string.
        /// Allocates unmanaged memory for the SDDL string, which is automatically freed.
        /// </summary>
        /// <returns>
        /// A string in Security Descriptor Definition Language (SDDL) format representing the current security of the theme section.
        /// </returns>
        private static string GetSectionSecurity()
        {
            using (SafeObjectAttributes obj = new(ThemeSectionPath))
            using (SafeSecurityDescriptor mem = new())
            {
                NativeMethods.NTDLL.NtOpenSection(out IntPtr handle, NativeMethods.NTDLL.READ_CONTROL, ref obj.Obj);
                NativeMethods.NTDLL.NtQuerySecurityObject(handle, NativeMethods.NTDLL.DACL_SECURITY_INFORMATION, out IntPtr sdPtr);
                NativeMethods.ADVAPI.ConvertSecurityDescriptorToStringSecurityDescriptor(sdPtr, 1, NativeMethods.NTDLL.DACL_SECURITY_INFORMATION, out mem.SddlPtr, out _);

                string sddl = Marshal.PtrToStringUni(mem.SddlPtr);
                NativeMethods.NTDLL.NtClose(handle);
                return sddl;
            }
        }

        /// <summary>
        /// Helper class that wraps a security descriptor pointer and SDDL pointer and frees them automatically.
        /// Use for memory safety when working with unmanaged security descriptors and strings.
        /// </summary>
        private sealed class SafeSecurityDescriptor : IDisposable
        {
            /// <summary>
            /// Pointer to the unmanaged security descriptor.
            /// </summary>
            public IntPtr SecurityDescriptor { get; private set; }

            /// <summary>
            /// Pointer to the unmanaged SDDL string returned by ConvertSecurityDescriptorToStringSecurityDescriptor.
            /// </summary>
            public IntPtr SddlPtr;

            /// <summary>
            /// Initializes an empty descriptor for use with SDDL string output.
            /// </summary>
            public SafeSecurityDescriptor() { }

            /// <summary>
            /// Converts an SDDL string to an unmanaged security descriptor and stores it.
            /// </summary>
            /// <param name="sddl">The SDDL string to convert.</param>
            public SafeSecurityDescriptor(string sddl)
            {
                if (!NativeMethods.ADVAPI.ConvertStringSecurityDescriptorToSecurityDescriptor(sddl, 1, out var sd, out _))
                    throw new InvalidOperationException("Failed to convert SDDL.");

                SecurityDescriptor = sd;
            }

            /// <summary>
            /// Frees all unmanaged memory associated with this object.
            /// </summary>
            public void Dispose()
            {
                if (SecurityDescriptor != IntPtr.Zero)
                {
                    NativeMethods.Kernel32.LocalFree(SecurityDescriptor);
                    SecurityDescriptor = IntPtr.Zero;
                }

                if (SddlPtr != IntPtr.Zero)
                {
                    NativeMethods.Kernel32.LocalFree(SddlPtr);
                    SddlPtr = IntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// Helper class that wraps OBJECT_ATTRIBUTES and frees the unmanaged object name automatically.
        /// Use for memory safety when working with NT object attributes.
        /// </summary>
        private sealed class SafeObjectAttributes : IDisposable
        {
            /// <summary>
            /// The managed OBJECT_ATTRIBUTES structure.
            /// </summary>
            public NativeMethods.NTDLL.OBJECT_ATTRIBUTES Obj;

            /// <summary>
            /// Allocates an OBJECT_ATTRIBUTES structure for the given NT path.
            /// </summary>
            /// <param name="path">The NT object path.</param>
            public SafeObjectAttributes(string path)
            {
                Obj = NativeMethods.NTDLL.BuildObjectAttributes(path);
            }

            /// <summary>
            /// Frees the unmanaged memory allocated for the object name.
            /// </summary>
            public void Dispose()
            {
                Marshal.FreeHGlobal(Obj.ObjectName);
            }
        }
    }
}