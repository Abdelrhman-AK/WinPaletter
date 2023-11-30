using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("ToolStripMenuItem fixed to respect dark/light mode")]
    public class ToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;

            G.TextRenderingHint = Program.Style.RenderingHint;
            G.Clear(BackColor);

            using (StringFormat sf = base.TextAlign.ToStringFormat())
            {
                using (SolidBrush br = new(ForeColor))
                {
                    G.DrawString(Text, Font, br, new Rectangle(0, 0, Width, Height), sf);
                }
            }

            base.OnPaint(e);
        }
    }
}