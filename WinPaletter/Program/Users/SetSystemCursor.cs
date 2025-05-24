using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides partial class implementation for interacting with the User32 (User Interface) APIs.
    /// This partial class may contain additional members related to User32 functionality.
    /// </summary>
    public partial class User32
    {
        /// <summary>
        /// Loads a cursor resource from a file handle.
        /// </summary>
        private partial class PrivateFunctions
        {
            [DllImport("user32.dll")]
            public static extern bool SetSystemCursor(IntPtr hcur, int id);
        }

        /// <summary>
        /// Logs the result of setting a system cursor.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="result"></param>
        /// <param name="file"></param>
        /// <param name="id"></param>
        static void Verboser_Cursors(TreeView treeView, bool result, string file, OCR_SYSTEM_CURSORS id)
        {
            if (!result)
            {
                // Get the last error code
                int Error = Marshal.GetLastWin32Error();

                // Log the error and details
                if (Error != 0)
                {
                    // Create a new exception with the error code
                    Win32Exception ex = new(Error);

                    // Log the error and details
                    if (treeView != null)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.User32_SSC, "user32.dll", "SetSystemCursor", $"\"{file}\"", id.ToString(), $"ERROR {Error}: {ex.Message}"), "dll");

                    // Add the exception to the list of exceptions
                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(string.Format(Program.Lang.Strings.ThemeManager.Advanced.User32_SSC, "user32.dll", "SetSystemCursor", $"\"{file}\"", id.ToString(), $"ERROR {Error}: {ex.Message}"), ex));

                    return;
                }
            }

            // If the operation succeeded, log the details
            if (treeView != null)
                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.User32_SSC, "user32.dll", "SetSystemCursor", $"\"{file}\"", id.ToString(), result.ToString().ToLower()), "dll");
        }

        /// <summary>
        /// Sets a system cursor from a file with the specified ID.
        /// </summary>
        /// <param name="file">The path to the cursor File.</param>
        /// <param name="id">The ID of the system cursor to set.</param>
        /// <param name="treeView">Optional TreeView for logging or user feedback.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool SetSystemCursor(string file, OCR_SYSTEM_CURSORS id, TreeView treeView = null)
        {
            bool result = false;

            if (User.SID == User.AdminSID_GrantedUAC)
            {
                // Set system cursor using the provided File path and ID
                result = PrivateFunctions.SetSystemCursor(LoadCursorFromFile(file), (int)id);
            }
            else
            {
                // Set the flag for switching to the Advapi32 impersonation
                bool advapi_switched = false;

                // Impersonate the user and switch to the Advapi32 impersonation
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Set system cursor using the provided File path and ID under impersonation
                    result = PrivateFunctions.SetSystemCursor(LoadCursorFromFile(file), (int)id);

                    // Revert the Advapi32 impersonation and undo the impersonation
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the result and details
            Verboser_Cursors(treeView, result, file, id);

            return result;
        }

        /// <summary>
        /// Sets a system cursor from a File with the specified ID.
        /// </summary>
        /// <param name="treeView">Optional TreeView for logging or user feedback.</param>
        /// <param name="file">The path to the cursor File.</param>
        /// <param name="id">The ID of the system cursor to set.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool SetSystemCursor(TreeView treeView, string file, OCR_SYSTEM_CURSORS id)
        {
            // Delegate to the main method for consistency.
            return SetSystemCursor(file, id, treeView);
        }

        /// <summary>
        /// Sets a system cursor from a handle with the specified ID.
        /// </summary>
        /// <param name="hcur">The handle to the cursor.</param>
        /// <param name="id">The ID of the system cursor to set.</param>
        /// <param name="treeView">Optional TreeView for logging or user feedback.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool SetSystemCursor(IntPtr hcur, OCR_SYSTEM_CURSORS id, TreeView treeView = null)
        {
            bool result = false;

            if (User.SID == User.AdminSID_GrantedUAC)
            {
                // Set system cursor using the provided handle and ID
                result = PrivateFunctions.SetSystemCursor(hcur, (int)id);
            }
            else
            {
                // Set the flag for switching to the Advapi32 impersonation
                bool advapi_switched = false;

                // Impersonate the user and switch to the Advapi32 impersonation
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // Impersonate the user and switch to the Advapi32 impersonation
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Set system cursor using the provided handle and ID under impersonation
                    result = PrivateFunctions.SetSystemCursor(hcur, (int)id);

                    // Revert the Advapi32 impersonation and undo the impersonation
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the result and details
            Verboser_Cursors(treeView, result, hcur.ToInt32().ToString(), id);

            return result;
        }

        /// <summary>
        /// Sets a system cursor from a handle with the specified ID.
        /// </summary>
        /// <param name="treeView">Optional TreeView for logging or user feedback.</param>
        /// <param name="hcur">The handle to the cursor.</param>
        /// <param name="id">The ID of the system cursor to set.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool SetSystemCursor(TreeView treeView, IntPtr hcur, OCR_SYSTEM_CURSORS id)
        {
            // Delegate to the main method for consistency.
            return SetSystemCursor(hcur, id, treeView);
        }

    }
}
