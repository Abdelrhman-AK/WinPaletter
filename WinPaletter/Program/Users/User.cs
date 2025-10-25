using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// Class for handling multiple users for WinPaletter
    /// </summary>
    public static class User
    {
        #region Events/Overrides
        /// <summary>
        /// EventArgs that have data of user change event
        /// </summary>
        public class UserChangeEventArgs : EventArgs
        {
            /// <summary>
            /// User SID that invoked the change event
            /// </summary>
            public string SID { get; set; }

            /// <summary>
            /// Know if event raised before or after switching user
            /// </summary>
            public Timings Timing { get; set; }

            /// <summary>
            /// Date and time of raising user change event
            /// </summary>
            public DateTime SwitchTime => DateTime.Now;

            /// <summary>
            /// Name of user who invoked the event
            /// </summary>
            public string UserName { get { return new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\\').Last(); ; } }

            /// <summary>
            /// Name of domain hosting users
            /// </summary>
            public string Domain { get { return new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\\').First(); ; } }

            /// <summary>
            /// Return if user that invoked the event is administrator or not
            /// </summary>
            public bool Administrator => IsAdmin(SID);

            /// <summary>
            /// Path of user profile picture that invoked the event
            /// </summary>
            public string ProfilePicturePath => Shell32.GetUserTilePath(UserName);

            /// <summary>
            /// Get path of user profile
            /// <br>- For example: C:\Users\...</br>
            /// </summary>
            public string UserProfilePath => GetUserProfilePath(SID);

            /// <summary>
            /// Enumeration for user switch timing (event raised before or after switching user)
            /// </summary>
            public enum Timings
            {
                /// <summary>
                /// Event raised before switching user
                /// </summary>
                BeforeChange,

                /// <summary>
                /// Event raised after switching user
                /// </summary>
                AfterChange
            }
        }

        /// <summary>
        /// Delegate for user change event
        /// </summary>
        /// <param name="e"></param>
        public delegate void UserChangeEventHandler(UserChangeEventArgs e);

        /// <summary>
        /// Event for user change
        /// </summary>
        public static event UserChangeEventHandler UserSwitch;

        /// <summary>
        /// Void method occurs on user change event
        /// </summary>
        /// <param name="e"></param>
        public static void OnUserSwitch(UserChangeEventArgs e)
        {
            switch (e.Timing)
            {
                // The event is raised before switching user
                case User.UserChangeEventArgs.Timings.BeforeChange:
                    {
                        // Save settings before switching user
                        Program.Settings.Save(Settings.Source.Registry);
                        break;
                    }

                // The event is raised after switching user
                case User.UserChangeEventArgs.Timings.AfterChange:
                    {
                        // Change Windows cursor into wait cursor to indicate that the application is busy changing user
                        Cursor.Current = Cursors.WaitCursor;

                        // Impersonate the selected user to do operations on his profile
                        using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                        {
                            // Check if main form is opened or not
                            bool MainFormIsOpened = Application.OpenForms[Forms.MainForm.Name] is not null;

                            // List of opened forms before switching user
                            List<Form> OpenForms = [];
                            foreach (Form f in Application.OpenForms)
                            {
                                // Exclude GlassWindow and UserSwitch forms from the list of opened forms
                                if (f != Forms.GlassWindow && f != Forms.UserSwitch && f.Visible)
                                {
                                    OpenForms.Add(f);

                                    // Hide opened forms before switching user
                                    f.Visible = false;
                                }
                            }

                            // Update paths variables from new user SID
                            User.UpdatePathsFromSID(User.SID);

                            // Load settings for new user
                            Program.Settings = new(Settings.Source.Registry);

                            // Check if main form is opened or not
                            if (MainFormIsOpened)
                            {
                                // Check if there are unsaved changes in the current theme
                                if (Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation && (Program.TM != Program.TM_Original))
                                {
                                    // Ask user to save changes before switching user
                                    Forms.MainForm.ExitWithChangedFileResponse();
                                }
                            }

                            // Reinitialize the application to apply new settings for new user
                            Program.InitializeApplication();

                            // Load data for Home form if it is opened
                            if (MainFormIsOpened) { Forms.Home.LoadData(); }

                            // Hide opened forms after switching user
                            foreach (Form f in OpenForms)
                            {
                                f.Visible = true;
                            }

                            // Change Windows cursor into default cursor to indicate that the application is ready
                            Cursor.Current = Cursors.Default;

                            // Undo impersonation after finishing operations on user profile
                            wic.Undo();

                            Program.Log?.Write(LogEventLevel.Information, @$"User selected: `{Domain}\{Name}`.");
                        }

                        break;
                    }
            }
        }

        #endregion

        #region Security identifiers

        private static string _sid = GetActiveSessionSID();
        /// <summary>
        /// Current selected user security identifier for WinPaletter, to be operated on it (read\write registry)
        /// </summary>
        public static string SID
        {
            get => _sid;

            set
            {
                // Check if user is changed or not
                bool changed = false;

                // Raise user change event before switching user
                if (UserSwitch != null && _sid != value)
                {
                    changed = true;
                    UserSwitch(new UserChangeEventArgs() { SID = _sid, Timing = UserChangeEventArgs.Timings.BeforeChange });
                }

                // Update user SID
                _sid = value;

                // Reset user password after switching user
                Password = string.Empty;

                // Raise user change event after switching user
                if (changed && UserSwitch != null)
                {
                    // Check if user is system profile or not
                    // THIS IS A SPECIAL CASE FOR SYSTEM PROFILES (NT AUTHORITY)
                    // IT IS HIGHLY RISKY AND NOT RECOMMENDED TO LOGIN INTO SYSTEM PROFILES
                    if (value == "S-1-5-18" || value == "S-1-5-19" || value == "S-1-5-20")
                    {
                        // Try to login into system profile without password
                        try { UpdateToken(Domain, Name, null, false); } catch (Exception ex) { Forms.BugReport.ThrowError(ex); }

                        // Raise user change event after switching user
                        UserSwitch(new UserChangeEventArgs() { SID = value, Timing = UserChangeEventArgs.Timings.AfterChange });
                    }

                    // Check if selected user is the same as the user who opened WinPaletter after granting UAC dialog
                    else if (_sid != AdminSID_GrantedUAC)
                    {
                        if (UpdateToken(Domain, Name, Password, true) && Password != null)
                        {
                            // Raise user change event after switching user
                            UserSwitch(new UserChangeEventArgs() { SID = value, Timing = UserChangeEventArgs.Timings.AfterChange });
                        }
                        else
                        {
                            // Ignore password and continue using current user that opened WinPaletter
                            Token = IntPtr.Zero;
                            Identity = WindowsIdentity.GetCurrent();

                            // Raise user change event after switching user
                            UserSwitch(new UserChangeEventArgs() { SID = AdminSID_GrantedUAC, Timing = UserChangeEventArgs.Timings.AfterChange });
                        }
                    }

                    // The user couldn't be processed. Continue using current user that opened WinPaletter
                    else
                    {
                        // Ignore password and continue using current user that opened WinPaletter
                        Token = IntPtr.Zero;
                        Identity = WindowsIdentity.GetCurrent();

                        // Raise user change event after switching user
                        UserSwitch(new UserChangeEventArgs() { SID = AdminSID_GrantedUAC, Timing = UserChangeEventArgs.Timings.AfterChange });
                    }
                }
            }
        }

        /// <summary>
        /// Update user token provided by a password
        /// </summary>
        /// <param name="Domain"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="ignoreError"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="NoTokenException"></exception>
        /// <exception cref="LogonFailureException"></exception>
        /// <exception cref="AccountRestrictionException"></exception>
        /// <exception cref="InvalidLogonHoursException"></exception>
        /// <exception cref="PasswordExpiredException"></exception>
        /// <exception cref="AccountDisabledException"></exception>
        /// <exception cref="LogonTypeNotGrantedException"></exception>
        /// <exception cref="AccountLockedOutException"></exception>
        /// <exception cref="NoSuchUserException"></exception>
        /// <exception cref="NoLogonServersException"></exception>
        /// <exception cref="PasswordMustChangeException"></exception>
        /// <exception cref="Exception"></exception>
        public static bool UpdateToken(string Domain, string UserName, string Password, bool ignoreError = false)
        {
            // Close previous token if it is opened
            if (Token != IntPtr.Zero) { Kernel32.CloseHandle(Token); }

            // Reset token and identity
            IntPtr token = IntPtr.Zero;
            int error;

            // Check if user is system profile or not
            bool SystemProfile = Domain.ToUpper() == "NT AUTHORITY";

            // Disable LimitBlankPasswordUse to allow login without password
            bool OldValue = ReadReg("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Lsa", "LimitBlankPasswordUse", true);
            WriteReg("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Lsa", "LimitBlankPasswordUse", false);

            bool result;
            if (!SystemProfile)
            {
                // Logon user with password if it is not a system profile
                result = ADVAPI.LogonUser(UserName, Domain, Password, ADVAPI.LOGON32_LOGON_INTERACTIVE, ADVAPI.LOGON32_PROVIDER_DEFAULT, ref token);
                error = Marshal.GetLastWin32Error();
            }
            else
            {
                // Logon user without password if it is a system profile
                result = ADVAPI.LogonUser(UserName, Domain, string.Empty, ADVAPI.LOGON32_LOGON_SERVICE, ADVAPI.LOGON32_PROVIDER_DEFAULT, ref token);
                error = Marshal.GetLastWin32Error();
            }

            if (result)
            {
                // Logon user is successful. Update token and identity
                Token = token;
                Identity = new(Token);

                // Restore LimitBlankPasswordUse to its original value
                WriteReg("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Lsa", "LimitBlankPasswordUse", OldValue);
                return true;
            }
            else
            {
                // Couldn't logon user. Reset token and identity
                Token = IntPtr.Zero;
                Identity = WindowsIdentity.GetCurrent();

                // Restore LimitBlankPasswordUse to its original value
                WriteReg("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Lsa", "LimitBlankPasswordUse", OldValue);

                // Check if error should be ignored or not
                if (!ignoreError)
                {
                    // Raise exception based on error code

                    if (error == 5)
                    {
                        // Access denied. User is not allowed to logon
                        throw new UnauthorizedAccessException($"ERROR_LOGON_ACCESS_DENIED ({error}): {Program.Lang.Strings.Users.ERROR_LOGON_ACCESS_DENIED}");
                    }
                    if (error == 87)
                    {
                        // Invalid parameter. User is not allowed to logon
                        throw new UnauthorizedAccessException($"ERROR_INVALID_PARAMETER ({error}): {Program.Lang.Strings.Users.ERROR_INVALID_PARAMETER}");
                    }
                    else if (error == 1008)
                    {
                        // No token. User is not allowed to logon
                        throw new NoTokenException($"ERROR_NO_TOKEN ({error}): {Program.Lang.Strings.Users.ERROR_NO_TOKEN}");
                    }
                    else if (error == 1326)
                    {
                        // Logon failure. User is not allowed to logon
                        throw new LogonFailureException($"ERROR_LOGON_FAILURE ({error}): {Program.Lang.Strings.Users.ERROR_LOGON_FAILURE}");
                    }
                    else if (error == 1327)
                    {
                        // Account restriction. User is not allowed to logon
                        throw new AccountRestrictionException($"ERROR_ACCOUNT_RESTRICTION ({error}): {Program.Lang.Strings.Users.ERROR_ACCOUNT_RESTRICTION}");
                    }
                    else if (error == 1328)
                    {
                        // Invalid logon hours. User is not allowed to logon
                        throw new InvalidLogonHoursException($"ERROR_INVALID_LOGON_HOURS ({error}): {Program.Lang.Strings.Users.ERROR_INVALID_LOGON_HOURS}");
                    }
                    else if (error == 1330)
                    {
                        // Password expired. User is not allowed to logon
                        throw new PasswordExpiredException($"ERROR_PASSWORD_EXPIRED ({error}): {Program.Lang.Strings.Users.ERROR_PASSWORD_EXPIRED}");
                    }
                    else if (error == 1331)
                    {
                        // Account disabled. User is not allowed to logon
                        throw new AccountDisabledException($"ERROR_ACCOUNT_DISABLED ({error}): {Program.Lang.Strings.Users.ERROR_ACCOUNT_DISABLED}");
                    }
                    else if (error == 1385)
                    {
                        // Logon type not granted. User is not allowed to logon
                        throw new LogonTypeNotGrantedException($"ERROR_LOGON_TYPE_NOT_GRANTED ({error}): {Program.Lang.Strings.Users.ERROR_LOGON_TYPE_NOT_GRANTED}");
                    }
                    else if (error == 1909)
                    {
                        // Account locked out. User is not allowed to logon
                        throw new AccountLockedOutException($"ERROR_ACCOUNT_LOCKED_OUT ({error}): {Program.Lang.Strings.Users.ERROR_ACCOUNT_LOCKED_OUT}");
                    }
                    else if (error == 1317)
                    {
                        // No such user. User is not allowed to logon
                        throw new NoSuchUserException($"ERROR_NO_SUCH_USER ({error}): {Program.Lang.Strings.Users.ERROR_NO_SUCH_USER}");
                    }
                    else if (error == 1311)
                    {
                        // No logon servers. User is not allowed to logon
                        throw new NoLogonServersException($"ERROR_NO_LOGON_SERVERS ({error}): {Program.Lang.Strings.Users.ERROR_NO_LOGON_SERVERS}");
                    }
                    else if (error == 1907)
                    {
                        // Password must change. User is not allowed to logon
                        throw new PasswordMustChangeException($"ERROR_PASSWORD_MUST_CHANGE ({error}): {Program.Lang.Strings.Users.ERROR_PASSWORD_MUST_CHANGE}");
                    }
                    else if (error == 0)
                    {
                        // Unknown error. User is not allowed to logon
                        throw new LogonFailureException($"ERROR_LOGON_FAILURE ({error}): {Program.Lang.Strings.Users.ERROR_LOGON_FAILURE}");
                    }
                    else if (token == IntPtr.Zero)
                    {
                        // No token. User is not allowed to logon
                        throw new Exception($"ERROR ({error}): {Program.Lang.Strings.Users.ERROR_UNKNOWN}");
                    }
                    else
                    {
                        // Unknown error. User is not allowed to logon
                        throw new Exception($"ERROR ({error}): {Program.Lang.Strings.Users.ERROR_UNKNOWN}");
                    }
                }

                // Couldn't logon user. Return false
                return false;
            }
        }

        /// <summary>
        /// Administrator user security identifier that opened WinPaletter after granting UAC dialog
        /// </summary>
        public static readonly string AdminSID_GrantedUAC = WindowsIdentity.GetCurrent().User.Value;

        /// <summary>
        /// User's SID who opened WinPaletter before granting UAC dialog
        /// </summary>
        public static readonly string UserSID_OpenedWP = GetActiveSessionSID();

        #endregion

        #region Properties
        /// <summary>
        /// Name of current user
        /// </summary>
        public static string Name => new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\\').Last();

        /// <summary>
        /// Name of computer hosting current user
        /// </summary>
        public static string Domain => new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\\').First();

        /// <summary>
        /// Return if current user is administrator or not
        /// </summary>
        public static bool Administrator => IsAdmin(SID);

        /// <summary>
        /// Path of current user profile picture
        /// </summary>
        public static string ProfilePicturePath => Shell32.GetUserTilePath(Name);

        /// <summary>
        /// Path of current user profile picture
        /// </summary>
        public static Bitmap ProfilePicture => Shell32.GetUserAccountPicture(Name) as Bitmap;

        /// <summary>
        /// Handle to current user profile
        /// </summary>
        public static IntPtr Token = IntPtr.Zero;

        /// <summary>
        /// Password of current user profile
        /// </summary>
        public static string Password = string.Empty;

        /// <summary>
        /// Current user Windows identity, used to impersonate user profile to do codes and operations on this user.
        /// </summary>
        public static WindowsIdentity Identity = WindowsIdentity.GetCurrent();

        /// <summary>
        /// Administrator user Windows identity, used to impersonate administrator to do codes and operations require elevation.
        /// </summary>
        public static WindowsIdentity Identity_Admin = WindowsIdentity.GetCurrent();

        /// <summary>
        /// Get path of current user profile
        /// <br>- For example: C:\Users\...</br>
        /// </summary>
        public static string UserProfilePath => GetUserProfilePath(SID);
        #endregion

        #region Methods

        /// <summary>
        /// Get dictionary of SID, USERNAME\DOMAIN of all users
        /// </summary>
        /// <param name="includeSystemProfiles"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetUsers(bool includeSystemProfiles = false)
        {
            // Build up a list of SIDs and their corresponding USERNAME\DOMAIN
            Dictionary<string, string> result = [];

            // List of found SIDs
            List<string> FoundSIDs = [];

            // Get all user profiles from using WMI or registry if WMI has failed
            if (!OS.WXP)
            {
                try
                {
                    // Use WMI method
                    SelectQuery query = new("Win32_UserProfile");
                    ManagementObjectSearcher searcher = new(query);
                    ManagementObjectCollection managementObjects = searcher.Get();

                    // Add all found SIDs to the list
                    foreach (ManagementObject SID in managementObjects.Cast<ManagementObject>()) { FoundSIDs.Add(SID["SID"].ToString()); }
                }
                catch
                {
                    // WMI method has failed. Use registry method

                    // Clear list of found SIDs before adding new SIDs to clear any previous SIDs
                    FoundSIDs?.Clear();

                    // Get all user profiles from registry
                    foreach (string SID in GetSubKeys("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList")) { FoundSIDs.Add(SID); }
                }
            }
            else
            {
                // Use registry method as WMI is not available in Windows XP
                foreach (string SID in GetSubKeys("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList")) { FoundSIDs.Add(SID); }
            }

            // Process all found SIDs
            foreach (string sid in FoundSIDs)
            {
                try
                {
                    // Get username from SID
                    string username = new SecurityIdentifier(sid).Translate(typeof(NTAccount)).ToString();

                    // Check if user is system profile or not
                    bool condition_base = includeSystemProfiles |
                        !(username.ToUpper().StartsWith("NT AUTHORITY", StringComparison.OrdinalIgnoreCase) ||
                          username.ToUpper().StartsWith("NT SERVICE", StringComparison.OrdinalIgnoreCase) ||
                          username.ToUpper().Split('\\')[1].StartsWith("WSIACCOUNT", StringComparison.OrdinalIgnoreCase));

                    // Check if user profile is loaded or not
                    bool condition_DAT_Loaded = includeSystemProfiles || (condition_base && RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32).GetSubKeyNames().Contains(sid));

                    // Check if user profile is unloaded or not
                    bool condition_DAT_Unloaded = !condition_DAT_Loaded && condition_base && File.Exists($"{User.GetUserProfilePath(sid)}\\NTUSER.DAT");

                    // Load user profile if it is unloaded into registry key HKEY_USERS to make WinPaletter able to read and write on it
                    if (condition_DAT_Unloaded)
                    {
                        Program.SendCommand($"reg load HKU\\{sid} \"{User.GetUserProfilePath(sid)}\\NTUSER.DAT\"");

                        // Recheck if user profile is loaded or not
                        condition_DAT_Loaded = includeSystemProfiles || (condition_base && RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32).GetSubKeyNames().Contains(sid));
                        condition_DAT_Unloaded = !condition_DAT_Loaded && condition_base && File.Exists($"{User.GetUserProfilePath(sid)}\\NTUSER.DAT");
                    }

                    // Add user to the list if it is loaded or unloaded
                    if (condition_DAT_Loaded || condition_DAT_Unloaded)
                    {
                        result.Add(sid, username);
                    }

                }
                catch { } // Don't list a user that couldn't be got from SID
            }

            return result;
        }

        /// <summary>
        /// Change current active user for WinPaletter if there are multiple Windows profiles
        /// <br>- If there is only one user, it will be used without showing the user switch form</br>
        /// <br>- If there are multiple users, the user switch form will be shown to pick a user</br>
        /// <br>- If there are multiple users and the user switch form is forced to show, it will be shown to pick a user</br>
        /// <param name="ForceShow">Force showing user switch form even if there is only one user</param>
        /// <param name="SkipToCurrentUser">Skip showing user switch form and use current user</param>
        /// </summary>
        public static void Login(bool ForceShow = false, bool SkipToCurrentUser = false)
        {
            // Get list of all users
            Dictionary<string, string> UsersList = GetUsers();

            // Save settings into current user before reloading settings for new user
            if (!SkipToCurrentUser) Program.Settings.Save(Settings.Source.Registry);

            // If skip to current user is enabled, use current user without showing user switch form
            if (SkipToCurrentUser) { User.SID = GetActiveSessionSID(); }

            // If there are more than one user or force show is enabled, show user switch form to pick a user
            else if (ForceShow || UsersList.Count > 1) { Forms.UserSwitch.PickUser(UsersList); }

            // If one user if found, there is no need to login, use current windows identity
            else { User.SID = WindowsIdentity.GetCurrent().User.Value; }
        }

        /// <summary>
        /// Login into user profile using SID
        /// </summary>
        /// <param name="_sid"></param>
        /// <returns></returns>
        public static bool SID_Credentials_Result(string _sid)
        {
            bool result = false;
            string userName = new SecurityIdentifier(_sid).Translate(typeof(NTAccount)).ToString().Split('\\').Last();
            string doamin = new SecurityIdentifier(_sid).Translate(typeof(NTAccount)).ToString().Split('\\').First();

            (string domain, string username, string password, bool isPasswordCorrect, bool canceled) credsResult = (doamin, userName, null, false, false);

            // When password is incorrect, show the password dialog until the user enters the correct password or cancel the dialog

            bool userEnteredWrongPassword = false;

            while (!credsResult.isPasswordCorrect)
            {
                if (credsResult.canceled)
                {
                    // User canceled the password dialog
                    result = false;
                    break;
                }

                // Make it the last line in loop to make it gets credentials correctly
                credsResult = Credui.Login(Forms.UserSwitch.Handle,
                    !userEnteredWrongPassword ? string.Format(Program.Lang.Strings.Users.EnterPassword_Caption, userName) : Program.Lang.Strings.Users.IncorrectPassword,
                    !userEnteredWrongPassword ? Program.Lang.Strings.Users.WindowsHello_NotSupported : string.Format(Program.Lang.Strings.Users.EnterPassword_Caption, userName) + "\r\n\r\n" + Program.Lang.Strings.Users.WindowsHello_NotSupported,
                    doamin, userName);

                // The bool is repeated to avoid printing the alert twice
                userEnteredWrongPassword = !credsResult.isPasswordCorrect;
            }

            if (credsResult.isPasswordCorrect)
            {
                result = true;
                Password = credsResult.password;
            }

            return result;
        }

        /// <summary>
        /// Get active user SID who opened WinPaletter before granting UAC dialog
        /// <br>- After granting UAC dialog, the current user becomes the administrator one. This may cause a conflict between multiple users and so this method is created.</br>
        /// </summary>
        /// <returns></returns>
        private static string GetActiveSessionSID()
        {
            string result = User.AdminSID_GrantedUAC;

            // Attempt using WMI if possible (fallback)
            if (IsWMIAvailable())
            {
                ManagementObjectSearcher searcher = new("SELECT * FROM Win32_ComputerSystem");
                foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                {
                    string currentUser = queryObj["UserName"]?.ToString();
                    if (!string.IsNullOrEmpty(currentUser))
                    {
                        // Extract the username from domain\username format
                        string username = currentUser.Split('\\').Last();
                        result = GetUsers().FirstOrDefault(x => x.Value.Split('\\').Last().ToLower() == username.ToLower()).Key;
                        break;
                    }
                }
            }

            // Fall back to additional registry checks if WMI fails
            if (string.IsNullOrEmpty(result))
            {
                if (!OS.WXP && !OS.WVista && !OS.W7)
                {
                    result = ReadReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Authentication\\LogonUI", "LastLoggedOnUserSID", result);
                }
                else
                {
                    string username = ReadReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Authentication\\LogonUI", "LastLoggedOnUser", string.Empty);
                    username = username.Split('\\').Last();
                    result = GetUsers().FirstOrDefault(x => x.Value.Split('\\').Last().ToLower() == username.ToLower()).Key;
                }
            }

            // If no result found, use the default admin SID
            return string.IsNullOrEmpty(result) ? User.AdminSID_GrantedUAC : result;
        }

        private static bool IsWMIAvailable()
        {
            try
            {
                ManagementObjectSearcher searcher = new("SELECT * FROM Win32_ComputerSystem");
                return searcher.Get() != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get path of user profile
        /// <br>- For example: C:\Users\...</br>
        /// </summary>
        public static string GetUserProfilePath(string SID = null)
        {
            try
            {
                string result = ReadReg($"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList\\{SID}", "ProfileImagePath", string.Empty);

                if (!string.IsNullOrWhiteSpace(result))
                {
                    return Environment.ExpandEnvironmentVariables(result);
                }
                else
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                }
            }
            catch // Couldn't get user profile path, return generic path
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            }
        }

        /// <summary>
        /// Update WinPaletter paths variables from user SID
        /// </summary>
        public static void UpdatePathsFromSID(string SID = null)
        {
            // Update paths variables from user SID
            if (SID != null)
            {
                // Get user profile path from SID
                SysPaths.UserProfile = $"{GetUserProfilePath(SID)}";

                // Set application data path based on Windows version
                if (!OS.WXP)
                {
                    SysPaths.LocalAppData = $"{SysPaths.UserProfile}\\AppData\\Local";
                    SysPaths.appData = $"{SysPaths.LocalAppData}\\{Application.CompanyName}\\{Application.ProductName}";
                }
                else
                {
                    SysPaths.LocalAppData = $"{SysPaths.UserProfile}\\Local Settings\\Application Data";
                    SysPaths.appData = $"{SysPaths.LocalAppData}\\{Application.CompanyName}\\{Application.ProductName}";
                }

                // Impersonate an administrator user to set full control to the user profile directory
                using (WindowsImpersonationContext wic = Identity_Admin.Impersonate())
                {
                    // Create a DirectoryInfo object
                    DirectoryInfo directoryInfo = new(SysPaths.ProgramFilesData);

                    // Get the existing access control settings
                    DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

                    // Define the security rules (example: allow full control to administrators)
                    SecurityIdentifier allUsers = new(WellKnownSidType.BuiltinUsersSid, null);
                    FileSystemAccessRule rule = new(allUsers, FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);

                    // Add the rule to the security settings
                    directorySecurity.AddAccessRule(rule);

                    // Apply the new security settings to the directory
                    directoryInfo.SetAccessControl(directorySecurity);

                    // Undo impersonation after finishing operations on user profile
                    wic.Undo();
                }
            }
            else
            {
                // Get user profile path from current user as SID is not provided
                SysPaths.UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                SysPaths.LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                SysPaths.appData = Directory.GetParent(Application.LocalUserAppDataPath).FullName;
            }

            // Create directories if they are not exist
            if (!Directory.Exists(SysPaths.UserProfile)) { Directory.CreateDirectory(SysPaths.UserProfile); }
            if (!Directory.Exists(SysPaths.LocalAppData)) { Directory.CreateDirectory(SysPaths.LocalAppData); }
            if (!Directory.Exists(SysPaths.appData)) { Directory.CreateDirectory(SysPaths.appData); }
        }

        /// <summary>
        /// Determines whether an User is an administrator, in the current machine.
        /// </summary>
        /// <returns><c>true</c> if user is an administrator, <c>false</c> otherwise.</returns>
        public static bool IsAdmin(string SID)
        {
            try
            {
                // Get user groups of Administrators
                SecurityIdentifier AdminGroupSID = new("S-1-5-32-544");
                PrincipalContext pContext = new(ContextType.Machine);
                UserPrincipal pUser = new(pContext);
                PrincipalSearcher pSearcher = new(pUser);
                Principal User = (from u in pSearcher.FindAll() where u.Sid.ToString().Equals(SID, StringComparison.OrdinalIgnoreCase) select u).FirstOrDefault();

                // Check if user is system profile or not. System profiles are always administrators
                if (User is null)
                {
                    return SID.ToUpper() is "S-1-5-18" or "S-1-5-19" or "S-1-5-20";
                }

                // Check if user is an administrator or not
                bool IsAdmin = (from Group in User.GetGroups() where Group.Sid == AdminGroupSID select Group).Any();

                // Dispose objects
                pContext?.Dispose();
                pSearcher?.Dispose();
                pUser?.Dispose();

                return IsAdmin;
            }
            catch { return false; } // Couldn't get user groups, return false and we will assume that the user is not administrator
        }

        #endregion

        #region Exceptions

        /// <summary>
        /// Exceptions for user login failure
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure exception
        /// </remarks>
        /// <param name="message"></param>
        public class LogonFailureException(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure due to invalid parameters
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to invalid parameters exception
        /// </remarks>
        /// <param name="message"></param>
        public class LogonInvalidParameters(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure due to no token exists
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to no token exists exception
        /// </remarks>
        /// <param name="message"></param>
        public class NoTokenException(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure due to logon type is not granted
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to logon type is not granted exception
        /// </remarks>
        /// <param name="message"></param>
        public class LogonTypeNotGrantedException(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure due to account restriction
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to account restriction exception
        /// </remarks>
        /// <param name="message"></param>
        public class AccountRestrictionException(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure due to invalid logon hours
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to invalid logon hours exception
        /// </remarks>
        /// <param name="message"></param>
        public class InvalidLogonHoursException(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure due to password expiry
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to password expiry exception
        /// </remarks>
        /// <param name="message"></param>
        public class PasswordExpiredException(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure as account is disabled
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to account disabled exception
        /// </remarks>
        /// <param name="message"></param>
        public class AccountDisabledException(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure as account is locked out
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to account locked out exception
        /// </remarks>
        /// <param name="message"></param>
        public class AccountLockedOutException(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure as there is no such user
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to no such user exception
        /// </remarks>
        /// <param name="message"></param>
        public class NoSuchUserException(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure as there are no logon servers
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to no logon servers exception
        /// </remarks>
        /// <param name="message"></param>
        public class NoLogonServersException(string message) : Exception(message) { }

        /// <summary>
        /// Exceptions for user login failure as password must be change
        /// </summary>
        /// <remarks>
        /// Constructor for user login failure due to password must change exception
        /// </remarks>
        /// <param name="message"></param>
        public class PasswordMustChangeException(string message) : Exception(message) { }

        #endregion
    }
}
