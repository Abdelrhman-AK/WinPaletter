﻿using System;
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

        #region Variables

        private IntPtr _textHdc = IntPtr.Zero;
        private IntPtr _dibSectionRef;

        #endregion

        #region Properties

        public bool DrawOnGlass { get; set; } = false;

        protected override CreateParams CreateParams
        {
            get
            {
                var cpar = base.CreateParams;
                if (!DesignMode)
                {
                    cpar.ExStyle |= 0x20;
                    return cpar;
                }
                else
                {
                    return cpar;
                }
            }
        }
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
            Graphics G = e.Graphics;

            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Config.RenderingHint;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            using (SolidBrush br = new(BackColor)) { G.FillRectangle(br, new Rectangle(0, 0, Width, Height)); }

            try
            {
                if (DesignMode || !DrawOnGlass)
                {
                    using (SolidBrush br = new(ForeColor)) { G.DrawString(Text, Font, br, new Rectangle(0, 0, Width, Height), base.TextAlign.ToStringFormat()); }
                }

                else if (!DesignMode & DrawOnGlass)
                {
                    Ookii.Dialogs.WinForms.Glass.DrawCompositedText(G, Text, Font, new Rectangle(0, 0, Width, Height), Padding, ForeColor, 10, ReturnFormatFlags(Text));
                }
            }
            catch
            {
                using (SolidBrush br = new(ForeColor)) { G.DrawString(Text, Font, br, new Rectangle(0, 0, Width, Height), base.TextAlign.ToStringFormat()); }
            }
        }

    }

}