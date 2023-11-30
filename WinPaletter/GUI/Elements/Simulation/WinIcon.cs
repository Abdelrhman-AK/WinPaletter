using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Simulation
{
    [Description("Simulated Windows desktop icons")]
    public class WinIcon : Panel
    {
        public WinIcon()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        #region Properties

        public Color ColorText { get; set; } = Color.White;
        public Color ColorGlow { get; set; } = Color.FromArgb(50, 0, 0, 0);
        public Icon Icon { get; set; }

        private int _IconSize = 32;
        public int IconSize
        {
            get => _IconSize;
            set
            {
                if (value != _IconSize)
                {
                    _IconSize = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cpar = base.CreateParams;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = Program.Style.RenderingHint;
            DoubleBuffered = true;

            Rectangle IconRect = new(0, 0, Width - 1, Height - 30);

            Rectangle LabelRect = new(0, Height - 35, Width - 1, 30);
            Rectangle LabelRectShadow = new(1, Height - 34, Width - 1, 30);

            if (_IconSize < 16)
                _IconSize = 16;
            if (_IconSize > 256)
                _IconSize = 256;

            Rectangle IconRectX = new((int)Math.Round(IconRect.X + (IconRect.Width - _IconSize) / 2d), (int)Math.Round(IconRect.Y + (IconRect.Height - _IconSize) / 2d), _IconSize, _IconSize);

            if (Icon is not null)
            {
                Icon ico = new(Icon, _IconSize, _IconSize);
                G.DrawIcon(ico, IconRectX);
                ico.Dispose();
            }

            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
            {
                if (ColorGlow.A > 0)
                    G.DrawString(Text, Font, Brushes.Black, LabelRectShadow, sf);

                G.DrawGlowString(1, Text, Font, ColorText, ColorGlow, new Rectangle(0, 0, Width - 1, Height - 1), LabelRect, sf);
            }

            base.OnPaint(e);
        }
    }
}