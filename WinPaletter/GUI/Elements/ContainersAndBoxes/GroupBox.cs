using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Themed GroupBox for WinPaletter UI")]
    [DefaultEvent("Click")]
    public class GroupBox : Panel
    {

        public GroupBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var G = e.Graphics;
            DoubleBuffered = true;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            var Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            var ParentColor = this.GetParentColor();

            G.Clear(ParentColor);
            BackColor = ParentColor.CB((float)(ParentColor.IsDark() ? 0.04d : -0.05d));
            using (var br = new SolidBrush(BackColor))
            {
                G.FillRoundedRect(br, Rect);
            }
            using (var P = new Pen(ParentColor.CB((float)(ParentColor.IsDark() ? 0.06d : -0.07d))))
            {
                G.DrawRoundedRect(P, Rect);
            }
        }

    }

}