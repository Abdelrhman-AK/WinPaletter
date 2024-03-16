using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides partial class implementation for interacting with the User Experience (UX) Theme APIs.
    /// This partial class may contain additional members related to UxTheme functionality.
    /// </summary>
    public partial class UxTheme
    {
        private partial class PrivateFunctions
        {
            [DllImport("UxTheme.DLL", BestFitMapping = false, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, EntryPoint = "#65")]
            public static extern int SetSystemVisualStyle(string pszFilename, string pszColor, string pszSize, int dwReserved);

            [DllImport("uxtheme", ExactSpelling = true)]
            public static extern int EnableTheming(int fEnable);

            [DllImport("uxtheme", CharSet = CharSet.Unicode)]
            public static extern int GetCurrentThemeName(StringBuilder stringThemeName, int lengthThemeName, StringBuilder stringColorName, int lengthColorName, StringBuilder stringSizeName, int lengthSizeName);
        }

        static void Verboser_SetSystemVisualStyle(TreeView treeView, bool result, string pszFilename, string pszColor, string pszSize, int dwReserved)
        {
            if (!result)
            {
                int Error = Marshal.GetLastWin32Error();

                if (Error != 0)
                {
                    Win32Exception ex = new(Error);

                    if (treeView != null)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "uxtheme.dll", pszFilename, pszColor, pszSize, dwReserved, $"ERROR {Error}: {ex.Message}"), "dll");

                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(string.Format(Program.Lang.Verbose_UxTheme_SSVS, "uxtheme.dll", pszFilename, pszColor, pszSize, dwReserved, $"ERROR {Error}: {ex.Message}"), ex));

                    return;
                }
            }

            if (treeView != null)
                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_UxTheme_SSVS, "uxtheme.dll", pszFilename, pszColor, pszSize, dwReserved, result.ToString().ToLower()), "dll");
        }

        static void Verboser_EnableTheming(TreeView treeView, bool result, int fEnable)
        {
            if (!result)
            {
                int Error = Marshal.GetLastWin32Error();

                if (Error != 0)
                {
                    Win32Exception ex = new(Error);

                    if (treeView != null)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "uxtheme.dll", "EnableTheming", fEnable, $"ERROR {Error}: {ex.Message}"), "dll");

                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(string.Format(Program.Lang.Verbose_UxTheme_ET, "uxtheme.dll", "EnableTheming", fEnable, $"ERROR {Error}: {ex.Message}"), ex));

                    return;
                }
            }

            if (treeView != null)
                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "uxtheme.dll", "EnableTheming", fEnable, result.ToString().ToLower()), "dll");
        }

        static void Verboser_GetCurrentThemeName(TreeView treeView, bool result, string themename, string colorname, string sizename)
        {
            if (!result)
            {
                int Error = Marshal.GetLastWin32Error();

                if (Error != 0)
                {
                    Win32Exception ex = new(Error);

                    if (treeView != null)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "uxtheme.dll", "GetCurrentThemeName", string.Empty, $"ERROR {Error}: {ex.Message}"), "dll");

                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(string.Format(Program.Lang.Verbose_UxTheme_ET, "uxtheme.dll", "GetCurrentThemeName", string.Empty, $"ERROR {Error}: {ex.Message}"), ex));

                    return;
                }
            }

            if (treeView != null)
                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Verbose_UxTheme_ET, "uxtheme.dll", "GetCurrentThemeName", string.Empty, $"\"{themename}\", {colorname}, {sizename}"), "dll");
        }

        /// <summary>
        /// Sets the system visual style using specified parameters and handles UAC elevation if necessary.
        /// </summary>
        /// <param name="pszFilename">The filename of the visual style.</param>
        /// <param name="pszColor">The color of the visual style.</param>
        /// <param name="pszSize">The size of the visual style.</param>
        /// <param name="dwReserved">Reserved parameter for future use.</param>
        /// <param name="treeView">Optional TreeView control for verbose logging.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public static bool SetSystemVisualStyle(string pszFilename, string pszColor, string pszSize, int dwReserved, TreeView treeView = null)
        {
            bool result = false;

            // Check if the user has administrator privileges
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                // Set the system visual style directly if the user is an administrator
                result = PrivateFunctions.SetSystemVisualStyle(pszFilename, pszColor, pszSize, dwReserved) == 1;
            }
            else
            {
                // If not an administrator, impersonate the user and attempt to set the system visual style
                bool advapi_switched = false;

                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Set the system visual style while impersonating the user
                    result = PrivateFunctions.SetSystemVisualStyle(pszFilename, pszColor, pszSize, dwReserved) == 1;

                    // Revert impersonation and undo changes
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    wic.Undo();
                }
            }

            // Log details of the operation using Verboser_SetSystemVisualStyle function
            Verboser_SetSystemVisualStyle(treeView, result, pszFilename, pszColor, pszSize, dwReserved);

            return result;
        }

        /// <summary>
        /// Calls the SetSystemVisualStyle function with parameters reordered for convenience.
        /// </summary>
        /// <param name="treeView">Optional TreeView control for verbose logging.</param>
        /// <param name="pszFilename">The filename of the visual style.</param>
        /// <param name="pszColor">The color of the visual style.</param>
        /// <param name="pszSize">The size of the visual style.</param>
        /// <param name="dwReserved">Reserved parameter for future use.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public static bool SetSystemVisualStyle(TreeView treeView, string pszFilename, string pszColor, string pszSize, int dwReserved)
        {
            return SetSystemVisualStyle(pszFilename, pszColor, pszSize, dwReserved, treeView);
        }

        /// <summary>
        /// Enables or disables theming based on the specified flag, handling UAC elevation if required.
        /// </summary>
        /// <param name="fEnable">The flag indicating whether theming should be enabled (1) or disabled (0).</param>
        /// <param name="treeView">Optional TreeView control for verbose logging.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public static bool EnableTheming(int fEnable, TreeView treeView = null)
        {
            bool result = false;

            // Check if the user has administrator privileges
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                // Enable or disable theming directly if the user is an administrator
                result = PrivateFunctions.EnableTheming(fEnable) == 1;
            }
            else
            {
                // If not an administrator, impersonate the user and attempt to enable or disable theming
                bool advapi_switched = false;

                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Enable or disable theming while impersonating the user
                    result = PrivateFunctions.EnableTheming(fEnable) == 1;

                    // Revert impersonation and undo changes
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    wic.Undo();
                }
            }

            // Log details of the operation using Verboser_EnableTheming function
            Verboser_EnableTheming(treeView, result, fEnable);

            return result;
        }

        /// <summary>
        /// Calls the EnableTheming function with parameters reordered for convenience.
        /// </summary>
        /// <param name="treeView">Optional TreeView control for verbose logging.</param>
        /// <param name="fEnable">The flag indicating whether theming should be enabled (1) or disabled (0).</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public static bool EnableTheming(TreeView treeView, int fEnable)
        {
            return EnableTheming(fEnable, treeView);
        }

        /// <summary>
        /// Get current applied visual styles data
        /// </summary>
        /// <returns>
        /// <code>Item1: Theme file
        /// Item2: Color name
        /// Item3: Size name
        /// </code></returns>
        public static Tuple<string, string, string> GetCurrentVS(TreeView treeView = null)
        {
            bool result = false;
            StringBuilder vsFile = new(260);
            StringBuilder colorName = new(260);
            StringBuilder sizeName = new(260);

            if (User.SID == User.AdminSID_GrantedUAC)
            {
                PrivateFunctions.GetCurrentThemeName(vsFile, vsFile.Capacity, colorName, colorName.Capacity, sizeName, sizeName.Capacity);
                result = !string.IsNullOrWhiteSpace(vsFile.ToString()) && System.IO.File.Exists(vsFile.ToString());
            }

            else
            {
                bool advapi_switched = false;

                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    PrivateFunctions.GetCurrentThemeName(vsFile, vsFile.Capacity, colorName, colorName.Capacity, sizeName, sizeName.Capacity);
                    result = !string.IsNullOrWhiteSpace(vsFile.ToString()) && System.IO.File.Exists(vsFile.ToString());

                    if (advapi_switched) { advapi.RevertToSelf(); }

                    wic.Undo();
                }
            }

            Verboser_GetCurrentThemeName(treeView, result, vsFile.ToString(), colorName.ToString(), sizeName.ToString());

            if (result)
            {
                return new Tuple<string, string, string>(vsFile.ToString(), colorName.ToString(), sizeName.ToString());
            }
            else
            {
                return new Tuple<string, string, string>(string.Empty, string.Empty, string.Empty);
            }
        }
    }
}
