using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WinPaletter.NativeMethods
{
    public class Shell32
    {
        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(int wEventId, int uFlags, int dwItem1, int dwItem2);
        [DllImport("Shell32.dll", EntryPoint = "SHDefExtractIconW")]
        public static extern int SHDefExtractIconW([MarshalAs(UnmanagedType.LPTStr)] string pszIconFile, int iIndex, uint uFlags, ref IntPtr phiconLarge, ref IntPtr phiconSmall, uint nIconSize);
        [DllImport("Shell32.dll", SetLastError = false)]
        public static extern int SHGetStockIconInfo(SHSTOCKICONID siid, SHGSI uFlags, ref SHSTOCKICONINFO psii);

        [DllImport("shell32.dll", EntryPoint = "#261", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void GetUserTilePath(
          string username,
          UInt32 whatever, // 0x80000000
          StringBuilder picpath, int maxLength);

        public static string GetUserTilePath(string username)
        {   // username: use null for current user
            var sb = new StringBuilder(1000);
            GetUserTilePath(username, 0x80000000, sb, sb.Capacity);
            return sb.ToString();
        }

        public static Image GetUserAccountPicture(string username)
        {
            string file = GetUserTilePath(username);
            if (System.IO.File.Exists(file))
            {
                return Image.FromFile(file);
            }
            else
            {
                return (Image)Color.Black.ToBitmap(new Size(128, 128));
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHSTOCKICONINFO
        {
            public uint cbSize;
            public IntPtr hIcon;
            public int iSysIconIndex;
            public int iIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szPath;
        }
        public enum SHSTOCKICONID
        {
            /// <summary>
            /// Blank document icon (Document Of a type With no associated application).
            /// </summary>
            DOCNOASSOC = 0,

            /// <summary>
            /// Application-associated document icon (Document Of a type With an associated application).
            /// </summary>
            DOCASSOC = 1,

            /// <summary>
            /// Generic application With no custom icon.
            /// </summary>
            APPLICATION = 2,

            /// <summary>
            /// Folder (generic unspecified state).
            /// </summary>
            FOLDER = 3,

            /// <summary>
            /// Folder (open).
            /// </summary>
            FOLDEROPEN = 4,

            /// <summary>
            /// 5.25-inch disk drive.
            /// </summary>
            DRIVE525 = 5,

            /// <summary>
            /// 3.5-inch disk drive.
            /// </summary>
            DRIVE35 = 6,

            /// <summary>
            /// Removable drive.
            /// </summary>
            DRIVEREMOVE = 7,

            /// <summary>
            /// Fixed drive (hard disk).
            /// </summary>
            DRIVEFIXED = 8,

            /// <summary>
            /// Network drive (connected).
            /// </summary>
            DRIVENET = 9,

            /// <summary>
            /// Network drive (disconnected).
            /// </summary>
            DRIVENETDISABLED = 10,

            /// <summary>
            /// CD drive.
            /// </summary>
            DRIVECD = 11,

            /// <summary>
            /// RAM disk drive.
            /// </summary>
            DRIVERAM = 12,

            /// <summary>
            /// The entire network.
            /// </summary>
            WORLD = 13,

            /// <summary>
            /// A computer On the network.
            /// </summary>
            SERVER = 15,

            /// <summary>
            /// A local printer Or print destination.
            /// </summary>
            PRINTER = 16,

            /// <summary>
            /// The Network virtual folder (FOLDERID_NetworkFolder/CSIDL_NETWORK).
            /// </summary>
            MYNETWORK = 17,

            /// <summary>
            /// The Search feature.
            /// </summary>
            FIND = 22,

            /// <summary>
            /// The Help And Support feature.
            /// </summary>
            HELP = 23,

            /// <summary>
            /// Overlay For a Shared item.
            /// </summary>
            SHARE = 28,

            /// <summary>
            /// Overlay For a shortcut.
            /// </summary>
            LINK = 29,

            /// <summary>
            /// Overlay For items that are expected To be slow To access.
            /// </summary>
            SLOWFILE = 30,

            /// <summary>
            /// The Recycle Bin (empty).
            /// </summary>
            RECYCLER = 31,

            /// <summary>
            /// The Recycle Bin (Not empty).
            /// </summary>
            RECYCLERFULL = 32,

            /// <summary>
            /// Audio CD media.
            /// </summary>
            MEDIACDAUDIO = 40,

            /// <summary>
            /// Security lock.
            /// </summary>
            LOCK = 47,

            /// <summary>
            /// A virtual folder that contains the results Of a search.
            /// </summary>
            AUTOLIST = 49,

            /// <summary>
            /// A network printer.
            /// </summary>
            PRINTERNET = 50,

            /// <summary>
            /// A server Shared On a network.
            /// </summary>
            SERVERSHARE = 51,

            /// <summary>
            /// A local fax printer.
            /// </summary>
            PRINTERFAX = 52,

            /// <summary>
            /// A network fax printer.
            /// </summary>
            PRINTERFAXNET = 53,

            /// <summary>
            /// A file that receives the output Of a Print To file operation.
            /// </summary>
            PRINTERFILE = 54,

            /// <summary>
            /// A category that results from a Stack by command To organize the contents Of a folder.
            /// </summary>
            STACK = 55,

            /// <summary>
            /// Super Video CD (SVCD) media.
            /// </summary>
            MEDIASVCD = 56,

            /// <summary>
            /// A folder that contains only subfolders As child items.
            /// </summary>
            STUFFEDFOLDER = 57,

            /// <summary>
            /// Unknown drive type.
            /// </summary>
            DRIVEUNKNOWN = 58,

            /// <summary>
            /// DVD drive.
            /// </summary>
            DRIVEDVD = 59,

            /// <summary>
            /// DVD media.
            /// </summary>
            MEDIADVD = 60,

            /// <summary>
            /// DVD-RAM media.
            /// </summary>
            MEDIADVDRAM = 61,

            /// <summary>
            /// DVD-RW media.
            /// </summary>
            MEDIADVDRW = 62,

            /// <summary>
            /// DVD-R media.
            /// </summary>
            MEDIADVDR = 63,

            /// <summary>
            /// DVD-ROM media.
            /// </summary>
            MEDIADVDROM = 64,

            /// <summary>
            /// CD+ (enhanced audio CD) media.
            /// </summary>
            MEDIACDAUDIOPLUS = 65,

            /// <summary>
            /// CD-RW media.
            /// </summary>
            MEDIACDRW = 66,

            /// <summary>
            /// CD-R media.
            /// </summary>
            MEDIACDR = 67,

            /// <summary>
            /// A writeable CD In the process Of being burned.
            /// </summary>
            MEDIACDBURN = 68,

            /// <summary>
            /// Blank writable CD media.
            /// </summary>
            MEDIABLANKCD = 69,

            /// <summary>
            /// CD-ROM media.
            /// </summary>
            MEDIACDROM = 70,

            /// <summary>
            /// An audio file.
            /// </summary>
            AUDIOFILES = 71,

            /// <summary>
            /// An image file.
            /// </summary>
            IMAGEFILES = 72,

            /// <summary>
            /// A video file.
            /// </summary>
            VIDEOFILES = 73,

            /// <summary>
            /// A mixed file.
            /// </summary>
            MIXEDFILES = 74,

            /// <summary>
            /// Folder back.
            /// </summary>
            FOLDERBACK = 75,

            /// <summary>
            /// Folder front.
            /// </summary>
            FOLDERFRONT = 76,

            /// <summary>
            /// Security shield. Use For UAC prompts only.
            /// </summary>
            SHIELD = 77,

            /// <summary>
            /// Warning.
            /// </summary>
            WARNING = 78,

            /// <summary>
            /// Informational.
            /// </summary>
            INFO = 79,

            /// <summary>
            /// Error.
            /// </summary>
            Error = 80,

            /// <summary>
            /// Key.
            /// </summary>
            KEY = 81,

            /// <summary>
            /// Software.
            /// </summary>
            SOFTWARE = 82,

            /// <summary>
            /// A UI item such As a button that issues a rename command.
            /// </summary>
            RENAME = 83,

            /// <summary>
            /// A UI item such As a button that issues a delete command.
            /// </summary>
            DELETE = 84,

            /// <summary>
            /// Audio DVD media.
            /// </summary>
            MEDIAAUDIODVD = 85,

            /// <summary>
            /// Movie DVD media.
            /// </summary>
            MEDIAMOVIEDVD = 86,

            /// <summary>
            /// Enhanced CD media.
            /// </summary>
            MEDIAENHANCEDCD = 87,

            /// <summary>
            /// Enhanced DVD media.
            /// </summary>
            MEDIAENHANCEDDVD = 88,

            /// <summary>
            /// High definition DVD media In the HD DVD format.
            /// </summary>
            MEDIAHDDVD = 89,

            /// <summary>
            /// High definition DVD media In the Blu-ray Disc™ format.
            /// </summary>
            MEDIABLURAY = 90,

            /// <summary>
            /// Video CD (VCD) media.
            /// </summary>
            MEDIAVCD = 91,

            /// <summary>
            /// DVD+R media.
            /// </summary>
            MEDIADVDPLUSR = 92,

            /// <summary>
            /// DVD+RW media.
            /// </summary>
            MEDIADVDPLUSRW = 93,

            /// <summary>
            /// A desktop computer.
            /// </summary>
            DESKTOPPC = 94,

            /// <summary>
            /// A mobile computer (laptop).
            /// </summary>
            MOBILEPC = 95,

            /// <summary>
            /// The User Accounts Control PanelR item.
            /// </summary>
            USERS = 96,

            /// <summary>
            /// Smart media.
            /// </summary>
            MEDIASMARTMEDIA = 97,

            /// <summary>
            /// CompactFlash media.
            /// </summary>
            MEDIACOMPACTFLASH = 98,

            /// <summary>
            /// A cell phone.
            /// </summary>
            DEVICECELLPHONE = 99,

            /// <summary>
            /// A digital camera.
            /// </summary>
            DEVICECAMERA = 100,

            /// <summary>
            /// A digital video camera.
            /// </summary>
            DEVICEVIDEOCAMERA = 101,

            /// <summary>
            /// An audio player.
            /// </summary>
            DEVICEAUDIOPLAYER = 102,

            /// <summary>
            /// Connect To network.
            /// </summary>
            NETWORKCONNECT = 103,

            /// <summary>
            /// The Network And Internet Control PanelR item.
            /// </summary>
            INTERNET = 104,

            /// <summary>
            /// A compressed file With a .zip file name extension.
            /// </summary>
            ZIPFILE = 105,

            /// <summary>
            /// The Additional Options Control PanelR item.
            /// </summary>
            SETTINGS = 106,

            /// <summary>
            /// High definition DVD drive (any type - HD DVD-ROM HD DVD-R HD-DVD-RAM) that uses the HD DVD format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            DRIVEHDDVD = 132,

            /// <summary>
            /// High definition DVD drive (any type - BD-ROM BD-R BD-RE) that uses the Blu-ray Disc format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            DRIVEBD = 133,

            /// <summary>
            /// High definition DVD-ROM media In the HD DVD-ROM format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIAHDDVDROM = 134,

            /// <summary>
            /// High definition DVD-R media In the HD DVD-R format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIAHDDVDR = 135,

            /// <summary>
            /// High definition DVD-RAM media In the HD DVD-RAM format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIAHDDVDRAM = 136,

            /// <summary>
            /// High definition DVD-ROM media In the Blu-ray Disc BD-ROM format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIABDROM = 137,

            /// <summary>
            /// High definition write-once media In the Blu-ray Disc BD-R format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIABDR = 138,

            /// <summary>
            /// High definition read/write media In the Blu-ray Disc BD-RE format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIABDRE = 139,

            /// <summary>
            /// A cluster disk array.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            CLUSTEREDDRIVE = 140,

            /// <summary>
            /// The highest valid value In the enumeration. Values over 160 are Windows 7-only icons.
            /// </summary>
            MAX_ICONS = 174
        }
        [Flags]
        public enum SHGSI
        {
            ICONLOCATION = 0,
            ICON = 0x100,
            SYSICONINDEX = 0x4000,
            LINKOVERLAY = 0x8000,
            SELECTED = 0x10000,
            LARGEICON = 0x0,
            SMALLICON = 0x1,
            SHELLICONSIZE = 0x4
        }

        public const int MAX_PATH = 260;
        public const int SHCNE_ASSOCCHANGED = 0x8000000;
        public const int SHCNF_IDLIST = 0;
    }
}
