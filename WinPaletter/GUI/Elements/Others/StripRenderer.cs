using System.ComponentModel;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("StripRenderer fixed to respect dark/light mode")]
    public class StripRenderer : ToolStripSystemRenderer
    {
        public StripRenderer()
        {
        }
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
        }
    }

}