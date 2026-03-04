using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace WinPaletter.UI.WP
{
    [Description("AlertBox for WinPaletter UI")]
    public class AlertBox : Control
    {
        // Layout constants
        private const int PAD = 6;  // outer padding on every edge
        private const int PAD_AUTO = 2;  // small padding for AutoSize text-only mode
        private const int INNER_PAD = 4;  // gap between image and text
        private const int MIN_HEIGHT = 14; // never shrink below this
        private const int MIN_WIDTH = 20; // never shrink below this

        public AlertBox()
        {
            TabStop = false;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9f);
            Size = new Size(300, 40);
            CenterText = false;
            CustomColor = Color.FromArgb(0, 81, 210);
            BackColor = Color.Transparent;
        }

        #region Enums

        public enum Style
        {
            Adaptive,
            Simple,
            Success,
            Notice,
            Warning,
            Information,
            Indigo,
            Custom
        }

        #endregion

        #region Fields

        private Style _alertStyle;
        private Image _image;
        private bool _centerText;
        private string _text = string.Empty;
        private int _parentLevel;

        #endregion

        #region Properties

        [Category("Appearance")]
        public Style AlertStyle
        {
            get => _alertStyle;
            set
            {
                if (value == _alertStyle) return;
                _alertStyle = value;
                RefreshLayout();
            }
        }

        [Category("Appearance")]
        public Image Image
        {
            get => _image;
            set
            {
                _image = value;
                RefreshLayout();
            }
        }

        [Category("Appearance")]
        public Color CustomColor { get; set; }

        [Category("Appearance")]
        public bool CenterText
        {
            get => _centerText;
            set
            {
                if (value == _centerText) return;
                _centerText = value;
                RefreshLayout();
            }
        }

        /// <summary>
        /// When true the control resizes both width and height to exactly fit its content.
        /// Properly overrides the base Control.AutoSize so the designer serialises the value.
        /// </summary>
        [Browsable(true)]
        [Category("Layout")]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool AutoSize
        {
            get => base.AutoSize;
            set
            {
                if (base.AutoSize == value) return;
                base.AutoSize = value;
                // Tell the layout engine to re-evaluate immediately
                PerformLayout();
                RefreshLayout();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        public override string Text
        {
            get => _text;
            set
            {
                if (value == _text) return;
                _text = value ?? string.Empty;
                RefreshLayout();
            }
        }

        #endregion

        #region Layout helpers

        /// <summary>
        /// <summary>
        /// Returns the content rect, using zero padding when AutoSize and no image
        /// (the control is already sized to the raw text, so any inset would clip it).
        /// </summary>
        private Rectangle ContentRect(int controlWidth, int controlHeight)
        {
            int pad = (base.AutoSize && _image == null) ? PAD_AUTO : PAD;
            return new Rectangle(pad, pad,
                Math.Max(controlWidth - pad * 2, 0),
                Math.Max(controlHeight - pad * 2, 0));
        }

        private void CalcRects(Rectangle content, bool rtl, out Rectangle imageRect, out Rectangle textRect)
        {
            bool hasImage = _image != null;

            int imgW = hasImage ? _image.Width : 0;
            int imgH = hasImage ? _image.Height : 0;

            int textAvailW = content.Width - (hasImage ? imgW + INNER_PAD : 0);

            int imgX = content.Left;
            int textX = content.Left + (hasImage ? imgW + INNER_PAD : 0);

            if (rtl)
            {
                imgX = content.Right - imgW;
                textX = content.Left;
            }

            // Clamp all dimensions to zero — a negative width/height throws "Parameter is not valid"
            // when the control is very small (e.g. just dropped into the designer).
            imgW = Math.Max(imgW, 0);
            imgH = Math.Max(imgH, 0);
            int safeTextW = Math.Max(textAvailW, 0);
            int safeTextH = Math.Max(content.Height, 0);

            imageRect = hasImage
                ? new Rectangle(imgX, content.Top, imgW, imgH)
                : Rectangle.Empty;

            textRect = new Rectangle(textX, content.Top, safeTextW, safeTextH);
        }

        /// <summary>
        /// Measures text using the same GDI+ path as DrawString so AutoSize sizing
        /// and actual rendering are always consistent — no ellipsis on exact-fit strings.
        /// </summary>
        private SizeF MeasureTextExact(string text)
        {
            if (string.IsNullOrEmpty(text)) text = " ";
            using StringFormat sf = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip);
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            sf.Trimming = StringTrimming.None;
            // Use a Graphics from the screen at design-time, or from the control when live.
            using Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            return g.MeasureString(text, Font, int.MaxValue, sf);
        }

        /// <summary>
        /// Calculates the preferred size to exactly fit image + text.
        /// Both axes are derived from content — the current Size is never used as a constraint.
        /// </summary>
        private Size CalcPreferredSize()
        {
            int imgW = _image?.Width ?? 0;
            int imgH = _image?.Height ?? 0;
            bool hasImage = _image != null;

            SizeF textSz = MeasureTextExact(_text);

            int contentW = (hasImage ? imgW + INNER_PAD : 0) + (int)Math.Ceiling(textSz.Width);
            int contentH = (int)Math.Ceiling(textSz.Height);
            if (hasImage) contentH = Math.Max(contentH, imgH);

            // Text-only AutoSize uses a small padding instead of zero or full PAD.
            int padW = hasImage ? PAD * 2 : PAD_AUTO * 2;
            int padH = hasImage ? PAD * 2 : PAD_AUTO * 2;

            return new Size(
                Math.Max(contentW + padW, MIN_WIDTH),
                Math.Max(contentH + padH, MIN_HEIGHT));
        }

        /// <summary>
        /// Called whenever any property that affects layout changes.
        /// Resizes the control when AutoSize is on, then invalidates.
        /// </summary>
        private void RefreshLayout()
        {
            if (base.AutoSize)
            {
                Size preferred = CalcPreferredSize();
                if (preferred != Size)
                    Size = preferred;
            }
            Invalidate();
        }

        // The designer and layout engine use it when AutoSize is true.
        public override Size GetPreferredSize(Size proposedSize) => base.AutoSize ? CalcPreferredSize() : base.GetPreferredSize(proposedSize);

        #endregion

        #region Overrides

        protected override void Dispose(bool disposing)
        {
            if (disposing) _image?.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            _parentLevel = this.Level();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            RefreshLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // When something external resizes us while AutoSize is on, snap back.
            if (base.AutoSize) RefreshLayout();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Intentionally empty — keeps the control background transparent.
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Must be first: redraws parent background behind this transparent control.
            InvokePaintBackground(this, e);

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            bool rtl = (int)RightToLeft == 1;
            bool darkMode = Program.Style.DarkMode;

            Color borderColor, innerColor, textColor;

            Config.Scheme scheme1 = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Config.Scheme scheme2 = Enabled ? Program.Style.Schemes.Secondary : Program.Style.Schemes.Disabled;
            Config.Scheme scheme3 = Enabled ? Program.Style.Schemes.Tertiary : Program.Style.Schemes.Disabled;

            switch (_alertStyle)
            {
                case Style.Simple:
                    borderColor = scheme1.Colors.Line_Hover(_parentLevel + 4);
                    innerColor = scheme1.Colors.Back(_parentLevel);
                    textColor = scheme1.Colors.ForeColor;
                    break;

                case Style.Success:
                    borderColor = scheme3.Colors.Line_Checked_Hover;
                    innerColor = scheme3.Colors.Back_Checked_Hover;
                    textColor = scheme3.Colors.ForeColor_Accent;
                    break;

                case Style.Notice:
                    using (var c = new Config.Colors_Collection(Color.FromArgb(115, 113, 6), default, darkMode))
                    {
                        borderColor = c.Line_Checked;
                        innerColor = c.Back_Checked;
                        textColor = c.ForeColor_Accent;
                    }
                    break;

                case Style.Warning:
                    borderColor = scheme2.Colors.Line_Checked;
                    innerColor = scheme2.Colors.Back_Checked;
                    textColor = scheme2.Colors.ForeColor_Accent;
                    break;

                case Style.Information:
                    borderColor = scheme1.Colors.Line_Checked;
                    innerColor = scheme1.Colors.Back_Checked;
                    textColor = scheme1.Colors.ForeColor_Accent;
                    break;

                case Style.Indigo:
                    using (var c = new Config.Colors_Collection(Color.FromArgb(76, 0, 142), default, darkMode))
                    {
                        borderColor = c.Line_Checked_Hover;
                        innerColor = c.Back_Checked_Hover;
                        textColor = c.ForeColor_Accent;
                    }
                    break;

                case Style.Custom:
                    using (var c = new Config.Colors_Collection(CustomColor, default, darkMode))
                    {
                        borderColor = c.Line_Checked_Hover;
                        innerColor = c.Back_Checked_Hover;
                        textColor = c.ForeColor_Accent;
                    }
                    break;

                case Style.Adaptive:
                default:
                    Color seed = _image?.AverageColor() ?? CustomColor;
                    using (var c = new Config.Colors_Collection(seed, default, darkMode))
                    {
                        borderColor = c.Line_Checked;
                        innerColor = c.Back_Checked;
                        textColor = c.ForeColor_Accent;
                    }
                    break;
            }

            if (textColor.A == 0)
                textColor = darkMode ? Color.White : Color.Black;

            // Background + border
            Rectangle outerRect = new(0, 0, Width - 1, Height - 1);
            using (SolidBrush br = new(innerColor)) G.FillRoundedRect(br, outerRect);
            using (Pen p = new(borderColor)) G.DrawRoundedRect(p, outerRect);

            // Content layout
            Rectangle content = ContentRect(Width, Height);
            CalcRects(content, rtl, out Rectangle imageRect, out Rectangle textRect);

            if (_image != null && imageRect != Rectangle.Empty) G.DrawImage(_image, imageRect);

            if (!string.IsNullOrEmpty(_text) && textRect.Width > 0 && textRect.Height > 0)
            {
                using StringFormat sf = new(StringFormatFlags.NoClip);
                sf.Alignment = _centerText || AutoSize ? StringAlignment.Center : (rtl ? StringAlignment.Far : StringAlignment.Near);
                sf.LineAlignment = _image != null ? StringAlignment.Near : StringAlignment.Center;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                if (rtl) sf.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

                using SolidBrush br = new(textColor);
                G.DrawString(_text, Font, br, textRect, sf);
            }

            base.OnPaint(e);
        }

        #endregion
    }
}