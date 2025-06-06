﻿using CommandLine;
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
            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, ref NONCLIENTMETRICS lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, ref ICONMETRICS lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, ref ANIMATIONINFO lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, ref HIGHCONTRAST lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, ref bool lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, ref int lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, ref uint lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, int lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, uint lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, uint uParam, int lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, string lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, int uParam, bool lpvParam, SPIF fuWinIni);

            /// <summary>
            /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
            /// </summary>
            /// <param name="uAction"></param>
            /// <param name="uParam"></param>
            /// <param name="lpvParam"></param>
            /// <param name="fuWinIni"></param>
            /// <returns></returns>
            [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SystemParametersInfo(SPI uAction, bool uParam, int lpvParam, SPIF fuWinIni);
        }

        /// <summary>
        /// Logs the result of a SystemParametersInfo function call to a TreeView control.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="result"></param>
        /// <param name="uAction"></param>
        /// <param name="uParam"></param>
        /// <param name="lpvParam"></param>
        /// <param name="fuWinIni"></param>
        static void Verboser_SPI(TreeView treeView, bool result, SPI uAction, object uParam, object lpvParam, SPIF fuWinIni)
        {
            if (!result)
            {
                // Get the last error code
                int Error = Marshal.GetLastWin32Error();

                // If the function call failed, log the error message
                if (Error != 0)
                {
                    // Create a new Win32Exception with the error code
                    Win32Exception ex = new(Error);

                    // Log the error message
                    if (treeView != null)
                        ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.User32_SPI, "user32.dll", "SystemParameterInfo", uAction.ToString(), uParam.ToString(), lpvParam.ToString(), fuWinIni.ToString(), $"ERROR {Error}: {ex.Message}"), "dll");

                    Program.Log?.Write(Serilog.Events.LogEventLevel.Error, string.Format(Program.Lang.Strings.ThemeManager.Advanced.User32_SPI, "user32.dll", "SystemParameterInfo", uAction.ToString(), uParam.ToString(), lpvParam.ToString(), fuWinIni.ToString(), $"ERROR {Error}: {ex.Message}"));

                    // Add the exception to the appropriate list
                    if (uAction.ToString().StartsWith("SPI_GET", StringComparison.OrdinalIgnoreCase))
                    {
                        Exceptions.ThemeLoad.Add(new Tuple<string, Exception>(string.Format(Program.Lang.Strings.ThemeManager.Advanced.User32_SPI, "user32.dll", "SystemParameterInfo", uAction.ToString(), uParam.ToString(), lpvParam.ToString(), fuWinIni.ToString(), $"ERROR {Error}: {ex.Message}"), ex));
                    }
                    else if (uAction.ToString().StartsWith("SPI_SET", StringComparison.OrdinalIgnoreCase))
                    {
                        Exceptions.ThemeApply.Add(new Tuple<string, Exception>(string.Format(Program.Lang.Strings.ThemeManager.Advanced.User32_SPI, "user32.dll", "SystemParameterInfo", uAction.ToString(), uParam.ToString(), lpvParam.ToString(), fuWinIni.ToString(), $"ERROR {Error}: {ex.Message}"), ex));
                    }

                    // Return if the function call failed
                    return;
                }
            }

            // If the function call succeeded, log the result
            if (treeView != null)
                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.User32_SPI, "user32.dll", "SystemParameterInfo", uAction.ToString(), uParam.ToString(), lpvParam.ToString(), fuWinIni.ToString(), result.ToString().ToLower()), "dll");

            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, string.Format(Program.Lang.Strings.ThemeManager.Advanced.User32_SPI, "user32.dll", "SystemParameterInfo", uAction.ToString(), uParam.ToString(), lpvParam.ToString(), fuWinIni.ToString(), result.ToString().ToLower()));
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to a NONCLIENTMETRICS structure, which contains information about the non-client area of a window. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, ref NONCLIENTMETRICS lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to a NONCLIENTMETRICS structure, which contains information about the non-client area of a window. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, ref NONCLIENTMETRICS lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, treeView);
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to a NONCLIENTMETRICS structure, which contains information about the non-client area of a window. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, ref HIGHCONTRAST lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter. This method is designed to interact with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to a HIGHCONTRAST structure. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, ref HIGHCONTRAST lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter related to icon metrics. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to an ICONMETRICS structure, which contains information about icon metrics. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, ref ICONMETRICS lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter related to icon metrics. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to an ICONMETRICS structure, which contains information about icon metrics. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, ref ICONMETRICS lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter related to animation information. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to an ANIMATIONINFO structure, which contains information about animation effects. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, ref ANIMATIONINFO lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter related to animation information. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to an ANIMATIONINFO structure, which contains information about animation effects. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, ref ANIMATIONINFO lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as a boolean. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to a boolean value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, ref bool lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as a boolean. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to a boolean value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, ref bool lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as an integer. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to an integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, ref int lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as an integer. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to an integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, ref int lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as an unsigned integer. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to an unsigned integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, ref uint lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as an unsigned integer. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A reference to an unsigned integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, ref uint lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, ref lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as an integer. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">An integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, int lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as an integer. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">An integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, int lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as an unsigned integer. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">An unsigned integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, uint lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as an unsigned integer. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">An unsigned integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, uint lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as an integer. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">An unsigned integer value providing additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">An integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, uint uParam, int lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as an integer. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">An unsigned integer value providing additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">An integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, uint uParam, int lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as a string. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A string representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, string lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as a string. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A string representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, string lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as a boolean. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A boolean value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, int uParam, bool lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as a boolean. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">Additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">A boolean value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, int uParam, bool lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as a boolean. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">A boolean value providing additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">An integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(SPI uAction, bool uParam, int lpvParam, SPIF fuWinIni, TreeView treeView = null)
        {
            // Create a boolean to track the result of the function call
            bool result = false;

            // If the selected user is the same as current Windows user, perform the function call directly
            if (User.SID == User.AdminSID_GrantedUAC)
            {
                result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);
            }

            // If curent Windows user is not the same as the selected user, impersonate the selected user to perform the action
            else
            {
                // Create a boolean to track if the user has been impersonated
                bool advapi_switched = false;

                // Impersonate the selected user
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // If the user has a token, impersonate the user using Advapi32
                    if (User.Token != IntPtr.Zero) { advapi_switched = advapi.ImpersonateLoggedOnUser(User.Token); }

                    // Perform the SystemParametersInfo function call
                    result = PrivateFunctions.SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni);

                    // If the user was impersonated, revert to the original user
                    if (advapi_switched) { advapi.RevertToSelf(); }

                    // Undo the impersonation
                    wic.Undo();
                }
            }

            // Log the function call details
            Verboser_SPI(treeView, result, uAction, uParam, lpvParam, fuWinIni);

            // Return the result of the function call
            return result;
        }

        /// <summary>
        /// Retrieves or sets the value of a system parameter represented as a boolean. This method interacts with the user32.dll library's SystemParametersInfo function.
        /// </summary>
        /// <param name="uAction">The system parameter to query or set. This can be one of the SPI_* constants defined in the Windows API.</param>
        /// <param name="uParam">A boolean value providing additional information about the system parameter. The meaning of this parameter depends on the uAction being performed.</param>
        /// <param name="lpvParam">An integer value representing the system parameter. This parameter is used based on the uAction being performed.</param>
        /// <param name="fuWinIni">Flags specifying whether the system should write the information to the user profile. This can be a combination of SPIF_UPDATEINIFILE and SPIF_SENDCHANGE.</param>
        /// <param name="treeView">An optional TreeView control for logging purposes. If provided, logs the function call details.</param>
        /// <returns>
        /// <c>true</c> if the system parameter is successfully retrieved or set; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs the SystemParametersInfo function in an elevated context if the user has administrative privileges. If not, it impersonates the user to perform the action. 
        /// </remarks>
        public static bool SystemParametersInfo(TreeView treeView, SPI uAction, bool uParam, int lpvParam, SPIF fuWinIni)
        {
            return SystemParametersInfo(uAction, uParam, lpvParam, fuWinIni, treeView);
        }


        /// <summary>
        /// Specifies flags for controlling the behavior of the SystemParametersInfo function.
        /// </summary>
        [Flags]
        public enum SPIF : int
        {
            /// <summary>
            /// Used for getting values
            /// </summary>
            SPIF_NONE = 0x00,

            /// <summary>
            /// Writes the new system-wide parameter setting to the user profile.
            /// </summary>
            SPIF_UPDATEINIFILE = 0x01,

            /// <summary>
            /// Broadcasts the <c><b>WM_SETTINGCHANGE</b></c> message after updating the user profile.
            /// <br><b>It is temporary until you logoff.</b></br>
            /// </summary>
            SPIF_SENDCHANGE = 0x02,

            /// <summary>
            /// Same as <c><b>SPIF_SENDCHANGE</b></c>: Broadcasts the <c><b>WM_SETTINGCHANGE</b></c> message after updating the user profile.
            /// <br><b>It is temporary until you logoff.</b></br>
            /// </summary>
            SPIF_SENDWININICHANGE = 0x02,

            /// <summary>
            /// Writes the new system-wide parameter setting to the user profile and also broadcasts the <c><b>WM_SETTINGCHANGE</b></c> message after updating the user profile.
            /// </summary>
            SPIF_WRITEANDNOTIFY = SPIF_UPDATEINIFILE | SPIF_SENDCHANGE
        }
    }
}
