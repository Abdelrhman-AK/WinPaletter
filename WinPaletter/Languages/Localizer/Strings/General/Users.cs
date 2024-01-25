namespace WinPaletter
{
    public partial class Localizer
    {
        public string UserSwitch_Computer { get; set; } = "Computer";
        public string UserSwitch_OnComputer { get; set; } = "On computer: {0}";
        public string UserSwitch_TypeAdministrator { get; set; } = "Type: Administrator";
        public string UserSwitch_TypeLocalUser { get; set; } = "Type: Local user";
        public string UserSwitch_TypeSystem { get; set; } = "Type: System profile";
        public string UserSwitch_LoginAs { get; set; } = "Log in as {0}";
        public string UserSwitch_SYSTEM_Alert0 { get; set; } = "You should know that system accounts are generally reserved for very specific system-level tasks and should be approached with extreme caution. Incorrect use of SYSTEM-level access can lead to system instability. Always follow best practices for security; this is to avoid system corruption.";
        public string UserSwitch_SYSTEM_Alert1 { get; set; } = "Do you want to continue with this user? Pressing 'No' will continue with the administrator user that opened WinPaletter.";
        public string UserSwitch_ERROR_UNKNOWN { get; set; } = "WinPaletter failed to log in to the selected user due to an unknown reason. Try continuing without a password.";
        public string UserSwitch_ERROR_LOGON_FAILURE { get; set; } = "The credentials provided for logging in (username and password) do not match a valid user account on the system or are incorrect.";
        public string UserSwitch_ERROR_LOGON_ACCESS_DENIED { get; set; } = "Access denied to the system account. Try continuing without a password.";
        public string UserSwitch_ERROR_INVALID_PARAMETER { get; set; } = "Invalid or incorrect username, domain name, logon type, or logon provider specified, or problems with the password or other credentials, or incorrect values for any of the parameters passed to the function.";
        public string UserSwitch_ERROR_NO_TOKEN { get; set; } = "Can't get a token to call functions on the system account. Try continuing without a password.";
        public string UserSwitch_ERROR_LOGON_TYPE_NOT_GRANTED { get; set; } = "Logon type requested is not granted for the user.";
        public string UserSwitch_ERROR_ACCOUNT_RESTRICTION { get; set; } = "There are account restrictions preventing the user from logging in at the current time or from the current location.";
        public string UserSwitch_ERROR_INVALID_LOGON_HOURS { get; set; } = "The user is restricted from logging in due to invalid logon hours.";
        public string UserSwitch_ERROR_PASSWORD_EXPIRED { get; set; } = "User's password has expired, and it needs to be changed.";
        public string UserSwitch_ERROR_ACCOUNT_DISABLED { get; set; } = "User account is disabled and cannot be used for authentication.";
        public string UserSwitch_ERROR_ACCOUNT_LOCKED_OUT { get; set; } = "User account has been locked out due to too many login attempts with incorrect credentials.";
        public string UserSwitch_ERROR_NO_SUCH_USER { get; set; } = "Specified user does not exist on the system.";
        public string UserSwitch_ERROR_NO_LOGON_SERVERS { get; set; } = "There are no logon servers available to service the logon request. This may indicate a network or domain connectivity issue.";
        public string UserSwitch_ERROR_PASSWORD_MUST_CHANGE { get; set; } = "User's password must be changed before logging in.";

    }
}
