using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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

            Menu.ItemHeight = Height;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        public UI.WP.ContextMenuStrip Menu = new() { ShowImageMargin = true, AllowTransparency = true };
        private int MenuSplitterWidth = 15;
        Rectangle MenuSplitterRectangle;
        bool isMouseOverMenuSplitter = false;

        private readonly TextureBrush Noise = new(Properties.Resources.Noise.Fade(0.6f));
        private Color imageColor;

        public MouseState State = MouseState.None;
        private Point hoverPosition;
        private Rectangle hoverRect;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        private bool _ripple = true;
        private bool ForceUpdateImageVector = false;

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new Color BackColor { get => Color.Transparent; set {; } }

        private Image _image;
        public new Image Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;

                    if (_image is not null)
                    {
                        imageColor = Image.AverageColor();

                        if (Flag == Flags.None) { Flag = Flags.TintedOnHover; }

                        if (!_imageAsVector) _color = Colorize();
                    }

                    Refresh();
                }
            }
        }

        private Image _imageVectorUnmodified;
        private Image _imageVector;
        private Image _imageVectorOver;
        private Image _imageVectorDown;
        public Image ImageVector
        {
            get
            {
                if (!DesignMode)
                {
                    switch (State)
                    {
                        case MouseState.None:
                            return Program.Style.DarkMode ? _imageVector : ((Bitmap)_imageVector)?.Invert();

                        case MouseState.Over:
                            return _imageVectorOver;

                        case MouseState.Down:
                            return _imageVectorDown;

                        default:
                            return Program.Style.DarkMode ? _imageVector : ((Bitmap)_imageVector)?.Invert();
                    }
                }
                else
                {
                    return _imageVector;
                }
            }
            set
            {
                if (_imageVectorUnmodified != value || ForceUpdateImageVector)
                {
                    _imageVectorUnmodified = value;

                    if (_imageAsVector) _color = Colorize();

                    using (Config.Colors_Collection colors = new(_customColor, _customColor, Program.Style.DarkMode))
                    {
                        _imageVector = value;
                        _imageVectorOver = ((Bitmap)value)?.Tint(colors.ForeColor_Accent);
                        _imageVectorDown = ((Bitmap)value)?.Tint(colors.ForeColor_Accent);
                    }

                    Refresh();
                }
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
                    Refresh();
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
                    Refresh();
                }
            }
        }


        private Color _lineColor;
        private Color _rippleColor;

        private Color Colorize()
        {



            Color AccentColor = scheme1.Colors.Accent;
            Color resultColor = scheme1.Colors.Back;

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

            using (Config.Colors_Collection imageColors = new(imageColor, imageColor, Program.Style.DarkMode))
            using (Config.Colors_Collection accentColors = new(AccentColor, AccentColor, Program.Style.DarkMode))
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
                                if (!_imageAsVector && _image != null)
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

                    case MouseState.Over:
                        {
                            if (Flag == Flags.None) { resultColor = scheme1.Colors.Button_Over; }

                            else
                            {
                                if (!_imageAsVector && _image != null)
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
                                if (!_imageAsVector && _image != null)
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
            float coloringFactor_Circle_Over_Light = 0.04f;

            float coloringFactor_Circle_Down_Dark = 0.3f;
            float coloringFactor_Circle_Down_Light = 0.06f;

            switch (State)
            {
                case MouseState.Over:
                    {
                        _rippleColor = Program.Style.DarkMode ? resultColor.Light(coloringFactor_Circle_Over_Dark) : resultColor.Dark(coloringFactor_Circle_Over_Light);
                        break;
                    }

                case MouseState.Down:
                    {
                        _rippleColor = Program.Style.DarkMode ? resultColor.Light(coloringFactor_Circle_Down_Dark) : resultColor.Dark(coloringFactor_Circle_Down_Light);
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
                        return _color.CB(Color.IsDark() ? 0.02f : -0.04f);
                    }

                case MouseState.Over:
                    {
                        return _color.CB(Color.IsDark() ? 0.07f : -0.1f);
                    }

                case MouseState.Down:
                    {
                        return _color.CB(Color.IsDark() ? 0.1f : -0.08f);
                    }
                default:
                    {
                        return _color.CB(Color.IsDark() ? 0.02f : -0.04f);
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
                    Refresh();
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

        private Config.Scheme scheme1 => Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
        private Config.Scheme scheme2 => Enabled ? Program.Style.Schemes.Secondary : Program.Style.Schemes.Disabled;
        private Config.Scheme scheme3 => Enabled ? Program.Style.Schemes.Tertiary : Program.Style.Schemes.Disabled;


        private bool _imageAsVector = false;
        public bool ImageAsVector
        {
            get => _imageAsVector;
            set
            {
                if (_imageAsVector != value)
                {
                    _imageAsVector = value;
                    _color = Colorize();
                    Refresh();
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
                FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    FluentTransitions.Transition.With(this, nameof(HoverSize), Math.Max(Width, Height)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
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
                FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
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
                FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    FluentTransitions.Transition.With(this, nameof(HoverSize), Math.Max(Width, Height) * 5).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
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
            State = MouseState.Over;

            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    FluentTransitions.Transition.With(this, nameof(HoverSize), Math.Max(Width, Height)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                }
            }
            else
            {
                alpha = 255;
                HoverSize = Math.Max(Width, Height);
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
                FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    FluentTransitions.Transition.With(this, nameof(HoverSize), Math.Max(Width, Height) * 5).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
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
            hoverRect = new((int)(hoverPosition.X - 0.5d * _hoverSize), (int)(hoverPosition.Y - 0.5d * _hoverSize), _hoverSize, _hoverSize);

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
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
                    Menu.Show(this, new Point(0, this.Height), ToolStripDropDownDirection.Default);
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

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            isMouseOverMenuSplitter = Menu.Items.Count > 0 && MenuSplitterRectangle.Contains(PointToClient(MousePosition));

            if (CanAnimate && _ripple && State != MouseState.None)
            {
                hoverPosition = this.PointToClient(MousePosition);
                hoverRect.X = (int)(hoverPosition.X - 0.5d * _hoverSize);
                hoverRect.Y = (int)(hoverPosition.Y - 0.5d * _hoverSize);
                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            State = MouseState.None;

            Animate();

            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                if (_ripple)
                {
                    FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                }
            }
            else
            {
                alpha = 0;
                HoverSize = 0;
            }

            base.OnLostFocus(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            UpdateStyleSchemes();
            Refresh();

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

            ForceUpdateImageVector = true;
            ImageVector = _imageVectorUnmodified;
            ForceUpdateImageVector = false;

            Animate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Activated += Form_Activated;
                FindForm().Deactivate += Form_Deactivate; ;
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Activated -= Form_Activated;
                FindForm().Deactivate -= Form_Deactivate; ;
            }

            base.OnHandleDestroyed(e);
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(FocusAlpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                FocusAlpha = 255;
            }
        }

        private void Form_Deactivate(object sender, EventArgs e)
        {
            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(FocusAlpha), 100).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                FocusAlpha = 100;
            }
        }

        #endregion

        #region Animator
        private void Animate()
        {
            if (CanAnimate)
            {
                if (State != MouseState.None) { hoverRect = new((int)(hoverPosition.X - 0.5d * _hoverSize), (int)(hoverPosition.Y - 0.5d * _hoverSize), _hoverSize, _hoverSize); }

                FluentTransitions.Transition.With(this, nameof(Color), Colorize()).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
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
            set { _alpha = value; Refresh(); }
        }

        private int _focusAlpha = 255;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int FocusAlpha
        {
            get => _focusAlpha;
            set
            {
                _focusAlpha = value;
                Refresh();
            }
        }


        private int _hoverSize;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int HoverSize
        {
            get => _hoverSize;
            set
            {
                _hoverSize = value;
                hoverRect = new((int)(hoverPosition.X - 0.5d * _hoverSize), (int)(hoverPosition.Y - 0.5d * _hoverSize), _hoverSize, _hoverSize);
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
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Program.Style.RenderingHint;

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

            if (!_imageAsVector || DesignMode)
            {
                using (SolidBrush br = new(Color.FromArgb(FocusAlpha, Color))) { G.FillRoundedRect(br, RectInner); }

                using (Pen P = new(Color.FromArgb(FocusAlpha, _lineColor)))
                {
                    if (State != MouseState.Down)
                        G.DrawRoundedRect_LikeW11(P, RectInner);
                    else
                        G.DrawRoundedRect(P, RectInner);
                }

                using (SolidBrush br = new(Color.FromArgb(alpha, Color))) { G.FillRoundedRect(br, Rect); }
            }

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
                        SurroundColors = new Color[] { Color.Transparent }
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
                using (SolidBrush br = new(Color.FromArgb(FocusAlpha, scheme1.Colors.Back_Checked))) { G.FillRoundedRect(br, Rect_Menu); }
            }

            // contextMenu split button line
            if (Menu.Items.Count > 0)
            {
                using (Pen P = new(Color.FromArgb(255, _lineColor)))
                {
                    G.DrawLine(P, Rect_Menu.Left - 1, RectInner.Y, Rect_Menu.Left - 1, RectInner.Y + RectInner.Height);
                }
            }

            // Button border
            using (Pen P = new(Color.FromArgb(alpha, _lineColor)))
            {
                if (State != MouseState.Down)
                    G.DrawRoundedRect_LikeW11(P, Rect);
                else
                    G.DrawRoundedRect(P, Rect);
            }

            #endregion

            #region Text and img Render
            using (StringFormat sf = TextAlign.ToStringFormat((int)base.RightToLeft == 1))
            using (SolidBrush fc = new(ForeColor))
            {
                Image img = !_imageAsVector ? Image : ImageVector;

                if (img == null)
                {
                    //Fix label maladjustment in center position
                    if (TextAlign == ContentAlignment.MiddleCenter)
                    {
                        if (Width % 2 != 0) TextAndImageRect.Offset(1, 0);
                        if (Height % 2 != 0) TextAndImageRect.Offset(0, 1);
                    }

                    G.DrawString(Text, Font, fc, TextAndImageRect, sf);
                }

                else if (string.IsNullOrWhiteSpace(Text) && img != null)
                {
                    Rectangle imageRect = GetImageRectangle(TextAndImageRect, img.Size, ImageAlign);

                    //Fix image maladjustment in center position
                    if (ImageAlign == ContentAlignment.MiddleCenter)
                    {
                        if (Width % img.Width != 0) imageRect.Offset(1, 0);
                        if (Height % img.Height != 0) imageRect.Offset(0, 1);
                    }

                    G.DrawImage(Enabled ? img : img.Grayscale(), imageRect);
                }

                else
                {
                    if (TextImageRelation == TextImageRelation.Overlay)
                    {
                        Rectangle imageRect = GetImageRectangle(TextAndImageRect, img.Size, ImageAlign);

                        //Fix image maladjustment in center position
                        if (ImageAlign == ContentAlignment.MiddleCenter)
                        {
                            if (Width % img.Width != 0) imageRect.Offset(1, 0);
                            if (Height % img.Height != 0) imageRect.Offset(0, 1);
                        }

                        //Fix label maladjustment in center position
                        if (TextAlign == ContentAlignment.MiddleCenter)
                        {
                            if (Width % 2 != 0) TextAndImageRect.Offset(1, 0);
                            if (Height % 2 != 0) TextAndImageRect.Offset(0, 1);
                        }

                        G.DrawImage(Enabled ? img : img.Grayscale(), imageRect);
                        G.DrawString(Text, Font, fc, TextAndImageRect, sf);
                    }
                    else
                    {
                        int innerSpacing = 1;
                        Size ImageSize = img.Size;
                        SizeF TextSizeF = G.MeasureString(Text, Font, TextAndImageRect.Size, sf);
                        Size TextSize = new((int)TextSizeF.Width + innerSpacing * 2, (int)TextSizeF.Height);

                        Rectangle space = new();
                        Rectangle textRect = new();
                        Rectangle imageRect = new();

                        if (TextImageRelation == TextImageRelation.ImageAboveText)
                        {
                            space.Width = Math.Max(ImageSize.Width, TextSize.Width);
                            space.Height = ImageSize.Height + TextSize.Height + innerSpacing;

                            space = GetImageRectangle(TextAndImageRect, space.Size, ImageAlign);

                            imageRect = GetImageRectangle(space, img.Size, ImageAlign);
                            imageRect.Y = space.Y;

                            textRect.Width = space.Width;
                            textRect.Height = TextSize.Height;
                            textRect.X = space.X;
                            textRect.Y = imageRect.Bottom + innerSpacing;
                        }

                        else if (TextImageRelation == TextImageRelation.TextAboveImage)
                        {
                            space.Width = Math.Max(ImageSize.Width, TextSize.Width);
                            space.Height = ImageSize.Height + TextSize.Height + innerSpacing;

                            space = GetImageRectangle(TextAndImageRect, space.Size, ImageAlign);

                            textRect.Width = space.Width;
                            textRect.Height = TextSize.Height;
                            textRect.X = space.X;
                            textRect.Y = space.Y;

                            imageRect = GetImageRectangle(space, img.Size, ImageAlign);
                            imageRect.Y = textRect.Bottom + innerSpacing;
                        }

                        else if (TextImageRelation == TextImageRelation.ImageBeforeText)
                        {
                            space.Width = ImageSize.Width + TextSize.Width + innerSpacing;
                            space.Height = Math.Max(ImageSize.Height, TextSize.Height);

                            space = GetImageRectangle(TextAndImageRect, space.Size, ImageAlign);

                            imageRect = GetImageRectangle(space, ImageSize, ContentAlignment.MiddleLeft);

                            textRect.X = imageRect.Right + innerSpacing;
                            textRect.Y = space.Y + 1;
                            textRect.Width = TextSize.Width;
                            textRect.Height = space.Height;
                        }

                        else if (TextImageRelation == TextImageRelation.TextBeforeImage)
                        {
                            space.Width = ImageSize.Width + TextSize.Width + innerSpacing;
                            space.Height = Math.Max(ImageSize.Height, TextSize.Height);

                            space = GetImageRectangle(TextAndImageRect, space.Size, ImageAlign);

                            textRect.X = space.X;
                            textRect.Y = space.Y;
                            textRect.Width = TextSize.Width;
                            textRect.Height = space.Height;

                            imageRect.X = textRect.Right + innerSpacing;
                            imageRect.Y = space.Y;
                            imageRect.Width = ImageSize.Width;
                            imageRect.Height = ImageSize.Height;
                        }

                        G.DrawImage(Enabled ? img : img.Grayscale(), imageRect);
                        G.DrawString(Text, Font, fc, textRect, sf);
                    }
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

        private Rectangle GetImageRectangle(Rectangle rect, Size size, ContentAlignment contentAlignment)
        {
            float CenterWidthD = rect.X + (float)(rect.Width - size.Width) / 2;
            float CenterHeightD = rect.Y + (float)(rect.Height - size.Height) / 2;

            int CenterWidth = (int)Math.Round(CenterWidthD, 2);
            int CenterHeight = (int)Math.Round(CenterHeightD, 2);

            switch (contentAlignment)
            {
                case ContentAlignment.TopLeft:
                    return new(rect.X, rect.Y, size.Width, size.Height);

                case ContentAlignment.TopRight:
                    return new(rect.Right - size.Width, rect.Y, size.Width, size.Height);

                case ContentAlignment.TopCenter:
                    return new(CenterWidth, rect.Y, size.Width, size.Height);

                case ContentAlignment.MiddleLeft:
                    return new(rect.X, CenterHeight, size.Width, size.Height);

                case ContentAlignment.MiddleCenter:
                    return new(CenterWidth, CenterHeight, size.Width, size.Height);

                case ContentAlignment.MiddleRight:
                    return new(rect.Right - size.Width, CenterHeight, size.Width, size.Height);

                case ContentAlignment.BottomLeft:
                    return new(rect.X, rect.Bottom - size.Height, size.Width, size.Height);

                case ContentAlignment.BottomCenter:
                    return new(CenterWidth, rect.Bottom - size.Height, size.Width, size.Height);

                case ContentAlignment.BottomRight:
                    return new(rect.Right - size.Width, rect.Bottom - size.Height, size.Width, size.Height);

                default:
                    return new(CenterWidth, CenterHeight, size.Width, size.Height);
            }
        }
    }
}