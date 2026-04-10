using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A magnified preview of a 3D panel with Windows 9x style.
    /// </summary>
    [Description("Retro 3D Panel Preview with Windows 9x style")]
    public class Preview3D : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Preview3D"/> class.
        /// </summary>
        public Preview3D()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        #region Private fields

        private Color _windowFrame = Color.Black;
        private Color _buttonShadow = Color.FromArgb(128, 128, 128);
        private Color _buttonDkShadow = Color.Black;
        private Color _buttonHilight = Color.White;
        private Color _buttonLight = Color.FromArgb(192, 192, 192);
        private int _lineSize = 6;

        // Cached brushes, rebuilt only when their source color changes.
        private SolidBrush _brushDkShadow = new(Color.Black);
        private SolidBrush _brushShadow = new(Color.FromArgb(128, 128, 128));
        private SolidBrush _brushHilight = new(Color.White);
        private SolidBrush _brushLight = new(Color.FromArgb(192, 192, 192));
        private SolidBrush _brushBack = new(Color.Empty);
        private SolidBrush _brushFore = new(Color.Empty);

        // Cached geometry, rebuilt only when LineSize or control size changes.
        private Rectangle _hilightTopRect;
        private Rectangle _hilightLeftRect;
        private Rectangle _lightTopRect;
        private Rectangle _lightLeftRect;
        private Rectangle _dkShadowRightRect;
        private Rectangle _dkShadowBottomRect;
        private Rectangle _shadowRightRect;
        private Rectangle _shadowBottomRect;
        private Rectangle _filling;
        private RectangleF _accentRect;

        #endregion

        #region Properties

        /// <summary>
        /// BackColor property (button face color).
        /// </summary>
        public new Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor == value) return;
                base.BackColor = value;
                ReplaceBrush(ref _brushBack, value);
                Invalidate();
            }
        }

        /// <summary>
        /// ForeColor property (accent fill color).
        /// </summary>
        public new Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                if (base.ForeColor == value) return;
                base.ForeColor = value;
                ReplaceBrush(ref _brushFore, value);
                Invalidate();
            }
        }

        /// <summary>
        /// Color of the border of a focused 3D button.
        /// </summary>
        public Color WindowFrame
        {
            get => _windowFrame;
            set
            {
                if (_windowFrame == value) return;
                _windowFrame = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Color of the shadow of a 3D button.
        /// </summary>
        public Color ButtonShadow
        {
            get => _buttonShadow;
            set
            {
                if (_buttonShadow == value) return;
                _buttonShadow = value;
                ReplaceBrush(ref _brushShadow, value);
                Invalidate();
            }
        }

        /// <summary>
        /// Color of the dark shadow of a 3D button.
        /// </summary>
        public Color ButtonDkShadow
        {
            get => _buttonDkShadow;
            set
            {
                if (_buttonDkShadow == value) return;
                _buttonDkShadow = value;
                ReplaceBrush(ref _brushDkShadow, value);
                Invalidate();
            }
        }

        /// <summary>
        /// Color of the hilight of a 3D button.
        /// </summary>
        public Color ButtonHilight
        {
            get => _buttonHilight;
            set
            {
                if (_buttonHilight == value) return;
                _buttonHilight = value;
                ReplaceBrush(ref _brushHilight, value);
                Invalidate();
            }
        }

        /// <summary>
        /// Color of the light of a 3D button.
        /// </summary>
        public Color ButtonLight
        {
            get => _buttonLight;
            set
            {
                if (_buttonLight == value) return;
                _buttonLight = value;
                ReplaceBrush(ref _brushLight, value);
                Invalidate();
            }
        }

        /// <summary>
        /// Thickness in pixels of each border layer of the 3D button.
        /// </summary>
        public int LineSize
        {
            get => _lineSize;
            set
            {
                if (_lineSize == value) return;
                _lineSize = value;
                RebuildGeometry();
                Invalidate();
            }
        }

        #endregion

        #region Geometry cache

        /// <summary>
        /// Recomputes all cached rectangles from the current <see cref="LineSize"/> and control bounds.
        /// Called on resize and whenever <see cref="LineSize"/> changes.
        /// </summary>
        private void RebuildGeometry()
        {
            int s = _lineSize;

            _hilightTopRect = new Rectangle(s, s, Width - s * 2, s);
            _hilightLeftRect = new Rectangle(s, s, s, Height - s * 2);

            _lightTopRect = new Rectangle(s * 2, s * 2, Width - s * 4, s);
            _lightLeftRect = new Rectangle(s * 2, s * 2, s, Height - s * 4);

            _dkShadowRightRect = new Rectangle(Width - s * 2, s, s, Height - s * 2);
            _dkShadowBottomRect = new Rectangle(s, Height - s * 2, Width - s * 2, s);

            _shadowRightRect = new Rectangle(Width - s * 3, s * 2, s, Height - s * 4);
            _shadowBottomRect = new Rectangle(s * 2, Height - s * 3, Width - s * 4, s);

            _filling = new Rectangle(
                _lightLeftRect.Right,
                _lightTopRect.Bottom,
                _shadowRightRect.Left - _lightLeftRect.Right,
                _shadowBottomRect.Top - _lightTopRect.Bottom
            );

            float tw = _filling.Width / 2f;
            float th = s * 1.75f;
            _accentRect = new RectangleF(
                _filling.X + (_filling.Width - tw) / 2f,
                _filling.Y + (_filling.Height - th) / 2f,
                tw,
                th
            );
        }

        /// <summary>
        /// Rebuilds geometry whenever the control is resized.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RebuildGeometry();
        }

        #endregion

        #region Brush helpers

        /// <summary>
        /// Disposes the existing brush and allocates a new one with the given color.
        /// </summary>
        private static void ReplaceBrush(ref SolidBrush brush, Color color)
        {
            brush?.Dispose();
            brush = new SolidBrush(color);
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the control.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            g.Clear(_windowFrame);

            g.FillRectangle(_brushHilight, _hilightTopRect);
            g.FillRectangle(_brushHilight, _hilightLeftRect);

            g.FillRectangle(_brushLight, _lightTopRect);
            g.FillRectangle(_brushLight, _lightLeftRect);

            g.FillRectangle(_brushDkShadow, _dkShadowRightRect);
            g.FillRectangle(_brushDkShadow, _dkShadowBottomRect);

            g.FillRectangle(_brushShadow, _shadowRightRect);
            g.FillRectangle(_brushShadow, _shadowBottomRect);

            g.FillRectangle(_brushBack, _filling);
            g.FillRectangle(_brushFore, _accentRect);
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Releases all cached GDI brushes when the control is disposed.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _brushDkShadow?.Dispose();
                _brushShadow?.Dispose();
                _brushHilight?.Dispose();
                _brushLight?.Dispose();
                _brushBack?.Dispose();
                _brushFore?.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}