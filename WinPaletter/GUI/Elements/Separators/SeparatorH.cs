using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("Horizontal separator for WinPaletter UI")]
    public class SeparatorH : Control
    {
        public SeparatorH()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            TabStop = false;
            Text = string.Empty;
        }

        #region Properties

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        public bool AlternativeLook { get; set; } = false;

        #endregion

        #region Events/Overrides

        protected override void OnResize(EventArgs e)
        {
            Size = new(Width, !AlternativeLook ? 1 : 2);

            base.OnResize(e);
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
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
            G.SmoothingMode = SmoothingMode.AntiAlias;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Color Line = AlternativeLook ? Color.DarkRed : Color.FromArgb(50, scheme.Colors.ForeColor);

            using (Pen P = new(Line, !AlternativeLook ? 1 : 2))
            {
                G.DrawLine(P, new Point(0, 0), new Point(Width, 0));
                G.DrawLine(P, new Point(0, 1), new Point(Width, 1));
            }

            base.OnPaint(e);


        }
    }
}