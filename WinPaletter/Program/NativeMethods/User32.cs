﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    public partial class User32
    {
        [DllImport("user32.dll")]
        public static extern int LoadCursor(int hInstance, int lpCursorName);

        [DllImport("user32.dll")]
        public static extern int SetCursor(int hCursor);

        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);

        [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern bool SetSysColors(int cElements, int[] lpaElements, uint[] lpaRgbValues);

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "LoadCursorFromFileA")]
        public static extern IntPtr LoadCursorFromFile(string lpFileName);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern int SetBkColor(IntPtr hDC, int crColor);

        [DllImport("gdi32.dll")]
        public static extern int SetTextColor(IntPtr hDC, int crColor);

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }
        public enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19,
            WCA_USEDARKMODECOLORS = 26
        }
        internal enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_TRANSPARANT = 6,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
        }

        [Flags]
        public enum AnimateWindowFlags
        {
            AW_HOR_POSITIVE = 0x0,
            AW_HOR_NEGATIVE = 0x2,
            AW_VER_POSITIVE = 0x4,
            AW_VER_NEGATIVE = 0x8,
            AW_CENTER = 0x10,
            AW_HIDE = 0x10000,
            AW_ACTIVATE = 0x20000,
            AW_SLIDE = 0x40000,
            AW_BLEND = 0x80000
        }
        public enum OCR_SYSTEM_CURSORS : int
        {

            // Standard arrow And small hourglass
            OCR_APPSTARTING = 32650,

            // Standard arrow
            OCR_NORMAL = 32512,

            // Crosshair
            OCR_CROSS = 32515,

            // Windows 2000/XP: Hand
            OCR_HAND = 32649,

            // Arrow And question mark
            OCR_HELP = 32651,

            // I-beam
            OCR_IBEAM = 32513,

            // Slashed circle
            OCR_NO = 32648,

            // Four-pointed arrow pointing north south east And west
            OCR_SIZEALL = 32646,

            // Double-pointed arrow pointing northeast And southwest
            OCR_SIZENESW = 32643,

            // Double-pointed arrow pointing north And south
            OCR_SIZENS = 32645,

            // Double-pointed arrow pointing northwest And southeast
            OCR_SIZENWSE = 32642,

            // Double-pointed arrow pointing west And east
            OCR_SIZEWE = 32644,

            // Vertical arrow
            OCR_UP = 32516,

            // Hourglass
            OCR_WAIT = 32514
        }
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0U,
            SMTO_BLOCK = 0x1U,
            SMTO_ABORTIFHUNG = 0x2U,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8U
        }

        public static int WM_DWMCOLORIZATIONCOLORCHANGED = 0x320;
        public static int WM_DWMCOMPOSITIONCHANGED = 0x31E;
        public static int WM_THEMECHANGED = 0x31A;
        public static int WM_SYSCOLORCHANGE = 0x15;
        public static int WM_PALETTECHANGED = 0x311;
        public static uint WM_WININICHANGE = 0x1AU;
        public static uint WM_SETTINGCHANGE = WM_WININICHANGE;
        public static int HWND_MESSAGE = -0x3;
        public static IntPtr HWND_BROADCAST = new IntPtr(0xFFFF);
        public static int MSG_TIMEOUT = 5000;
        public static UIntPtr RESULT;

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, IntPtr lParam);

        private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

        public static List<IntPtr> GetChildWindowHandles(System.Windows.Forms.IWin32Window win32Window)
        {
            List<IntPtr> childHandles = new List<IntPtr>();

            IntPtr hWndParent = win32Window.Handle;

            EnumChildProc childProc = (hWnd, lParam) =>
            {
                childHandles.Add(hWnd);
                return true;
            };

            EnumChildWindows(hWndParent, childProc, IntPtr.Zero);

            return childHandles;
        }
    }
}
