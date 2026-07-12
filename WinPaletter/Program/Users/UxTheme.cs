using Serilog.Events;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides partial class implementation for interacting with the User Experience (UX) WinTheme APIs.
    /// This partial class may contain additional members related to UxTheme functionality.
    /// </summary>
    public partial class UxTheme
    {
        /// <summary>
        /// Contains private functions for interacting with the User Experience (UX) WinTheme APIs.
        /// </summary>
        private partial class PrivateFunctions
        {
            /// <summary>
            /// Sets the system visual style using specified parameters.
            /// </summary>
            /// <param name="pszFilename"></param>
            /// <param name="pszColor"></param>
            /// <param name="pszSize"></param>
            /// <param name="dwReserved"></param>
            /// <returns></returns>
            [DllImport("UxTheme.DLL", BestFitMapping = false, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, EntryPoint = "#65")]
            public static extern int SetSystemVisualStyle(string pszFilename, string pszColor, string pszSize, int dwReserved);

            /// <summary>
            /// Enables or disables theming based on the specified flag.
            /// </summary>
            /// <param name="fEnable"></param>
            /// <returns></returns>
            [DllImport("uxtheme", ExactSpelling = true)]
            public static extern int EnableTheming(int fEnable);

            /// <summary>
            /// Retrieves the current theme name, color name, and size name.
            /// </summary>
            /// <param name="stringThemeName"></param>
            /// <param name="lengthThemeName"></param>
            /// <param name="stringColorName"></param>
            /// <param name="lengthColorName"></param>
            /// <param name="stringSizeName"></param>
            /// <param name="lengthSizeName"></param>
            /// <returns></returns>
            [DllImport("uxtheme", CharSet = CharSet.Unicode)]
            public static extern int GetCurrentThemeName(StringBuilder stringThemeName, int lengthThemeName, StringBuilder stringColorName, int lengthColorName, StringBuilder stringSizeName, int lengthSizeName);

            /// <summary>
            /// Safe, documented check for whether a visual theme (non-classic) is currently active.
            /// Used as a guard before calling the private/ordinal-based APIs above, which are known
            /// to misbehave (including native access violations) on Windows 7 when the classic theme
            /// is in effect and the theme manager isn't fully initialized.
            /// </summary>
            /// <returns></returns>
            [DllImport("uxtheme", ExactSpelling = true)]
            public static extern bool IsThemeActive();
        }

        /// <summary>
        /// Logs the result of setting the system visual style using the specified parameters.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="result"></param>
        /// <param name="pszFilename"></param>
        /// <param name="pszColor"></param>
        /// <param name="pszSize"></param>
        /// <param name="dwReserved"></param>
        static void Verboser_SetSystemVisualStyle(TreeView treeView, bool result, string pszFilename, string pszColor, string pszSize, int dwReserved)
        {
            // Check if the operation was successful
            if (!result)
            {
                // Get the last Win32 error code
                int Error = Marshal.GetLastWin32Error();

                // Check if the error code is not 0
                if (Error != 0)
                {
                    // Create a new Win32Exception using the error code
                    Win32Exception ex = new(Error);

                    // Log the error message using the ThemeLog class
                    if (treeView != null)
                        ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_SettingVS, "uxtheme.dll", pszFilename, pszColor, pszSize, dwReserved, $"ERROR {Error}: {ex.Message}"), "dll");

                    Program.Log?.Write(LogEventLevel.Error, string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_SettingVS, "uxtheme.dll", pszFilename, pszColor, pszSize, dwReserved, $"ERROR {Error}: {ex.Message}"));

                    // Add the error message to the Exceptions.ThemeApply list
                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_SettingVS, "uxtheme.dll", pszFilename, pszColor, pszSize, dwReserved, $"ERROR {Error}: {ex.Message}"), ex));

                    // Exit the function
                    return;
                }
            }

            // Log the result of the operation if it is successful using the ThemeLog class
            if (treeView != null)
                ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_SettingVS, "uxtheme.dll", pszFilename, pszColor, pszSize, dwReserved, result.ToString().ToLower()), "dll");

            Program.Log?.Write(LogEventLevel.Information, string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_SettingVS, "uxtheme.dll", pszFilename, pszColor, pszSize, dwReserved, result.ToString().ToLower()));
        }

        /// <summary>
        /// Logs the result of enabling or disabling theming based on the specified flag.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="result"></param>
        /// <param name="fEnable"></param>
        static void Verboser_EnableTheming(TreeView treeView, bool result, int fEnable)
        {
            // Check if the operation was successful
            if (!result)
            {
                // Get the last Win32 error code
                int Error = Marshal.GetLastWin32Error();

                // Check if the error code is not 0
                if (Error != 0)
                {
                    // Create a new Win32Exception using the error code
                    Win32Exception ex = new(Error);

                    // Log the error message using the ThemeLog class
                    if (treeView != null)
                        ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_EnableTheme, "uxtheme.dll", "EnableTheming", fEnable, $"ERROR {Error}: {ex.Message}"), "dll");

                    // Add the error message to the Exceptions.ThemeApply list
                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_EnableTheme, "uxtheme.dll", "EnableTheming", fEnable, $"ERROR {Error}: {ex.Message}"), ex));

                    // Exit the function
                    return;
                }
            }

            // Log the result of the operation if it is successful using the ThemeLog class
            if (treeView != null)
                ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_EnableTheme, "uxtheme.dll", "EnableTheming", fEnable, result.ToString().ToLower()), "dll");
        }

        /// <summary>
        /// Logs the current theme name, color name, and size name.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="result"></param>
        /// <param name="themename"></param>
        /// <param name="colorname"></param>
        /// <param name="sizename"></param>
        static void Verboser_GetCurrentThemeName(TreeView treeView, bool result, string themename, string colorname, string sizename)
        {
            // Check if the operation was successful
            if (!result)
            {
                // Get the last Win32 error code
                int Error = Marshal.GetLastWin32Error();

                // Check if the error code is not 0
                if (Error != 0)
                {
                    // Create a new Win32Exception using the error code
                    Win32Exception ex = new(Error);

                    // Log the error message using the ThemeLog class
                    if (treeView != null)
                        ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_EnableTheme, "uxtheme.dll", "GetCurrentThemeName", string.Empty, $"ERROR {Error}: {ex.Message}"), "dll");

                    // Add the error message to the Exceptions.ThemeApply list
                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_EnableTheme, "uxtheme.dll", "GetCurrentThemeName", string.Empty, $"ERROR {Error}: {ex.Message}"), ex));

                    // Exit the function
                    return;
                }
            }

            // Log the result of the operation if it is successful using the ThemeLog class
            if (treeView != null)
                ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.UxTheme_EnableTheme, "uxtheme.dll", "GetCurrentThemeName", string.Empty, $"\"{themename}\", {colorname}, {sizename}"), "dll");
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
        [HandleProcessCorruptedStateExceptions]
        public static bool SetSystemVisualStyle(string pszFilename, string pszColor, string pszSize, int dwReserved, TreeView treeView = null)
        {
            // Initialize the result as false
            bool result = false;

            try
            {
                // Check if the user has administrator privileges
                if (User.SID == User.AdminSID_GrantedUAC)
                {
                    // Set the system visual style directly if the user is an administrator
                    result = PrivateFunctions.SetSystemVisualStyle(pszFilename, pszColor, pszSize, dwReserved) == 1;
                }
                else if (User.Identity != null)
                {
                    bool advapi_switched = false;

                    // Impersonate the selected user and attempt to set the system visual style
                    using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                    {
                        try
                        {
                            // Impersonate the user if a token is available using the advapi32 library
                            if (User.Token != IntPtr.Zero) { advapi_switched = ADVAPI.ImpersonateLoggedOnUser(User.Token); }

                            // Set the system visual style while impersonating the selected user
                            result = PrivateFunctions.SetSystemVisualStyle(pszFilename, pszColor, pszSize, dwReserved) == 1;
                        }
                        finally
                        {
                            // Revert impersonation and undo changes
                            if (advapi_switched) { ADVAPI.RevertToSelf(); }

                            // Undo impersonation
                            wic.Undo();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // The private ordinal-based SetSystemVisualStyle export (#65) is undocumented and has
                // been observed to fault natively (including access violations) on Windows 7 when the
                // classic theme is active and the theme manager hasn't been fully initialized. Catching
                // here (together with HandleProcessCorruptedStateExceptions) prevents that from taking
                // the whole process down; the caller just sees a failed result instead.
                result = false;
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
        [HandleProcessCorruptedStateExceptions]
        public static bool EnableTheming(int fEnable, TreeView treeView = null)
        {
            // Initialize the result as false
            bool result = false;

            try
            {
                // Check if the user has administrator privileges
                if (User.SID == User.AdminSID_GrantedUAC)
                {
                    // Enable or disable theming directly if the user is an administrator
                    result = PrivateFunctions.EnableTheming(fEnable) == 1;
                }
                else if (User.Identity != null)
                {
                    // If not an administrator, impersonate the user and attempt to enable or disable theming
                    bool advapi_switched = false;

                    // Impersonate the selected user and attempt to enable or disable theming
                    using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                    {
                        try
                        {
                            // Impersonate the user if a token is available using the advapi32 library
                            if (User.Token != IntPtr.Zero) { advapi_switched = ADVAPI.ImpersonateLoggedOnUser(User.Token); }

                            // Enable or disable theming while impersonating the selected user
                            result = PrivateFunctions.EnableTheming(fEnable) == 1;
                        }
                        finally
                        {
                            // Revert impersonation and undo changes
                            if (advapi_switched) { ADVAPI.RevertToSelf(); }

                            // Undo impersonation
                            wic.Undo();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Same rationale as SetSystemVisualStyle: guard against native faults from the
                // ordinal-based EnableTheming export under classic theme on Windows 7.
                result = false;
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
        /// <code>Item1: WinTheme File
        /// Item2: Color name
        /// Item3: Size name
        /// </code></returns>
        [HandleProcessCorruptedStateExceptions]
        public static Tuple<string, string, string> GetCurrentVS(TreeView treeView = null)
        {
            Tuple<string, string, string> empty = new(string.Empty, string.Empty, string.Empty);

            // Fast, safe, documented check first. When no visual theme is active (classic theme,
            // which is exactly the state that has been observed to crash the private/ordinal-based
            // APIs on Windows 7), skip GetCurrentThemeName entirely rather than calling into a theme
            // manager that isn't initialized.
            bool themeActive;
            try
            {
                themeActive = PrivateFunctions.IsThemeActive();
            }
            catch (Exception)
            {
                themeActive = false;
            }

            if (!themeActive)
            {
                Verboser_GetCurrentThemeName(treeView, false, string.Empty, string.Empty, string.Empty);
                return empty;
            }

            // Initialize the result as false
            bool result = false;

            // Initialize string builders for theme, color, and size names
            StringBuilder vsFile = new(260);
            StringBuilder colorName = new(260);
            StringBuilder sizeName = new(260);

            try
            {
                // Check if the user is the user who granted UAC elevation dialog during the application startup
                if (User.SID == User.AdminSID_GrantedUAC)
                {
                    // Get the current theme name, color name, and size name directly
                    PrivateFunctions.GetCurrentThemeName(vsFile, vsFile.Capacity, colorName, colorName.Capacity, sizeName, sizeName.Capacity);

                    // Check if the theme file exists and is not empty
                    result = !string.IsNullOrWhiteSpace(vsFile.ToString()) && File.Exists(vsFile.ToString());
                }
                else if (User.Identity != null)
                {
                    bool advapi_switched = false;

                    // Impersonate the selected user and attempt to get the current theme name, color name, and size name
                    using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                    {
                        try
                        {
                            // Impersonate the user if a token is available using the advapi32 library
                            if (User.Token != IntPtr.Zero) { advapi_switched = ADVAPI.ImpersonateLoggedOnUser(User.Token); }

                            // Get the current theme name, color name, and size name while impersonating the selected user
                            PrivateFunctions.GetCurrentThemeName(vsFile, vsFile.Capacity, colorName, colorName.Capacity, sizeName, sizeName.Capacity);

                            // Check if the theme file exists and is not empty
                            result = !string.IsNullOrWhiteSpace(vsFile.ToString()) && File.Exists(vsFile.ToString());
                        }
                        finally
                        {
                            // Revert impersonation
                            if (advapi_switched) { ADVAPI.RevertToSelf(); }

                            wic.Undo();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Same rationale as SetSystemVisualStyle/EnableTheming: guard against native faults
                // from GetCurrentThemeName under classic theme on Windows 7.
                result = false;
            }

            // Log details of the operation using Verboser_GetCurrentThemeName function
            Verboser_GetCurrentThemeName(treeView, result, vsFile.ToString(), colorName.ToString(), sizeName.ToString());

            if (result)
            {
                // Return the current theme name, color name, and size name
                return new Tuple<string, string, string>(vsFile.ToString(), colorName.ToString(), sizeName.ToString());
            }
            else
            {
                // Return empty strings if the operation was not successful
                return empty;
            }
        }
    }
}