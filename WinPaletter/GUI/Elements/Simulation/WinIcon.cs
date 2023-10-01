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
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        #region Properties

        public Color ColorText { get; set; } = Color.White;
        public Color ColorGlow { get; set; } = Color.FromArgb(50, 0, 0, 0);
        public Icon Icon { get; set; }

        private int _IconSize = 32;
        public int IconSize
        {
            get
            {
                return _IconSize;
            }
            set
            {
                _IconSize = value;
                Invalidate();
            }
        }


        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = "";

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = My.Env.RenderingHint;
            DoubleBuffered = true;

            var IconRect = new Rectangle(0, 0, Width - 1, Height - 30);

            var LabelRect = new Rectangle(0, Height - 35, Width - 1, 30);
            var LabelRectShadow = new Rectangle(1, Height - 34, Width - 1, 30);

            if (_IconSize < 16)
                _IconSize = 16;
            if (_IconSize > 256)
                _IconSize = 256;

            var IconRectX = new Rectangle((int)Math.Round(IconRect.X + (IconRect.Width - _IconSize) / 2d), (int)Math.Round(IconRect.Y + (IconRect.Height - _IconSize) / 2d), _IconSize, _IconSize);

            if (Icon is not null)
            {
                var ico = new Icon(Icon, _IconSize, _IconSize);
                G.DrawIcon(ico, IconRectX);
                ico.Dispose();
            }

            if (ColorGlow.A > 0)
                G.DrawString(Text, Font, Brushes.Black, LabelRectShadow, ContentAlignment.MiddleCenter.ToStringFormat());

            G.DrawGlowString(1, Text, Font, ColorText, ColorGlow, new Rectangle(0, 0, Width - 1, Height - 1), LabelRect, ContentAlignment.MiddleCenter.ToStringFormat());
        }

    }

}