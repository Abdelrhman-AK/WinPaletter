namespace WinPaletter
{
    public partial class Localizer
    {
        public string SvcInstaller_Title_Install { get; set; } = "WinPaletter will install the system events sounds service.";
        public string SvcInstaller_Title_Update { get; set; } = "WinPaletter is updating the system events sounds service.";
        public string SvcInstaller_Title_Uninstall { get; set; } = "WinPaletter is uninstalling the system events sounds service.";
        public string SvcInstaller_Description { get; set; } = "This service is a service that monitors Windows events and plays sounds based on the received events. It serves as a deflection method for charger connection/disconnection and Wi-Fi connection/disconnection/connection failure sounds (compatible with any version of Windows) and logoff, logon, lock, unlock, and shutdown sounds (in Windows 8 and higher).";
        public string SvcInstaller_Stopping { get; set; } = "Stopping {0} service if it is started.";
        public string SvcInstaller_Extracting { get; set; } = "Extracting {0} service.";
        public string SvcInstaller_Uninstalling { get; set; } = "Uninstalling {0} service if it is installed.";
        public string SvcInstaller_Installing { get; set; } = "Installing {0} service.";
        public string SvcInstaller_Starting { get; set; } = "Starting {0} service.";
        public string SvcInstaller_InstallCompleted { get; set; } = "Service installation is completed.";
        public string SvcInstaller_UninstallCompleted { get; set; } = "Service uninstallation is completed.";
        public string SvcInstaller_MissingInstallutil { get; set; } = "Couldn't find installutil.exe in the .NET Framework directory. Try reinstalling or repairing .NET Framework and try again.";

    }
}
