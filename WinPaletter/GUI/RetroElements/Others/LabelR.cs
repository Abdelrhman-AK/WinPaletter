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
    /// A retro label with color-edit hit-testing on the text region.
    /// </summary>
    public partial class LabelR : Label
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelR"/> class.
        /// </summary>
        public LabelR()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        #region Private fields

        // Tight StringFormat shared between measure and draw so glyph bounds are identical.
        private static readonly StringFormat MeasureFormat = new(StringFormat.GenericTypographic)
        {
            FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces
        };

        // Cached geometry — rebuilt when size, font, text, or alignment changes.
        private Rectangle _rectControl;  // full control bounds for DrawString layout
        private Rectangle _rectHitText;  // tight glyph bounds for hit-test and overlay

        // Hover state.
        private bool _cursorOnText;

        // Cached overlay blend color — recomputed only when BackColor changes.
        private Color _overlayColor;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the control colors can be edited by clicking.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        private bool IsEditText => EnableEditingColors && _cursorOnText;

        #endregion

        #region Editor event

        /// <summary>
        /// Raised when the user clicks the text region while <see cref="EnableEditingColors"/> is true.
        /// </summary>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Raised when the user clicks the text region while <see cref="EnableEditingColors"/> is true.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        #endregion

        #region Geometry cache

        /// <summary>
        /// Recomputes the tight text hit-test rect from the current Text, Font, Size, and TextAlign.
        /// Uses the same GDI+ measurement path as OnPaint so the overlay matches rendered glyphs exactly.
        /// </summary>
        private void RebuildGeometry()
        {
            _rectControl = new Rectangle(0, 0, Width - 1, Height - 1);

            if (string.IsNullOrEmpty(Text) || !IsHandleCreated)
            {
                _rectHitText = Rectangle.Empty;
                return;
            }

            using (Graphics g = Graphics.FromHwnd(Handle))
            using (StringFormat sf = TextAlign.ToStringFormat())
            {
                SizeF measured = g.MeasureString(Text, Font, _rectControl.Size, sf);

                int tw = (int)Math.Ceiling(measured.Width);
                int th = (int)Math.Ceiling(measured.Height);

                int x = TextAlign switch
                {
                    ContentAlignment.TopLeft or
                    ContentAlignment.MiddleLeft or
                    ContentAlignment.BottomLeft => 0,
                    ContentAlignment.TopCenter or
                    ContentAlignment.MiddleCenter or
                    ContentAlignment.BottomCenter => (Width - tw) / 2,
                    _ => Width - tw
                };

                int y = TextAlign switch
                {
                    ContentAlignment.TopLeft or
                    ContentAlignment.TopCenter or
                    ContentAlignment.TopRight => 0,
                    ContentAlignment.MiddleLeft or
                    ContentAlignment.MiddleCenter or
                    ContentAlignment.MiddleRight => (Height - th) / 2,
                    _ => Height - th
                };

                _rectHitText = new Rectangle(x, y, tw - 1, th);
            }
        }

        #endregion

        #region Overrides — layout triggers

        /// <summary>
        /// Rebuilds geometry when the control is resized.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RebuildGeometry();
            Invalidate();
        }

        /// <summary>
        /// Rebuilds geometry when the font changes.
        /// </summary>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (IsHandleCreated) RebuildGeometry();
        }

        /// <summary>
        /// Rebuilds geometry when the text changes.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (IsHandleCreated) RebuildGeometry();
        }

        /// <summary>
        /// Rebuilds geometry when text alignment changes.
        /// </summary>
        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            RebuildGeometry();
            Invalidate();
        }

        /// <summary>
        /// Rebuilds the cached overlay color when BackColor changes.
        /// </summary>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _overlayColor = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
        }

        #endregion

        #region Overrides — mouse interaction

        /// <summary>
        /// Updates hover state and invalidates only when it changes.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode || !EnableEditingColors) return;

            bool onText = _rectHitText.Contains(e.Location);

            if (onText != _cursorOnText)
            {
                _cursorOnText = onText;
                Invalidate();
            }
        }

        /// <summary>
        /// Clears hover state and invalidates only when it changes.
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!DesignMode && EnableEditingColors && _cursorOnText)
            {
                _cursorOnText = false;
                Invalidate();
            }
        }

        /// <summary>
        /// Invokes the color editor when the text region is clicked.
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (!DesignMode && IsEditText)
            {
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.WindowText)));
            }
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the label text and, when in color-edit mode, a hover overlay over the glyph bounds.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Hover overlay drawn before text so glyphs render cleanly on top.
            if (IsEditText)
            {
                using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
                using (Pen p = new(_overlayColor))
                {
                    G.FillRectangle(hb, _rectHitText);
                    G.DrawRectangle(p, _rectHitText);
                }
            }

            // Text rendered with the same MeasureFormat used in RebuildGeometry so
            // the overlay rect and the glyph positions are pixel-identical.
            using (StringFormat sf = TextAlign.ToStringFormat())
            using (SolidBrush br = new(ForeColor))
            {
                G.DrawString(Text, Font, br, _rectControl, sf);
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Releases the shared StringFormat on final disposal.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing) MeasureFormat.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}