using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for Kernel32 (Windows Kernel) functions.
    /// </summary>
    public class Kernel32
    {
        /// <summary>
        /// Disables file system redirection for the calling thread.
        /// </summary>
        /// <param name="ptr">A pointer to a value that receives the address of the Wow64 redirection information.</param>
        /// <returns>Returns true if the function succeeds; otherwise, false.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        /// <summary>
        /// Reverts the file system redirection previously disabled for the calling thread.
        /// </summary>
        /// <param name="ptr">A pointer to the Wow64 redirection information obtained from a previous call to <see cref="Wow64DisableWow64FsRedirection"/>.</param>
        /// <returns>Returns true if the function succeeds; otherwise, false.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        /// <summary>
        /// Writes data to the specified section of an initialization file.
        /// </summary>
        /// <param name="section">The name of the section to which the data is to be copied.</param>
        /// <param name="key">The name of the key whose associated value is to be set.</param>
        /// <param name="val">The string to be written.</param>
        /// <param name="filePath">The name of the initialization file.</param>
        /// <returns>Returns nonzero if the function succeeds; otherwise, zero.</returns>
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// Retrieves a string from the specified section in an initialization file.
        /// </summary>
        /// <param name="section">The name of the section containing the key name.</param>
        /// <param name="key">The name of the key whose associated value is to be retrieved.</param>
        /// <param name="def">A default value.</param>
        /// <param name="retVal">A StringBuilder that receives the retrieved string.</param>
        /// <param name="size">The size of the buffer pointed to by the retVal parameter, in characters.</param>
        /// <param name="filePath">The name of the initialization file.</param>
        /// <returns>Returns the number of characters copied to the buffer, excluding the null-terminating character.</returns>
        [DllImport("kernel32.dll")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject">A handle to an open object.</param>
        /// <returns>Returns true if the function succeeds; otherwise, false.</returns>
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// Enumerates process access flags.
        /// </summary>
        [Flags]
        private enum ProcessAccessFlags : uint
        {
            /// <summary>
            /// Query information.
            /// </summary>
            QueryLimitedInformation = 0x00001000
        }

        /// <summary>
        /// Retrieves the full path and file name of the executable file for a specified process.
        /// </summary>
        /// <param name="hProcess">A handle to the process.</param>
        /// <param name="dwFlags">This parameter is reserved for future use. It must be zero.</param>
        /// <param name="lpExeName">A pointer to a buffer that receives the null-terminated string specifying the executable file for the process.</param>
        /// <param name="lpdwSize">On input, specifies the size of the lpExeName buffer, in characters. On success, receives the number of characters written to the buffer, excluding the null-terminating character.</param>
        /// <returns>Returns true if the function succeeds; otherwise, false.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool QueryFullProcessImageName(
            [In] IntPtr hProcess,
            [In] int dwFlags,
            [Out] StringBuilder lpExeName,
            ref int lpdwSize);

        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="processAccess">The access to the process object.</param>
        /// <param name="bInheritHandle">If this value is true, processes created by this process will inherit the handle.</param>
        /// <param name="processId">The identifier of the local process to be opened.</param>
        /// <returns>If the function succeeds, the return value is an open handle to the specified process. If the function fails, the return value is IntPtr.Zero.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        /// <summary>
        /// Retrieves the full path and file name of the executable file for a specified process.
        /// </summary>
        /// <param name="p">The process for which to retrieve the filename.</param>
        /// <returns>The full path and filename of the executable file for the specified process.</returns>
        public static string GetProcessFilename(Process p)
        {
            int capacity = 2000;
            StringBuilder builder = new StringBuilder(capacity);
            IntPtr ptr = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, p.Id);
            if (!QueryFullProcessImageName(ptr, 0, builder, ref capacity))
            {
                return string.Empty;
            }

            return builder.ToString();
        }

        /// <summary>
        /// GetModuleHandle function: Retrieves a module handle for the specified module.
        /// If the function succeeds, the return value is a handle to the specified module.
        /// If the function fails, the return value is IntPtr.Zero.
        /// </summary>
        /// <param name="lpModuleName">The name of the loaded module (usually a DLL) or NULL to get the handle of the calling module.</param>
        /// <returns>If the function succeeds, the return value is a handle to the specified module.
        /// If the function fails, the return value is IntPtr.Zero.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
