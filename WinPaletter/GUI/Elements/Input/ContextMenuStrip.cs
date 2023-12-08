using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [ToolboxItem(false)]
    public class ContextMenuStripRenderer : ToolStripRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                Graphics G = e.Graphics;
                G.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle itemRectangle = e.Item.ContentRectangle;
                itemRectangle.Height -= 1;

                G.FillRoundedRect(Program.Style.Schemes.Main.Brushes.Back_Checked_Hover, itemRectangle);
                G.DrawRoundedRect_LikeW11(Program.Style.Schemes.Main.Pens.Line_Checked_Hover, itemRectangle);
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                e.TextColor = Program.Style.Schemes.Main.Colors.ForeColor_Accent;
            }
            else
            {
                e.TextColor = Program.Style.DarkMode ? Color.White : Color.Black;
            }

            base.OnRenderItemText(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle itemRectangle = e.Item.ContentRectangle;
            itemRectangle.Y += 1;

            G.DrawLine(Program.Style.Schemes.Main.Pens.Line_Hover, itemRectangle.Location, itemRectangle.Location + new Size(e.Vertical ? 0 : itemRectangle.Width, e.Vertical ? itemRectangle.Height : 0));

            base.OnRenderSeparator(e);
        }
    }

    public partial class ContextMenuStrip : System.Windows.Forms.ContextMenuStrip
    {
        public ContextMenuStrip()
        {
            this.AllowTransparency = true;
            this.AutoClose = true;
            this.DropShadowEnabled = false;

            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Renderer = new ContextMenuStripRenderer();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Program.Style.RenderingHint;

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            G.FillRectangle(scheme.Brushes.Back, rect);
            G.DrawRectangle(scheme.Pens.Line_Hover, rect);

            base.OnPaint(e);    
        }
    }
}
