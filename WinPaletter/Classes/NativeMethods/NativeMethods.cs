using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Functions not found internally in system DLLs, but uses the functions in DLLs to do something DLLs Functions cannot do alone.
    /// </summary>
    public class DLLFunc
    {
        #region User32\Shell32
        private static int MAKEICONSIZE(int low, int high)
        {
            return high << 16 | low & 0xFFFF;
        }
        public static object ExtractSmallIcon(string Path, int IconIndex = 0)
        {
            Icon ico = null;
            // Make the nIconSize value (See the Msdn documents). The LOWORD is the Large Icon Size. The HIWORD is the Small Icon Size.
            // The largest size for an icon is 256.
            uint LargeAndSmallSize = (uint)MAKEICONSIZE(256, 16);

            var hLrgIcon = IntPtr.Zero;
            var hSmlIcon = IntPtr.Zero;

            int result = Shell32.SHDefExtractIconW(Path, IconIndex, 0U, ref hLrgIcon, ref hSmlIcon, LargeAndSmallSize);

            if (result == 0)
            {
                if (ico is not null)
                    ico.Dispose();

                // if the large and/or small icons where created in the unmanaged memory successfully then create
                // a clone of them in the managed icons and delete the icons in the unmanaged memory.

                if (hSmlIcon != IntPtr.Zero)
                {
                    ico = (Icon)Icon.FromHandle(hSmlIcon).Clone();
                    User32.DestroyIcon(hSmlIcon);
                }

            }

            return ico;
        }
        public static Icon GetSystemIcon(Shell32.SHSTOCKICONID _Icon, Shell32.SHGSI _Type)
        {
            try
            {
                var sii = new Shell32.SHSTOCKICONINFO() { cbSize = (uint)Marshal.SizeOf(typeof(Shell32.SHSTOCKICONINFO)) };
                Shell32.SHGetStockIconInfo(_Icon, _Type, ref sii);
                if (sii.hIcon != null && sii.hIcon != IntPtr.Zero)
                {
                    return Icon.FromHandle(sii.hIcon);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region DWMAPI
        public static void DarkTitlebar(IntPtr hWnd, bool DarkMode)
        {
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8 || OS.W81)
                return;

            int argattrValue = DarkMode ? 1 : 0;
            DWMAPI.DwmSetWindowAttribute(hWnd, 20, ref argattrValue, Marshal.SizeOf<int>());

            int argattrValue1 = DarkMode ? 1 : 0;
            DWMAPI.DwmSetWindowAttribute(hWnd, 19, ref argattrValue1, Marshal.SizeOf<int>());
        }
        #endregion

        #region UxTheme
        public static void RemoveFormTitlebarTextAndIcon(IntPtr Handle)
        {
            var ops = new UxTheme.WTA_OPTIONS()
            {
                Flags = UxTheme.WTNCA_NODRAWCAPTION | UxTheme.WTNCA_NODRAWICON,
                Mask = UxTheme.WTNCA_NODRAWCAPTION | UxTheme.WTNCA_NODRAWICON
            };

            UxTheme.SetWindowThemeAttribute(Handle, UxTheme.WindowThemeAttributeType.WTA_NONCLIENT, ref ops, (uint)Marshal.SizeOf(ops));
        }

        #endregion

        #region Winmm
        public static void PlayAudio(string File)
        {
            if (System.IO.File.Exists(File))
            {
                Winmm.mciSendString("close myWAV", null, 0, (IntPtr)0);
                Winmm.mciSendString("open \"" + File + "\" type mpegvideo alias myWAV", null, 0, (IntPtr)0);
                Winmm.mciSendString("play myWAV", null, 0, (IntPtr)0);
                int Volume = 1000; // Sets it to use entire range of volume control
                Winmm.mciSendString("setaudio myWAV volume to " + Volume.ToString(), null, 0, (IntPtr)0);
            }
        }

        public static void StopAudio()
        {
            Winmm.mciSendString("seek myWAV to start", null, 0, IntPtr.Zero);
            Winmm.mciSendString("stop myWAV", null, 0, IntPtr.Zero);
        }
        #endregion

    }
}