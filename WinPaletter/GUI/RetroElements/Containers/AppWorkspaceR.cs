using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinPaletter.Templates;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A retro-styled panel simulating the app workspace.
    /// </summary>
    public class AppWorkspaceR : Panel
    {
        // Cached overlay color, recomputed only when BackColor changes.
        private Color _overlayColor = Color.Transparent;

        // Tracks whether the cursor is currently hovering over the panel.
        private bool _cursorOnMe = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppWorkspaceR"/> class.
        /// </summary>
        public AppWorkspaceR()
        {
            BorderStyle = BorderStyle.None;
        }

        /// <summary>
        /// Event raised when the user clicks the panel while color editing is active.
        /// </summary>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Event raised when the user clicks the panel while color editing is active.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// Gets or sets a value indicating whether color editing interactions are enabled.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        /// <summary>
        /// Returns true when the panel is in an interactive color-edit hover state.
        /// </summary>
        private bool IsColorEditActive => EnableEditingColors && _cursorOnMe;

        /// <summary>
        /// Recomputes the cached overlay color when BackColor changes.
        /// </summary>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _overlayColor = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
        }

        /// <summary>
        /// Sets the hover state and invalidates only when the state actually changes.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (EnableEditingColors && !_cursorOnMe)
            {
                _cursorOnMe = true;
                Invalidate();
            }
        }

        /// <summary>
        /// Clears the hover state and invalidates only when the state actually changes.
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (EnableEditingColors && _cursorOnMe)
            {
                _cursorOnMe = false;
                Invalidate();
            }
        }

        /// <summary>
        /// Invokes the editor when clicked in color-edit mode.
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (!DesignMode && IsColorEditActive)
            {
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.AppWorkspace)));
            }
        }

        /// <summary>
        /// Paints the hover overlay when color editing is active.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!IsColorEditActive) return;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
            {
                e.Graphics.FillRectangle(hb, rect);
            }
        }
    }
}