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
    /// A retro tooltip control styled to match the Windows 9x tooltip appearance.
    /// Auto-sizes to fit its text. InfoWindow = background, InfoText = foreground, black single-pixel border.
    /// </summary>
    public class ToolTipR : ContainerControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolTipR"/> class.
        /// </summary>
        public ToolTipR()
        {
            AutoSize = false;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        #region Private fields

        // Win9x tooltip metrics.
        // Border:       1px solid black (COLOR_WINDOWFRAME).
        // Left indent:  2px between border and text.
        // Right pad:    2px between text and right border.
        // Vertical pad: 0px above and below text.
        private const int BorderThickness = 1;
        private const int TextPadLeft = 2;
        private const int TextPadRight = 4;
        private const int TextPadV = 0;

        // Cached geometry — rebuilt on resize or text/font change.
        private Rectangle _rectOuter;    // border outline
        private Rectangle _rectFace;     // InfoWindow fill area (inside border)
        private Rectangle _rectRender;   // full face height — passed to TextRenderer so
                                         // VerticalCenter works against the true available space
        private Rectangle _rectHitText;  // inset by TextPadV — registers as "text" for hit-test
        private Rectangle _rectHitFace;  // full face — registers as "background" for hit-test
        private Rectangle _rectHitBorder;  // 1px border strip — registers as "border" for hit-test

        // Hover state for color-edit overlays.
        private bool _cursorOnFace;
        private bool _cursorOnText;
        private bool _cursorOnBorder;

        // Cached overlay blend color — recomputed only when BackColor changes.
        private Color _overlayColor;

        // Re-entry guard: prevents RefreshPreferredSize from cycling through OnResize.
        private bool _updatingSize;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the colors of this control
        /// can be edited interactively by clicking on them.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        private bool IsEditFace => EnableEditingColors && _cursorOnFace;
        private bool IsEditText => EnableEditingColors && _cursorOnText;
        private bool IsEditBorder => EnableEditingColors && _cursorOnBorder;

        private Color _windowFrame = Color.Black;
        public Color WindowFrame
        {
            get => _windowFrame;
            set
            {
                _windowFrame = value;
                Invalidate(_rectOuter.IsEmpty ? new Rectangle(0, 0, Width - 1, Height - 1) : _rectOuter);
            }
        }

        #endregion

        #region Editor event

        /// <summary>
        /// Raised when the user clicks a color region while <see cref="EnableEditingColors"/> is true.
        /// </summary>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Raised when the user clicks a color region while <see cref="EnableEditingColors"/> is true.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        #endregion

        #region Geometry cache

        // Tight StringFormat for measuring only — GenericTypographic removes all
        // GDI+ internal string padding so the measured size matches DrawText output.
        private static readonly StringFormat MeasureFormat = new(StringFormat.GenericTypographic)
        {
            FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces
        };

        private void RefreshPreferredSize()
        {
            if (_updatingSize) return;

            SizeF measured;

            if (string.IsNullOrEmpty(Text))
            {
                measured = new SizeF(40, Font.Height);
            }
            else
            {
                // Use a temporary Graphics from a control handle for DPI-accurate measurement.
                // GenericTypographic eliminates the ~3px GDI+ padding that inflates MeasureString.
                using (Graphics G = Graphics.FromHwnd(Handle))
                {
                    measured = G.MeasureString(Text, Font, int.MaxValue, MeasureFormat);
                }
            }

            int w = (int)Math.Ceiling(measured.Width) + TextPadLeft + TextPadRight + BorderThickness * 2;
            int h = (int)Math.Ceiling(measured.Height) + TextPadV * 2 + BorderThickness * 2;

            if (Width == w && Height == h)
            {
                Invalidate();
                return;
            }

            _updatingSize = true;
            Size = new Size(w, h);
            _updatingSize = false;
        }

        /// <summary>
        /// Derives all cached rectangles from the current control dimensions.
        /// Called from OnResize and therefore kept allocation-free.
        /// </summary>
        private void RebuildGeometry()
        {
            // DrawRectangle is inclusive so subtract 1 from each dimension.
            _rectOuter = new Rectangle(0, 0, Width - 1, Height - 1);

            _rectFace = new Rectangle(BorderThickness, BorderThickness, Width - BorderThickness * 2 - 1, Height - BorderThickness * 2 - 1);

            // Render rect spans the full face height so VerticalCenter aligns against
            // the true available space rather than a pre-shrunk rect.
            _rectRender = new Rectangle(_rectFace.X + TextPadLeft, _rectFace.Y, _rectFace.Width - TextPadLeft - TextPadRight, _rectFace.Height);

            // Hit-test text rect: inset vertically by TextPadV so the padding strips
            // at top and bottom always register as background, not text.
            _rectHitText = new Rectangle(_rectRender.X, _rectFace.Y + TextPadV + 2, _rectRender.Width, _rectFace.Height - TextPadV * 2 - 4);

            // Full face is the background hit zone; OnMouseMove resolves priority by
            // checking text first and treating everything else as face.
            _rectHitFace = _rectFace;

            // Border hit zone is the full outer rect excluding the face interior.
            // Since _rectOuter is 1px thick, we simply use it directly — OnMouseMove
            // checks border last so face and text take priority over the shared edge pixels.
            _rectHitBorder = _rectOuter;
        }

        #endregion

        #region Overrides — layout triggers

        /// <summary>
        /// Resizes the control to fit the new text.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            RefreshPreferredSize();
        }

        /// <summary>
        /// Resizes the control to fit text rendered in the new font.
        /// </summary>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            RefreshPreferredSize();
        }

        /// <summary>
        /// Rebuilds cached geometry whenever the control dimensions change.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RebuildGeometry();
            Invalidate();
        }

        /// <summary>
        /// Rebuilds the cached overlay blend color when BackColor changes.
        /// </summary>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _overlayColor = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
            Invalidate();
        }

        #endregion

        #region Overrides — mouse interaction

        /// <summary>
        /// Clears hover state and invalidates only when state actually changes.
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!DesignMode && EnableEditingColors && (_cursorOnFace || _cursorOnText || _cursorOnBorder))
            {
                _cursorOnFace = false;
                _cursorOnText = false;
                _cursorOnBorder = false;
                Invalidate();
            }
        }

        /// <summary>
        /// Updates hover state using the dedicated hit-test rects and invalidates
        /// only when the active zone changes.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode || !EnableEditingColors) return;

            bool onText = _rectHitText.Contains(e.Location);
            bool onFace = !onText && _rectHitFace.Contains(e.Location);
            bool onBorder = !onText && !onFace && _rectOuter.BordersContains(e.Location);

            if (onText != _cursorOnText || onFace != _cursorOnFace || onBorder != _cursorOnBorder)
            {
                _cursorOnText = onText;
                _cursorOnFace = onFace;
                _cursorOnBorder = onBorder;
                Invalidate();
            }
        }

        /// <summary>
        /// Invokes the color editor for whichever zone the cursor is in.
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (DesignMode || !EnableEditingColors) return;

            if (IsEditText)
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.InfoText)));
            else if (IsEditFace)
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.InfoWindow)));
            else if (IsEditBorder)
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.WindowFrame)));
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the control to match the Windows 9x tooltip appearance.
        /// Draw order: background fill → overlays → text → border.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.None;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.SystemDefault : Program.Style.TextRenderingHint;

            // 1. Flat InfoWindow background — Win9x tooltips have no gradient or bevel.
            G.Clear(BackColor);

            // 2. Background hover overlay.
            if (IsEditFace)
            {
                using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
                {
                    G.FillRectangle(hb, _rectHitFace);
                }
            }

            // 3. InfoText — rendered with GDI+ DrawString using the same format as measurement
            //    so glyph bounds are pixel-identical to the sized control.
            using (SolidBrush br = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(Text, Font, br, _rectRender, MeasureFormat);
            }

            // 4. Text hover overlay with a 1px outline around the hit-test zone.
            if (IsEditText)
            {
                using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
                using (Pen P = new(_overlayColor))
                {
                    G.FillRectangle(hb, _rectHitText);
                    G.DrawRectangle(P, _rectHitText);
                }
            }

            // 5. Single-pixel black border drawn last so fills never overwrite it.
            //    Win9x COLOR_WINDOWFRAME is always black on the default theme.
            using (Pen P = new(_windowFrame))
            {
                G.DrawRectangle(P, _rectOuter);
            }

            // 6. Border hover overlay.
            if (IsEditBorder)
            {
                using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
                using (Pen p = new(_overlayColor, BorderThickness))
                {
                    G.DrawRectangle(p, _rectOuter);
                }
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing) MeasureFormat.Dispose();
            base.Dispose(disposing);
        }
    }
}