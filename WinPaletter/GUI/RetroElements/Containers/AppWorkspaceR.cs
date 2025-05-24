using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A retro-styled panel siumulating the app workspace.
    /// </summary>
    public class AppWorkspaceR : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppWorkspaceR"/> class.
        /// </summary>
        public AppWorkspaceR()
        {
            BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        /// <summary>
        /// Event handler for the editor invoker (Click to edit color).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Event handler for the editor invoker (Click to edit color).
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// Handles the MouseMove event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // If editing colors is enabled, set the cursor on the panel to indicate that it is clickable.
            if (EnableEditingColors)
            {
                CursorOnMe = true;
                Refresh();
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Handles the MouseLeave event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(System.EventArgs e)
        {
            // If editing colors is enabled, reset cursor on the panel flag.
            if (EnableEditingColors)
            {
                CursorOnMe = false;
                Refresh();
            }

            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Handles the Click event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(System.EventArgs e)
        {
            // If editing colors is enabled, invoke the editor.
            if (!DesignMode && _ColorEdit) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.AppWorkspace)));

            base.OnClick(e);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the color editing is enabled.
        /// </summary>

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        /// <summary>
        /// Gets or sets the flag indicating whether the cursor is on the panel or not.
        /// </summary>
        private bool CursorOnMe;

        /// <summary>
        /// Gets a value indicating whether the color editing is enabled and also the cursor is on the panel.
        /// </summary>
        private bool _ColorEdit => EnableEditingColors && CursorOnMe;

        /// <summary>
        /// Handles the Paint event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // If editing colors is enabled, draw a semi-transparent overlay on the panel to indicate that it is clickable.
            if (_ColorEdit)
            {
                Rectangle rect = new(0, 0, Width - 1, Height - 1);
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { e.Graphics.FillRectangle(hb, rect); }
            }
        }
    }
}
