using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows icons
    /// </summary>
    public class Icons : ICloneable
    {
        /// <summary>
        /// List of Control Panel CLSIDs and their icons (used as helpers)
        /// </summary>
        public static readonly List<Tuple<string, string, string>> ControlPanelCLSIDs =
        [
            Tuple.Create("{D20EA4E1-3957-11d2-A40B-0C5020524153}", "Administrative Tools", $"{SysPaths.imageres},-114"),
            Tuple.Create("{9C60DE1E-E5FC-40f4-A487-460851A8D915}", "AutoPlay", $"{SysPaths.System32}\\autoplay.dll,-1"),
            Tuple.Create("{B98A2BEA-7D42-4558-8BD1-832F41BAC6FD}", "Backup and Restore (Windows 7)", $"{SysPaths.System32}\\sdcpl.dll,-1"),
            Tuple.Create("{D9EF8727-CAC2-4e60-809E-86F80A666C91}", "BitLocker Drive Encryption", $"{SysPaths.System32}\\fvecpl.dll,-1"),
            Tuple.Create("{B2C761C6-29BC-4f19-9251-E6195265BAF1}", "Color Management", $"{SysPaths.System32}\\colorcpl.exe,-5"),
            Tuple.Create("{1206F5F1-0569-412C-8FEC-3204630DFB70}", "Credential Manager", $"{SysPaths.System32}\\Vault.dll,-1"),
            Tuple.Create("{E2E7934B-DCE5-43C4-9576-7FE4F75E7480}", "Date and Time", $"{SysPaths.System32}\\timedate.cpl,-50"),
            Tuple.Create("{17cd9488-1228-4b2f-88ce-4298e93e0966}", "Default Programs", $"{SysPaths.imageres},-24"),
            Tuple.Create("{74246bfc-4c96-11d0-abef-0020af6b0b7a}", "Device Manager", $"{SysPaths.System32}\\devmgr.dll,-201"),
            Tuple.Create("{A8A91A66-3A7D-4424-8D24-04E180695C7A}", "Devices and Printers", $"{SysPaths.System32}\\DeviceCenter.dll,-1"),
            Tuple.Create("{D555645E-D4F8-4c29-A827-D93C859C4F2A}", "Ease of Access Center", $"{SysPaths.System32}\\accessibilitycpl.dll,-1"),
            Tuple.Create("{6DFD7C5C-2451-11d3-A299-00C04F8EF6AF}", "File Explorer Options", $"{SysPaths.imageres},-166"),
            Tuple.Create("{F6B6E965-E9B2-444B-9286-10C9152EDBC5}", "File History", $"{SysPaths.System32}\\FileHistory.exe,0"),
            Tuple.Create("{BD84B380-8CA2-1069-AB1D-08000948F534}", "Fonts", $"{SysPaths.System32}\\fontext.dll,0"),
            Tuple.Create("{87D66A43-7B11-4A28-9811-C86EE395ACF7}", "Indexing Options", $"{SysPaths.System32}\\srchadmin.dll,-201"),
            Tuple.Create("{A0275511-0E86-4ECA-97C2-ECD8F1221D08}", "Infrared", $"{SysPaths.System32}\\irprops.cpl,-129"),
            Tuple.Create("{A3DD4F92-658A-410F-84FD-6FBBBEF2FFFE}", "Internet Options", $"{SysPaths.System32}\\inetcpl.cpl,-4487"),
            Tuple.Create("{725BE8F7-668E-4C7B-8F90-46BDB0936430}", "Keyboard", $"{SysPaths.System32}\\main.cpl,-200"),
            Tuple.Create("{6C8EEC18-8D75-41B2-A177-8831D59D2D50}", "Mouse", $"{SysPaths.System32}\\main.cpl,-100"),
            Tuple.Create("{8E908FC9-BECC-40f6-915B-F4CA0E70D03D}", "Network and Sharing Center", $"{SysPaths.System32}\\netcenter.dll,-1"),
            Tuple.Create("{F82DF8F7-8B9F-442E-A48C-818EA735FF9B}", "Pen and Touch", $"{SysPaths.System32}\\tabletpc.cpl,-10201"),
            Tuple.Create("{40419485-C444-4567-851A-2DD7BFA1684D}", "Phone and Modem", $"{SysPaths.System32}\\telephon.cpl,-100"),
            Tuple.Create("{025A5937-A6BE-4686-A844-36FE4BEC8B6D}", "Power Options", $"{SysPaths.System32}\\powercpl.dll,-1"),
            Tuple.Create("{7b81be6a-ce2b-4676-a29e-eb907a5126c5}", "Programs and Features", $"{SysPaths.imageres},-87"),
            Tuple.Create("{9FE63AFD-59CF-4419-9775-ABCC3849F861}", "Recovery", $"{SysPaths.imageres},-1022"),
            Tuple.Create("{62D8ED13-C9D0-4CE8-A914-47DD628FB1B0}", "Region", $"{SysPaths.System32}\\intl.cpl,-200"),
            Tuple.Create("{241D7C96-F8BF-4F85-B01F-E2B043341A4B}", "RemoteApp and Desktop Connections", $"{SysPaths.System32}\\tsworkspace.dll,0"),
            Tuple.Create("{BB64F8A7-BEE7-4E1A-AB8D-7D8273F7FDB6}", "Security and Maintenance", $"{SysPaths.System32}\\ActionCenterCPL.dll,-1"),
            Tuple.Create("{F2DDFC82-8F12-4CDD-B7DC-D4FE1425AA4D}", "Sound", $"{SysPaths.System32}\\mmsys.cpl,-100"),
            Tuple.Create("{58E3C745-D971-4081-9034-86E34B30836A}", "Speech Recognition", $"{SysPaths.System32}\\Speech\\SpeechUX\\speechuxcpl.dll,-1"),
            Tuple.Create("{F942C606-0914-47AB-BE56-1321B8035096}", "Storage Spaces", $"{SysPaths.System32}\\SpaceControl.dll,-1"),
            Tuple.Create("{9C73F5E5-7AE7-4E32-A8E8-8D23B85255BF}", "Sync Center", $"{SysPaths.System32}\\SyncCenter.dll,-1000"),
            Tuple.Create("{BB06C0E4-D293-4f75-8A90-CB05B6477EEE}", "System", $"{SysPaths.imageres},-149"),
            Tuple.Create("{80F3F1D5-FECA-45F3-BC32-752C152E456E}", "Tablet PC Settings", $"{SysPaths.System32}\\tabletpc.cpl,-10200"),
            Tuple.Create("{0DF44EAA-FF21-4412-828E-260A8728E7F1}", "Taskbar and Navigation", $"{SysPaths.imageres},-80"),
            Tuple.Create("{C58C4893-3BE0-4B45-ABB5-A63E4B8C8651}", "Troubleshooting", $"{SysPaths.System32}\\DiagCpl.dll,-1"),
            Tuple.Create("{60632754-c523-4b62-b45c-4172da012619}", "User Accounts", $"{SysPaths.System32}\\usercpl.dll,-1"),
            Tuple.Create("{4026492F-2F69-46B8-B9BF-5654FC07E423}", "Windows Defender Firewall", $"{SysPaths.System32}\\FirewallControlPanel.dll,-1"),
            Tuple.Create("{5ea4f148-308c-46d7-98a9-49041b1dd468}", "Windows Mobility Center", $"{SysPaths.System32}\\mblctr.exe,0"),
            Tuple.Create("{8E0C279D-0BD1-43C3-9EBD-31C3DC5B8A77}", "Windows To Go", $"{SysPaths.System32}\\pwcreator.exe,-2001"),
            Tuple.Create("{ECDB0924-4208-451E-8EE0-373C0956DE16}", "Work Folders", $"{SysPaths.System32}\\WorkfoldersControl.dll,1"),
        ];

        /// <summary>
        /// List of Desktop CLSIDs and their icons (used as helpers)
        /// </summary>
        public static readonly List<Tuple<string, string, string>> DesktopCLSIDs =
        [
            Tuple.Create("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", "Computer", $"{SysPaths.imageres},-109"),
            Tuple.Create("{5399E694-6CE5-4D6C-8FCE-1D8870FDCBA0}", "Control Panel", $"{SysPaths.imageres},-27"),
            Tuple.Create("{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}", "Network", $"{SysPaths.imageres},-25"),
            Tuple.Create("{59031a47-3f72-44a7-89c5-5595fe6b30ee}", "User", $"{SysPaths.imageres},-123"),
            Tuple.Create("{645FF040-5081-101B-9F08-00AA002F954E}", "Recycle Bin", $"{SysPaths.imageres},-55|{SysPaths.imageres},-54"/*empty_full*/),
        ];

        /// <summary>
        /// List of Explorer CLSIDs and their icons (used as helpers)
        /// </summary>
        public static readonly List<Tuple<string, string, string>> ExplorerCLSIDs =
        [
            Tuple.Create("{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}", "Desktop", $"{SysPaths.imageres},-183"),
            Tuple.Create("{D3162B92-9365-467A-956B-92703ACA08AF}", "Documents", $"{SysPaths.imageres},-112"),
            Tuple.Create("{088e3905-0323-4b02-9826-5d99428e115f}", "Downloads", $"{SysPaths.imageres},-184"),
            Tuple.Create("{3DFDF296-DBEC-4FB4-81D1-6A3438BCF4DE}", "Music", $"{SysPaths.imageres},-108"),
            Tuple.Create("{24AD3AD4-A569-4530-98E1-AB02F9417AA8}", "Pictures", $"{SysPaths.imageres},-113"),
            Tuple.Create("{F86FA3AB-70D2-4FC7-9C99-FCBF05467F3A}", "Videos", $"{SysPaths.imageres},-189"),
            Tuple.Create("{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}", "3D Objects", $"{SysPaths.imageres},-198"),
            Tuple.Create("{679f85cb-0220-4080-b29b-5540cc05aab6}", "Quick Access", $"{SysPaths.System32}\\shell32.dll,-51380"),
        ];

        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = false;

        /// <summary>
        /// List of Shell32 wrapper data: (value, data)
        /// <br><c>value</c> is the index of icon in real <c>shell32.dll</c> in your system</br>
        /// <br><c>data</c> is the deflected icon path. If File is a PE File <c>(*.exe,*.dll,...)</c>, you can specify index by '<c>File.dll,i</c>' without quotes where <c>i</c> is the index of icon insie this PE File.</br>
        /// </summary>
        public Dictionary<string, string> Shell32Wrapper = [];

        /// <summary>
        /// List of Control Panel wrapper data: (value, data)
        /// <br><c>value</c> is the CLSID of Control Panel item</br>
        /// <br><c>data</c> is the deflected icon path. If File is a PE File <c>(*.exe,*.dll,...)</c>, you can specify index by '<c>File.dll,i</c>' without quotes where <c>i</c> is the index of icon insie this PE File.</br>
        /// </summary>
        public Dictionary<string, string> ControlPanelWrapper = [];

        /// <summary>
        /// List of Explorer icons wrapper data: (value, data)
        /// <br><c>value</c> is the CLSID of Explorer item</br>
        /// <br><c>data</c> is the deflected icon path. If File is a PE File <c>(*.exe,*.dll,...)</c>, you can specify index by '<c>File.dll,i</c>' without quotes where <c>i</c> is the index of icon insie this PE File.</br>
        /// </summary>
        public Dictionary<string, string> ExplorerWrapper = [];

        /// <summary>
        /// Path to Computer icon
        /// </summary>
        public string Computer = DesktopCLSIDs.ElementAt(0).Item3;

        /// <summary>
        /// Path to Control Panel icon
        /// </summary>
        public string ControlPanel = DesktopCLSIDs.ElementAt(1).Item3;

        /// <summary>
        /// Path to Network icon
        /// </summary>
        public string Network = DesktopCLSIDs.ElementAt(2).Item3;

        /// <summary>
        /// Path to User icon
        /// </summary>
        public string User = DesktopCLSIDs.ElementAt(3).Item3;

        /// <summary>
        /// Path to Recycle Bin empty icon
        /// </summary>
        public string RecycleBinEmpty = DesktopCLSIDs.ElementAt(4).Item3.Split('|')[0];

        /// <summary>
        /// Path to Recycle Bin full icon
        /// </summary>
        public string RecycleBinFull = DesktopCLSIDs.ElementAt(4).Item3.Split('|')[1];

        /// <summary>
        /// Hide Computer icon in desktop
        /// </summary>
        public bool Computer_HideInDesktop = true;

        /// <summary>
        /// Hide Control Panel icon in desktop
        /// </summary>
        public bool ControlPanel_HideInDesktop = true;

        /// <summary>
        /// Hide Network icon in desktop
        /// </summary>
        public bool Network_HideInDesktop = true;

        /// <summary>
        /// Hide User icon in desktop
        /// </summary>
        public bool User_HideInDesktop = true;

        /// <summary>
        /// Hide Recycle Bin icon in desktop
        /// </summary>
        public bool RecycleBin_HideInDesktop = false;

        /// <summary>
        /// Icon used for system drive
        /// </summary>
        public string SystemDriveIcon = string.Empty;

        /// <summary>
        /// Creates a new Icons structure with default values
        /// </summary>
        public Icons() { }

        /// <summary>
        /// Loads Icons data from registry
        /// </summary>
        /// <param name="default">Default Icons data structure</param>
        public void Load(Icons @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows icons from registry.");

            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Icons", string.Empty, @default.Enabled));

            Shell32Wrapper = [];
            Shell32Wrapper.Clear();

            foreach (string value in GetValueNames("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Icons"))
            {
                object result = GetReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\explorer\\Shell Icons", value, null);
                if (result != null)
                {
                    Shell32Wrapper.Add(value, result.ToString());
                }
            }

            ControlPanelWrapper = [];
            ControlPanelWrapper.Clear();

            foreach (Tuple<string, string, string> item in ControlPanelCLSIDs)
            {
                if (RegKeyExists($"HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{item.Item1}\\DefaultIcon"))
                {
                    ControlPanelWrapper.Add(item.Item1, GetReg($"HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{item.Item1}\\DefaultIcon", string.Empty, string.Empty).ToString());
                }
            }

            ExplorerWrapper = [];
            ExplorerWrapper.Clear();

            foreach (Tuple<string, string, string> item in ExplorerCLSIDs)
            {
                if (RegKeyExists($"HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{item.Item1}\\DefaultIcon"))
                {
                    ExplorerWrapper.Add(item.Item1, GetReg($"HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{item.Item1}\\DefaultIcon", string.Empty, string.Empty).ToString());
                }
            }

            Computer = GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(0).Item1}\DefaultIcon", string.Empty, @default.Computer).ToString();
            ControlPanel = GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(1).Item1}\DefaultIcon", string.Empty, @default.ControlPanel).ToString();
            Network = GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(2).Item1}\DefaultIcon", string.Empty, @default.Network).ToString();
            User = GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(3).Item1}\DefaultIcon", string.Empty, @default.User).ToString();
            RecycleBinEmpty = GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(4).Item1}\DefaultIcon", "empty", @default.RecycleBinEmpty).ToString();
            RecycleBinFull = GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(4).Item1}\DefaultIcon", "full", @default.RecycleBinFull).ToString();

            Computer_HideInDesktop = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", DesktopCLSIDs.ElementAt(0).Item1, @default.Computer_HideInDesktop));
            ControlPanel_HideInDesktop = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", DesktopCLSIDs.ElementAt(1).Item1, @default.ControlPanel_HideInDesktop));
            Network_HideInDesktop = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", DesktopCLSIDs.ElementAt(2).Item1, @default.Network_HideInDesktop));
            User_HideInDesktop = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", DesktopCLSIDs.ElementAt(3).Item1, @default.User_HideInDesktop));
            RecycleBin_HideInDesktop = Convert.ToBoolean(GetReg(@$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", DesktopCLSIDs.ElementAt(4).Item1, @default.RecycleBin_HideInDesktop));

            SystemDriveIcon = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Icons", "SystemDrive", @default.SystemDriveIcon).ToString();
        }

        /// <summary>
        /// Saves Icons data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows icons into registry.");

            SaveToggleState(treeView);

            if (Enabled)
            {
                foreach (string value in GetValueNames("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Icons"))
                {
                    DelValue(treeView, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Icons", value);
                }

                foreach (KeyValuePair<string, string> item in Shell32Wrapper)
                {
                    EditReg(treeView, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Icons", item.Key, item.Value, RegistryValueKind.String);
                }

                foreach (Tuple<string, string, string> item in ControlPanelCLSIDs)
                {
                    if (RegKeyExists($"HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{item.Item1}\\DefaultIcon"))
                    {
                        DelKey(treeView, $"HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{item.Item1}\\DefaultIcon");
                    }
                }

                foreach (KeyValuePair<string, string> item in ControlPanelWrapper)
                {
                    EditReg(treeView, $"HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{item.Key}\\DefaultIcon", string.Empty, item.Value, RegistryValueKind.String);
                }

                foreach (Tuple<string, string, string> item in ExplorerCLSIDs)
                {
                    if (RegKeyExists($"HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{item.Item1}\\DefaultIcon"))
                    {
                        DelKey(treeView, $"HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{item.Item1}\\DefaultIcon");
                    }
                }

                foreach (KeyValuePair<string, string> item in ExplorerWrapper)
                {
                    EditReg(treeView, $"HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{item.Key}\\DefaultIcon", string.Empty, item.Value, RegistryValueKind.String);
                }

                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(0).Item1}\DefaultIcon", string.Empty, Computer, RegistryValueKind.String);
                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(1).Item1}\DefaultIcon", string.Empty, ControlPanel, RegistryValueKind.String);
                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(2).Item1}\DefaultIcon", string.Empty, Network, RegistryValueKind.String);
                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(3).Item1}\DefaultIcon", string.Empty, User, RegistryValueKind.String);
                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(4).Item1}\DefaultIcon", "empty", RecycleBinEmpty, RegistryValueKind.String);
                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(4).Item1}\DefaultIcon", string.Empty, RecycleBinFull, RegistryValueKind.String);
                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{DesktopCLSIDs.ElementAt(4).Item1}\DefaultIcon", "full", RecycleBinFull, RegistryValueKind.String);

                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", DesktopCLSIDs.ElementAt(0).Item1, Computer_HideInDesktop);
                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", DesktopCLSIDs.ElementAt(1).Item1, ControlPanel_HideInDesktop);
                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", DesktopCLSIDs.ElementAt(2).Item1, Network_HideInDesktop);
                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", DesktopCLSIDs.ElementAt(3).Item1, User_HideInDesktop);
                EditReg(treeView, @$"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", DesktopCLSIDs.ElementAt(4).Item1, RecycleBin_HideInDesktop);

                EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Icons", "SystemDrive", SystemDriveIcon, RegistryValueKind.String);
                string sysDrive = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);

                if (!string.IsNullOrWhiteSpace(SystemDriveIcon))
                {
                    EditReg($"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\DriveIcons\\{sysDrive}\\DefaultIcon", string.Empty, SystemDriveIcon, RegistryValueKind.String);
                }
                else
                {
                    DelKey($"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\DriveIcons\\{sysDrive}\\DefaultIcon");
                }
            }
        }

        /// <summary>
        /// Saves Icons toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Icons", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two Icons structures are equal</summary>
        public static bool operator ==(Icons First, Icons Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Icons structures are not equal</summary>
        public static bool operator !=(Icons First, Icons Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Icons structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Icons structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Icons structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
