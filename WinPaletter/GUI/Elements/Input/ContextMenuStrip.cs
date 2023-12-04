using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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
                G.TextRenderingHint = TextRenderingHint.SystemDefault;

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
    }

    public partial class ContextMenuStrip : System.Windows.Forms.ContextMenuStrip
    {
        public ContextMenuStrip()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Renderer = new ContextMenuStripRenderer();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.SystemDefault;
            DoubleBuffered = true;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            G.Clear(Color.Transparent);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            G.FillRoundedRect(scheme.Brushes.Back, rect);
            G.DrawRoundedRect_LikeW11(scheme.Pens.Line, rect);

            base.OnPaint(e);    
        }
    }
}
