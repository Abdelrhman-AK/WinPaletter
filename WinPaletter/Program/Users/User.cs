using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.GitHub;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// Class for handling multiple users for WinPaletter
    /// <br>- On Windows 9x (<c>OS.W9x</c>) there are no SIDs, tokens, impersonation or user enumeration. Only the current user is supported, and its logon name is used in place of a SID.</br>
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
            /// User SID that invoked the change event (the logon name on Windows 9x)
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
            public string UserName => AccountName(SID);

            /// <summary>
            /// Name of domain hosting users (the computer name on Windows 9x)
            /// </summary>
            public string Domain => AccountDomain(SID);

            /// <summary>
            /// Return if user that invoked the event is administrator or not
            /// </summary>
            public bool Administrator => IsAdmin(SID);

            /// <summary>
            /// FileSystem of user profile picture that invoked the event (null on Windows 9x)
            /// </summary>
            public string ProfilePicturePath => OS.W9x ? null : Shell32.GetUserTilePath(UserName);

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

                        // Impersonate the selected user to do operations on his profile (not available on Windows 9x)
                        WindowsImpersonationContext wic = null;

                        try
                        {
                            if (!OS.W9x && User.Identity is not null) wic = User.Identity.Impersonate();

                            // Check if main form is opened or not
                            bool MainFormIsOpened = Application.OpenForms[Forms.MainForm.Name] is not null;

                            // List of opened forms before switching user
                            List<System.Windows.Forms.Form> OpenForms = [];
                            foreach (System.Windows.Forms.Form f in Application.OpenForms)
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

                            // Show the forms that were hidden before switching user
                            foreach (System.Windows.Forms.Form f in OpenForms)
                            {
                                f.Visible = true;
                            }
                        }
                        finally
                        {
                            // Change Windows cursor into default cursor to indicate that the application is ready (even if an error occurred)
                            Cursor.Current = Cursors.Default;

                            // Undo impersonation after finishing operations on user profile
                            wic?.Undo();
                            wic?.Dispose();
                        }

                        Program.Log?.Write(LogEventLevel.Information, @$"User selected: `{Domain}\{Name}`.");

                        break;
                    }
            }
        }

        #endregion

        #region Security identifiers

        // IMPORTANT: static fields are initialized in textual order. These three must stay in this order,
        // because GetActiveSessionSID() falls back to AdminSID_GrantedUAC.

        /// <summary>
        /// Administrator user security identifier that opened WinPaletter after granting UAC dialog
        /// <br>- On Windows 9x, this is the logon name of the current user</br>
        /// </summary>
        public static readonly string AdminSID_GrantedUAC = GetCurrentUserId();

        /// <summary>
        /// User's SID who opened WinPaletter before granting UAC dialog
        /// <br>- On Windows 9x, this is the logon name of the current user</br>
        /// </summary>
        public static readonly string UserSID_OpenedWP = GetActiveSessionSID();

        private static string _sid = UserSID_OpenedWP;

        /// <summary>
        /// Current selected user security identifier for WinPaletter, to be operated on it (read\write registry)
        /// <br>- On Windows 9x, this is the logon name of the current user</br>
        /// </summary>
        public static string SID
        {
            get => _sid;

            set
            {
                // Copy the handler to avoid a race if someone unsubscribes in the middle
                UserChangeEventHandler handler = UserSwitch;

                // Check if user is changed or not
                bool changed = handler != null && _sid != value;

                // Raise user change event before switching user
                if (changed) handler(new UserChangeEventArgs() { SID = _sid, Timing = UserChangeEventArgs.Timings.BeforeChange });

                // Update user SID
                _sid = value;

                // Reset user password after switching user
                Password = string.Empty;

                if (!changed) return;

                // Windows 9x has no tokens, impersonation or other users. Just notify that the (only) user is selected
                if (OS.W9x)
                {
                    handler(new UserChangeEventArgs() { SID = value, Timing = UserChangeEventArgs.Timings.AfterChange });
                    return;
                }

                // Check if user is system profile or not
                // THIS IS A SPECIAL CASE FOR SYSTEM PROFILES (NT AUTHORITY)
                // IT IS HIGHLY RISKY AND NOT RECOMMENDED TO LOGIN INTO SYSTEM PROFILES
                if (value is "S-1-5-18" or "S-1-5-19" or "S-1-5-20")
                {
                    // Try to login into system profile without password
                    try { UpdateToken(Domain, Name, null, false); } catch (Exception ex) { Forms.BugReport.Throw(ex); }

                    // Raise user change event after switching user
                    handler(new UserChangeEventArgs() { SID = value, Timing = UserChangeEventArgs.Timings.AfterChange });
                }
                else
                {
                    // Selected user must differ from the user who opened WinPaletter after granting UAC dialog, and the login must succeed
                    bool switched = value != AdminSID_GrantedUAC && UpdateToken(Domain, Name, Password, true) && Password != null;

                    if (switched)
                    {
                        // Raise user change event after switching user
                        handler(new UserChangeEventArgs() { SID = value, Timing = UserChangeEventArgs.Timings.AfterChange });
                    }
                    else
                    {
                        // The user couldn't be processed. Ignore password and continue using current user that opened WinPaletter
                        ResetToCurrentIdentity();

                        // Raise user change event after switching user
                        handler(new UserChangeEventArgs() { SID = AdminSID_GrantedUAC, Timing = UserChangeEventArgs.Timings.AfterChange });
                    }
                }
            }
        }

        /// <summary>
        /// Update user token provided by a password
        /// <br>- On Windows 9x there are no logon tokens, so this does nothing and returns true</br>
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
            // Windows 9x: no LogonUser, no tokens
            if (OS.W9x)
            {
                ResetToCurrentIdentity();
                return true;
            }

            // Close previous token if it is opened
            if (Token != IntPtr.Zero)
            {
                Kernel32.CloseHandle(Token);
                Token = IntPtr.Zero;
            }

            IntPtr token = IntPtr.Zero;
            int error;
            bool result;

            // Check if user is system profile or not
            bool SystemProfile = Domain.Equals("NT AUTHORITY", StringComparison.OrdinalIgnoreCase);

            const string lsaKey = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Lsa";

            // Disable LimitBlankPasswordUse to allow login without password
            bool OldValue = ReadReg(lsaKey, "LimitBlankPasswordUse", true);

            try
            {
                WriteReg(lsaKey, "LimitBlankPasswordUse", false);

                if (!SystemProfile)
                {
                    // Logon user with password if it is not a system profile
                    result = ADVAPI.LogonUser(UserName, Domain, Password, ADVAPI.LOGON32_LOGON_INTERACTIVE, ADVAPI.LOGON32_PROVIDER_DEFAULT, ref token);
                }
                else
                {
                    // Logon user without password if it is a system profile
                    result = ADVAPI.LogonUser(UserName, Domain, string.Empty, ADVAPI.LOGON32_LOGON_SERVICE, ADVAPI.LOGON32_PROVIDER_DEFAULT, ref token);
                }

                // Must be read right after LogonUser
                error = Marshal.GetLastWin32Error();
            }
            finally
            {
                // Restore LimitBlankPasswordUse to its original value, whatever happens
                WriteReg(lsaKey, "LimitBlankPasswordUse", OldValue);
            }

            if (result)
            {
                // Logon user is successful. Update token and identity
                Token = token;
                Identity = new(Token);
                return true;
            }

            // Couldn't logon user. Reset token and identity
            ResetToCurrentIdentity();

            // Raise exception based on error code if error should not be ignored
            if (!ignoreError) ThrowLogonError(error);

            // Couldn't logon user. Return false
            return false;
        }

        /// <summary>
        /// Throws the exception that matches a LogonUser Win32 error code
        /// </summary>
        private static void ThrowLogonError(int error)
        {
            switch (error)
            {
                case 5: throw new UnauthorizedAccessException($"ERROR_LOGON_ACCESS_DENIED ({error}): {Program.Localization.Strings.Users.ERROR_LOGON_ACCESS_DENIED}");
                case 87: throw new UnauthorizedAccessException($"ERROR_INVALID_PARAMETER ({error}): {Program.Localization.Strings.Users.ERROR_INVALID_PARAMETER}");
                case 1008: throw new NoTokenException($"ERROR_NO_TOKEN ({error}): {Program.Localization.Strings.Users.ERROR_NO_TOKEN}");
                case 1311: throw new NoLogonServersException($"ERROR_NO_LOGON_SERVERS ({error}): {Program.Localization.Strings.Users.ERROR_NO_LOGON_SERVERS}");
                case 1317: throw new NoSuchUserException($"ERROR_NO_SUCH_USER ({error}): {Program.Localization.Strings.Users.ERROR_NO_SUCH_USER}");
                case 1326: throw new LogonFailureException($"ERROR_LOGON_FAILURE ({error}): {Program.Localization.Strings.Users.ERROR_LOGON_FAILURE}");
                case 1327: throw new AccountRestrictionException($"ERROR_ACCOUNT_RESTRICTION ({error}): {Program.Localization.Strings.Users.ERROR_ACCOUNT_RESTRICTION}");
                case 1328: throw new InvalidLogonHoursException($"ERROR_INVALID_LOGON_HOURS ({error}): {Program.Localization.Strings.Users.ERROR_INVALID_LOGON_HOURS}");
                case 1330: throw new PasswordExpiredException($"ERROR_PASSWORD_EXPIRED ({error}): {Program.Localization.Strings.Users.ERROR_PASSWORD_EXPIRED}");
                case 1331: throw new AccountDisabledException($"ERROR_ACCOUNT_DISABLED ({error}): {Program.Localization.Strings.Users.ERROR_ACCOUNT_DISABLED}");
                case 1385: throw new LogonTypeNotGrantedException($"ERROR_LOGON_TYPE_NOT_GRANTED ({error}): {Program.Localization.Strings.Users.ERROR_LOGON_TYPE_NOT_GRANTED}");
                case 1907: throw new PasswordMustChangeException($"ERROR_PASSWORD_MUST_CHANGE ({error}): {Program.Localization.Strings.Users.ERROR_PASSWORD_MUST_CHANGE}");
                case 1909: throw new AccountLockedOutException($"ERROR_ACCOUNT_LOCKED_OUT ({error}): {Program.Localization.Strings.Users.ERROR_ACCOUNT_LOCKED_OUT}");

                // Error 0 means LogonUser failed without setting a code
                case 0: throw new LogonFailureException($"ERROR_LOGON_FAILURE ({error}): {Program.Localization.Strings.Users.ERROR_LOGON_FAILURE}");

                default: throw new Exception($"ERROR ({error}): {Program.Localization.Strings.Users.ERROR_UNKNOWN}");
            }
        }

        /// <summary>
        /// Drops the selected user's token and goes back to the identity of the user who opened WinPaletter
        /// <br>- Identity is null on Windows 9x</br>
        /// </summary>
        private static void ResetToCurrentIdentity()
        {
            Token = IntPtr.Zero;
            Identity = GetCurrentIdentity();
        }

        /// <summary>
        /// Gets the current Windows identity, or null on Windows 9x where <see cref="WindowsIdentity"/> is not supported
        /// </summary>
        private static WindowsIdentity GetCurrentIdentity() => OS.W9x ? null : WindowsIdentity.GetCurrent();

        /// <summary>
        /// Gets the SID of the current process user (the logon name on Windows 9x)
        /// </summary>
        private static string GetCurrentUserId() => OS.W9x ? GetW9xUserName() : WindowsIdentity.GetCurrent().User.Value;

        #endregion

        #region Properties

        /// <summary>
        /// User logged into GitHub
        /// </summary>
        public static Octokit.User GitHub
        {
            get => gitHub;
            set
            {
                if (gitHub != value)
                {
                    gitHub = value;
                }
            }
        }
        private static Octokit.User gitHub;

        public static GitHub.Repository RepoManager { get; set; } = new();

        /// <summary>
        /// A boolean that represents if the user is logged in to GitHub or not
        /// </summary>
        public static bool GitHub_LoggedIn
        {
            get => gitHub_LoggedIn && GitHub is not null;
            private set
            {
                if (value != gitHub_LoggedIn) gitHub_LoggedIn = value;
            }
        }
        private static bool gitHub_LoggedIn = false;

        private static Bitmap github_avatar;
        private static Task avatarTask;
        private static readonly object avatarLock = new();

        // These are computed on every access (not static readonly), because SysPaths.appData changes when another user is selected
        private static string AvatarCache => Path.Combine(SysPaths.appData, "GitHub", "Avatar.png");
        private static string ETagPath => Path.Combine(SysPaths.appData, "GitHub", "Avatar.etag");

        private static volatile bool avatarLoading;
        private static string _lastAvatarUser;
        private static int _refreshVersion;
        private static int _currentVersion;

        /// <summary>
        /// Avatar of the signed-in GitHub user
        /// </summary>
        public static Bitmap GitHub_Avatar
        {
            get
            {
                if (!GitHub_LoggedIn || gitHub?.AvatarUrl == null)
                {
                    DisposeAvatar();
                    return null;
                }

                EnsureAvatarPipelineStarted();

                lock (avatarLock)
                {
                    if (github_avatar != null && _lastAvatarUser != gitHub?.Login)
                    {
                        RefreshAvatar();
                        return null;
                    }
                    return github_avatar; // now safe
                }
            }
        }

        private static void EnsureAvatarPipelineStarted()
        {
            if (avatarLoading) return;

            lock (avatarLock)
            {
                if (avatarLoading) return;

                avatarLoading = true;
                avatarTask = LoadAvatarPipelineAsync();
            }
        }

        private static async Task LoadAvatarPipelineAsync()
        {
            int version;
            lock (avatarLock)
            {
                version = _refreshVersion;
            }

            try
            {
                string cache = AvatarCache;
                bool hadCache = false;

                if (File.Exists(cache))
                {
                    using (Bitmap bmp = BitmapMgr.Load(cache))
                    {
                        // Only apply if this task is still the latest
                        lock (avatarLock)
                        {
                            if (version == _refreshVersion)
                                ReplaceAvatar(bmp.Resize(256, 256));
                        }
                    }
                    hadCache = true;
                }

                bool updated = await DownloadAvatarInternalAsync().ConfigureAwait(false);

                if (updated || !hadCache)
                {
                    if (File.Exists(cache))
                    {
                        using (Bitmap bmp = BitmapMgr.Load(cache))
                        {
                            lock (avatarLock)
                            {
                                if (version == _refreshVersion)
                                    ReplaceAvatar(bmp.Resize(256, 256));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, "Failed to load GitHub avatar.", ex);
            }
            finally
            {
                lock (avatarLock)
                {
                    // Only reset loading flag if this task is still the latest
                    if (version == _refreshVersion)
                        avatarLoading = false;
                }
            }
        }

        public static async Task<bool> DownloadAvatarInternalAsync()
        {
            if (!Program.IsNetworkAvailable)
            {
                Program.Log?.Write(LogEventLevel.Warning, "Network unavailable. Cannot download GitHub avatar.");
                return false;
            }

            // Capture values once, as the user may log out while the download is running
            string url = gitHub?.AvatarUrl;
            if (string.IsNullOrEmpty(url)) return false;

            string cache = AvatarCache;
            string etagPath = ETagPath;

            Directory.CreateDirectory(Path.GetDirectoryName(cache)!);

            string savedETag = File.Exists(etagPath) ? File.ReadAllText(etagPath) : null;

            using (HttpClient client = new())
            {
                if (!string.IsNullOrEmpty(savedETag))
                {
                    client.DefaultRequestHeaders.IfNoneMatch.ParseAdd(savedETag);
                }

                using (HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.NotModified) return false;

                    // Don't try to decode an error page as an image
                    if (!response.IsSuccessStatusCode) return false;

                    byte[] data = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                    using (MemoryStream ms = new(data))
                    using (Bitmap bmp = Image.FromStream(ms) as Bitmap)
                    {
                        bmp.Save(cache, ImageFormat.Png);
                    }

                    if (response.Headers.ETag != null)
                    {
                        File.WriteAllText(etagPath, response.Headers.ETag.Tag);
                    }

                    return true;
                }
            }
        }

        private static void ReplaceAvatar(Bitmap newAvatar)
        {
            if (ReferenceEquals(github_avatar, newAvatar)) return;

            Bitmap old = github_avatar;
            github_avatar = newAvatar;
            _lastAvatarUser = gitHub?.Login;
            old?.Dispose();

            Events.OnGitHubAvatarUpdated();
        }

        private static void DisposeAvatar()
        {
            Bitmap old = github_avatar;
            github_avatar = null;
            old?.Dispose();
        }

        /// <summary>
        /// Refresh avatar - checks for updates but preserves disk cache
        /// </summary>
        public static void RefreshAvatar()
        {
            lock (avatarLock)
            {
                // Increment version to invalidate older tasks
                _refreshVersion++;
                avatarLoading = false;
                EnsureAvatarPipelineStarted();
            }
        }

        /// <summary>
        /// Clear avatar from memory on logout, preserve disk cache for next login
        /// </summary>
        public static void ClearAvatar()
        {
            lock (avatarLock)
            {
                // Clear GitHub user
                GitHub = null;

                // Clear avatar from memory only
                DisposeAvatar();

                // Reset pipeline state
                avatarLoading = false;
                avatarTask = null;

                // DO NOT delete cache files - keep them for next login
                // This preserves offline capability and ETag for conditional requests
            }
        }

        /// <summary>
        /// Name of current user
        /// </summary>
        public static string Name => AccountName(SID);

        /// <summary>
        /// Name of computer hosting current user
        /// </summary>
        public static string Domain => AccountDomain(SID);

        /// <summary>
        /// Return if current user is administrator or not
        /// <br>- Always true on Windows 9x, which has no privilege separation</br>
        /// </summary>
        public static bool Administrator => IsAdmin(SID);

        /// <summary>
        /// FileSystem of current user profile picture (null on Windows 9x, which has no user tiles)
        /// </summary>
        public static string ProfilePicturePath => OS.W9x ? null : Shell32.GetUserTilePath(Name);

        /// <summary>
        /// Current user profile picture (null on Windows 9x, which has no user tiles)
        /// </summary>
        public static Bitmap ProfilePicture => OS.W9x ? null : Shell32.GetUserAccountPicture(Name) as Bitmap;

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
        /// <br>- Null on Windows 9x</br>
        /// </summary>
        public static WindowsIdentity Identity = GetCurrentIdentity();

        /// <summary>
        /// Administrator user Windows identity, used to impersonate administrator to do codes and operations require elevation.
        /// <br>- Null on Windows 9x</br>
        /// </summary>
        public static WindowsIdentity Identity_Admin = GetCurrentIdentity();

        /// <summary>
        /// Get path of current user profile
        /// <br>- For example: C:\Users\...</br>
        /// </summary>
        public static string UserProfilePath => GetUserProfilePath(SID);
        #endregion

        #region Methods

        /// <summary>
        /// Update GitHub user logged in flag
        /// </summary>
        /// <param name="status"></param>
        public static void UpdateGitHubLoginStatus(bool status)
        {
            GitHub_LoggedIn = status;
            Events.OnGitHubUserSwitch(new() { IsLoggedIn = status });
        }

        /// <summary>
        /// Translates a SID into <c>DOMAIN\Name</c>
        /// <br>- On Windows 9x the "SID" is the logon name, and the computer name is used as the domain</br>
        /// </summary>
        private static string TranslateSID(string sid)
        {
            if (OS.W9x) return $"{Environment.MachineName}\\{sid}";

            return new SecurityIdentifier(sid).Translate(typeof(NTAccount)).ToString();
        }

        private static string AccountName(string sid) => TranslateSID(sid).Split('\\').Last();

        private static string AccountDomain(string sid) => TranslateSID(sid).Split('\\').First();

        /// <summary>
        /// Finds the SID of a user by his name (case-insensitive). Returns null if not found.
        /// </summary>
        private static string FindSidByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            string name = userName.Split('\\').Last();

            return GetUsers().FirstOrDefault(x => x.Value.Split('\\').Last().Equals(name, StringComparison.OrdinalIgnoreCase)).Key;
        }

        /// <summary>
        /// Get dictionary of SID, DOMAIN\USERNAME of all users
        /// <br>- On Windows 9x, only the current user is returned (key is the logon name)</br>
        /// </summary>
        /// <param name="includeSystemProfiles"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetUsers(bool includeSystemProfiles = false)
        {
            // Build up a list of SIDs and their corresponding DOMAIN\USERNAME
            Dictionary<string, string> result = [];

            // Windows 9x: no SIDs and no profile enumeration. Only the current user
            if (OS.W9x)
            {
                string current = GetW9xUserName();
                result.Add(current, $"{Environment.MachineName}\\{current}");
                return result;
            }

            // List of found SIDs
            List<string> FoundSIDs = [];

            // Get all user profiles from using WMI or registry if WMI has failed
            if (!OS.W9x && !OS.WXP)
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
                    FoundSIDs.Clear();

                    // Get all user profiles from registry
                    foreach (string SID in GetSubKeys("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList")) { FoundSIDs.Add(SID); }
                }
            }
            else
            {
                // Use registry method as WMI is not available in Windows 9x/XP
                foreach (string SID in GetSubKeys("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList")) { FoundSIDs.Add(SID); }
            }

            // Checks if a user hive is currently loaded into HKEY_USERS
            static bool IsHiveLoaded(string sid) => Registry.Users.GetSubKeyNames().Contains(sid, StringComparer.OrdinalIgnoreCase);

            // Process all found SIDs
            foreach (string sid in FoundSIDs)
            {
                try
                {
                    // Get username from SID
                    string username = new SecurityIdentifier(sid).Translate(typeof(NTAccount)).ToString();

                    // Check if user is system profile or not
                    bool condition_base = includeSystemProfiles ||
                        !(username.StartsWith("NT AUTHORITY", StringComparison.OrdinalIgnoreCase) ||
                          username.StartsWith("NT SERVICE", StringComparison.OrdinalIgnoreCase) ||
                          username.Split('\\').Last().StartsWith("WSIACCOUNT", StringComparison.OrdinalIgnoreCase));

                    // Check if user profile is loaded or not
                    bool condition_DAT_Loaded = includeSystemProfiles || (condition_base && IsHiveLoaded(sid));

                    // Check if user profile is unloaded or not
                    bool condition_DAT_Unloaded = !condition_DAT_Loaded && condition_base && File.Exists($"{User.GetUserProfilePath(sid)}\\NTUSER.DAT");

                    // Load user profile if it is unloaded into registry key HKEY_USERS to make WinPaletter able to read and write on it
                    if (condition_DAT_Unloaded)
                    {
                        Program.SendCommand($"reg load HKU\\{sid} \"{User.GetUserProfilePath(sid)}\\NTUSER.DAT\"");

                        // Recheck if user profile is loaded or not
                        condition_DAT_Loaded = includeSystemProfiles || (condition_base && IsHiveLoaded(sid));
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
        /// <br>- On Windows 9x, the current user is always used</br>
        /// <param name="ForceShow">Force showing user switch form even if there is only one user</param>
        /// <param name="SkipToCurrentUser">Skip showing user switch form and use current user</param>
        /// </summary>
        public static void Login(bool ForceShow = false, bool SkipToCurrentUser = false)
        {
            // Windows 9x: no user switching, only the current user
            if (OS.W9x)
            {
                User.SID = GetW9xUserName();
                return;
            }

            // Get list of all users
            Dictionary<string, string> UsersList = GetUsers();

            // Save settings into current user before reloading settings for new user
            if (!SkipToCurrentUser) Program.Settings.Save(Settings.Source.Registry);

            // If skip to current user is enabled, use current user without showing user switch form
            if (SkipToCurrentUser) { User.SID = GetActiveSessionSID(); }

            // If there are more than one user or force show is enabled, show user switch form to pick a user
            else if (ForceShow || UsersList.Count > 1) { Forms.UserSwitch.PickUser(UsersList); }

            // If one user if found, there is no need to login, use current windows identity
            else { User.SID = GetCurrentUserId(); }
        }

        /// <summary>
        /// Login into user profile using SID
        /// <br>- On Windows 9x there are no passwords to check, so this returns true</br>
        /// </summary>
        /// <param name="_sid"></param>
        /// <returns></returns>
        public static bool SID_Credentials_Result(string _sid)
        {
            // Windows 9x: no per-account passwords to verify
            if (OS.W9x) return true;

            bool result = false;

            string account = TranslateSID(_sid);
            string userName = account.Split('\\').Last();
            string domainName = account.Split('\\').First();

            (string domain, string username, string password, bool isPasswordCorrect, bool canceled) credsResult = (domainName, userName, null, false, false);

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
                    !userEnteredWrongPassword ? string.Format(Program.Localization.Strings.Users.EnterPassword_Caption, userName) : Program.Localization.Strings.Users.IncorrectPassword,
                    !userEnteredWrongPassword ? Program.Localization.Strings.Users.WindowsHello_NotSupported : string.Format(Program.Localization.Strings.Users.EnterPassword_Caption, userName) + "\r\n\r\n" + Program.Localization.Strings.Users.WindowsHello_NotSupported,
                    domainName, userName);

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
        /// <br>- On Windows 9x, returns the logon name of the current user</br>
        /// </summary>
        /// <returns></returns>
        private static string GetActiveSessionSID()
        {
            // Windows 9x has no SIDs, no WMI and no LogonUI keys. The "user id" is just the logon name
            if (OS.W9x) return GetW9xUserName();

            string result = null;

            // Attempt using WMI if possible
            try
            {
                using (ManagementObjectSearcher searcher = new("SELECT UserName FROM Win32_ComputerSystem"))
                {
                    foreach (ManagementObject queryObj in searcher.Get().Cast<ManagementObject>())
                    {
                        string currentUser = queryObj["UserName"]?.ToString();

                        if (!string.IsNullOrEmpty(currentUser))
                        {
                            // Extract the username from domain\username format
                            result = FindSidByUserName(currentUser);
                            break;
                        }
                    }
                }
            }
            catch { } // WMI is not available. Use registry checks below

            // Fall back to additional registry checks if WMI fails
            if (string.IsNullOrEmpty(result))
            {
                if (OS.W9x || OS.WXP && !OS.WVista && !OS.W7)
                {
                    result = ReadReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Authentication\\LogonUI", "LastLoggedOnUserSID", string.Empty);
                }
                else
                {
                    string username = ReadReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Authentication\\LogonUI", "LastLoggedOnUser", string.Empty);
                    result = FindSidByUserName(username);
                }
            }

            // If no result found, use the default admin SID
            return string.IsNullOrEmpty(result) ? User.AdminSID_GrantedUAC : result;
        }

        /// <summary>
        /// Gets the logon name of the current user on Windows 9x, which has no SIDs
        /// </summary>
        private static string GetW9xUserName()
        {
            string name = string.Empty;

            // Logon name written by the Windows 9x logon dialog (exists when a network client / user profiles are used)
            try { name = ReadReg(@"HKEY_LOCAL_MACHINE\Network\Logon", "username", string.Empty); } catch { }

            // Win32 GetUserName via .NET
            if (string.IsNullOrWhiteSpace(name))
            {
                try { name = Environment.UserName; } catch { }
            }

            // Last resort: the owner name entered during Windows setup (not necessarily the current user)
            if (string.IsNullOrWhiteSpace(name))
            {
                try { name = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion", "RegisteredOwner", string.Empty); } catch { }
            }

            // The name is used as a key/ID, so it must never be empty
            return string.IsNullOrWhiteSpace(name) ? "User" : name.Trim();
        }

        /// <summary>
        /// Get path of user profile
        /// <br>- For example: C:\Users\...</br>
        /// <br>- On Windows 9x, <paramref name="SID"/> is the logon name</br>
        /// </summary>
        public static string GetUserProfilePath(string SID = null)
        {
            // Windows 9x: profiles are under HKLM\...\Windows\CurrentVersion\ProfileList\<name>
            if (OS.W9x) return GetW9xProfilePath(SID);

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
        /// Get path of user profile on Windows 9x
        /// <br>- With user profiles enabled: C:\WINDOWS\Profiles\&lt;name&gt;</br>
        /// <br>- With user profiles disabled (single user), the Windows directory is shared by everyone</br>
        /// </summary>
        private static string GetW9xProfilePath(string userName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    string path = ReadReg($@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\ProfileList\{userName}", "ProfileImagePath", string.Empty);

                    if (!string.IsNullOrWhiteSpace(path)) return Environment.ExpandEnvironmentVariables(path);
                }
            }
            catch { }

            // SpecialFolder.UserProfile is not supported on Windows 9x, so don't use it here
            string windir = Environment.GetEnvironmentVariable("windir");
            return string.IsNullOrWhiteSpace(windir) ? @"C:\WINDOWS" : windir;
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
                if (OS.W9x)
                {
                    // Windows 9x has a single "Application Data" folder (no AppData\Local, no Local Settings)
                    SysPaths.LocalAppData = $"{SysPaths.UserProfile}\\Application Data";
                }
                else if (!OS.WXP)
                {
                    SysPaths.LocalAppData = $"{SysPaths.UserProfile}\\AppData\\Local";
                }
                else
                {
                    SysPaths.LocalAppData = $"{SysPaths.UserProfile}\\Local Settings\\Application Data";
                }

                SysPaths.appData = $"{SysPaths.LocalAppData}\\{Application.CompanyName}\\{Application.ProductName}";

                // Windows 9x has no NTFS permissions
                if (!OS.W9x) GrantUsersFullControl(SysPaths.ProgramFilesData);
            }
            else
            {
                // Get user profile path from current user as SID is not provided
                SysPaths.UserProfile = OS.W9x ? GetW9xProfilePath(null) : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                SysPaths.LocalAppData = OS.W9x ? $"{SysPaths.UserProfile}\\Application Data" : Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                SysPaths.appData = OS.W9x ? $"{SysPaths.LocalAppData}\\{Application.CompanyName}\\{Application.ProductName}" : Directory.GetParent(Application.LocalUserAppDataPath).FullName;
            }

            // Create directories if they are not exist
            if (!string.IsNullOrEmpty(SysPaths.UserProfile) && !Directory.Exists(SysPaths.UserProfile)) { Directory.CreateDirectory(SysPaths.UserProfile); }
            if (!string.IsNullOrEmpty(SysPaths.LocalAppData) && !Directory.Exists(SysPaths.LocalAppData)) { Directory.CreateDirectory(SysPaths.LocalAppData); }
            if (!string.IsNullOrEmpty(SysPaths.appData) && !Directory.Exists(SysPaths.appData)) { Directory.CreateDirectory(SysPaths.appData); }
        }

        /// <summary>
        /// Gives all local users full control over a directory (NTFS only), impersonating an administrator
        /// </summary>
        private static void GrantUsersFullControl(string path)
        {
            if (Identity_Admin is null || string.IsNullOrEmpty(path) || !Directory.Exists(path)) return;

            WindowsImpersonationContext wic = null;

            try
            {
                // Impersonate an administrator user to set full control to the directory
                wic = Identity_Admin.Impersonate();

                // Create a DirectoryInfo object
                DirectoryInfo directoryInfo = new(path);

                // Get the existing access control settings
                DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

                // Define the security rules (allow full control to all local users)
                SecurityIdentifier allUsers = new(WellKnownSidType.BuiltinUsersSid, null);
                FileSystemAccessRule rule = new(allUsers, FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);

                // Add the rule to the security settings
                directorySecurity.AddAccessRule(rule);

                // Process the new security settings to the directory
                directoryInfo.SetAccessControl(directorySecurity);
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Warning, $"Couldn't set permissions on `{path}`.", ex);
            }
            finally
            {
                // Undo impersonation after finishing operations on user profile
                wic?.Undo();
                wic?.Dispose();
            }
        }

        /// <summary>
        /// Determines whether an User is an administrator, in the current machine.
        /// <br>- Windows 9x has no accounts or groups, so every user is treated as an administrator</br>
        /// </summary>
        /// <returns><c>true</c> if user is an administrator, <c>false</c> otherwise.</returns>
        public static bool IsAdmin(string SID)
        {
            if (OS.W9x) return true;

            try
            {
                // Get user groups of Administrators
                SecurityIdentifier AdminGroupSID = new("S-1-5-32-544");

                using (PrincipalContext pContext = new(ContextType.Machine))
                using (UserPrincipal pUser = new(pContext))
                using (PrincipalSearcher pSearcher = new(pUser))
                {
                    Principal principal = pSearcher.FindAll().FirstOrDefault(u => u.Sid.ToString().Equals(SID, StringComparison.OrdinalIgnoreCase));

                    // Check if user is system profile or not. System profiles are always administrators
                    if (principal is null)
                    {
                        return SID.ToUpperInvariant() is "S-1-5-18" or "S-1-5-19" or "S-1-5-20";
                    }

                    // Check if user is an administrator or not
                    return principal.GetGroups().Any(g => g.Sid == AdminGroupSID);
                }
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