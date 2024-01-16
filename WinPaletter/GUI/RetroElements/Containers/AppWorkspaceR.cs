using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    public class AppWorkspaceR : System.Windows.Forms.Panel
    {
        public AppWorkspaceR()
        {
            BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (EnableEditingColors)
            {
                CursorOnMe = true;
                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            if (EnableEditingColors)
            {
                CursorOnMe = false;
                Refresh();
            }

            base.OnMouseLeave(e);
        }

        protected override void OnClick(System.EventArgs e)
        {
            if (!DesignMode && _ColorEdit) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.AppWorkspace)));

            base.OnClick(e);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        private bool CursorOnMe;
        private bool _ColorEdit => EnableEditingColors && CursorOnMe;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_ColorEdit)
            {
                Rectangle rect = new(0, 0, Width - 1, Height - 1);
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { e.Graphics.FillRectangle(hb, rect); }
            }
        }
    }
}
