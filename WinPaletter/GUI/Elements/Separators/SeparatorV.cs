using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("Vertical separator for WinPaletter UI")]
    public class SeparatorV : Control
    {
        public SeparatorV()
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
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;
        public bool AlternativeLook { get; set; } = false;

        #endregion

        #region Events/Overrides

        protected override void OnResize(EventArgs e)
        {
            Size = new(!AlternativeLook ? 1 : 2, Height);

            base.OnResize(e);
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

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Color Line = AlternativeLook ? Color.DarkRed : Color.FromArgb(128, scheme.Colors.Line_Hover_Level2);

            using (Pen C = new(Line, !AlternativeLook ? 1 : 2))
            {
                G.DrawLine(C, new Point(0, 0), new Point(0, Height));
                G.DrawLine(C, new Point(1, 0), new Point(1, Height));
            }

            base.OnPaint(e);
        }
    }
}