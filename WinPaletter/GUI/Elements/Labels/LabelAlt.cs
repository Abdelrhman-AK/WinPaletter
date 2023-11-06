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
                    Ookii.Dialogs.WinForms.Glass.DrawCompositedText(e.Graphics, Text, Font, new Rectangle(0, 0, Width, Height), Padding, ForeColor, 10, ReturnFormatFlags(Text));
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