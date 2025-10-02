using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides utility methods for interacting with system resources, window appearance, and audio playback.
    /// </summary>
    /// <remarks>The <see cref="Helpers"/> class contains static methods for performing various tasks, such as
    /// extracting icons, modifying window appearance, and controlling audio playback. These methods leverage
    /// platform-specific APIs (e.g., User32, Shell32, DWMAPI) and are primarily designed for use on Windows operating
    /// systems.</remarks>
    public class Helpers
    {
        #region User32\Shell32

        /// <summary>
        /// Extracts a small icon from a File.
        /// </summary>
        /// <param name="Path">The path of the File.</param>
        /// <param name="IconIndex">Optional index of the icon in the File. Default is 0.</param>
        /// <returns>An Icon object representing the extracted small icon.</returns>
        public static object ExtractSmallIcon(string Path, int IconIndex = 0)
        {
            // Create a null Icon object.
            Icon ico = null;

            // Make the nIconSize value (See the Msdn documents). 
            // The LOWORD is the Large Icon Size. The HIWORD is the Small Icon Size.
            // The largest size for an icon is 256.
            uint LargeAndSmallSize = 256 << 16 | 16 & 0xFFFF;

            // Initialize handles for large and small icons.
            IntPtr hLrgIcon = IntPtr.Zero;
            IntPtr hSmlIcon = IntPtr.Zero;

            // Call the SHDefExtractIconW function to extract icons.
            int result = Shell32.SHDefExtractIconW(Path, IconIndex, 0U, ref hLrgIcon, ref hSmlIcon, LargeAndSmallSize);

            // Check if the extraction was successful (result == 0).
            if (result == 0)
            {
                // Dispose the existing Icon if not null.
                if (ico is not null)
                    ico.Dispose();

                // If the small icon was created in unmanaged memory, clone it to managed memory and then delete the unmanaged icon.
                if (hSmlIcon != IntPtr.Zero)
                {
                    ico = (Icon)Icon.FromHandle(hSmlIcon).Clone();
                    User32.DestroyIcon(hSmlIcon);
                }
            }

            // Return the extracted small icon.
            return ico;
        }

        /// <summary>
        /// Gets a system icon using Shell32.SHSTOCKICONID and Shell32.SHGSI values.
        /// </summary>
        /// <param name="_Icon">The SHSTOCKICONID representing the desired system icon.</param>
        /// <param name="_Type">The SHGSI value specifying the type of system icon to retrieve.</param>
        /// <returns>An Icon object representing the system icon.</returns>
        public static Icon GetSystemIcon(Shell32.SHSTOCKICONID _Icon, Shell32.SHGSI _Type)
        {
            try
            {
                // Initialize a SHSTOCKICONINFO structure.
                Shell32.SHSTOCKICONINFO sii = new() { cbSize = (uint)Marshal.SizeOf(typeof(Shell32.SHSTOCKICONINFO)) };

                // Call SHGetStockIconInfo to get system icon information.
                Shell32.SHGetStockIconInfo(_Icon, _Type, ref sii);

                // Check if the icon handle is not null and not zero.
                if (sii.hIcon != null && sii.hIcon != IntPtr.Zero)
                {
                    // Return an Icon object created from the icon handle.
                    return Icon.FromHandle(sii.hIcon);
                }
                else
                {
                    // Return null if the icon handle is null or zero.
                    return null;
                }
            }
            catch
            {
                // Return null in case of any exceptions.
                return null;
            }
        }
        #endregion

        #region DWMAPI

        /// <summary>
        /// Configures the title bar of a window to use dark mode or light mode, depending on the specified setting.
        /// </summary>
        /// <remarks>This method adjusts the appearance of the title bar and, on supported systems, the
        /// window border and backdrop type. It is only effective on Windows 10 and later versions. On earlier versions
        /// of Windows, the method has no effect.</remarks>
        /// <param name="hWnd">A handle to the window whose title bar appearance is being modified.</param>
        /// <param name="darkMode">A boolean value indicating whether to enable dark mode.  <see langword="true"/> enables dark mode; <seelangword="false"/> enables light mode.</param>
        /// <param name="wholeWindow">A boolean value indicating whether to apply the dark mode setting to the entire window. Default is <see langword="true"/>.</param>
        public static void SetHWNDDarkMode(IntPtr hWnd, bool darkMode, bool wholeWindow = true)
        {
            // Check if the operating system is Windows XP, Vista, 7, 8, or 8.1
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) return;

            // Determine if custom styling is applicable based on program settings and operating system
            bool useRoundedCorners = Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors && !OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81 && !OS.W10;
            int attributeValue = darkMode ? 1 : 0;
            int backdropType = OS.W10 ? 3 : 2;

            // Make the form have rounded corners if the operating system is Windows 11 or 12
            // It should be used as a fallback for the custom styling. Make both start by 'If' statement, not 'Else If'
            if (OS.W12 || OS.W11)
            {
                int argpvAttribute = (int)DWMAPI.FormCornersType.Default;
                DWMAPI.DwmSetWindowAttribute(hWnd, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));

                // Apply rectangular window corners if custom styling is enabled and rounded corners are disabled
                // Make both start by 'If' statement, not 'Else If'
                if (useRoundedCorners && !Program.Settings.Appearance.RoundedCorners)
                {
                    int argpvAttribute1 = (int)DWMAPI.FormCornersType.Rectangular;
                    DWMAPI.DwmSetWindowAttribute(hWnd, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                }
            }

            // Set the dark mode attribute for the titlebar
            DWMAPI.DwmSetWindowAttribute(hWnd, (int)DWMAPI.DWMWINDOWATTRIBUTE.USE_IMMERSIVE_DARK_MODE, ref attributeValue, Marshal.SizeOf<int>());
            if (OS.W10_1909_AndBelow) DWMAPI.DwmSetWindowAttribute(hWnd, (int)DWMAPI.DWMWINDOWATTRIBUTE.USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, ref attributeValue, Marshal.SizeOf<int>());

            if (!OS.W10)
            {
                // Set the dark mode attribute for the border
                DWMAPI.DwmSetWindowAttribute(hWnd, (int)DWMAPI.DWMWINDOWATTRIBUTE.MICA_EFFECT, ref attributeValue, Marshal.SizeOf<int>());
            }

            DWMAPI.DwmSetWindowAttribute(hWnd, (int)DWMAPI.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, ref backdropType, Marshal.SizeOf<int>());

            if (wholeWindow) SetControlTheme(hWnd, Program.Style.DarkMode ? CtrlTheme.DarkExplorer : CtrlTheme.Default);
        }

        #endregion

        #region UxTheme
        /// <summary>
        /// Removes the titlebar text and icon from a specified form.
        /// </summary>
        /// <param name="handle">The handle of the form.</param>
        public static void RemoveFormTitlebarTextAndIcon(IntPtr handle)
        {
            // Set the non-client rendering options to remove the titlebar text and icon
            UxTheme.WTA_OPTIONS options = new()
            {
                Flags = UxTheme.WTNCA_NODRAWCAPTION | UxTheme.WTNCA_NODRAWICON,
                Mask = UxTheme.WTNCA_NODRAWCAPTION | UxTheme.WTNCA_NODRAWICON
            };

            UxTheme.SetWindowThemeAttribute(handle, UxTheme.WindowThemeAttributeType.WTA_NONCLIENT, ref options, (uint)Marshal.SizeOf(options));
        }
        #endregion

        #region Winmm
        /// <summary>
        /// Plays an audio File.
        /// </summary>
        /// <param name="file">The path to the audio File.</param>
        public static void PlayAudio(string file)
        {
            if (File.Exists(file))
            {
                // Close any existing audio player
                Winmm.mciSendString("close myWAV", null, 0, IntPtr.Zero);

                // Open the specified audio File
                Winmm.mciSendString($"open \"{file}\" type mpegvideo alias myWAV", null, 0, IntPtr.Zero);

                // Play the audio File
                Winmm.mciSendString("play myWAV", null, 0, IntPtr.Zero);

                // Set the volume to maximum
                int volume = 1000; // Sets it to use the entire range of volume control
                Winmm.mciSendString($"setaudio myWAV volume to {volume}", null, 0, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Stops playing the currently playing audio.
        /// </summary>
        public static void StopAudio()
        {
            // Seek to the start of the audio File
            Winmm.mciSendString("seek myWAV to start", null, 0, IntPtr.Zero);

            // Stop playing the audio File
            Winmm.mciSendString("stop myWAV", null, 0, IntPtr.Zero);
        }
        #endregion
    }
}