using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Class for handling multiple users for WinPaletter
    /// </summary>
    public class User
    {
        #region Events
        /// <summary>
        /// EventArgs that have data of user change event
        /// </summary>
        public class UserChangeEventArgs : EventArgs
        {
            /// <summary>
            /// User SID that invoked the event
            /// </summary>
            public string SID { get; set; }

            /// <summary>
            /// Know if event raised before or after switching user
            /// </summary>
            public Timings Timing { get; set; }

            /// <summary>
            /// Date and time of raising event
            /// </summary>
            public DateTime SwitchTime { get { return DateTime.Now; } }

            /// <summary>
            /// Name of user who invoked the event
            /// </summary>
            public string UserName { get { return new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\\').Last(); ; } }

            /// <summary>
            /// Name of computer hosting users
            /// </summary>
            public string ComputerName { get { return new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\\').First(); ; } }

            /// <summary>
            /// Return if user that invoked the event is Administrator or not
            /// </summary>
            public bool Administrator { get { return IsAdmin(SID); } }

            /// <summary>
            /// Path of user profile picture that invoked the event
            /// </summary>
            public string ProfilePicturePath { get { return NativeMethods.Shell32.GetUserTilePath(UserName); } }

            /// <summary>
            /// Get path of user profile
            /// <br>- For example: C:\Users\...</br>
            /// </summary>
            public string UserProfilePath { get { return GetUserProfilePath(SID); } }

            /// <summary>
            /// Enumeration for user switch timing (event raised before or after switching user)
            /// </summary>
            public enum Timings
            {
                BeforeChange,
                AfterChange
            }
        }


        public delegate void UserChangeEventHandler(UserChangeEventArgs e);


        public static event UserChangeEventHandler UserSwitch;

        public static void OnUserSwitch(User.UserChangeEventArgs e)
        {
            switch (e.Timing)
            {
                case User.UserChangeEventArgs.Timings.BeforeChange:
                    {
                        Program.Settings.Save(WPSettings.Mode.Registry);
                        break;
                    }

                case User.UserChangeEventArgs.Timings.AfterChange:
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        using (WindowsImpersonationContext impersonationContext = User.Identity.Impersonate())
                        {
                            bool MainFormIsOpened = Application.OpenForms[Forms.MainFrm.Name] is not null;

                            List<Form> OpenForms = new();
                            foreach (Form f in Application.OpenForms)
                            {
                                if (f != Forms.BK && f != Forms.UserSwitch && f.Visible)
                                {
                                    OpenForms.Add(f);
                                    f.Visible = false;
                                }
                            }

                            User.UpdatePathsFromSID(User.SID);
                            Program.Settings = new(WPSettings.Mode.Registry);

                            if (MainFormIsOpened)
                            {
                                if (Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation && (Program.TM != Program.TM_Original))
                                {
                                    Forms.ComplexSave.GetResponse(Forms.MainFrm.SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));
                                }
                            }

                            Program.InitializeApplication(false);

                            if (MainFormIsOpened) { Forms.MainFrm.LoadData(); }

                            foreach (Form f in OpenForms)
                            {
                                f.Visible = true;
                            }

                            Cursor.Current = Cursors.Default;

                            impersonationContext.Undo();
                        }
                        break;
                    }
            }
        }

        #endregion

        #region Security identifiers

        private static string _SID;
        /// <summary>
        /// Current selected user security identifier for WinPaletter, to be operated on it (read\write registry)
        /// </summary>
        public static string SID
        {
            get
            {
                return _SID;
            }
            set
            {
                bool changed = false;

                if (UserSwitch != null && _SID != value)
                {
                    changed = true;
                    UserSwitch(new UserChangeEventArgs() { SID = _SID, Timing = UserChangeEventArgs.Timings.BeforeChange });
                }

                _SID = value;
                Password = "";

                if (changed && UserSwitch != null)
                {
                    if (value == "S-1-5-18" || value == "S-1-5-19" || value == "S-1-5-20")
                    {
                        try { UpdateToken(null, false); } catch (Exception ex) { Forms.BugReport.ThrowError(ex); }

                        UserSwitch(new UserChangeEventArgs() { SID = value, Timing = UserChangeEventArgs.Timings.AfterChange });
                    }

                    else if (_SID != AdminSID_GrantedUAC)
                    {
                        // Try to login into user without password
                        bool LoginSuccess0 = UpdateToken(Password, true);
                        bool LoginSuccess1 = false;
                        Tuple<DialogResult, string> result = new(DialogResult.None, null);
                        DialogResult Response = DialogResult.None;

                        if (!LoginSuccess0)
                        {
                            // User has password. Entering it is required.
                            result = Forms.UserPassword.GetPassword(value);
                            Response = result.Item1;
                            Password = result.Item2;
                            LoginSuccess1 = Response == DialogResult.OK && Password != null;
                        }

                        if (LoginSuccess0 || LoginSuccess1)
                        {
                            UserSwitch(new UserChangeEventArgs() { SID = value, Timing = UserChangeEventArgs.Timings.AfterChange });
                        }
                        else
                        {
                            Token = IntPtr.Zero;
                            Identity = WindowsIdentity.GetCurrent();

                            if (Response == DialogResult.Cancel)
                            {
                                // Ignore password and continue using current user that opened WinPaletter
                                _SID = AdminSID_GrantedUAC;
                            }

                            else
                            {
                                // Ignore password and continue using new switched user
                                _SID = value;
                            }

                            UserSwitch(new UserChangeEventArgs() { SID = _SID, Timing = UserChangeEventArgs.Timings.AfterChange });
                        }
                    }

                    else
                    {
                        Token = IntPtr.Zero;
                        Identity = WindowsIdentity.GetCurrent();
                        UserSwitch(new UserChangeEventArgs() { SID = AdminSID_GrantedUAC, Timing = UserChangeEventArgs.Timings.AfterChange });
                    }
                }
            }
        }

        public static bool UpdateToken(string Password, bool ignoreError = false)
        {
            if (Token != IntPtr.Zero) { NativeMethods.Kernel32.CloseHandle(Token); }

            IntPtr token = IntPtr.Zero;
            int error;
            bool SystemProfile = User.ComputerName.ToUpper() == "NT AUTHORITY";

            bool OldValue = Convert.ToBoolean(GetReg("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Lsa", "LimitBlankPasswordUse", true));
            EditReg("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Lsa", "LimitBlankPasswordUse", false);

            // Logon
            bool result;
            if (!SystemProfile)
            {
                result = NativeMethods.advapi.LogonUser(UserName, ComputerName, Password, NativeMethods.advapi.LOGON32_LOGON_INTERACTIVE, NativeMethods.advapi.LOGON32_PROVIDER_DEFAULT, ref token);
                error = Marshal.GetLastWin32Error();
            }
            else
            {
                result = NativeMethods.advapi.LogonUser(UserName, ComputerName, "", NativeMethods.advapi.LOGON32_LOGON_SERVICE, NativeMethods.advapi.LOGON32_PROVIDER_DEFAULT, ref token);
                error = Marshal.GetLastWin32Error();
            }

            if (result)
            {
                Token = token;
                Identity = new WindowsIdentity(Token);
                EditReg("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Lsa", "LimitBlankPasswordUse", OldValue);
                return true;
            }
            else
            {
                Token = IntPtr.Zero;
                Identity = WindowsIdentity.GetCurrent();
                EditReg("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Lsa", "LimitBlankPasswordUse", OldValue);

                if (!ignoreError)
                {
                    if (error == 5)
                    {
                        throw new UnauthorizedAccessException($"ERROR_LOGON_ACCESS_DENIED ({error}): {Program.Lang.UserSwitch_ERROR_LOGON_ACCESS_DENIED}");
                    }
                    if (error == 87)
                    {
                        throw new UnauthorizedAccessException($"ERROR_INVALID_PARAMETER ({error}): {Program.Lang.UserSwitch_ERROR_INVALID_PARAMETER}");
                    }
                    else if (error == 1008)
                    {
                        throw new NoTokenException($"ERROR_NO_TOKEN ({error}): {Program.Lang.UserSwitch_ERROR_NO_TOKEN}");
                    }
                    else if (error == 1326)
                    {
                        throw new LogonFailureException($"ERROR_LOGON_FAILURE ({error}): {Program.Lang.UserSwitch_ERROR_LOGON_FAILURE}");
                    }
                    else if (error == 1327)
                    {
                        throw new AccountRestrictionException($"ERROR_ACCOUNT_RESTRICTION ({error}): {Program.Lang.UserSwitch_ERROR_ACCOUNT_RESTRICTION}");
                    }
                    else if (error == 1328)
                    {
                        throw new InvalidLogonHoursException($"ERROR_INVALID_LOGON_HOURS ({error}): {Program.Lang.UserSwitch_ERROR_INVALID_LOGON_HOURS}");
                    }
                    else if (error == 1330)
                    {
                        throw new PasswordExpiredException($"ERROR_PASSWORD_EXPIRED ({error}): {Program.Lang.UserSwitch_ERROR_PASSWORD_EXPIRED}");
                    }
                    else if (error == 1331)
                    {
                        throw new AccountDisabledException($"ERROR_ACCOUNT_DISABLED ({error}): {Program.Lang.UserSwitch_ERROR_ACCOUNT_DISABLED}");
                    }
                    else if (error == 1385)
                    {
                        throw new LogonTypeNotGrantedException($"ERROR_LOGON_TYPE_NOT_GRANTED ({error}): {Program.Lang.UserSwitch_ERROR_LOGON_TYPE_NOT_GRANTED}");
                    }
                    else if (error == 1909)
                    {
                        throw new AccountLockedOutException($"ERROR_ACCOUNT_LOCKED_OUT ({error}): {Program.Lang.UserSwitch_ERROR_ACCOUNT_LOCKED_OUT}");
                    }
                    else if (error == 1317)
                    {
                        throw new NoSuchUserException($"ERROR_NO_SUCH_USER ({error}): {Program.Lang.UserSwitch_ERROR_NO_SUCH_USER}");
                    }
                    else if (error == 1311)
                    {
                        throw new NoLogonServersException($"ERROR_NO_LOGON_SERVERS ({error}): {Program.Lang.UserSwitch_ERROR_NO_LOGON_SERVERS}");
                    }
                    else if (error == 1907)
                    {
                        throw new PasswordMustChangeException($"ERROR_PASSWORD_MUST_CHANGE ({error}): {Program.Lang.UserSwitch_ERROR_PASSWORD_MUST_CHANGE}");
                    }
                    else if (error == 0)
                    {
                        throw new LogonFailureException($"ERROR_LOGON_FAILURE ({error}): {Program.Lang.UserSwitch_ERROR_LOGON_FAILURE}");
                    }
                    else if (token == IntPtr.Zero)
                    {
                        throw new Exception($"ERROR ({error}): {Program.Lang.UserSwitch_ERROR_UNKNOWN}");
                    }
                    else
                    {
                        throw new Exception($"ERROR ({error}): {Program.Lang.UserSwitch_ERROR_UNKNOWN}");
                    }
                }

                return false;
            }

        }

        /// <summary>
        /// Administrator user security identifier that opened WinPaletter after granting UAC dialog
        /// </summary>
        public readonly static string AdminSID_GrantedUAC = WindowsIdentity.GetCurrent().User.Value;
        #endregion

        #region Properties
        /// <summary>
        /// Name of current user
        /// </summary>
        public static string UserName { get { return new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\\').Last(); ; } }

        /// <summary>
        /// Name of computer hosting current user
        /// </summary>
        public static string ComputerName { get { return new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\\').First(); ; } }

        /// <summary>
        /// Return if current user is Administrator or not
        /// </summary>
        public static bool Administrator { get { return IsAdmin(SID); } }

        /// <summary>
        /// Path of current user profile picture
        /// </summary>
        public static string ProfilePicturePath { get { return NativeMethods.Shell32.GetUserTilePath(UserName); } }

        /// <summary>
        /// Path of current user profile picture
        /// </summary>
        public static Bitmap ProfilePicture
        {
            get
            {
                return (Bitmap)NativeMethods.Shell32.GetUserAccountPicture(UserName);
            }
        }

        /// <summary>
        /// Handle to current user profile
        /// </summary>
        public static IntPtr Token = IntPtr.Zero;

        /// <summary>
        /// Password of current user profile
        /// </summary>
        public static string Password = "";

        /// <summary>
        /// Current user Windows identity, used to impersonate user profile to do codes and operations on this user.
        /// </summary>
        public static WindowsIdentity Identity = WindowsIdentity.GetCurrent();

        /// <summary>
        /// Get path of current user profile
        /// <br>- For example: C:\Users\...</br>
        /// </summary>
        public static string UserProfilePath { get { return GetUserProfilePath(SID); } }
        #endregion

        #region Voids
        /// <summary>
        /// Get dictionary of SID, USERNAME\DOMAIN of all users
        /// </summary>
        /// <param name="includeSystemProfiles"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetUsers(bool includeSystemProfiles = false)
        {
            Dictionary<string, string> result = new();
            List<string> FoundSIDs = new();

            if (!OS.WXP)
            {
                SelectQuery query = new("Win32_UserProfile");
                ManagementObjectSearcher searcher = new(query);
                ManagementObjectCollection managementObjects = searcher.Get();

                foreach (ManagementObject SID in managementObjects.Cast<ManagementObject>()) { FoundSIDs.Add(SID["SID"].ToString()); }
            }
            else
            {
                foreach (string SID in Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList").GetSubKeyNames()) { FoundSIDs.Add(SID); }
            }

            foreach (string sid in FoundSIDs)
            {
                try
                {
                    string username = new SecurityIdentifier(sid).Translate(typeof(NTAccount)).ToString();
                    bool condition_base = includeSystemProfiles | !username.ToUpper().StartsWith("NT AUTHORITY", StringComparison.OrdinalIgnoreCase);
                    bool condition_DAT_Loaded = includeSystemProfiles || (condition_base && RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32).GetSubKeyNames().Contains(sid));
                    bool condition_DAT_Unloaded = !condition_DAT_Loaded && condition_base && System.IO.File.Exists(User.GetUserProfilePath(sid) + "\\NTUSER.DAT");

                    if (condition_DAT_Unloaded)
                    {
                        Program.SendCommand($"reg load HKU\\{sid} \"{User.GetUserProfilePath(sid)}\\NTUSER.DAT\"");
                    }

                    if (condition_DAT_Loaded || condition_DAT_Unloaded)
                    {
                        result.Add(sid, username);
                    }

                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// Change current active user for WinPaletter if there are multiple Windows profiles
        /// </summary>
        public static void Login(bool ForceShow = false)
        {
            Dictionary<string, string> UsersList = GetUsers();

            // Save settings into current user before reloading settings for new user
            Program.Settings.Save(WPSettings.Mode.Registry);

            if (ForceShow || UsersList.Count > 1) { Forms.UserSwitch.PickUser(UsersList); }

            // If one user if found, there is no need to login, use current windows identity
            else if (UsersList.Count == 1 || UsersList.Count == 0)
            {
                User.SID = WindowsIdentity.GetCurrent().User.Value;
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
                var keyPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList\" + SID;

                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath);
                if (key == null)
                {
                    //handle error
                    return null;
                }

                return Environment.ExpandEnvironmentVariables(key.GetValue("ProfileImagePath").ToString());
            }
            catch
            {
                //handle exception
                return null;
            }
        }

        /// <summary>
        /// Update WinPaletter paths variables from user SID
        /// </summary>
        public static void UpdatePathsFromSID(string SID = null)
        {
            if (SID != null)
            {
                PathsExt.UserProfile = $"{GetUserProfilePath(SID)}";

                if (!OS.WXP)
                {
                    PathsExt.LocalAppData = $"{PathsExt.UserProfile}\\AppData\\Local";
                    PathsExt.appData = $"{PathsExt.LocalAppData}\\{Application.CompanyName}\\{Application.ProductName}";
                }
                else
                {
                    PathsExt.LocalAppData = $"{PathsExt.UserProfile}\\Local Settings\\Application Data";
                    PathsExt.appData = $"{PathsExt.LocalAppData}\\{Application.CompanyName}\\{Application.ProductName}";
                }

            }
            else
            {
                PathsExt.UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                PathsExt.LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                PathsExt.appData = System.IO.Directory.GetParent(Application.LocalUserAppDataPath).FullName;
            }

            if (!System.IO.Directory.Exists(PathsExt.UserProfile)) { System.IO.Directory.CreateDirectory(PathsExt.UserProfile); }
            if (!System.IO.Directory.Exists(PathsExt.LocalAppData)) { System.IO.Directory.CreateDirectory(PathsExt.LocalAppData); }
            if (!System.IO.Directory.Exists(PathsExt.appData)) { System.IO.Directory.CreateDirectory(PathsExt.appData); }
        }

        /// <summary>
        /// Determines whether an User is an Administrator, in the current machine.
        /// </summary>
        /// <returns><c>true</c> if user is an Administrator, <c>false</c> otherwise.</returns>
        public static bool IsAdmin(string SID)
        {
            var AdminGroupSID = new SecurityIdentifier("S-1-5-32-544");

            var pContext = new PrincipalContext(ContextType.Machine);
            var pUser = new UserPrincipal(pContext);
            var pSearcher = new PrincipalSearcher(pUser);

            Principal User = (from u in pSearcher.FindAll() where u.Sid.ToString().Equals(SID, StringComparison.OrdinalIgnoreCase) select u).FirstOrDefault();

            if (User is null)
            {
                if (SID.ToUpper() == "S-1-5-18" || SID.ToUpper() == "S-1-5-19" || SID.ToUpper() == "S-1-5-20")
                {
                    return true;
                }
                else
                {
                    throw new Exception(string.Format("User with SID '{0}' not found.", SID));
                }
            }

            bool IsAdmin = (from Group in User.GetGroups() where Group.Sid == AdminGroupSID select Group).Any();

            pContext.Dispose();
            pSearcher.Dispose();
            pUser.Dispose();

            return IsAdmin;
        }
        #endregion

        #region Exceptions

        public class LogonFailureException : Exception
        {
            public LogonFailureException(string message) : base(message) { }
        }

        public class LogonInvalidParameters : Exception
        {
            public LogonInvalidParameters(string message) : base(message) { }
        }

        public class NoTokenException : Exception
        {
            public NoTokenException(string message) : base(message) { }
        }

        public class LogonTypeNotGrantedException : Exception
        {
            public LogonTypeNotGrantedException(string message) : base(message) { }
        }

        public class AccountRestrictionException : Exception
        {
            public AccountRestrictionException(string message) : base(message) { }
        }

        public class InvalidLogonHoursException : Exception
        {
            public InvalidLogonHoursException(string message) : base(message) { }
        }

        public class PasswordExpiredException : Exception
        {
            public PasswordExpiredException(string message) : base(message) { }
        }

        public class AccountDisabledException : Exception
        {
            public AccountDisabledException(string message) : base(message) { }
        }

        public class AccountLockedOutException : Exception
        {
            public AccountLockedOutException(string message) : base(message) { }
        }

        public class NoSuchUserException : Exception
        {
            public NoSuchUserException(string message) : base(message) { }
        }

        public class NoLogonServersException : Exception
        {
            public NoLogonServersException(string message) : base(message) { }
        }

        public class PasswordMustChangeException : Exception
        {
            public PasswordMustChangeException(string message) : base(message) { }
        }

        #endregion
    }
}
