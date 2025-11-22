using FluentTransitions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Properties;
using static WinPaletter.UI.Style.Config;

namespace WinPaletter.UI.WP
{
    [Description("Themed Button for WinPaletter UI")]
    public class Button : System.Windows.Forms.Button
    {
        public Button()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            _color = Colorize();
            _lineColor = LineColor(_color);

            _hoverSize = 0;

            Font = new("Segoe UI", 9f);
            Image = base.Image;
            if (Image is not null) { Flag = Flags.TintedOnHover; }

            _alpha = 0;
            MenuSplitterRectangle = new(Width - MenuSplitterWidth, 0, MenuSplitterWidth, Height);

            Menu.ItemClicked += SplitMenuStrip_ItemClicked;

            Menu.ItemHeight = 24;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this is not null && Visible && Parent is not null && Parent.Visible && FindForm() is not null && FindForm().Visible;

        public ContextMenuStrip Menu = new() { ShowImageMargin = true, AllowTransparency = true };
        private readonly int MenuSplitterWidth = 15;
        Rectangle MenuSplitterRectangle;
        bool isMouseOverMenuSplitter = false;

        private static readonly TextureBrush Noise = new(Resources.Noise.Fade(0.6f));
        private Color imageColor;

        public MouseState State = MouseState.None;
        private Point hoverPosition;
        private RectangleF hoverRect;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        private readonly bool _ripple = true;
        private bool ForceUpdateImageGlyph = false;

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new Color BackColor { get => Color.Transparent; set {; } }

        private Image _image;
        private Image _imageDisabled;
        public new Image Image
        {
            get => Enabled ? _image : _imageDisabled;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    _imageDisabled = value?.Grayscale() ?? null;

                    if (_image is not null)
                    {
                        imageColor = Image.AverageColor();

                        if (Flag == Flags.None) { Flag = Flags.TintedOnHover; }

                        if (!_imageGlyphEnabled) _color = Colorize();
                    }

                    Invalidate();
                }
            }
        }

        private Image _imageGlyphUnmodified;
        private Image _imageGlyph;
        private Image _imageGlyphOver;
        private Image _imageGlyphDown;
        private Image _imageGlyphDisabled;

        public Image ImageGlyph
        {
            get
            {
                if (Enabled)
                {
                    if (!DesignMode)
                    {
                        return State switch
                        {
                            MouseState.None => Program.Style.DarkMode ? _imageGlyph : ((Bitmap)_imageGlyph)?.Invert().Fade(0.85f),
                            MouseState.Over => _imageGlyphOver,
                            MouseState.Down => _imageGlyphDown,
                            _ => Program.Style.DarkMode ? _imageGlyph : ((Bitmap)_imageGlyph)?.Invert().Fade(0.85f),
                        };
                    }
                    else
                    {
                        return _imageGlyph;
                    }
                }
                else
                {
                    return _imageGlyphDisabled;
                }
            }
            set
            {
                if (_imageGlyphUnmodified != value || ForceUpdateImageGlyph)
                {
                    UpdateImageGlyph(value);
                    Invalidate();
                }
            }
        }

        private void UpdateImageGlyph(Image glyph)
        {
            _imageGlyphUnmodified = glyph;

            if (_imageGlyphEnabled) _color = Colorize();

            using (Colors_Collection colors = new(_customColor, _customColor, Program.Style.DarkMode))
            {
                _imageGlyph = glyph;
                _imageGlyphOver = ((Bitmap)glyph)?.Tint(colors.ForeColor_Accent);
                _imageGlyphDown = ((Bitmap)glyph)?.Tint(colors.ForeColor_Accent);
                _imageGlyphDisabled = ((Bitmap)glyph)?.Fade(0.5f);
            }
        }

        private Color _color;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Color Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    _lineColor = LineColor(value);
                    Invalidate();
                }
            }
        }


        private Color _customColor;
        public Color CustomColor
        {
            get => _customColor;
            set
            {
                if (_customColor != value)
                {
                    _customColor = value;
                    _color = Colorize();
                    _lineColor = LineColor(value);
                    UpdateImageGlyph(_imageGlyphUnmodified);
                    Invalidate();
                }
            }
        }


        private Color _lineColor;
        private Color _rippleColor;

        private Color Colorize()
        {
            Color AccentColor = scheme1.Colors.Accent;
            Color resultColor = scheme1.Colors.Back(parentLevel);

            switch (Flag)
            {
                case Flags.AlwaysTinted:
                    {
                        AccentColor = scheme1.Colors.AccentAlt;
                        break;
                    }
                case Flags.AlwaysError:
                    {
                        AccentColor = scheme2.Colors.AccentAlt;
                        break;
                    }
                case Flags.AlwaysTertiary:
                    {
                        AccentColor = scheme3.Colors.AccentAlt;
                        break;
                    }
                case Flags.AlwaysCustomColor:
                    {
                        AccentColor = _customColor;
                        break;
                    }
                case Flags.TintedOnHover:
                    {
                        AccentColor = scheme1.Colors.AccentAlt;
                        break;
                    }
                case Flags.ErrorOnHover:
                    {
                        AccentColor = scheme2.Colors.AccentAlt;
                        break;
                    }
                case Flags.TertiaryOnHover:
                    {
                        AccentColor = scheme3.Colors.AccentAlt;
                        break;
                    }
                case Flags.CustomColorOnHover:
                    {
                        AccentColor = _customColor;
                        break;
                    }
            }

            using (Colors_Collection imageColors = new(imageColor, imageColor, Program.Style.DarkMode))
            using (Colors_Collection accentColors = new(AccentColor, AccentColor, Program.Style.DarkMode))
            {
                switch (State)
                {
                    case MouseState.None:
                        {
                            if (Flag == Flags.None || Flag == Flags.TintedOnHover || Flag == Flags.ErrorOnHover || Flag == Flags.TertiaryOnHover || Flag == Flags.CustomColorOnHover)
                            {
                                resultColor = scheme1.Colors.Button;
                            }
                            else
                            {
                                if (!_imageGlyphEnabled && _image != null)
                                {
                                    resultColor = Enabled ? imageColors.Back_Checked : accentColors.Back_Checked;
                                }

                                else
                                {
                                    resultColor = accentColors.Back_Checked;
                                }
                            }

                            break;
                        }

                    case MouseState.Over:
                        {
                            if (Flag == Flags.None) { resultColor = scheme1.Colors.Button_Over; }

                            else
                            {
                                if (!_imageGlyphEnabled && _image != null)
                                {
                                    resultColor = imageColors.Back_Checked;
                                }

                                else
                                {
                                    resultColor = accentColors.Back_Checked;
                                }
                            }

                            break;
                        }

                    case MouseState.Down:
                        {
                            if (Flag == Flags.None) { resultColor = scheme1.Colors.Button_Down; }

                            else
                            {
                                if (!_imageGlyphEnabled && _image != null)
                                {
                                    resultColor = imageColors.Back_Checked_Hover;
                                }

                                else
                                {
                                    resultColor = accentColors.Back_Checked_Hover;
                                }
                            }

                            break;
                        }
                }
            }

            float coloringFactor_Circle_Over_Dark = 0.35f;
            float coloringFactor_Circle_Over_Light = -0.08f;

            float coloringFactor_Circle_Down_Dark = 0.3f;
            float coloringFactor_Circle_Down_Light = -0.1f;

            switch (State)
            {
                case MouseState.Over:
                    {
                        _rippleColor = Program.Style.DarkMode ? resultColor.Light(coloringFactor_Circle_Over_Dark) : resultColor.CB(coloringFactor_Circle_Over_Light);
                        break;
                    }

                case MouseState.Down:
                    {
                        _rippleColor = Program.Style.DarkMode ? resultColor.Light(coloringFactor_Circle_Down_Dark) : resultColor.CB(coloringFactor_Circle_Down_Light);
                        break;
                    }
            }

            _lineColor = LineColor(resultColor);

            return resultColor;
        }

        private Color LineColor(Color baseColor = default)
        {
            if (baseColor == default) baseColor = _color;

            switch (State)
            {
                case MouseState.None:
                    {
                        return baseColor.CB(Color.IsDark() ? 0.01f : -0.07f);
                    }

                case MouseState.Over:
                    {
                        return baseColor.CB(Color.IsDark() ? 0.07f : -0.1f);
                    }

                case MouseState.Down:
                    {
                        return baseColor.CB(Color.IsDark() ? 0.1f : -0.08f);
                    }
                default:
                    {
                        return baseColor.CB(Color.IsDark() ? 0.02f : -0.04f);
                    }
            }
        }


        private Flags _flag = Flags.None;
        public Flags Flag
        {
            get => _flag;
            set
            {
                if (_flag != value)
                {
                    _flag = value;
                    _color = Colorize();
                    _lineColor = LineColor(_color);
                    Invalidate();
                }
            }
        }

        [Flags]
        public enum Flags
        {
            None,
            TintedOnHover,
            ErrorOnHover,
            TertiaryOnHover,
            CustomColorOnHover,
            AlwaysTinted,
            AlwaysError,
            AlwaysTertiary,
            AlwaysCustomColor
        }

        private Scheme scheme1 => Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
        private Scheme scheme2 => Enabled ? Program.Style.Schemes.Secondary : Program.Style.Schemes.Disabled;
        private Scheme scheme3 => Enabled ? Program.Style.Schemes.Tertiary : Program.Style.Schemes.Disabled;


        private bool _imageGlyphEnabled = false;
        public bool ImageGlyphEnabled
        {
            get => _imageGlyphEnabled;
            set
            {
                if (_imageGlyphEnabled != value)
                {
                    _imageGlyphEnabled = value;
                    _color = Colorize();
                    Invalidate();
                }
            }
        }

        #endregion

        #region Events/Overrides

        #region OnMouse
        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            isMouseOverMenuSplitter = Menu.Items.Count > 0 && MenuSplitterRectangle.Contains(PointToClient(MousePosition));

            Animate();

            if (CanAnimate)
            {
                Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    Transition.With(this, nameof(HoverSize), Math.Max(Width, Height)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                }
            }
            else
            {
                alpha = 255;
                HoverSize = Math.Max(Width, Height);
            }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = MouseState.None;

            Animate();

            if (CanAnimate)
            {
                Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    Transition.With(this, nameof(HoverSize), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                }
            }
            else
            {
                alpha = 0;
                HoverSize = 0;
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = MouseState.Down;

            Animate();

            if (CanAnimate)
            {
                Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    Transition.With(this, nameof(HoverSize), Math.Max(Width, Height) * 5).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                }
            }
            else
            {
                alpha = 0;
                HoverSize = Math.Max(Width, Height) * 5;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = ClientRectangle.Contains(e.Location) ? MouseState.Over : MouseState.None;

            if (CanAnimate)
            {
                Transition.With(this, nameof(alpha), ContainsFocus && State == MouseState.Over ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    Transition.With(this, nameof(HoverSize), ContainsFocus && State == MouseState.Over ? Math.Max(Width, Height) : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                }
            }
            else
            {
                alpha = ContainsFocus && State == MouseState.Over ? 255 : 0;
                HoverSize = ContainsFocus && State == MouseState.Over ? Math.Max(Width, Height) : 0;
            }

            base.OnMouseUp(e);
        }

        #endregion

        #region OnKey

        protected override void OnKeyDown(KeyEventArgs e)
        {
            State = MouseState.Down;

            Animate();

            if (CanAnimate)
            {
                Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    Transition.With(this, nameof(HoverSize), Math.Max(Width, Height) * 5).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                }
            }
            else
            {
                alpha = 0;
                HoverSize = Math.Max(Width, Height) * 5;
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            State = MouseState.None;

            _hoverSize = Math.Max(Width, Height);
            hoverRect = new(hoverPosition.X - 0.5f * _hoverSize, hoverPosition.Y - 0.5f * _hoverSize, _hoverSize, _hoverSize);

            if (CanAnimate) { Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnKeyUp(e);
        }

        #endregion

        public delegate void ItemClickedEventHandler(object sender, ToolStripItemClickedEventArgs e);

        public event ItemClickedEventHandler ItemClicked;

        private void SplitMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (ItemClicked != null) { ItemClicked(this, e); }
        }

        protected override void OnClick(EventArgs e)
        {
            if (Menu.Items.Count > 0)
            {
                if (MenuSplitterRectangle.Contains(PointToClient(MousePosition)))
                {
                    Menu.Show(this, new Point(15, Height));
                }
                else
                {
                    base.OnClick(e);
                }
            }
            else
            {
                base.OnClick(e);
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            State = MouseState.None;
            isMouseOverMenuSplitter = false;

            Animate();

            if (CanAnimate) { Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            isMouseOverMenuSplitter = Menu.Items.Count > 0 && MenuSplitterRectangle.Contains(PointToClient(MousePosition));

            if (CanAnimate && _ripple && State != MouseState.None)
            {
                hoverPosition = e.Location;
                hoverRect.X = hoverPosition.X - 0.5f * _hoverSize;
                hoverRect.Y = hoverPosition.Y - 0.5f * _hoverSize;

                Invalidate();
            }

            base.OnMouseMove(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            State = MouseState.None;

            Animate();

            if (CanAnimate)
            {
                Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    Transition.With(this, nameof(HoverSize), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                }
            }
            else
            {
                alpha = 0;
                HoverSize = 0;
            }

            base.OnLostFocus(e);
        }

        protected override async void OnEnabledChanged(EventArgs e)
        {
            UpdateStyleSchemes();

            await Task.Delay(10);
            Invalidate();

            base.OnEnabledChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            MenuSplitterRectangle = new(Width - MenuSplitterWidth, 0, MenuSplitterWidth, Height);
            base.OnSizeChanged(e);
        }

        public void UpdateStyleSchemes()
        {
            alpha = 0;
            HoverSize = 0;
            State = MouseState.None;

            _color = Colorize();
            _lineColor = LineColor(_color);

            ForceUpdateImageGlyph = true;
            ImageGlyph = _imageGlyphUnmodified;
            ForceUpdateImageGlyph = false;

            Animate();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            //Menu?.Dispose();
            //_image?.Dispose();   
            //_imageGlyph?.Dispose();
            //_imageGlyphOver?.Dispose();
            //_imageGlyphDown?.Dispose();
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }


        #endregion

        #region Animator
        private void Animate()
        {
            if (CanAnimate)
            {
                if (State != MouseState.None) { hoverRect = new(hoverPosition.X - 0.5f * _hoverSize, hoverPosition.Y - 0.5f * _hoverSize, _hoverSize, _hoverSize); }

                Transition.With(this, nameof(Color), Colorize()).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                Color = Colorize();
            }
        }

        private int _alpha = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Invalidate(); }
        }

        private int _hoverSize;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int HoverSize
        {
            get => _hoverSize;
            set
            {
                _hoverSize = value;
                hoverRect = new(hoverPosition.X - 0.5f * _hoverSize, hoverPosition.Y - 0.5f * _hoverSize, _hoverSize, _hoverSize);
            }
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

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            // ################################################################################# Customizer

            int MenuModifier = Menu.Items.Count > 0 ? MenuSplitterWidth : 0;

            Rectangle Rect = new(0, 0, Width - 1, Height - 1);
            Rectangle RectText = new(0, 0, Width - 1 - MenuModifier, Height - 1);
            Rectangle RectInner = new(1, 1, Width - 3, Height - 3);
            Rectangle PaddingRect = Rectangle.FromLTRB(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);
            Rectangle TextAndImageRect = new(RectText.X + 3 + PaddingRect.X, RectText.Y + 3 + PaddingRect.Y, Width - 7 - PaddingRect.Width * 2 - RectText.X * 2 - MenuModifier, Height - 7 - PaddingRect.Height * 2 - RectText.Y * 2);

            Rectangle Rect_Menu = new(Rect.Right - MenuModifier, 1, MenuModifier - 1, Height - 1);

            // #################################################################################

            #region Button render

            if (!_imageGlyphEnabled || DesignMode)
            {
                if (Enabled)
                {
                    if (Flag == Flags.None || Flag == Flags.TintedOnHover || Flag == Flags.ErrorOnHover || Flag == Flags.TertiaryOnHover || Flag == Flags.CustomColorOnHover)
                    {
                        using (SolidBrush br = new(Color.FromArgb(255, Color))) { G.FillRoundedRect(br, RectInner); }
                    }
                    else
                    {
                        using (Colors_Collection colors = new(Color, Color, Program.Style.DarkMode))
                        using (LinearGradientBrush br = new(Rect, Color.FromArgb(255, Color), colors.Back_Checked, LinearGradientMode.ForwardDiagonal))
                        {
                            G.FillRoundedRect(br, RectInner);
                        }
                    }
                }
                else
                {
                    G.FillRoundedRect(scheme1.Brushes.BackColor, RectInner);
                }

                using (Pen P = new(Color.FromArgb(255, _lineColor)))
                {
                    if (State != MouseState.Down) G.DrawRoundedRectBeveled(P, RectInner);
                    else G.DrawRoundedRectBeveledReverse(P, RectInner);
                }
            }

            using (SolidBrush br = new(Color.FromArgb(alpha, Color))) { G.FillRoundedRect(br, Rect); }

            if (!(State == MouseState.None) && !isMouseOverMenuSplitter)
            {
                if (_ripple && CanAnimate && hoverRect.Width > 0 && hoverRect.Height > 0)
                {
                    GraphicsPath path = Program.Style.RoundedCorners ? Rect.Round() : new GraphicsPath();
                    if (!Program.Style.RoundedCorners) { path.AddRectangle(Rect); }

                    G.SetClip(path);

                    GraphicsPath gp = new();

                    gp.AddEllipse(hoverRect);
                    PathGradientBrush pgb = new(gp)
                    {
                        CenterPoint = hoverPosition,
                        CenterColor = Color.FromArgb(Math.Max(100, alpha), _rippleColor),
                        SurroundColors = [Color.Transparent]
                    };

                    G.FillEllipse(pgb, hoverRect);

                    G.ResetClip();

                    path.Dispose();
                    gp.Dispose();
                    pgb.Dispose();
                }

                G.FillRoundedRect(Noise, Rect);
            }

            // contextMenu split button
            if (Menu.Items.Count > 0 && isMouseOverMenuSplitter && State != MouseState.None)
            {
                using (SolidBrush br = new(Color.FromArgb(255, scheme1.Colors.Back_Checked))) { G.FillRoundedRect(br, Rect_Menu); }
            }

            // contextMenu split button line
            if (Menu.Items.Count > 0)
            {
                using (Pen P = new(Color.FromArgb(255, _lineColor)))
                {
                    G.DrawLine(P, Rect_Menu.Left - 1, RectInner.Y + 1, Rect_Menu.Left - 1, RectInner.Y + RectInner.Height - 1);
                }
            }

            // Button border
            using (Pen P = new(Color.FromArgb(alpha, _lineColor)))
            {
                if (State == MouseState.Down)
                    G.DrawRoundedRectBeveledReverse(P, Rect);
                else
                    G.DrawRoundedRectBeveled(P, Rect);
            }

            #endregion

            #region Text and image render

            this.GetTextAndImageRectangles(TextAndImageRect, out RectangleF imageRectF, out RectangleF textRectF);

            Image img = !_imageGlyphEnabled ? Image : ImageGlyph;

            Rectangle imageRect = Rectangle.Round(imageRectF);

            if (!_imageGlyphEnabled)
            {
                imageRect.X++;
                imageRect.Y++;
            }

            // Draw image
            if (img != null) G.DrawImage(img, Rectangle.Round(imageRect));

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

            if (Menu.Items.Count > 0)
            {
                using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                using (SolidBrush fc = new(ForeColor))
                using (Font f = new("Marlett", 8f))
                {
                    G.DrawString("u", f, fc, Rect_Menu, sf);
                }
            }

            #endregion

            // Never use base.OnPaint(e) for buttons, it will overwrite the previous graphics
            // base.OnPaint(e);
        }
    }
}