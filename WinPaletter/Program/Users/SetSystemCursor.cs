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
        private partial class PrivateFunctions
        {
            [DllImport("user32.dll")]
            public static extern bool SetSystemCursor(IntPtr hcur, int id);
        }

        static void Verboser_Cursors(TreeView TreeView, bool result, string file, OCR_SYSTEM_CURSORS id)
        {
            if (!result)
            {
                int Error = Marshal.GetLastWin32Error();

                if (Error != 0)
                {
                    Win32Exception ex = new(Error);

                    if (TreeView != null)
                        Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "user32.dll", "SetSystemCursor", $"\"{file}\"", id.ToString(), $"ERROR {Error}: " + ex.Message), "dll");

                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(string.Format(Program.Lang.Verbose_User32_SSC, "user32.dll", "SetSystemCursor", $"\"{file}\"", id.ToString(), $"ERROR {Error}: " + ex.Message), ex));

                    return;
                }
            }

            if (TreeView != null)
                Theme.Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "user32.dll", "SetSystemCursor", $"\"{file}\"", id.ToString(), result.ToString().ToLower()), "dll");
        }

        /// <summary>
        /// Sets a system cursor from a file with the specified ID.
        /// </summary>
        /// <param name="file">The path to the cursor file.</param>
        /// <param name="id">The ID of the system cursor to set.</param>
        /// <param name="TreeView">Optional TreeView for logging or user feedback.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool SetSystemCursor(string file, OCR_SYSTEM_CURSORS id, TreeView TreeView = null)
        {
            bool result = false;

            if (User.SID == User.AdminSID_GrantedUAC)
            {
                // Set system cursor using the provided file path and ID
                result = PrivateFunctions.SetSystemCursor(LoadCursorFromFile(file), (int)id);
            }
            else
            {
                bool advapi_switched = false;

                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Set system cursor using the provided file path and ID under impersonation
                    result = PrivateFunctions.SetSystemCursor(LoadCursorFromFile(file), (int)id);

                    if (advapi_switched) { advapi.RevertToSelf(); }

                    wic.Undo();
                }
            }

            // Log the result and details
            Verboser_Cursors(TreeView, result, file, id);

            return result;
        }

        /// <summary>
        /// Sets a system cursor from a file with the specified ID.
        /// </summary>
        /// <param name="TreeView">Optional TreeView for logging or user feedback.</param>
        /// <param name="file">The path to the cursor file.</param>
        /// <param name="id">The ID of the system cursor to set.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool SetSystemCursor(TreeView TreeView, string file, OCR_SYSTEM_CURSORS id)
        {
            // Delegate to the main method for consistency.
            return SetSystemCursor(file, id, TreeView);
        }

        /// <summary>
        /// Sets a system cursor from a handle with the specified ID.
        /// </summary>
        /// <param name="hcur">The handle to the cursor.</param>
        /// <param name="id">The ID of the system cursor to set.</param>
        /// <param name="TreeView">Optional TreeView for logging or user feedback.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool SetSystemCursor(IntPtr hcur, OCR_SYSTEM_CURSORS id, TreeView TreeView = null)
        {
            bool result = false;

            if (User.SID == User.AdminSID_GrantedUAC)
            {
                // Set system cursor using the provided handle and ID
                result = PrivateFunctions.SetSystemCursor(hcur, (int)id);
            }
            else
            {
                bool advapi_switched = false;

                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Set system cursor using the provided handle and ID under impersonation
                    result = PrivateFunctions.SetSystemCursor(hcur, (int)id);

                    if (advapi_switched) { advapi.RevertToSelf(); }

                    wic.Undo();
                }
            }

            // Log the result and details
            Verboser_Cursors(TreeView, result, hcur.ToInt32().ToString(), id);

            return result;
        }

        /// <summary>
        /// Sets a system cursor from a handle with the specified ID.
        /// </summary>
        /// <param name="TreeView">Optional TreeView for logging or user feedback.</param>
        /// <param name="hcur">The handle to the cursor.</param>
        /// <param name="id">The ID of the system cursor to set.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public static bool SetSystemCursor(TreeView TreeView, IntPtr hcur, OCR_SYSTEM_CURSORS id)
        {
            // Delegate to the main method for consistency.
            return SetSystemCursor(hcur, id, TreeView);
        }

    }
}
