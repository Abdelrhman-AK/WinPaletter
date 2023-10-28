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
    public static class Users
    {
        /// <summary>
        /// Current selected user SID for WinPaletter, to be operated on it (read\write registry)
        /// </summary>
        public static string UserSID = WindowsIdentity.GetCurrent().User.Value;

        /// <summary>
        /// Administrator user SID that opened WinPaletter after granting UAC dialog
        /// </summary>
        public readonly static string AdminSID_GrantedUAC = WindowsIdentity.GetCurrent().User.Value;

        /// <summary>
        /// Domain\Username format (For example: DESKTOP0985\User)
        /// </summary>
        public static string Domain_UserName = "";

        /// <summary>
        /// Change current active user for WinPaletter if there are multiple Windows profiles
        /// </summary>
        public static void Login(bool ForceShow = false)
        {
            Dictionary<string, string> UsersList = new();
            SelectQuery query = new("Win32_UserProfile");
            ManagementObjectSearcher searcher = new(query);
            Program.LoadedNTUSER_DAT.Clear();

            foreach (ManagementObject sid in searcher.Get().Cast<ManagementObject>())
            {
                try
                {
                    string username = new SecurityIdentifier(sid["SID"].ToString()).Translate(typeof(NTAccount)).ToString();

                    bool condition_base = !username.ToUpper().StartsWith("NT AUTHORITY", StringComparison.OrdinalIgnoreCase);
                    bool condition_DAT_Loaded = condition_base && RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32).GetSubKeyNames().Contains(sid["SID"].ToString());
                    bool condition_DAT_Unloaded = !condition_DAT_Loaded && condition_base && System.IO.File.Exists(Users.GetUserProfilePath(sid["SID"].ToString()) + "\\NTUSER.DAT");

                    if (condition_DAT_Unloaded)
                    {
                        Program.SendCommand($"reg load HKU\\{sid["SID"]} \"{Users.GetUserProfilePath(sid["SID"].ToString())}\\NTUSER.DAT\"");
                        Program.LoadedNTUSER_DAT.Add(sid["SID"].ToString());
                    }

                    if (condition_DAT_Loaded || condition_DAT_Unloaded)
                    {
                        UsersList.Add(sid["SID"].ToString(), username.Split('\"').Last());
                    }
                }
                catch { }
            }

            // Save settings into current user before reloading settings for new user
            Program.Settings.Save(WPSettings.Mode.Registry);

            bool result;

            // Show login dialog by force (for settings)
            if (ForceShow) { result = Forms.UserSelect.PickUser(UsersList) == DialogResult.OK; }

            // Found more than 1 user, show login dialog
            else if (UsersList.Count > 1)
            {
                // If not remember last user, show login dialog
                if (!Program.Settings.UsersServices.RemeberLastUser) { result = Forms.UserSelect.PickUser(UsersList) == DialogResult.OK; }

                // If remember last user, don't show login dialog and load SID from saved SID
                else if (UsersList.Keys.Contains(Program.Settings.UsersServices.LastUserSID))
                {
                    Users.UserSID = Program.Settings.UsersServices.LastUserSID;
                    result = true;
                }

                // If remember last user, show login dialog if SID is not saved in registry
                else { result = Forms.UserSelect.PickUser(UsersList) == DialogResult.OK; }
            }

            // If one user if found, there is no need to login, use current windows identity
            else if (UsersList.Count == 1 || UsersList.Count == 0)
            {
                Users.UserSID = WindowsIdentity.GetCurrent().User.Value;
                result = false;
            }

            else { result = false; }

            // Reload settings for current selected user
            if (result == true) { if (Users.UserSID != Users.AdminSID_GrantedUAC) { Program.Settings = new(WPSettings.Mode.Registry); } }
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

                var profilePath = key.GetValue("ProfileImagePath") as string;

                return profilePath;
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
                throw new Exception(string.Format("User with SID '{0}' not found.", SID));
            }

            bool IsAdmin = (from Group in User.GetGroups() where Group.Sid == AdminGroupSID select Group).Any();

            pContext.Dispose();
            pSearcher.Dispose();
            pUser.Dispose();

            return IsAdmin;
        }

        /// <summary>
        /// Determines if selected user is Administrator or not
        /// </summary>
        /// <returns><c>true</c> if user is an Administrator, <c>false</c> otherwise.</returns>
        public static bool IsAdmin() { return IsAdmin(UserSID); }

    }
}
