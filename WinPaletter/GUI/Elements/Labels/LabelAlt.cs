using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Label can be drawn on glass (Aero/Acrylic/Mica) for WinPaletter UI")]
    public class LabelAlt : Label
    {

        #region Variables

        private IntPtr _textHdc = IntPtr.Zero;
        private IntPtr _dibSectionRef;

        #endregion

        #region Properties

        public bool DrawOnGlass { get; set; } = false;

        #endregion

        #region Voids/Functions

        protected TextFormatFlags ReturnFormatFlags(string Text = "")
        {
            var format = TextFormatFlags.Default;

            if (TextAlign == ContentAlignment.BottomCenter)
            {
                format |= TextFormatFlags.HorizontalCenter;
                format |= TextFormatFlags.Bottom;
            }

            else if (TextAlign == ContentAlignment.BottomRight)
            {
                format |= TextFormatFlags.Right;
                format |= TextFormatFlags.Bottom;
            }

            else if (TextAlign == ContentAlignment.BottomLeft)
            {
                format |= TextFormatFlags.Left;
                format |= TextFormatFlags.Bottom;
            }

            else if (TextAlign == ContentAlignment.MiddleCenter)
            {
                format |= TextFormatFlags.HorizontalCenter;
                format |= TextFormatFlags.VerticalCenter;
            }

            else if (TextAlign == ContentAlignment.MiddleRight)
            {
                format |= TextFormatFlags.Right;
                format |= TextFormatFlags.VerticalCenter;
            }

            else if (TextAlign == ContentAlignment.MiddleLeft)
            {
                format |= TextFormatFlags.Left;
                format |= TextFormatFlags.VerticalCenter;
            }

            else if (TextAlign == ContentAlignment.TopCenter)
            {
                format |= TextFormatFlags.HorizontalCenter;
                format |= TextFormatFlags.Top;
            }

            else if (TextAlign == ContentAlignment.TopRight)
            {
                format |= TextFormatFlags.Right;
                format |= TextFormatFlags.Top;
            }

            else if (TextAlign == ContentAlignment.TopLeft)
            {
                format |= TextFormatFlags.Left;
                format |= TextFormatFlags.Top;
            }

            if (!Text.Contains("\r\n"))
                format |= TextFormatFlags.SingleLine;

            if (AutoEllipsis)
                format |= TextFormatFlags.EndEllipsis;

            if (RightToLeft == RightToLeft.Yes)
                format |= TextFormatFlags.RightToLeft;

            return format;
        }

        private IntPtr PrepareHdc(IntPtr outputHdc, int width, int height)
        {
            if (_textHdc != IntPtr.Zero)
            {
                NativeMethods.GDI32.DeleteObject(_dibSectionRef);
                NativeMethods.GDI32.DeleteDC(_textHdc);
            }
            _textHdc = NativeMethods.GDI32.CreateCompatibleDC(outputHdc);

            var bmp_info = new NativeMethods.GDI32.BitmapInfo()
            {
                biSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(NativeMethods.GDI32.BitmapInfo)),
                biWidth = width,
                biHeight = -height, // DIB use top-down ref system, so use negative height
                biPlanes = 1,
                biBitCount = 32,
                biCompression = 0
            };
            _dibSectionRef = NativeMethods.GDI32.CreateDIBSection(outputHdc, ref bmp_info, 0U, 0, IntPtr.Zero, 0U);
            NativeMethods.GDI32.SelectObject(_textHdc, _dibSectionRef);

            var hFont = Font.ToHfont();
            NativeMethods.GDI32.SelectObject(_textHdc, hFont);

            var Options = new NativeMethods.UxTheme.DttOpts()
            {
                dwSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(NativeMethods.UxTheme.DttOpts)),
                dwFlags = NativeMethods.UxTheme.DttOptsFlags.DTT_COMPOSITED | NativeMethods.UxTheme.DttOptsFlags.DTT_TEXTCOLOR,
                crText = ForeColor.ToArgb()
            };

            // Glow
            Options.dwFlags = Options.dwFlags | NativeMethods.UxTheme.DttOptsFlags.DTT_GLOWSIZE;
            Options.iGlowSize = 10;

            // Draw
            try
            {
                var Rectangle = new NativeMethods.UxTheme.RECT(Padding.Left, Padding.Top, width - Padding.Right, height - Padding.Bottom);  // Set full bounds with padding
                var ActiveTitlebarRenderer = new System.Windows.Forms.VisualStyles.VisualStyleRenderer(System.Windows.Forms.VisualStyles.VisualStyleElement.Window.Caption.Active);
                NativeMethods.UxTheme.DrawThemeTextEx(ActiveTitlebarRenderer.Handle, _textHdc, 0, 0, Text, -1, (int)ReturnFormatFlags(), ref Rectangle, ref Options);
            }
            catch
            {
            }

            NativeMethods.GDI32.DeleteObject(hFont);

            return _textHdc;
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = Config.RenderingHint;
            using (var br = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(br, new Rectangle(0, 0, Width, Height));
            }

            try
            {
                if (DesignMode || !DrawOnGlass)
                {
                    using (var br = new SolidBrush(ForeColor))
                    {
                        e.Graphics.DrawString(Text, Font, br, new Rectangle(0, 0, Width, Height), base.TextAlign.ToStringFormat());
                    }
                }

                else if (!DesignMode & DrawOnGlass)
                {
                    var outputHdc = e.Graphics.GetHdc();
                    var sourceHdc = PrepareHdc(outputHdc, Width, Height);
                    NativeMethods.GDI32.BitBlt(outputHdc, 0, 0, Width, Height, sourceHdc, 0, 0, NativeMethods.GDI32.BitBltOp.SRCCOPY);
                    e.Graphics.ReleaseHdc(outputHdc);

                }
            }
            catch
            {
                using (var br = new SolidBrush(ForeColor))
                {
                    e.Graphics.DrawString(Text, Font, br, new Rectangle(0, 0, Width, Height), base.TextAlign.ToStringFormat());
                }
            }
        }

    }

}