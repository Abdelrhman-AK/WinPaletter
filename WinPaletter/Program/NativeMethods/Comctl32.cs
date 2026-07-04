using System;
using System.Runtime.InteropServices;
using static WinPaletter.DarkTaskDialog;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// A class that contains methods and constants for interacting with the Comctl32.dll library.
    /// </summary>
    public class Comctl32
    {
        private const string _comctl32 = "comctl32.dll";

        [DllImport(_comctl32, CharSet = CharSet.Unicode)]
        public static extern int TaskDialogIndirect(ref TASKDIALOGCONFIG pTaskConfig, out int pnButton, out int pnRadioButton, out bool pfVerificationFlagChecked);

        [DllImport(_comctl32)]
        public static extern bool SetWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, UIntPtr uIdSubclass, IntPtr dwRefData);

        [DllImport(_comctl32)]
        public static extern bool GetWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, UIntPtr uIdSubclass, out IntPtr pdwRefData);

        [DllImport(_comctl32)]
        public static extern bool RemoveWindowSubclass(IntPtr hWnd, SUBCLASSPROC pfnSubclass, UIntPtr uIdSubclass);

        [DllImport(_comctl32)]
        public static extern IntPtr DefSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Task Dialog pane: Main instruction pane
        /// </summary>
        public const int TDLG_MAININSTRUCTIONPANE = 1;

        /// <summary>
        /// Task Dialog pane: Content pane
        /// </summary>
        public const int TDLG_CONTENTPANE = 2;

        // Common button IDs
        public const int IDOK = 1;
        public const int IDCANCEL = 2;
        public const int IDABORT = 3;
        public const int IDRETRY = 4;
        public const int IDIGNORE = 5;
        public const int IDYES = 6;
        public const int IDNO = 7;
        public const int IDCLOSE = 8;
        public const int IDHELP = 9;
        public const int IDTRYAGAIN = 10;
        public const int IDCONTINUE = 11;

        /// <summary>
        /// TASKDIALOGCONFIG structure - MUST match exact Windows API binary layout
        /// 
        /// This is the correct layout from the official Windows SDK (comctl32.h)
        /// The key insight: this struct should be exactly 96 bytes on x64 (or 52 bytes on x86)
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct TASKDIALOGCONFIG
        {
            public uint cbSize;                                    // 4 bytes
            public IntPtr hwndParent;                              // 8 bytes (x64) / 4 bytes (x86)
            public IntPtr hInstance;                               // 8 bytes (x64) / 4 bytes (x86)
            public TaskDialogFlags dwFlags;                        // 4 bytes
            public TaskDialogCommonButtonFlags dwCommonButtons;    // 4 bytes
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszWindowTitle;                          // 8 bytes (x64) / 4 bytes (x86)
            public IntPtr hMainIcon;                               // 8 bytes (x64) / 4 bytes (x86) - UNION with pszMainIcon
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszMainInstruction;                      // 8 bytes (x64) / 4 bytes (x86)
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszContent;                              // 8 bytes (x64) / 4 bytes (x86)
            public uint cButtons;                                  // 4 bytes
            public IntPtr pButtons;                                // 8 bytes (x64) / 4 bytes (x86)
            public int nDefaultButton;                             // 4 bytes
            public uint cRadioButtons;                             // 4 bytes
            public IntPtr pRadioButtons;                           // 8 bytes (x64) / 4 bytes (x86)
            public int nDefaultRadioButton;                        // 4 bytes
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszVerificationText;                     // 8 bytes (x64) / 4 bytes (x86)
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszExpandedInformation;                  // 8 bytes (x64) / 4 bytes (x86)
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszExpandedControlText;                  // 8 bytes (x64) / 4 bytes (x86)
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszCollapsedControlText;                 // 8 bytes (x64) / 4 bytes (x86)
            public IntPtr hFooterIcon;                             // 8 bytes (x64) / 4 bytes (x86) - UNION with pszFooterIcon
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszFooter;                               // 8 bytes (x64) / 4 bytes (x86)
            public TaskDialogCallback pfCallback;                  // 8 bytes (x64) / 4 bytes (x86)
            public IntPtr lpCallbackData;                          // 8 bytes (x64) / 4 bytes (x86)
            public uint cxWidth;                                   // 4 bytes
        }

        /// <summary>
        /// <see cref="TASKDIALOGCONFIG"/> view helper. Thin accessor over an existing TASKDIALOGCONFIG native buffer.
        /// Uses the properly defined TASKDIALOGCONFIG struct from Comctl32.
        /// </summary>
        public sealed class TaskDialogConfigView
        {
            public const int TDF_EXPANDED_BY_DEFAULT = 0x0080;
            public const int TDF_VERIFICATION_FLAG_CHECKED = 0x0100;
            public const int TDF_USE_HICON_MAIN = 0x0002;
            public const int TDF_USE_HICON_FOOTER = 0x0004;

            public const long TD_WARNING_ICON = unchecked((long)(ushort)0xFFFF);
            public const long TD_ERROR_ICON = unchecked((long)(ushort)0xFFFE);
            public const long TD_INFORMATION_ICON = unchecked((long)(ushort)0xFFFD);
            public const long TD_SHIELD_ICON = unchecked((long)(ushort)0xFFFC);

            public int dwFlags;
            public IntPtr hMainIcon;
            public IntPtr hFooterIcon;
            public IntPtr pszMainIcon;
            public IntPtr pszFooterIcon;

            public static TaskDialogConfigView FromPointer(IntPtr pCfg)
            {
                // Marshal the native buffer to the managed struct
                var config = Marshal.PtrToStructure<TASKDIALOGCONFIG>(pCfg);

                TaskDialogConfigView view = new()
                {
                    dwFlags = (int)config.dwFlags,
                    hMainIcon = config.hMainIcon,
                    hFooterIcon = config.hFooterIcon,
                    pszMainIcon = config.hMainIcon,   // Union: points to string if not using HICON
                    pszFooterIcon = config.hFooterIcon // Union: points to string if not using HICON
                };

                return view;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TASKDIALOG_BUTTON
        {
            public int nButtonID;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszButtonText;
        }

        public delegate IntPtr TaskDialogCallback(
            IntPtr hwnd,
            uint uNotification,
            IntPtr wParam,
            IntPtr lParam,
            IntPtr lpRefData);

        [Flags]
        public enum TaskDialogFlags : uint
        {
            EnableHyperlinks = 0x0001,
            UseHIconMain = 0x0002,
            UseHIconFooter = 0x0004,
            AllowDialogCancellation = 0x0008,
            UseCommandLinks = 0x0010,
            UseCommandLinksNoIcon = 0x0020,
            ExpandFooterArea = 0x0040,
            ExpandedByDefault = 0x0080,
            VerificationFlagChecked = 0x0100,
            ShowProgressBar = 0x0200,
            ShowMarqueeProgressBar = 0x0400,
            CallbackTimer = 0x0800,
            PositionRelativeToWindow = 0x1000,
            RTLLayout = 0x2000,
            NoDefaultRadioButton = 0x4000,
            CanBeMinimized = 0x8000,
        }

        [Flags]
        public enum TaskDialogCommonButtonFlags : uint
        {
            OK = 0x0001,
            Yes = 0x0002,
            No = 0x0004,
            Cancel = 0x0008,
            Retry = 0x0010,
            Close = 0x0020,
            YesNo = Yes | No,
            OKCancel = OK | Cancel,
            RetryCancel = Retry | Cancel,
        }

        public enum TaskDialogIcon : int
        {
            None = 0,
            Warning = (int)0xFFFF,     // TD_WARNING_ICON
            Error = (int)0xFFFE,       // TD_ERROR_ICON
            Information = (int)0xFFFD, // TD_INFORMATION_ICON
            Shield = (int)0xFFFC,      // TD_SHIELD_ICON
        }

        public static class TaskDialogNotification
        {
            public const uint Created = 0;             // TDN_CREATED
            public const uint Navigated = 1;           // TDN_NAVIGATED
            public const uint ButtonClicked = 2;       // TDN_BUTTON_CLICKED
            public const uint HyperlinkClicked = 3;    // TDN_HYPERLINK_CLICKED
            public const uint Timer = 4;               // TDN_TIMER
            public const uint Destroyed = 5;           // TDN_DESTROYED
            public const uint RadioButtonClicked = 6;  // TDN_RADIO_BUTTON_CLICKED
            public const uint DialogConstructed = 7;   // TDN_DIALOG_CONSTRUCTED
            public const uint VerificationClicked = 8; // TDN_VERIFICATION_CLICKED
            public const uint ExpandButtonClicked = 9; // TDN_EXPANDO_BUTTON_CLICKED
        }
    }
}