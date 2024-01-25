using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("ToolStripMenuItem fixed to respect dark/light mode")]
    public class ToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem
    {
        public ToolStripMenuItem()
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.TextRenderingHint = Program.Style.RenderingHint;

            if (this.Enabled && this.Selected)
            {
                G.FillRectangle(SystemBrushes.MenuHighlight, new Rectangle(1, 1, Width - 2, Height - 2));
            }
            else
            {
                using (SolidBrush br = new(BackColor))
                {
                    G.FillRectangle(br, new Rectangle(1, 1, Width - 2, Height - 2));
                }
            }

            using (StringFormat sf = base.TextAlign.ToStringFormat())
            {
                using (SolidBrush br = new(ForeColor))
                {
                    G.DrawString(Text, Font, br, new Rectangle(1, 1, Width - 2, Height - 2), sf);
                }
            }
        }
    }
}