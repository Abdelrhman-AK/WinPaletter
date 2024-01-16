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

        public LabelAlt()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        #region Properties

        public bool DrawOnGlass { get; set; } = false;

        #endregion

        #region Methods
        protected TextFormatFlags ReturnFormatFlags(string Text = "")
        {
            TextFormatFlags format = TextFormatFlags.Default;

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

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Program.Style.RenderingHint;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            using (SolidBrush br = new(BackColor)) { G.FillRectangle(br, new Rectangle(0, 0, Width, Height)); }

            try
            {
                if (DesignMode || !DrawOnGlass)
                {
                    using (StringFormat sf = TextAlign.ToStringFormat())
                    {
                        using (SolidBrush br = new(ForeColor))
                        {
                            G.DrawString(Text, Font, br, new Rectangle(0, 0, Width, Height), sf);
                        }
                    }
                }

                else if (!DesignMode & DrawOnGlass)
                {
                    Ookii.Dialogs.WinForms.Glass.DrawCompositedText(G, Text, Font, new Rectangle(0, 0, Width, Height), Padding, ForeColor, 10, ReturnFormatFlags(Text));
                }
            }
            catch
            {
                using (StringFormat sf = TextAlign.ToStringFormat())
                {
                    using (SolidBrush br = new(ForeColor))
                    {
                        G.DrawString(Text, Font, br, new Rectangle(0, 0, Width, Height), sf);
                    }
                }
            }

            // Don't use base.OnPaint(e) to avoid doubling graphics bug
            //// base.OnPaint(e);
        }
    }
}