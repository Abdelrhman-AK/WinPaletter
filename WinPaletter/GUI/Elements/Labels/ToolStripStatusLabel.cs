using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("ToolStripStatusLabel fixed to respect dark/light mode")]
    public class ToolStripStatusLabel : System.Windows.Forms.ToolStripStatusLabel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = Config.RenderingHint;

            using (var br = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(br, new Rectangle(0, 0, Width, Height));
            }
            using (var br = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(Text, Font, br, new Rectangle(0, 0, Width, Height), base.TextAlign.ToStringFormat());
            }
        }
    }

}