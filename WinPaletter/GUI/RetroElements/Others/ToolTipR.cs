using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A retro tooltip control.
    /// </summary>
    public class ToolTipR : ContainerControl
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolTipR"/> class.
        /// </summary>
        public ToolTipR()
        {
            AutoSize = false;
        }

        #region Variables

        Rectangle Rect;
        Rectangle RectText;

        #endregion

        #region Properties

        /// <summary>
        /// A value indicating whether the colors of control can be edited.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Events/Overrides

        /// <summary>
        /// Occurs when the editor is invoked to edit the colors after clicking on the control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Occurs when the editor is invoked to edit the colors after clicking on the control.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            // Set the style of the control based on the font.
            SetStyle();

            base.OnFontChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Set the style of the control based on the size.
            SetStyle();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            // Reset the flags
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnFace = false;
                CursorOnText = false;
                Refresh();
            }

            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Set the flags based on the cursor position on the control.
            if (!DesignMode && EnableEditingColors)
            {
                CursorOnText = RectText.Contains(e.Location);
                CursorOnFace = Rect.Contains(e.Location) && !CursorOnText;

                Refresh();
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            // Invoke the editor based on the cursor position.
            if (!DesignMode && EnableEditingColors)
            {
                // Invoke editing Info Text color based on the cursor position.
                if (CursorOnText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.InfoText)));

                // Invoke editing Info Window color based on the cursor position.
                else if (CursorOnFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.InfoWindow)));
            }

            base.OnClick(e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the style of the control based on the size and font.
        /// </summary>
        private void SetStyle()
        {
            SizeF TextSize = Text.Measure(Font) - new Size(10, 10);
            Size = new Size((int)TextSize.Width, (int)TextSize.Height) + new Size(10, 8);
            Rect = new(0, 0, Width - 1, Height - 1);

            RectText = new Rectangle(Rect.X + (Rect.Width - (int)TextSize.Width) / 2 + 1, Rect.Y + (Rect.Height - (int)TextSize.Height) / 2 + 1, (int)TextSize.Width, (int)TextSize.Height);
        }

        #endregion

        #region Colors editor

        private bool CursorOnFace, CursorOnText;
        private bool _ColorEdit_Face => EnableEditingColors && CursorOnFace;
        private bool _ColorEdit_Text => EnableEditingColors && CursorOnText;

        #endregion

        /// <summary>
        /// Paints the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;

            // Set the text rendering hint.
            G.TextRenderingHint = DesignMode ? System.Drawing.Text.TextRenderingHint.SystemDefault : Program.Style.TextRenderingHint;

            // Draw the background of the control.
            G.Clear(BackColor);

            #region Editor

            // Draw a hatch brush on the control to indicate that the color can be edited.
            if (_ColorEdit_Face)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent)) { G.FillRectangle(hb, Rect); }
            }

            #endregion

            // Draw the text of the control.
            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
            using (SolidBrush br = new(ForeColor))
            {
                G.DrawString(Text, Font, br, new Rectangle(Rect.X, Rect.Y + 1, Rect.Width, Rect.Height), sf);
            }

            #region Editor

            // Draw a hatch brush on the text of the control to indicate that the color can be edited.
            if (_ColorEdit_Text)
            {
                Color color = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
                using (Pen P = new(color))
                using (HatchBrush hb = new(HatchStyle.Percent25, color, Color.Transparent))
                {
                    G.FillRectangle(hb, RectText);
                    G.DrawRectangle(P, RectText);
                }
            }

            #endregion

            // Draw the border of the control.
            G.DrawRectangle(Pens.Black, Rect);


        }
    }
}
