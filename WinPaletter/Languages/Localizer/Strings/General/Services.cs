namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class Services_Cls
            {
                public string Title_Install { get; set; } = "WinPaletter will install the system events sounds service.";
                public string Title_Update { get; set; } = "WinPaletter is updating the system events sounds service.";
                public string Title_Uninstall { get; set; } = "WinPaletter is uninstalling the system events sounds service.";
                public string Description { get; set; } = "This service is a service that monitors Windows events and plays sounds based on the received events. It serves as a deflection method for charger connection/disconnection and Wi-Fi connection/disconnection/connection failure sounds (compatible with any version of Windows) and logoff, logon, lock, unlock, and shutdown sounds (in Windows 8 and higher).";
                public string Stopping { get; set; } = "Stopping {0} service if it is started.";
                public string Extracting { get; set; } = "Extracting {0} service.";
                public string Uninstalling { get; set; } = "Uninstalling {0} service if it is installed.";
                public string Installing { get; set; } = "Installing {0} service.";
                public string Starting { get; set; } = "Starting {0} service.";
                public string InstallCompleted { get; set; } = "Service installation is completed.";
                public string UninstallCompleted { get; set; } = "Service uninstallation is completed.";
                public string MissingInstallutil { get; set; } = "Couldn't find installutil.exe in the .NET Framework directory. Try reinstalling or repairing .NET Framework and try again.";
            }
        }
    }
}