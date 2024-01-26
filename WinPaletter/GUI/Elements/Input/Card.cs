using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter.UI
{
    [DefaultEvent("Click")]
    public class Card : ContainerControl
    {
        public Card()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        int shadowSize = 1;
        int margin => (int)((alpha / 255f) * 0.6 * shadowSize);
        Rectangle rect_all => new(0, 0, Width - 1, Height - 1);
        Rectangle rect => new(rect_all.X + shadowSize, rect_all.Y + shadowSize, rect_all.Width - shadowSize * 2, rect_all.Height - shadowSize * 2);
        Rectangle rect_margin => new(rect_all.X + shadowSize - margin, rect_all.Y + shadowSize - margin, rect_all.Width - shadowSize * 2 + margin * 2, rect_all.Height - shadowSize * 2 + margin * 2);


        public MouseState State = MouseState.None;

        private Point hoverPosition;
        private Rectangle hoverRect;
        private int radius = 8;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

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
                    Refresh();
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override async void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                FluentTransitions.Transition.With(this, nameof(HoverSize), (int)(Math.Min(Width, Height) * 1.5)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                alpha = 255;
                HoverSize = (int)(Math.Min(Width, Height) * 1.5);
            }

            await Task.Delay(10);
            Animate();

            base.OnMouseEnter(e);
        }

        private Color _color = Color.DodgerBlue;
        public Color Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    Refresh();
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = MouseState.None;

            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                alpha = 0;
                HoverSize = 0;
            }

            Animate();
            base.OnMouseLeave(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            Animate();

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = MouseState.Down;

            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                FluentTransitions.Transition.With(this, nameof(HoverSize), (int)(Math.Min(Width, Height) * 1.5) * 5).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                alpha = 0;
                HoverSize = (int)(Math.Min(Width, Height) * 1.5) * 5;
            }

            Animate();

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                FluentTransitions.Transition.With(this, nameof(HoverSize), (int)(Math.Min(Width, Height) * 1.5)).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                alpha = 255;
                HoverSize = (int)(Math.Min(Width, Height) * 1.5);
            }

            Animate();
            base.OnMouseUp(e);
        }

        protected override async void OnMouseMove(MouseEventArgs e)
        {
            if (CanAnimate && State != MouseState.None)
            {
                hoverPosition = this.PointToClient(MousePosition);
                hoverRect.X = (int)(hoverPosition.X - 0.5d * _hoverSize);
                hoverRect.Y = (int)(hoverPosition.Y - 0.5d * _hoverSize);

                await Task.Delay(10);
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
                FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                alpha = 0;
                HoverSize = 0;
            }

            base.OnLostFocus(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                FluentTransitions.Transition.With(this, nameof(HoverSize), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                alpha = 0;
                HoverSize = 0;
            }

            Animate();

            base.OnClick(e);
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public string Tag { get; set; }

        #region Animator
        private void Animate()
        {
            if (CanAnimate)
            {
                if (State != MouseState.None) { hoverRect = new((int)(hoverPosition.X - 0.5d * _hoverSize), (int)(hoverPosition.Y - 0.5d * _hoverSize), _hoverSize, _hoverSize); }
            }
        }

        private int _alpha = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Refresh(); }
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

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Program.Style.RenderingHint;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            GraphicsPath clipPath;

            if (Program.Style.RoundedCorners)
            {
                clipPath = rect_margin.Round(radius);
            }
            else
            {
                clipPath = new();
                clipPath.AddRectangle(rect);
            }

            if (Image is not null)
            {
                Rectangle imageRect = new(rect.X + rect.Width - Image.Width - 5, rect.Y + (rect.Height - Image.Height) / 2, Image.Width, Image.Height);

                ////Disabled for better performance
                //G.DrawGlow(rect_all, Color.FromArgb(alpha, _color.CB(-0.1f)), shadowSize, 30, true);

                using (Config.Colors_Collection colors = new(_color, _color, Program.Style.DarkMode))
                using (SolidBrush br0 = new(colors.Back_Checked))
                using (SolidBrush br1 = new(Color.FromArgb(alpha, colors.Back_Checked_Hover)))
                using (Pen P0 = new(colors.Line_Checked))
                using (Pen P1 = new(Color.FromArgb(alpha, colors.Line_Checked_Hover)))
                using (GraphicsPath gp = new())
                {
                    G.FillRoundedRect(br0, rect_margin, radius);
                    G.FillRoundedRect(br1, rect_margin, radius);

                    G.SetClip(clipPath, CombineMode.Intersect);

                    using (LinearGradientBrush br0G = new(rect_margin, colors.Back_Checked_Hover, Color.Transparent, LinearGradientMode.ForwardDiagonal))
                    using (LinearGradientBrush br1G = new(rect_margin, Color.Transparent, Color.FromArgb(alpha, Program.Style.DarkMode ? colors.Line_Checked.Dark() : colors.Line_Checked.Light()), LinearGradientMode.ForwardDiagonal))
                    {
                        G.FillRectangle(br0G, rect_margin);

                        G.DrawImage(Image, imageRect);

                        G.FillRectangle(br1G, rect_margin);
                    }

                    if (CanAnimate && hoverRect.Width > 0 && hoverRect.Height > 0)
                    {
                        gp.AddEllipse(hoverRect);

                        using (PathGradientBrush pgb = new(gp)
                        {
                            CenterPoint = hoverPosition,
                            CenterColor = Color.FromArgb(Math.Min(30, alpha), Program.Style.DarkMode ? Color.White : Color.Black),
                            SurroundColors = new Color[] { Color.Transparent }
                        })
                        {
                            G.FillEllipse(pgb, hoverRect);
                        }
                    }

                    G.ResetClip();

                    G.DrawRoundedRect_LikeW11(P0, rect_margin, radius);
                    G.DrawRoundedRect_LikeW11(P1, rect_margin, radius);
                }
            }
            else
            {
                ////Disabled for better performance
                //G.DrawGlow(rect_margin, Color.FromArgb(alpha, scheme.Colors.Back.CB(-0.1f)), shadowSize, 30, Program.Style.RoundedCorners);

                G.FillRoundedRect(scheme.Brushes.Back, rect_margin, radius);

                using (SolidBrush br = new(Color.FromArgb(alpha, scheme.Colors.Back_Checked)))
                {
                    G.FillRoundedRect(br, rect_margin, radius);
                }

                if (CanAnimate && hoverRect.Width > 0 && hoverRect.Height > 0)
                {
                    G.SetClip(clipPath, CombineMode.Intersect);

                    using (GraphicsPath gp = new())
                    {
                        gp.AddEllipse(hoverRect);
                        using (PathGradientBrush pgb = new(gp)
                        {
                            CenterPoint = hoverPosition,
                            CenterColor = Color.FromArgb(Math.Min(30, alpha), Program.Style.DarkMode ? Color.White : Color.Black),
                            SurroundColors = new Color[] { Color.Transparent }
                        })
                        {
                            G.FillEllipse(pgb, hoverRect);
                        }

                        G.ResetClip();
                    }
                }

                G.DrawRoundedRect(scheme.Pens.Line, rect_margin, radius);

                using (Pen P = new(Color.FromArgb(alpha, scheme.Colors.Line_Checked)))
                {
                    G.DrawRoundedRect_LikeW11(P, rect_margin, radius);
                }
            }

            using (Font titleFont = new(Font.Name, Font.Size + 1, FontStyle.Bold))
            {
                SizeF textSize = Text.Measure(titleFont, rect.Width);
                SizeF descriptionSize = SizeF.Empty;

                Rectangle textRect = Rectangle.Empty;
                Rectangle descriptionRect = Rectangle.Empty;

                if (Tag != null && !string.IsNullOrWhiteSpace(Tag))
                {
                    descriptionSize = (Tag).Measure(Font, rect.Width) + new SizeF(0, 10);

                    int totalHeight = (int)(textSize.Height + descriptionSize.Height);
                    int centerY = rect.Y + ((rect.Height - totalHeight) / 2);

                    textRect = new Rectangle(rect.X + 5, centerY, rect.Width - 10, (int)textSize.Height);

                    // Center description below the title
                    descriptionRect = new Rectangle(rect.X + 5, textRect.Bottom + 5, rect.Width - 10, (int)descriptionSize.Height);
                }
                else
                {
                    // Only the title is centered
                    textRect = new Rectangle(rect.X + 5, rect.Y + (rect.Height - (int)textSize.Height) / 2, rect.Width - 10, (int)textSize.Height);
                    descriptionRect = Rectangle.Empty;
                }

                if (descriptionSize != SizeF.Empty)
                {
                    // Calculate the initial centerY
                    int centerY = rect.Y + (rect.Height - (int)textSize.Height) / 2;

                    // Calculate the center of the title and description combined
                    int combinedCenterY = rect.Y + (rect.Height - (int)(textSize.Height + descriptionSize.Height)) / 2;

                    // Interpolate between the two states based on the alpha value
                    int interpolatedY = Lerp(centerY, combinedCenterY, alpha / 255f);

                    // Update the positions
                    textRect.Y = interpolatedY;
                    descriptionRect.Y = interpolatedY + textRect.Height + 5;
                }

                using (SolidBrush br0 = new(ForeColor))
                using (SolidBrush br1 = new(Color.FromArgb(alpha, ForeColor)))
                using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                {
                    G.SetClip(clipPath, CombineMode.Intersect);

                    G.DrawString(Text, titleFont, br0, textRect, sf);

                    if (descriptionRect != Rectangle.Empty) G.DrawString(Tag, Font, br1, descriptionRect, sf);

                    G.ResetClip();
                }
            }

            clipPath.Dispose();
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        // Utility method for linear interpolation with integer values
        public static int Lerp(int a, int b, float t)
        {
            return (int)Lerp((float)a, (float)b, t);
        }
    }
}