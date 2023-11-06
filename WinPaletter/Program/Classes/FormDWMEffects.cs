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
    public static class FormDWMEffects
    {
        /// <summary>
        /// Draw effect on form depending on both user choice (Tabbed\Mica\Acrylic\Aero) and current OS
        /// </summary>
        public static void DrawDWMEffect(this Form Form, Padding Margins, bool Border = true, FormStyle FormStyle = FormStyle.Mica)
        {

            if (Margins == null || Margins == Padding.Empty || Margins == new Padding(0))
                Margins = new Padding(-1, -1, -1, -1);

            bool CompositionEnabled = DWMAPI.IsCompositionEnabled();

            bool Transparency_W11_10;
            Transparency_W11_10 = (OS.W10 | OS.W11) && Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", true));

            try
            {
                if (OS.W11 && Transparency_W11_10)
                {
                    if (FormStyle == FormStyle.Mica)
                    {
                        Form.DrawMica(Margins, MicaStyle.Mica);
                    }

                    else if (FormStyle == FormStyle.Tabbed)
                    {
                        Form.DrawMica(Margins, MicaStyle.Tabbed);
                    }

                    else if (FormStyle == FormStyle.Acrylic)
                    {
                        Form.DrawAcrylic(Border);
                    }

                    else
                    {
                        Form.DrawMica(Margins, MicaStyle.Mica);
                    }
                }

                else if (OS.W10 && Transparency_W11_10)
                {
                    Form.DrawAcrylic(Border);
                }

                else if ((OS.W81 | OS.W8 | OS.W7 | OS.WVista) && CompositionEnabled)
                {
                    Form.DrawAero(Margins);
                }
            }
            catch { }

        }

        public static void DrawAcrylic(this Form Form, bool Border = true)
        {
            var accent = new User32.AccentPolicy() { AccentState = User32.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND };
            if (Border)
                accent.AccentFlags = 0x20 | 0x40 | 0x80 | 0x100;
            int accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var Data = new User32.WindowCompositionAttributeData()
            {
                Attribute = User32.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };
            User32.SetWindowCompositionAttribute(Form.Handle, ref Data);
            Marshal.FreeHGlobal(accentPtr);
        }

        public static void DrawAcrylic(IntPtr Handle, bool Border = true)
        {
            var accent = new User32.AccentPolicy() { AccentState = User32.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND };
            if (Border)
                accent.AccentFlags = 0x20 | 0x40 | 0x80 | 0x100;
            int accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var Data = new User32.WindowCompositionAttributeData()
            {
                Attribute = User32.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };
            User32.SetWindowCompositionAttribute(Handle, ref Data);
            Marshal.FreeHGlobal(accentPtr);
        }

        /// <summary>
        /// Draws Mica Style (Windows 11 and Higher - Tabbed Style is for Windows 11 Build 22523 and Higher, if not, Mica will be used instead)
        /// </summary>
        public static void DrawMica(this Form Form, Padding Margins, MicaStyle Style = MicaStyle.Mica)
        {
            var FS = new FormStyle();
            if (Style == MicaStyle.Mica)
                FS = FormStyle.Mica;
            if (Style == MicaStyle.Tabbed & OS.W11_22523)
                FS = FormStyle.Tabbed;
            else
                FS = FormStyle.Mica;

            if (Margins == null || Margins == Padding.Empty || Margins == new Padding(0))
                Margins = new Padding(-1, -1, -1, -1);
            var DWM_Margins = new DWMAPI.MARGINS() { leftWidth = Margins.Left, rightWidth = Margins.Right, topHeight = Margins.Top, bottomHeight = Margins.Bottom };

            DLLFunc.DarkTitlebar(Form.Handle, Program.Style.DarkMode);
            int argpvAttribute = (int)FS;
            DWMAPI.DwmSetWindowAttribute(Form.Handle, DWMAPI.DWMATTRIB.SYSTEMBACKDROP_TYPE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
            DWMAPI.DwmExtendFrameIntoClientArea(Form.Handle, ref DWM_Margins);
        }

        public static void DrawMica(IntPtr Handle, Padding Margins, MicaStyle Style = MicaStyle.Mica)
        {
            var FS = new FormStyle();
            if (Style == MicaStyle.Mica)
                FS = FormStyle.Mica;
            if (Style == MicaStyle.Tabbed & OS.W11_22523)
                FS = FormStyle.Tabbed;
            else
                FS = FormStyle.Mica;

            if (Margins == null || Margins == Padding.Empty || Margins == new Padding(0))
                Margins = new Padding(-1, -1, -1, -1);
            var DWM_Margins = new DWMAPI.MARGINS() { leftWidth = Margins.Left, rightWidth = Margins.Right, topHeight = Margins.Top, bottomHeight = Margins.Bottom };

            DLLFunc.DarkTitlebar(Handle, Program.Style.DarkMode);
            int argpvAttribute = (int)FS;
            DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMATTRIB.SYSTEMBACKDROP_TYPE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
            DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref DWM_Margins);
        }


        public static void DrawAero(this Form Form, Padding Margins)
        {
            if (Margins == null || Margins == Padding.Empty || Margins == new Padding(0))
                Margins = new Padding(-1, -1, -1, -1);
            var DWM_Margins = new DWMAPI.MARGINS() { leftWidth = Margins.Left, rightWidth = Margins.Right, topHeight = Margins.Top, bottomHeight = Margins.Bottom };
            DWMAPI.DwmExtendFrameIntoClientArea(Form.Handle, ref DWM_Margins);
        }

        public static void DrawAero(IntPtr Handle, Padding Margins)
        {
            if (Margins == null || Margins == Padding.Empty || Margins == new Padding(0))
                Margins = new Padding(-1, -1, -1, -1);
            var DWM_Margins = new DWMAPI.MARGINS() { leftWidth = Margins.Left, rightWidth = Margins.Right, topHeight = Margins.Top, bottomHeight = Margins.Bottom };
            DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref DWM_Margins);
        }

        public static void DrawTransparentGray(this Form Form, bool NoWindowBorders = true)
        {
            if (NoWindowBorders)
                Form.FormBorderStyle = FormBorderStyle.None;
            Form.BackColor = Color.FromArgb(5, 5, 5);
            Form.Opacity = 0.5d;
        }

        /// <summary>
        /// Sets Titlebar Backcolor, Forecolor and border color (Only for Windows 11 and Higher)
        /// </summary>
        public static void DrawCustomTitlebar(this Form Form, Color BackColor = default, Color BorderColor = default, Color ForeColor = default)
        {

            if (OS.W11)
            {
                try
                {
                    if (BackColor != null)
                    {
                        int argpvAttribute = ColorTranslator.ToWin32(BackColor);
                        DWMAPI.DwmSetWindowAttribute(Form.Handle, DWMAPI.DWMATTRIB.CAPTION_COLOR, ref argpvAttribute, Marshal.SizeOf(typeof(int)));
                    }
                }
                catch
                {
                }

                try
                {
                    if (ForeColor != null)
                    {
                        int argpvAttribute1 = ColorTranslator.ToWin32(ForeColor);
                        DWMAPI.DwmSetWindowAttribute(Form.Handle, DWMAPI.DWMATTRIB.TEXT_COLOR, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                    }
                }
                catch
                {
                }

                try
                {
                    if (BorderColor != null)
                    {
                        int argpvAttribute2 = ColorTranslator.ToWin32(BorderColor);
                        DWMAPI.DwmSetWindowAttribute(Form.Handle, DWMAPI.DWMATTRIB.BORDER_COLOR, ref argpvAttribute2, Marshal.SizeOf(typeof(int)));
                    }
                }
                catch
                {
                }

            }

        }

        public enum FormStyle
        {
            Auto,
            Default,
            Mica,
            Acrylic,
            Tabbed
        }

        public enum MicaStyle
        {
            Mica,
            Tabbed
        }

    }
}
