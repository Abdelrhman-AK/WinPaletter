using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// Functions that help you draw\drop special DWM effects (Tabbed\Mica\Acrylic\Aero) on a form
    /// </summary>
    public static class DWM
    {
        /// <summary>
        /// Applies a visual effect to a window, such as Mica, Acrylic, or Aero, based on the specified parameters and
        /// the operating system's capabilities.
        /// </summary>
        /// <remarks>The method determines the appropriate effect to apply based on the specified
        /// parameters and the operating system's version and settings. For example, Mica and Tabbed effects are
        /// available on Windows 11 and later, while Acrylic and Aero effects are supported on earlier versions. The
        /// method also respects the user's system transparency settings.</remarks>
        /// <param name="hwnd">The handle to the window to which the effect will be applied.</param>
        /// <param name="Margins">The margins defining the area of the window where the effect is applied. If not specified or set to default,
        /// the entire window is affected.</param>
        /// <param name="Border">A value indicating whether the window's border should be included in the effect. Defaults to <see
        /// langword="true"/>.</param>
        /// <param name="Style">The style of the backdrop effect to apply. Supported styles include Mica, Tabbed, Acrylic, and Aero.</param>
        /// <param name="useOldAcrylicMethod">A value indicating whether to use an older method for applying the Acrylic effect. This is relevant for
        /// compatibility with certain systems.</param>
        public static void DropEffect(IntPtr hwnd, Padding Margins = default, bool Border = true, BackdropStyles Style = BackdropStyles.Mica, bool useOldAcrylicMethod = false)
        {
            if (Margins == default || Margins == null || Margins == Padding.Empty || Margins == new Padding(0))
            {
                Margins = new(-1, -1, -1, -1);
            }

            bool CompositionEnabled = DWMAPI.IsCompositionEnabled();
            bool Transparency_W10x = (OS.W10 || OS.W11 || OS.W12) && ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", true);

            if ((OS.W12 || OS.W11) && Transparency_W10x)
            {
                switch (Style)
                {
                    case BackdropStyles.Mica:
                        {
                            DrawMica(hwnd, Margins, BackdropStyles.Mica);
                            return;
                        }

                    case BackdropStyles.Tabbed:
                        {
                            DrawMica(hwnd, Margins, BackdropStyles.Tabbed);
                            return;
                        }

                    case BackdropStyles.Acrylic:
                        {
                            DrawAcrylic(hwnd, Margins, Border, useOldAcrylicMethod);
                            return;
                        }

                    case BackdropStyles.Aero:
                        {
                            DrawAero(hwnd, Margins);
                            return;
                        }

                    case BackdropStyles.None:
                        {
                            DrawTransparentGray(hwnd);
                            return;
                        }

                    default:
                        {
                            DrawMica(hwnd, Margins, BackdropStyles.Mica);
                            return;
                        }
                }
            }

            else if (OS.W10 && Transparency_W10x)
            {
                switch (Style)
                {
                    case BackdropStyles.Acrylic:
                        {
                            DrawAcrylic(hwnd, Margins, Border, useOldAcrylicMethod);
                            return;
                        }

                    case BackdropStyles.Aero:
                        {
                            DrawAero(hwnd, Margins);
                            return;
                        }

                    case BackdropStyles.None:
                        {
                            DrawTransparentGray(hwnd);
                            return;
                        }

                    default:
                        {
                            DrawAcrylic(hwnd, Margins, Border, useOldAcrylicMethod);
                            return;
                        }
                }
            }

            else if ((OS.W8x || OS.W7 || OS.WVista) && CompositionEnabled)
            {
                DrawAero(hwnd, Margins);
                return;
            }

            else if (Style == BackdropStyles.None)
            {
                DrawTransparentGray(hwnd);
                return;
            }
        }

        /// <summary>
        /// Applies a visual drop effect to the specified <see cref="Form"/> using the provided settings.
        /// </summary>
        /// <remarks>This method customizes the appearance of a form by applying a drop effect, such as Mica or Acrylic,
        /// based on the specified style. Ensure that the form's handle is valid before calling this method.</remarks>
        /// <param name="Form">The <see cref="Form"/> to which the drop effect will be applied. Cannot be <see langword="null"/>.</param>
        /// <param name="Margins">The <see cref="Padding"/> that defines the margins for the drop effect. Defaults to <see cref="Padding.Empty"/> if
        /// not specified.</param>
        /// <param name="Border">A value indicating whether the form's border should be visible. Defaults to <see langword="true"/>.</param>
        /// <param name="FormStyle">The <see cref="BackdropStyles"/> that specifies the visual style of the drop effect. Defaults to <see
        /// cref="BackdropStyles.Mica"/>.</param>
        /// <param name="useOldAcrylicMethod">A value indicating whether to use the legacy acrylic method for the drop effect. Defaults to <see
        /// langword="false"/>.</param>
        public static void DropEffect(this Form Form, Padding Margins = default, bool Border = true, BackdropStyles FormStyle = BackdropStyles.Mica, bool useOldAcrylicMethod = false)
        {
            DropEffect(Form.Handle, Margins, Border, FormStyle, useOldAcrylicMethod);
        }

        /// <summary>
        /// Resets any visual effects applied to the specified <see cref="Form"/>.
        /// </summary>
        /// <remarks>This method performs the following actions to reset the visual effects of the form:
        /// <list type="bullet"> <item><description>Sets the system backdrop type to "None".</description></item>
        /// <item><description>Disables immersive dark mode if it was previously enabled.</description></item>
        /// <item><description>Clears any extended frame margins applied to the form.</description></item> </list> If
        /// the <paramref name="form"/> is null or disposed, the method does nothing.</remarks>
        /// <param name="form">The <see cref="Form"/> instance to reset. Must not be null or disposed.</param>
        public static void ResetEffect(this Form form)
        {
            if (form == null || form.IsDisposed) return;
            ResetEffect(form.Handle);
        }

        /// <summary>
        /// Resets the visual effects applied to a window, including backdrop styles, dark mode, and frame extensions.
        /// </summary>
        /// <remarks>This method restores the window to its default appearance by performing the following
        /// actions: <list type="bullet"> <item><description>Sets the backdrop style to "None".</description></item>
        /// <item><description>Disables immersive dark mode if it was previously enabled.</description></item>
        /// <item><description>Clears any extended frame margins applied to the window.</description></item> </list> If
        /// <paramref name="hwnd"/> is <see cref="IntPtr.Zero"/>, the method does nothing.</remarks>
        /// <param name="hwnd">A handle to the window whose effects are to be reset. Must not be <see cref="IntPtr.Zero"/>.</param>
        public static void ResetEffect(IntPtr hwnd)
        {
            if (OS.WXP) return; // Unsupported OS

            if (hwnd == IntPtr.Zero) return;

            // 1. Reset backdrop type to "None"
            int backdropNone = (int)BackdropStyles.None;
            DWMAPI.DwmSetWindowAttribute(hwnd, DWMAPI.DWMWINDOWATTRIBUTE.SYSTEMBACKDROP_TYPE, ref backdropNone, Marshal.SizeOf<int>());

            // 2. Disable dark mode if previously applied
            int useDarkMode = 0;
            DWMAPI.DwmSetWindowAttribute(hwnd, (int)DWMAPI.DWMWINDOWATTRIBUTE.USE_IMMERSIVE_DARK_MODE, ref useDarkMode, Marshal.SizeOf<int>());

            // 3. Clear any frame extension
            DWMAPI.MARGINS margins = new(); // all zeroes
            DWMAPI.DwmExtendFrameIntoClientArea(hwnd, ref margins);
        }

        /// <summary>
        /// Applies the Mica or Tabbed backdrop effect to a window on supported versions of Windows.
        /// </summary>
        /// <remarks>This method is only supported on Windows 11 and later. On earlier versions of
        /// Windows, the method will return without making any changes. The backdrop effect enhances the appearance of
        /// the window by applying a translucent material effect.</remarks>
        /// <param name="hwnd">A handle to the window to which the backdrop effect will be applied.</param>
        /// <param name="margins">The margins of the window where the backdrop effect should be applied.  Use <see cref="Padding.Empty"/> to
        /// apply the effect to the entire window.</param>
        /// <param name="style">The desired backdrop style to apply. Defaults to <see cref="BackdropStyles.Mica"/>.  If <see
        /// cref="BackdropStyles.Tabbed"/> is specified but not supported, the Mica style will be used instead.</param>
        public static void DrawMica(IntPtr hwnd, Padding margins, BackdropStyles style = BackdropStyles.Mica)
        {
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x || OS.W10) return; // Mica and Tabbed are only supported on Windows 11 and later

            if (margins == Padding.Empty) margins = new Padding(-1);

            DrawAero(hwnd, margins, false); // Extend frame into client area

            // Determine which style to apply
            int backdrop = (int)style;
            if (style == BackdropStyles.Tabbed && !OS.W11_22523) backdrop = (int)BackdropStyles.Mica;

            int darkMode = Program.Style.DarkMode ? 1 : 0;

            // Set the dark mode attribute for the titlebar
            DWMAPI.DwmSetWindowAttribute(hwnd, (int)DWMAPI.DWMWINDOWATTRIBUTE.USE_IMMERSIVE_DARK_MODE, ref darkMode, Marshal.SizeOf<int>());
            if (OS.W10_1909_AndBelow) DWMAPI.DwmSetWindowAttribute(hwnd, (int)DWMAPI.DWMWINDOWATTRIBUTE.USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, ref darkMode, Marshal.SizeOf<int>());

            DWMAPI.DwmSetWindowAttribute(hwnd, (int)DWMAPI.DWMWINDOWATTRIBUTE.MICA_EFFECT, ref backdrop, Marshal.SizeOf<int>());

            DWMAPI.DwmSetWindowAttribute(hwnd, (int)DWMAPI.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, ref backdrop, Marshal.SizeOf<int>());

            DWMAPI.DwmSetWindowAttribute(hwnd, DWMAPI.DWMWINDOWATTRIBUTE.SYSTEMBACKDROP_TYPE, ref backdrop, Marshal.SizeOf<int>());
        }

        /// <summary>
        /// Applies an acrylic effect to the specified window, optionally including a border and using an alternative
        /// method if specified.
        /// </summary>
        /// <remarks>This method applies an acrylic blur effect to the specified window. On Windows 10,
        /// the effect defaults to a blur behind the window,  while on Windows 11, the effect uses the acrylic backdrop
        /// style. If the operating system does not support these effects  (e.g., Windows 8.1 or earlier), the method
        /// will return without making any changes.</remarks>
        /// <param name="hwnd">A handle to the window to which the acrylic effect will be applied.</param>
        /// <param name="margins">The padding margins to define the area of the window where the effect is applied.</param>
        /// <param name="border">A boolean value indicating whether to include a border around the window.  <see langword="true"/> to include
        /// a border; otherwise, <see langword="false"/>. The default is <see langword="true"/>.</param>
        /// <param name="useOldMethod">A boolean value indicating whether to use an alternative method for applying the acrylic effect.  <see
        /// langword="true"/> to use the alternative method; otherwise, <see langword="false"/>. The default is <see
        /// langword="false"/>.</param>
        public static void DrawAcrylic(IntPtr hwnd, Padding margins, bool border = true, bool useOldMethod = false)
        {
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) return; // Unsupported OS

            if (OS.W10 || useOldMethod)
            {
                if (hwnd == IntPtr.Zero) return;

                var accent = new User32.AccentPolicy
                {
                    AccentState = OS.W10 ? User32.AccentState.ACCENT_ENABLE_BLURBEHIND : User32.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND,
                    AccentFlags = border ? 0x20 | 0x40 | 0x80 | 0x100 : 0,
                    GradientColor = 0,
                    AnimationId = 0
                };

                int size = Marshal.SizeOf(accent);
                IntPtr ptr = IntPtr.Zero;

                try
                {
                    ptr = Marshal.AllocHGlobal(size);
                    Marshal.StructureToPtr(accent, ptr, false);

                    User32.WindowCompositionAttributeData data = new()
                    {
                        Attribute = User32.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                        SizeOfData = size,
                        Data = ptr
                    };

                    User32.SetWindowCompositionAttribute(hwnd, ref data);
                }
                finally
                {
                    if (ptr != IntPtr.Zero) Marshal.FreeHGlobal(ptr);
                }
            }
            else
            {
                DrawMica(hwnd, margins, BackdropStyles.Acrylic);
            }
        }

        /// <summary>
        /// Extends the window frame into the client area, enabling the Aero Glass effect or similar visual styles.
        /// </summary>
        /// <remarks>This method uses the Desktop Window Manager (DWM) API to extend the window frame into
        /// the client area, creating a visually seamless effect. On operating systems prior to Windows 10, any existing
        /// backdrops are removed if <paramref name="eraseOldBackdrops"/> is set to <see langword="true"/>.</remarks>
        /// <param name="hwnd">A handle to the window for which the frame is to be extended.</param>
        /// <param name="Margins">The <see cref="Padding"/> structure specifying the margins for the extended frame. If the value is <see
        /// langword="null"/>, <see cref="Padding.Empty"/>, or a padding of all zeros, default margins of -1 are applied
        /// to extend the frame fully.</param>
        /// <param name="eraseOldBackdrops">A <see langword="bool"/> value indicating whether to erase any previously applied backdrops (e.g., Mica,
        /// Acrylic, or Tabbed effects) before extending the frame. This parameter is ignored on Windows 10 and later.</param>
        public static void DrawAero(IntPtr hwnd, Padding Margins, bool eraseOldBackdrops = true)
        {
            if (OS.WXP) return; // Unsupported OS

            if (Margins == default || Margins == null || Margins == Padding.Empty || Margins == new Padding(0))
            {
                Margins = new(-1, -1, -1, -1);
            }

            if (!OS.WVista && !OS.W7 && !OS.W8x && !OS.W10 && eraseOldBackdrops)
            {
                int backdropType = 0; // 1 = Mica, 2 = Acrylic, 3 = Tabbed, 0 = None
                DWMAPI.DwmSetWindowAttribute(hwnd, 38, ref backdropType, sizeof(int));
            }

            DWMAPI.MARGINS DWM_Margins = new() { leftWidth = Margins.Left, rightWidth = Margins.Right, topHeight = Margins.Top, bottomHeight = Margins.Bottom };
            DWMAPI.DwmExtendFrameIntoClientArea(hwnd, ref DWM_Margins);
        }

        /// <summary>
        /// Applies a semi-transparent gray overlay to the specified window handle.
        /// </summary>
        /// <param name="hWnd">Handle to the window.</param>
        /// <param name="noWindowBorders">
        /// If true, removes the standard window border and caption (makes the window borderless).
        /// </param>
        /// <param name="opacity">Opacity value from 0.0 (fully transparent) to 1.0 (fully opaque).</param>
        public static void DrawTransparentGray(IntPtr hWnd, bool noWindowBorders = true, double opacity = 0.5)
        {
            if (hWnd == IntPtr.Zero) return;

            const int GWL_STYLE = -16;
            const int WS_BORDER = 0x00800000;
            const int WS_CAPTION = 0x00C00000;
            const int LWA_ALPHA = 0x2;
            const int WS_EX_LAYERED = 0x00080000;
            const int GWL_EXSTYLE = -20;

            // Remove borders if requested
            if (noWindowBorders)
            {
                int style = User32.GetWindowLong(hWnd, GWL_STYLE);
                style &= ~WS_BORDER & ~WS_CAPTION;
                User32.SetWindowLong(hWnd, GWL_STYLE, style);
            }

            // Enable layered window style
            int exStyle = User32.GetWindowLong(hWnd, GWL_EXSTYLE);
            exStyle |= WS_EX_LAYERED;
            User32.SetWindowLong(hWnd, GWL_EXSTYLE, exStyle);

            // Set gray color and opacity
            byte alpha = (byte)(opacity * 255);
            var color = Color.FromArgb(5, 5, 5);
            uint colorRef = (uint)((color.R) | (color.G << 8) | (color.B << 16));

            // Apply transparency
            User32.SetLayeredWindowAttributes(hWnd, colorRef, alpha, LWA_ALPHA);
        }

        /// <summary>
        /// Specifies the available backdrop styles for a window's background effect.
        /// </summary>
        /// <remarks>Backdrop styles define the visual appearance of a window's background, such as transparency, blur,
        /// and noise effects. These styles are typically used to enhance the user interface by providing a modern and visually
        /// appealing background. The availability of certain styles may depend on the operating system version.</remarks>
        public enum BackdropStyles
        {
            /// <summary>
            /// No backdrop effect.
            /// </summary>
            None = 0,

            /// <summary>
            /// Mica effect: Windows desktop image is highly blurred, noised, and shown through the window background.
            /// </summary>
            Mica = 2,  // DWMSBT_MAINWINDOW

            /// <summary>
            /// Acrylic effect: More transparent and dynamic than Mica.
            /// </summary>
            Acrylic = 3,

            /// <summary>
            /// Tabbed Mica effect: Similar to Mica, but with stronger color saturation.
            /// Available on Windows 11 build 22523 and higher.
            /// </summary>
            Tabbed = 4,  // DWMSBT_TABBEDWINDOW

            /// <summary>
            /// Aero effect (Windows Vista and later)
            /// </summary>
            Aero,

            /// <summary>
            /// Automatically choose the best effect depending on the OS
            /// </summary>
            Auto,
        }
    }
}
