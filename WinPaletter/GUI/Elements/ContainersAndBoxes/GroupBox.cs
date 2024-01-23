using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinPaletter.TypesExtensions;

namespace WinPaletter.UI.WP
{
    [Description("Themed GroupBox for WinPaletter UI")]
    [DefaultEvent("Click")]
    public class GroupBox : Panel
    {
        public GroupBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Text = string.Empty;
        }

        #region Properties

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

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

            Rectangle Rect = new(0, 0, Width - 1, Height - 1);

            if (!DesignMode)
            {
                Color ParentColor = this.GetParentColor();

                G.Clear(ParentColor);
                BackColor = ParentColor.CB((float)(ParentColor.IsDark() ? 0.04d : -0.05d));

                using (SolidBrush br = new(BackColor)) { G.FillRoundedRect(br, Rect); }
                using (Pen P = new(ParentColor.CB((float)(ParentColor.IsDark() ? 0.06d : -0.07d)))) { G.DrawRoundedRect(P, Rect); }
            }
            else
            {
                G.FillRectangle(Program.Style.Schemes.Main.Brushes.Back, Rect);
                G.DrawRectangle(Program.Style.Schemes.Main.Pens.Line, Rect);
            }

            base.OnPaint(e);
        }
    }
}