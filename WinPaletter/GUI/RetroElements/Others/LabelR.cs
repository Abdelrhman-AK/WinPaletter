using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Templates;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A retro label with the ability to be drawn as single-bit per pixel.
    /// </summary>
    public partial class LabelR : Label
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelR"/> class.
        /// </summary>
        public LabelR()
        {
            DoubleBuffered = true;
            SetStyle();
        }

        #region Variables

        Rectangle rect;
        Rectangle itemText;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the control colors can be edited by the user after clicking on it.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Events/Overrides

        /// <summary>
        /// Occurs when the user clicks on the control to edit its colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Occurs when the user clicks on the control to edit its colors.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// Raises the <see cref="Control.Click"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                if (CursorOnText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.WindowText)));
            }

            base.OnClick(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.FontChanged"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            // Set the style after the font is changed.
            if (IsHandleCreated) SetStyle();

            base.OnFontChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.Resize"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            // Set the style after the control is resized.
            SetStyle();

            base.OnResize(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Check if the cursor is on the text.
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnText = itemText.Contains(e.Location);
                Refresh();
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseLeave"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                // Reset flag.
                CursorOnText = false;
                Refresh();
            }

            base.OnMouseLeave(e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the style of the control.
        /// </summary>
        private void SetStyle()
        {
            // Set the rectangle of the control.
            rect = new Rectangle(0, 0, Width - 1, Height - 1);

            // Measure the size of the first item.
            SizeF item0Size = Text.Measure(Font);

            // Set the rectangle of the text.
            if (AutoSize)
            {
                // Center the text.
                itemText = new Rectangle((Width / 2) - ((int)item0Size.Width / 2), (Height / 2) - ((int)item0Size.Height / 2), (int)item0Size.Width - 1, (int)item0Size.Height - 1);
            }
            else
            {
                // Set the rectangle of the text.
                itemText = TextAlign switch
                {
                    ContentAlignment.TopLeft => new Rectangle(0, 0, (int)item0Size.Width - 1, (int)item0Size.Height - 1),
                    ContentAlignment.TopCenter => new Rectangle((Width / 2) - ((int)item0Size.Width / 2), 0, (int)item0Size.Width - 1, (int)item0Size.Height - 1),
                    ContentAlignment.TopRight => new Rectangle(Width - (int)item0Size.Width, 0, (int)item0Size.Width - 1, (int)item0Size.Height - 1),
                    ContentAlignment.MiddleLeft => new Rectangle(0, (Height / 2) - ((int)item0Size.Height / 2), (int)item0Size.Width - 1, (int)item0Size.Height - 1),
                    ContentAlignment.MiddleCenter => new Rectangle((Width / 2) - ((int)item0Size.Width / 2), (Height / 2) - ((int)item0Size.Height / 2), (int)item0Size.Width - 1, (int)item0Size.Height - 1),
                    ContentAlignment.MiddleRight => new Rectangle(Width - (int)item0Size.Width, (Height / 2) - ((int)item0Size.Height / 2), (int)item0Size.Width - 1, (int)item0Size.Height - 1),
                    ContentAlignment.BottomLeft => new Rectangle(0, Height - (int)item0Size.Height, (int)item0Size.Width - 1, (int)item0Size.Height - 1),
                    ContentAlignment.BottomCenter => new Rectangle((Width / 2) - ((int)item0Size.Width / 2), Height - (int)item0Size.Height, (int)item0Size.Width - 1, (int)item0Size.Height - 1),
                    ContentAlignment.BottomRight => new Rectangle(Width - (int)item0Size.Width, Height - (int)item0Size.Height, (int)item0Size.Width - 1, (int)item0Size.Height - 1),
                    _ => new Rectangle(0, 0, (int)item0Size.Width - 1, (int)item0Size.Height - 1),
                };
            }
        }

        #endregion

        #region Colors editor

        private bool CursorOnText;
        private bool _ColorEdit_Text => EnableEditingColors && CursorOnText;

        #endregion

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Kept empty to draw transparent background.
            base.OnPaintBackground(pevent);
        }

        /// <summary>
        /// Paints the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            // Set the quality of the text.
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Draw the background.
            Rectangle rectangle = new(0, 0, Width - 1, Height - 1);

            // Draw a hatch brush if the user edit the colors by clicking on the control.
            if (_ColorEdit_Text)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (Pen P = new(color))
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                {
                    G.FillRectangle(hb, itemText);
                    G.DrawRectangle(P, itemText);
                }
            }

            // Draw the text.
            using (StringFormat sf = TextAlign.ToStringFormat())
            using (SolidBrush br = new(ForeColor)) { G.DrawString(Text, Font, br, rectangle, sf); }


        }
    }
}
