using System;
using System.Drawing;
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
        /// Draw effect on form depending on both user choice (Tabbed\Mica\Acrylic\Aero) and current OS
        /// </summary>
        public static void DropEffect(IntPtr Handle, Padding Margins = default, bool Border = true, FormStyle FormStyle = FormStyle.Mica)
        {
            if (Margins == default || Margins == null || Margins == Padding.Empty || Margins == new Padding(0))
            {
                Margins = new Padding(-1, -1, -1, -1);
            }

            bool CompositionEnabled = DWMAPI.IsCompositionEnabled();
            bool Transparency_W10x = (OS.W10 || OS.W11 || OS.W12) && Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", true));

            if ((OS.W12 || OS.W11) && Transparency_W10x)
            {
                switch (FormStyle)
                {
                    case FormStyle.Mica:
                        {
                            DrawMica(Handle, Margins, MicaStyle.Mica);
                            return;
                        }

                    case FormStyle.Tabbed:
                        {
                            DrawMica(Handle, Margins, MicaStyle.Tabbed);
                            return;
                        }

                    case FormStyle.Acrylic:
                        {
                            DrawAcrylic(Handle, Border);
                            return;
                        }

                    case FormStyle.Aero:
                        {
                            DrawAero(Handle, Margins);
                            return;
                        }

                    //case FormStyle.Transparent:
                    //    {
                    //        DrawTransparentGray((Form)Control.FromHandle(Handle), Margins);
                    //        return;
                    //    }

                    default:
                        {
                            DrawMica(Handle, Margins, MicaStyle.Mica);
                            return;
                        }
                }
            }

            else if (OS.W10 && Transparency_W10x)
            {
                switch (FormStyle)
                {
                    case FormStyle.Acrylic:
                        {
                            DrawAcrylic(Handle, Border);
                            return;
                        }

                    case FormStyle.Aero:
                        {
                            DrawAero(Handle, Margins);
                            return;
                        }

                    //case FormStyle.Transparent:
                    //    {
                    //        DrawTransparentGray((Form)Control.FromHandle(Handle), Margins);
                    //        return;
                    //    }

                    default:
                        {
                            DrawAcrylic(Handle, Border);
                            return;
                        }
                }
            }

            else if ((OS.W81 || OS.W8 || OS.W7 || OS.WVista) && CompositionEnabled)
            {
                DrawAero(Handle, Margins);
                return;
            }
        }

        /// <summary>
        /// Draw effect on form depending on both user choice (Tabbed\Mica\Acrylic\Aero) and current OS
        /// </summary>
        public static void DropEffect(this Form Form, Padding Margins = default, bool Border = true, FormStyle FormStyle = FormStyle.Mica)
        {
            DropEffect(Form.Handle, Margins, Border, FormStyle);
        }

        /// <summary>
        /// Draws mica/tabbed effect (Windows 11 and later - Tabbed Style is for Windows 11 Build 22523 and Higher, if not, Mica will be used instead)
        /// </summary>
        public static void DrawMica(IntPtr Handle, Padding Margins, MicaStyle Style = MicaStyle.Mica)
        {
            if (Margins == default || Margins == null || Margins == Padding.Empty || Margins == new Padding(0))
            {
                Margins = new Padding(-1, -1, -1, -1);
            }

            FormStyle FS = FormStyle.Mica;

            if (Style == MicaStyle.Tabbed && (OS.W11_22523 || OS.W12))
                FS = FormStyle.Tabbed;

            var DWM_Margins = new DWMAPI.MARGINS() { leftWidth = Margins.Left, rightWidth = Margins.Right, topHeight = Margins.Top, bottomHeight = Margins.Bottom };
            int argpvAttribute = (int)FS;

            DLLFunc.DarkTitlebar(Handle, Program.Style.DarkMode);
            DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMATTRIB.SYSTEMBACKDROP_TYPE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
            DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref DWM_Margins);
        }

        /// <summary>
        /// Draws acrylic effect (Windows 10 and later)
        /// </summary>
        public static void DrawAcrylic(IntPtr Handle, bool Border = true)
        {
            User32.AccentPolicy accent = new() { AccentState = User32.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND };

            if (Border)
                accent.AccentFlags = 0x20 | 0x40 | 0x80 | 0x100;

            int accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            User32.WindowCompositionAttributeData Data = new()
            {
                Attribute = User32.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            User32.SetWindowCompositionAttribute(Handle, ref Data);
            Marshal.FreeHGlobal(accentPtr);
        }

        /// <summary>
        /// Draws aero effect (Windows Vista and later)
        /// </summary>
        public static void DrawAero(IntPtr Handle, Padding Margins)
        {
            if (Margins == default || Margins == null || Margins == Padding.Empty || Margins == new Padding(0))
            {
                Margins = new Padding(-1, -1, -1, -1);
            }

            DWMAPI.MARGINS DWM_Margins = new() { leftWidth = Margins.Left, rightWidth = Margins.Right, topHeight = Margins.Top, bottomHeight = Margins.Bottom };
            DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref DWM_Margins);
        }

        /// <summary>
        /// Draws mica/tabbed effect (Windows 11 and later - Tabbed Style is for Windows 11 Build 22523 and Higher, if not, Mica will be used instead)
        /// </summary>
        public static void DrawMica(this Form Form, Padding Margins, MicaStyle Style = MicaStyle.Mica)
        {
            DrawMica(Form.Handle, Margins, Style);
        }

        /// <summary>
        /// Draws acrylic effect (Windows 10 and later)
        /// </summary>
        public static void DrawAcrylic(this Form Form, bool Border = true)
        {
            DrawAcrylic(Form.Handle, Border);
        }

        /// <summary>
        /// Draws aero effect (Windows Vista and later)
        /// </summary>
        public static void DrawAero(this Form Form, Padding Margins)
        {
            DrawAero(Form.Handle, Margins);
        }

        /// <summary>
        /// Make a form with transparent gray effect
        /// </summary>
        public static void DrawTransparentGray(this Form Form, bool NoWindowBorders = true)
        {
            if (NoWindowBorders)
                Form.FormBorderStyle = FormBorderStyle.None;
            Form.BackColor = Color.FromArgb(5, 5, 5);
            Form.Opacity = 0.5d;
        }

        public enum FormStyle
        {
            Auto,
            Default,
            Mica,
            Acrylic,
            Tabbed,
            Aero,
            Transparent
        }

        public enum MicaStyle
        {
            Mica,
            Tabbed
        }

    }
}
