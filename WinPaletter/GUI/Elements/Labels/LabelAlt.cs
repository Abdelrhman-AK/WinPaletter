using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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
            Graphics G = e.Graphics;

            // Nothing to draw into — bail before touching GDI+.
            if (Width <= 0 || Height <= 0) return;

            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Transparent background: ask the parent chain to paint through us.
            InvokePaintBackground(this, e);

            Rectangle bounds = new(0, 0, Width, Height);

            using (SolidBrush backBrush = new(BackColor))
            using (SolidBrush foreBrush = new(ForeColor))
            using (StringFormat sf = TextAlign.ToStringFormat())
            {
                G.FillRectangle(backBrush, bounds);

                bool drew = false;
                Exception firstFailure = null;

                try
                {
                    if (DesignMode || !DrawOnGlass)
                    {
                        G.DrawString(Text, Font, foreBrush, bounds, sf);
                        drew = true;
                    }
                    else if (!DesignMode && DrawOnGlass)
                    {
                        G.DrawCompositedText(Text, Font, bounds, Padding, ForeColor, 7, ReturnFormatFlags(Text), false);
                        drew = true;
                    }
                }
                catch (Exception ex)
                {
                    // Record the real cause once. Do NOT retry GDI+ with the same Graphics —
                    // on Win9x the same GDI+ status will just be returned again.
                    firstFailure = ex;
                }

                if (!drew)
                {
                    // GDI fallback: TextRenderer uses user32/gdi32, not gdiplus.dll,
                    // so it succeeds even when the Graphics / Font / DC are in a state
                    // that GDI+ 5.1 rejects with a generic error.
                    try
                    {
                        TextRenderer.DrawText(e.Graphics, Text, Font, bounds, ForeColor, ReturnFormatFlags(Text));
                    }
                    catch
                    {
                        // Give up silently. A paint failure must never propagate out of
                        // OnPaint or it will kill the message pump.
                    }
                }

                if (firstFailure != null)
                {
                    try
                    {
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Warning, $"LabelAlt GDI+ draw failed (fell back to GDI): {firstFailure}");
                    }
                    catch
                    {
                        // Logging must never take the app down either.
                    }
                }
            }

            // Don't use base.OnPaint(e) — the base Label paint would draw the text again.
        }
    }
}