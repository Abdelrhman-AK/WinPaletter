using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("ToolStripStatusLabel fixed to respect dark/light mode")]
    public class ToolStripStatusLabel : System.Windows.Forms.ToolStripStatusLabel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            using (SolidBrush br = new(BackColor))
            {
                G.FillRectangle(br, new Rectangle(0, 0, Width, Height));
            }

            using (StringFormat sf = base.TextAlign.ToStringFormat())
            {
                using (SolidBrush br = new(ForeColor))
                {
                    G.DrawString(Text, Font, br, new Rectangle(0, 0, Width, Height), sf);
                }
            }


        }
    }
}