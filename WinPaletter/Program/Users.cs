using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Class for handling multiple users for WinPaletter
    /// </summary>
    public class Users
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
            public string UserName { get { return new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\"').Last(); ; } }

            /// <summary>
            /// Name of computer hosting users
            /// </summary>
            public string ComputerName { get { return new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\"').First(); ; } }

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

        public static event UserChangeEventHandler UserChange;
        #endregion

        #region Security identifiers

        private static string _SID = WindowsIdentity.GetCurrent().User.Value;
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
                if (UserChange != null && _SID != value)
                {
                    changed = true;
                    UserChange(new UserChangeEventArgs() { SID = _SID, Timing = UserChangeEventArgs.Timings.BeforeChange });
                }
                _SID = value;
                if (changed && UserChange != null)
                {
                    UserChange(new UserChangeEventArgs() { SID = value, Timing = UserChangeEventArgs.Timings.AfterChange });
                }
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
        public string UserName { get { return new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\"').Last(); ; } }

        /// <summary>
        /// Name of computer hosting current user
        /// </summary>
        public string ComputerName { get { return new SecurityIdentifier(SID).Translate(typeof(NTAccount)).ToString().Split('\"').First(); ; } }

        /// <summary>
        /// Return if current user is Administrator or not
        /// </summary>
        public bool Administrator { get { return IsAdmin(SID); } }

        /// <summary>
        /// Path of current user profile picture
        /// </summary>
        public string ProfilePicturePath { get { return NativeMethods.Shell32.GetUserTilePath(UserName); } }

        /// <summary>
        /// Get path of current user profile
        /// <br>- For example: C:\Users\...</br>
        /// </summary>
        public string UserProfilePath { get { return GetUserProfilePath(SID); } }
        #endregion

        #region Voids
        public static Dictionary<string, string> GetUsers(bool includeSystemProfiles = false)
        {
            Dictionary<string, string> result = new();
            SelectQuery query = new("Win32_UserProfile");
            ManagementObjectSearcher searcher = new(query);

            foreach (ManagementObject sid in searcher.Get().Cast<ManagementObject>())
            {
                try
                {
                    string username = new SecurityIdentifier(sid["SID"].ToString()).Translate(typeof(NTAccount)).ToString();
                    bool condition_base = includeSystemProfiles | !username.ToUpper().StartsWith("NT AUTHORITY", StringComparison.OrdinalIgnoreCase);
                    bool condition_DAT_Loaded = includeSystemProfiles || (condition_base && RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32).GetSubKeyNames().Contains(sid["SID"].ToString()));
                    bool condition_DAT_Unloaded = !condition_DAT_Loaded && condition_base && System.IO.File.Exists(Users.GetUserProfilePath(sid["SID"].ToString()) + "\\NTUSER.DAT");

                    if (condition_DAT_Unloaded)
                    {
                        Program.SendCommand($"reg load HKU\\{sid["SID"]} \"{Users.GetUserProfilePath(sid["SID"].ToString())}\\NTUSER.DAT\"");
                    }

                    if (condition_DAT_Loaded || condition_DAT_Unloaded)
                    {
                        result.Add(sid["SID"].ToString(), username);
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

            if (ForceShow || UsersList.Count > 1) { Forms.UserSelect.PickUser(UsersList); }

            // If one user if found, there is no need to login, use current windows identity
            else if (UsersList.Count == 1 || UsersList.Count == 0)
            {
                Users.SID = WindowsIdentity.GetCurrent().User.Value;
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
                PathsExt.LocalAppData = $"{PathsExt.UserProfile}\\AppData\\Local";
                PathsExt.appData = $"{PathsExt.LocalAppData}\\{Application.CompanyName}\\{Application.ProductName}";
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
    }
}
