using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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

            hoverSize = Math.Max(Width, Height) + hoverFactor;
            hoverFactor = hoverSize / 8;

            Font = new Font("Segoe UI", 9f);
            Image = base.Image;
            if (Image is not null) { Flag = Flags.TintedOnHover; }

            LostFocus += Button_LostFocus;
            HandleCreated += Button_HandleCreated;
            HandleDestroyed += Button_HandleDestroyed;
            Timer.Tick += Timer_Tick;
            Timer_Hover.Tick += Timer_Hover_Tick;
        }

        #region Variables
        private Config.Scheme scheme1 = Program.Style.Schemes.Main;
        private Config.Scheme scheme2 = Program.Style.Schemes.Secondary;
        private Config.Scheme scheme3 = Program.Style.Schemes.Tertiary;

        private readonly TextureBrush Noise = new(Properties.Resources.GaussianBlur.Fade(0.6d));
        private bool _Shown = false;
        private Color imageColor;

        public MouseState State = MouseState.None;
        private Point hoverPosition;
        private Rectangle hoverRect;
        private int hoverSize;
        private int hoverFactor = 0;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        private bool _ripple = true;
        #endregion

        #region Properties

        public new Color BackColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {

            }
        }

        private Image _image;
        public new Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image != value)
                {
                    _image = value;

                    if (_image is not null)
                    {
                        imageColor = Image.AverageColor();

                        if (Flag == Flags.None) { Flag = Flags.TintedOnHover; }

                        _color = Colorize();
                    }

                    Refresh();
                }
            }
        }


        public bool DrawOnGlass { get; set; } = false;

        private Color _color;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Color Color
        {
            get
            {
                return _color;
            }

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
            get
            {
                return _customColor;
            }

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
            float coloringFactor_None_Dark = 0.2f;
            float coloringFactor_None_Light = 0.5f;

            float coloringFactor_Over_Dark = 0.4f;
            float coloringFactor_Over_Light = 0.7f;

            float coloringFactor_Down_Dark = 0.6f;
            float coloringFactor_Down_Light = 0.8f;

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
                            if (_image != null)
                            {
                                if (Program.Style.DarkMode)
                                {
                                    resultColor = imageColor.Dark(coloringFactor_None_Dark);
                                }
                                else
                                {
                                    resultColor = imageColor.CB(coloringFactor_None_Light);
                                }
                            }

                            else
                            {
                                if (Program.Style.DarkMode)
                                {
                                    resultColor = AccentColor.Dark(coloringFactor_None_Dark);
                                }
                                else
                                {
                                    resultColor = AccentColor.CB(coloringFactor_None_Light);
                                }
                            }
                        }

                        break;
                    }

                case MouseState.Over:
                    {
                        if (Flag == Flags.None) { resultColor = scheme1.Colors.Button_Over; }

                        else
                        {
                            if (_image != null)
                            {
                                if (Program.Style.DarkMode)
                                {
                                    resultColor = imageColor.Dark(coloringFactor_Over_Dark);
                                }
                                else
                                {
                                    resultColor = imageColor.CB(coloringFactor_Over_Light);
                                }
                            }

                            else
                            {
                                if (Program.Style.DarkMode)
                                {
                                    resultColor = AccentColor.Dark(coloringFactor_Over_Dark);
                                }
                                else
                                {
                                    resultColor = AccentColor.CB(coloringFactor_Over_Light);
                                }
                            }
                        }

                        break;
                    }

                case MouseState.Down:
                    {
                        if (Flag == Flags.None) { resultColor = scheme1.Colors.Button_Down; }

                        else
                        {
                            if (_image != null)
                            {
                                if (Program.Style.DarkMode)
                                {
                                    resultColor = imageColor.Dark(coloringFactor_Down_Dark);
                                }
                                else
                                {
                                    resultColor = imageColor.CB(coloringFactor_Down_Dark);
                                }
                            }

                            else
                            {
                                if (Program.Style.DarkMode)
                                {
                                    resultColor = AccentColor.Dark(coloringFactor_Down_Dark);
                                }
                                else
                                {
                                    resultColor = AccentColor.CB(coloringFactor_Down_Light);
                                }
                            }
                        }

                        break;
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
                        return _color.CB(Color.IsDark() ? 0.07 : -0.1f);
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

        protected override CreateParams CreateParams
        {
            get
            {
                var cpar = base.CreateParams;
                if (DrawOnGlass & !DesignMode)
                {
                    cpar.ExStyle = cpar.ExStyle | 0x20;
                    return cpar;
                }
                else
                {
                    return cpar;
                }
            }
        }

        private Flags _flag = Flags.None;
        public Flags Flag
        {
            get
            {
                return _flag;
            }
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

        #endregion

        #region Events

        #region OnMouse
        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            Animate();

            if (_Shown) 
            { 
                Timer.Enabled = true; Timer.Start();
                Invalidate();
            }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = MouseState.None;

            Animate();

            if (_Shown)
            {
                Timer.Enabled = true;
                Timer.Start();
                Invalidate();
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = MouseState.Down;

            Animate();

            if (_Shown)
            {
                Timer.Enabled = true; Timer.Start();
                Timer_Hover.Enabled = true; Timer_Hover.Start();
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;

            hoverSize = Math.Max(Width, Height);
            hoverRect = new((int)(hoverPosition.X - 0.5d * hoverSize), (int)(hoverPosition.Y - 0.5d * hoverSize), hoverSize, hoverSize);

            if (_Shown) 
            { 
                Timer.Enabled = true; Timer.Start();
                Invalidate();
            }

            Animate();


            base.OnMouseUp(e);
        }
        #endregion

        #region OnKey
        protected override void OnKeyDown(KeyEventArgs e)
        {
            State = MouseState.Down;

            Animate();

            Timer_Hover.Enabled = true; Timer_Hover.Start();

            Invalidate();

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            State = MouseState.None;

            hoverSize = Math.Max(Width, Height);
            hoverRect = new((int)(hoverPosition.X - 0.5d * hoverSize), (int)(hoverPosition.Y - 0.5d * hoverSize), hoverSize, hoverSize);

            if (_Shown) { Timer.Enabled = true; Timer.Start(); }

            Animate();

            Invalidate();

            base.OnKeyUp(e);
        }
        #endregion

        protected override void OnLeave(EventArgs e)
        {
            State = MouseState.None;

            Animate();

            if (_Shown) { Invalidate(); }

            base.OnLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (_Shown && _ripple && State != MouseState.None)
            {
                hoverPosition = this.PointToClient(MousePosition);
                hoverRect = new((int)(hoverPosition.X - 0.5d * hoverSize), (int)(hoverPosition.Y - 0.5d * hoverSize), hoverSize, hoverSize);
                Refresh();
            }

            base.OnMouseMove(mevent);
        }

        private void Button_LostFocus(object sender, EventArgs e)
        {
            State = MouseState.None;

            Animate();

            if (_Shown) { Invalidate(); }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            hoverSize = Math.Max(Width, Height);
            hoverFactor = hoverSize / 8;
            base.OnSizeChanged(e);
        }

        public void UpdateStyleSchemes()
        {
            Timer.Enabled = false; Timer.Stop();
            Timer_Hover.Enabled = false; Timer_Hover.Stop();

            alpha = 0;
            State = MouseState.None;

            scheme1 = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            scheme2 = Enabled ? Program.Style.Schemes.Secondary : Program.Style.Schemes.Disabled;
            scheme3 = Enabled ? Program.Style.Schemes.Tertiary : Program.Style.Schemes.Disabled;

            _color = Colorize();
            _lineColor = LineColor(_color);

            Animate();
        }

        private void Button_HandleCreated(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    try
                    {
                        FindForm().Load += Loaded;
                        FindForm().Shown += Showed;
                        FindForm().FormClosed += Closed;
                    }
                    catch { }
                }
                alpha = 0;
            }
            catch { }
        }

        private void Button_HandleDestroyed(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    try
                    {
                        FindForm().Load -= Loaded;
                        FindForm().Shown -= Showed;
                        FindForm().FormClosed -= Closed;
                    }
                    catch { }
                }
            }
            catch { }
        }

        public void Loaded(object sender, EventArgs e)
        {
            _Shown = false;
            Timer.Enabled = false; Timer.Stop();
            Timer_Hover.Enabled = false; Timer_Hover.Stop();
        }

        public void Showed(object sender, EventArgs e)
        {
            _Shown = true;
        }

        public void Closed(object sender, EventArgs e)
        {
            _Shown = false;
            Timer.Enabled = false; Timer.Stop();
            Timer_Hover.Enabled = false; Timer_Hover.Stop();
        }
        #endregion

        #region Animator

        private void Animate()
        {
            if (!DesignMode & _Shown)
            {
                if (State != MouseState.None)
                {
                    hoverRect = new((int)(hoverPosition.X - 0.5d * hoverSize), (int)(hoverPosition.Y - 0.5d * hoverSize), hoverSize, hoverSize);
                }

                FluentTransitions.Transition.With(this, nameof(Color), Colorize()).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
        }

        private int alpha;
        private readonly int Factor = 15;
        private readonly Timer Timer = new() { Enabled = false, Interval = 1 };
        private readonly Timer Timer_Hover = new() { Enabled = false, Interval = 1 };

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {

                    if (State == MouseState.Over)
                    {
                        if (alpha + Factor <= 255)
                        {
                            alpha += Factor;
                        }
                        else if (alpha + Factor > 255)
                        {
                            alpha = 255;
                            Timer.Enabled = false;
                            Timer.Stop();
                        }

                        if (_Shown)
                        {
                            System.Threading.Thread.Sleep(1);
                            Invalidate();
                        }
                    }

                    if (!(State == MouseState.Over))
                    {
                        if (alpha - Factor >= 0)
                        {
                            alpha -= Factor;
                        }
                        else if (alpha - Factor < 0)
                        {
                            alpha = 0;
                            Timer.Enabled = false;
                            Timer.Stop();
                        }

                        if (_Shown)
                        {
                            System.Threading.Thread.Sleep(1);
                            Invalidate();
                        }

                    }
                }
            }
            catch
            {
            }
        }

        private void Timer_Hover_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {

                    if (State == MouseState.Down)
                    {
                        hoverSize += hoverFactor;
                        hoverRect = new((int)(hoverPosition.X - 0.5d * hoverSize), (int)(hoverPosition.Y - 0.5d * hoverSize), hoverSize, hoverSize);

                        if (hoverSize > Math.Max(Width, Height) * 5)
                        {
                            hoverSize = Math.Max(Width, Height);
                            hoverRect = new((int)(hoverPosition.X - 0.5d * hoverSize), (int)(hoverPosition.Y - 0.5d * hoverSize), hoverSize, hoverSize);

                            Timer_Hover.Enabled = false;
                            Timer_Hover.Stop();
                        }

                        if (_Shown)
                        {
                            System.Threading.Thread.Sleep(1);
                            Refresh();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.SystemDefault;
            DoubleBuffered = true;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            // ################################################################################# Customizer
            Rectangle Rect = new(0, 0, Width - 1, Height - 1);
            Rectangle RectInner = new(1, 1, Width - 3, Height - 3);
            Rectangle PaddingRect = Rectangle.FromLTRB(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);
            Rectangle TextAndImageRect = new(3 + PaddingRect.X, 3 + PaddingRect.Y, Width - 7 - PaddingRect.Width * 2, Height - 7 - PaddingRect.Height * 2);
            // #################################################################################

            #region Button render
            //Color.FromArgb(150, Color)
            using (var br = new SolidBrush(Color.FromArgb(255, Color)))
            {
                G.FillRoundedRect(br, RectInner);
            }

            using (var br = new SolidBrush(Color.FromArgb(alpha, Color)))
            {
                G.FillRoundedRect(br, Rect);
            }

            if (!(State == MouseState.None))
            {
                if (_ripple)
                {
                    GraphicsPath path = Program.Style.RoundedCorners ? Rect.Round() : new GraphicsPath();

                    G.SetClip(path);

                    GraphicsPath gp = new();

                    gp.AddEllipse(hoverRect);
                    PathGradientBrush pgb = new(gp)
                    {
                        CenterPoint = hoverPosition,
                        CenterColor = Color.FromArgb(alpha, _rippleColor),
                        SurroundColors = new Color[] { Color.Transparent }
                    };

                    G.FillEllipse(pgb, hoverRect);

                    G.ResetClip();
                }

                G.FillRoundedRect(Noise, Rect);
            }

            using (var P = new Pen(Color.FromArgb(255 - alpha, _lineColor)))
            {
                G.DrawRoundedRect_LikeW11(P, RectInner);
            }

            using (var P = new Pen(Color.FromArgb(alpha, _lineColor)))
            {
                G.DrawRoundedRect_LikeW11(P, Rect);
            }
            #endregion

            #region Text and Image Render
            using (StringFormat sf = TextAlign.ToStringFormat((int)base.RightToLeft == 1))
            {
                using (SolidBrush fc = new(ForeColor))
                {
                    if (Image == null) 
                    {
                        //Fix label maladjustment in center position
                        if (TextAlign == ContentAlignment.MiddleCenter)
                        {
                            if (Width % 2 != 0) TextAndImageRect.Offset(1, 0);
                            if (Height % 2 != 0) TextAndImageRect.Offset(0, 1);
                        }

                        G.DrawString(Text, Font, fc, TextAndImageRect, sf); 
                    }

                    else
                    {
                        if (TextImageRelation == TextImageRelation.Overlay)
                        {
                            Rectangle imageRect = GetImageRectangle(TextAndImageRect, Image.Size, ImageAlign);

                            //Fix image maladjustment in center position
                            if (ImageAlign == ContentAlignment.MiddleCenter)
                            {
                                if (Width % Image.Width != 0) imageRect.Offset(1, 0);
                                if (Height % Image.Height != 0) imageRect.Offset(0, 1);
                            }

                            //Fix label maladjustment in center position
                            if (TextAlign == ContentAlignment.MiddleCenter)
                            {
                                if (Width % 2 != 0) TextAndImageRect.Offset(1, 0);
                                if (Height % 2 != 0) TextAndImageRect.Offset(0, 1);
                            }

                            G.DrawImage(Image, imageRect);
                            G.DrawString(Text, Font, fc, TextAndImageRect, sf);
                        }
                        else
                        {
                            int innerSpacing = 1;
                            Size ImageSize = Image.Size - new Size(1, 1);
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

                                imageRect = GetImageRectangle(space, Image.Size, ImageAlign);
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

                                imageRect = GetImageRectangle(space, Image.Size, ImageAlign);
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

                            G.DrawImage(Image, new Rectangle(imageRect.X, imageRect.Y, imageRect.Width + 1, imageRect.Height + 1));
                            G.DrawString(Text, Font, fc, textRect, sf);
                        }
                    }
                }
            }
            #endregion
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