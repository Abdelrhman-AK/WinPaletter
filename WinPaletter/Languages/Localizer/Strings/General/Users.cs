namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            /// <summary>
            /// A class that contains all the strings used for describing the users.
            /// </summary>
            public partial class Users_Cls
            {
                public string GitHub_NotSigned { get; set; } = "You are not signed in to GitHub. Sign in for more features!";
                public string Computer { get; set; } = "Computer";
                public string OnComputer { get; set; } = "On computer: {0}";
                public string TypeAdministrator { get; set; } = "Type: Administrator";
                public string TypeLocalUser { get; set; } = "Type: Local user";
                public string TypeSystem { get; set; } = "Type: System profile";
                public string LoginAs { get; set; } = "Log in as {0}";
                public string EnterPassword_Caption { get; set; } = "You are about to log in as '{0}', but this will only be a memory login and will not switch the Windows account.";
                public string WindowsHello_NotSupported { get; set; } = "Windows Hello methods such as PINs and biometrics are not supported; use your actual password instead.";
                public string IncorrectPassword { get; set; } = "Incorrect password. Please try again.";
                public string SYSTEM_Alert0 { get; set; } = "You should know that system accounts are generally reserved for very specific system-level tasks and should be approached with extreme caution. Incorrect use of SYSTEM-level access can lead to system instability. Always follow best practices for security; this is to avoid system corruption.";
                public string SYSTEM_Alert1 { get; set; } = "Do you want to continue with this user?";
                public string ERROR_UNKNOWN { get; set; } = "WinPaletter failed to log in to the selected user due to an unknown reason. Try continuing without a password.";
                public string ERROR_LOGON_FAILURE { get; set; } = "The credentials provided for logging in (username and password) do not match a valid user account on the system or are incorrect.";
                public string ERROR_LOGON_ACCESS_DENIED { get; set; } = "System account access was denied. To proceed, use an advanced process elevation tool (e.g., Nirsoft's AdvancedRun).";
                public string ERROR_INVALID_PARAMETER { get; set; } = "Invalid or incorrect username, domain name, logon type, or logon provider specified, or problems with the password or other credentials, or incorrect values for any of the parameters passed to the function.";
                public string ERROR_NO_TOKEN { get; set; } = "Can't get a token to call functions on the system account. Try continuing without a password.";
                public string ERROR_LOGON_TYPE_NOT_GRANTED { get; set; } = "Logon type requested is not granted for the user.";
                public string ERROR_ACCOUNT_RESTRICTION { get; set; } = "There are account restrictions preventing the user from logging in at the current time or from the current location.";
                public string ERROR_INVALID_LOGON_HOURS { get; set; } = "The user is restricted from logging in due to invalid logon hours.";
                public string ERROR_PASSWORD_EXPIRED { get; set; } = "User's password has expired, and it needs to be changed.";
                public string ERROR_ACCOUNT_DISABLED { get; set; } = "User account is disabled and cannot be used for authentication.";
                public string ERROR_ACCOUNT_LOCKED_OUT { get; set; } = "User account has been locked out due to too many login attempts with incorrect credentials.";
                public string ERROR_NO_SUCH_USER { get; set; } = "Specified user does not exist on the system.";
                public string ERROR_NO_LOGON_SERVERS { get; set; } = "There are no logon servers available to service the logon request. This may indicate a network or domain connectivity issue.";
                public string ERROR_PASSWORD_MUST_CHANGE { get; set; } = "User's password must be changed before logging in.";
            }
        }
    }
}