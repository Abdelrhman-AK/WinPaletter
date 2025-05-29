using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("RadioButton, but with image for WinPaletter UI")]
    [DefaultEvent("CheckedChanged")]
    public class RadioImage : Control
    {
        public RadioImage()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | (ControlStyles)139286 | ControlStyles.Selectable, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Font = new("Segoe UI", 9f);
            ForeColor = Color.White;
            Text = string.Empty;

            _alpha = 0; _alpha2 = _checked ? 255 : 0;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new Color BackColor { get => Color.Transparent; set {; } }

        private bool _checked;
        public bool Checked
        {
            get => _checked;
            set
            {
                if (value != _checked)
                {
                    _checked = value;
                    if (_checked) { UncheckOthersOnChecked(); }

                    if (CanAnimate)
                    {
                        FluentTransitions.Transition.With(this, nameof(alpha2), _checked ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                    }
                    else { alpha2 = Checked ? 255 : 0; }

                    CheckedChanged?.Invoke(this, new EventArgs());
                }
            }
        }


        private Image _image; 
        private Image _imageDisabled;
        public Image Image
        {
            get => Enabled ? _image : _imageDisabled;
            set
            {
                if (value != _image)
                {
                    _image = value;
                    _imageDisabled = value?.Grayscale() ?? null;
                    Invalidate();
                }
            }
        }


        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        public ContentAlignment TextAlign { get; set; } = ContentAlignment.MiddleCenter;
        public ContentAlignment ImageAlign { get; set; } = ContentAlignment.MiddleCenter;
        public TextImageRelation TextImageRelation { get; set; } = TextImageRelation.ImageAboveText;

        #endregion

        #region Events/Overrides

        public event EventHandler CheckedChanged;

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!Checked && e.Data.GetData(typeof(Controllers.ColorItem).FullName) is Controllers.ColorItem)
            {
                e.Effect = DragDropEffects.None;
                Checked = true;
            }
            else
            {
                return;
            }

            base.OnDragOver(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Checked = true;
            State = MouseState.Down;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), ContainsFocus ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = ContainsFocus ? 255 : 0; }

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = MouseState.None;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseLeave(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _image?.Dispose();
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }

        #endregion

        #region Methods
        private void UncheckOthersOnChecked()
        {
            if (Parent is null)
                return;

            foreach (Control C in Parent.Controls)
            {
                if (C != this && C is RadioImage)
                {
                    ((RadioImage)C).Checked = false;
                }
            }
        }

        #endregion

        #region Animator
        private int _alpha = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Invalidate(); }
        }

        private int _alpha2 = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha2
        {
            get => _alpha2;
            set { _alpha2 = value; Invalidate(); }
        }
        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            Rectangle MainRect = new(0, 0, Width - 1, Height - 1);
            Rectangle MainRectInner = new(1, 1, Width - 3, Height - 3);
            Rectangle PaddingRect = Rectangle.FromLTRB(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);
            Rectangle TextAndImageRect = new(3 + PaddingRect.X, 3 + PaddingRect.Y, Width - 7 - PaddingRect.Width * 2, Height - 7 - PaddingRect.Height * 2);

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Color back = Color.FromArgb(alpha, scheme.Colors.Back_Checked_Hover);
            Color line = Color.FromArgb(255 - alpha, _checked ? scheme.Colors.Line_Checked : State != MouseState.Over ? scheme.Colors.Line(parentLevel) : scheme.Colors.Line_Checked_Hover);
            Color line_hover = Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover);

            using (LinearGradientBrush br = new(MainRect, Color.FromArgb(_alpha2, scheme.Colors.Back_Checked), Color.FromArgb(_alpha2, scheme.Colors.Back_Checked_Hover), LinearGradientMode.ForwardDiagonal))
            {
                G.FillRoundedRect(br, MainRect);
            }

            using (SolidBrush br = new(Color.FromArgb(255 - _alpha2, scheme.Colors.Back(parentLevel)))) { G.FillRoundedRect(br, MainRectInner); }

            using (SolidBrush br = new(back)) { G.FillRoundedRect(br, MainRect); }

            using (Pen P = new(line)) { G.DrawRoundedRectBeveledReverse(P, MainRectInner); }

            using (Pen P = new(line_hover)) { G.DrawRoundedRectBeveledReverse(P, MainRect); }

            this.GetTextAndImageRectangles(TextAndImageRect, out RectangleF imageRectF, out RectangleF textRectF);

            // Draw image
            if (Image != null) G.DrawImage(Image, Rectangle.Round(imageRectF));

            // Draw text
            if (!string.IsNullOrEmpty(Text))
            {
                if (TextAndImageRect.Height % 2 == 0) textRectF.Y += 0.5f;

                if (Enabled)
                {
                    TextRenderer.DrawText(G, Text, Font, Rectangle.Round(textRectF), ForeColor, TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    Rectangle disabledTextRect = Rectangle.Round(textRectF);
                    disabledTextRect.X--; disabledTextRect.Y--;
                    ControlPaint.DrawStringDisabled(G, Text, Font, ForeColor, disabledTextRect, TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);
                }
            }

            base.OnPaint(e);
        }
    }
}