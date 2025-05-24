using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for Shell32 (Windows Shell) functions.
    /// </summary>
    public class Shell32
    {
        /// <summary>
        /// Extracts the icon associated with a File.
        /// </summary>
        /// <param name="szFileName"></param>
        /// <param name="nIconIndex"></param>
        /// <param name="phiconLarge"></param>
        /// <param name="phiconSmall"></param>
        /// <param name="nIcons"></param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern uint ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        /// <summary>
        /// Retrieves information about icons or icon-sized images from a specified File.
        /// </summary>
        /// <param name="lpszFile">The name of the File that contains the icon or icon-sized image.</param>
        /// <param name="nIconIndex">The zero-based index of the icon or image.</param>
        /// <param name="cxIcon">The desired width of the icon in pixels. The function uses this value to create a monochrome icon of the desired width.</param>
        /// <param name="cyIcon">The desired height of the icon in pixels. The function uses this value to create a monochrome icon of the desired height.</param>
        /// <param name="phicon">An array of icon or image handles to be filled by this function. The array should be pre-allocated to hold at least <paramref name="nIcons"/> elements.</param>
        /// <param name="piconid">An array of icon IDs. This parameter can be <see langword="null"/>.</param>
        /// <param name="nIcons">The number of icons to extract. This value is limited to the number of image bits in the icon resource.</param>
        /// <param name="flags">A combination of flags that specify the dimensions and behavior of the function.</param>
        /// <returns>The number of icons successfully extracted, or zero if the function fails.</returns>
        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint PrivateExtractIcons(string lpszFile, int nIconIndex, int cxIcon, int cyIcon, IntPtr[] phicon, uint[] piconid, uint nIcons, uint flags);

        /// <summary>
        /// Dispose (destroy) an icon handle.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool DestroyIcon(IntPtr handle);

        /// <summary>
        /// Hide default Windows icon picker dialog.
        /// </summary>
        /// <param name="hwndOwner"></param>
        /// <param name="lpstrFile"></param>
        /// <param name="nMaxFile"></param>
        /// <param name="lpdwIconIndex"></param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int PickIconDlg(IntPtr hwndOwner, StringBuilder lpstrFile, int nMaxFile, ref int lpdwIconIndex);

        /// <summary>
        /// Notifies the system of an event that an application has performed.
        /// </summary>
        /// <param name="wEventId">The event that has occurred.</param>
        /// <param name="uFlags">Flags that indicate the meaning of the dwItem1 and dwItem2 parameters.</param>
        /// <param name="dwItem1">First event-dependent value.</param>
        /// <param name="dwItem2">Second event-dependent value.</param>
        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(int wEventId, int uFlags, int dwItem1, int dwItem2);

        /// <summary>
        /// Retrieves the index and handles of stock icons.
        /// </summary>
        /// <param name="pszIconFile">The path of the File that contains the icon.</param>
        /// <param name="iIndex">The index of the icon in the File.</param>
        /// <param name="uFlags">A combination of flags that specify which information to retrieve.</param>
        /// <param name="phiconLarge">A pointer to the handle of the large icon.</param>
        /// <param name="phiconSmall">A pointer to the handle of the small icon.</param>
        /// <param name="nIconSize">The size of the icon.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport("Shell32.dll", EntryPoint = "SHDefExtractIconW")]
        public static extern int SHDefExtractIconW([MarshalAs(UnmanagedType.LPTStr)] string pszIconFile, int iIndex, uint uFlags, ref IntPtr phiconLarge, ref IntPtr phiconSmall, uint nIconSize);

        /// <summary>
        /// Retrieves information about a stock icon.
        /// </summary>
        /// <param name="siid">The identifier of the stock icon.</param>
        /// <param name="uFlags">A combination of flags that specify which information to retrieve.</param>
        /// <param name="psii">A pointer to a SHSTOCKICONINFO structure that receives the icon information.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport("shell32.dll", SetLastError = false)]
        public static extern int SHGetStockIconInfo(SHSTOCKICONID siid, SHGSI uFlags, ref SHSTOCKICONINFO psii);

        /// <summary>
        /// Retrieves the path to the user's account picture.
        /// </summary>
        /// <param name="username">The username of the user. Use null for the current user.</param>
        /// <param name="whatever">Reserved. Must be 0x80000000.</param>
        /// <param name="picpath">A StringBuilder that receives the path to the user's account picture.</param>
        /// <param name="maxLength">The maximum length of the Buffer pointed to by picpath.</param>
        [DllImport("shell32.dll", EntryPoint = "#261", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void GetUserTilePath(string username, UInt32 whatever, StringBuilder picpath, int maxLength);

        /// <summary>
        /// Gets the path to the user's account picture.
        /// </summary>
        /// <param name="username">The username of the user. Use null for the current user.</param>
        /// <returns>The path to the user's account picture.</returns>
        public static string GetUserTilePath(string username)
        {
            if (!OS.WXP)
            {
                StringBuilder sb = new(1000);
                GetUserTilePath(username, 0x80000000, sb, sb.Capacity);
                return sb.ToString();
            }
            else
            {
                string file = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\Microsoft\\User Account Pictures\\{username}.bmp";
                string @default = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\Microsoft\\User Account Pictures\\Default Pictures\\chess.bmp";
                return System.IO.File.Exists(file) ? file : @default;
            }
        }

        /// <summary>
        /// Gets the user's account picture as an Image.
        /// </summary>
        /// <param name="username">The username of the user. Use null for the current user.</param>
        /// <returns>The user's account picture as an Image.</returns>
        public static Image GetUserAccountPicture(string username)
        {
            string file = GetUserTilePath(username);

            if (System.IO.File.Exists(file))
            {
                return Image.FromFile(file);
            }
            else
            {
                return Color.Black.ToBitmap(new Size(128, 128));
            }
        }

        /// <summary>
        /// Struct that contains information about a stock icon.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHSTOCKICONINFO
        {
            /// <summary>
            /// The size of this structure.
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// A handle to the icon.
            /// </summary>
            public IntPtr hIcon;

            /// <summary>
            /// The system icon index.
            /// </summary>
            public int iSysIconIndex;

            /// <summary>
            /// The icon index.
            /// </summary>
            public int iIcon;

            /// <summary>
            /// The path to the icon File.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ShellConstants.MAX_PATH)]
            public string szPath;
        }

        /// <summary>
        /// Represents identifiers for stock icons used by the Shell.
        /// </summary>
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
            /// A File that receives the output Of a Print To File operation.
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
            /// An audio File.
            /// </summary>
            AUDIOFILES = 71,

            /// <summary>
            /// An image File.
            /// </summary>
            IMAGEFILES = 72,

            /// <summary>
            /// A video File.
            /// </summary>
            VIDEOFILES = 73,

            /// <summary>
            /// A mixed File.
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
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            DRIVEHDDVD = 132,

            /// <summary>
            /// High definition DVD media In the Blu-ray Disc™ format.
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

        /// <summary>
        /// Flags used to specify which attributes to retrieve in the <see cref="SHSTOCKICONINFO"/> structure.
        /// </summary>
        [Flags]
        public enum SHGSI
        {
            /// <summary>
            /// Retrieves the icon location.
            /// </summary>
            ICONLOCATION = 0,

            /// <summary>
            /// Retrieves the icon.
            /// </summary>
            ICON = 0x100,

            /// <summary>
            /// Retrieves the system icon index.
            /// </summary>
            SYSICONINDEX = 0x4000,

            /// <summary>
            /// Retrieves the link overlay.
            /// </summary>
            LINKOVERLAY = 0x8000,

            /// <summary>
            /// Retrieves the selected state.
            /// </summary>
            SELECTED = 0x10000,

            /// <summary>
            /// Retrieves the large icon.
            /// </summary>
            LARGEICON = 0x0,

            /// <summary>
            /// Retrieves the small icon.
            /// </summary>
            SMALLICON = 0x1,

            /// <summary>
            /// Retrieves the shell icon size.
            /// </summary>
            SHELLICONSIZE = 0x4
        }

        /// <summary>
        /// Represents constants used in shell operations.
        /// </summary>
        public static class ShellConstants
        {
            /// <summary>
            /// Maximum path length.
            /// </summary>
            public const int MAX_PATH = 260;

            /// <summary>
            /// Shell change event associated with changes in File associations.
            /// </summary>
            public const int SHCNE_ASSOCCHANGED = 0x8000000;

            /// <summary>
            /// Identifier list flag used in shell notifications.
            /// </summary>
            public const int SHCNF_IDLIST = 0;
        }
    }
}
