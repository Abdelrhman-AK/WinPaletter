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

            Timer = new Timer() { Enabled = false, Interval = 1 };
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9f);
            ForeColor = Color.White;
            Text = string.Empty;

            MouseEnter += RadioImage_MouseEnter;
            MouseLeave += RadioImage_MouseLeave;
            HandleCreated += RadioImage_HandleCreated;
            HandleDestroyed += RadioImage_HandleDestroyed;
            Timer.Tick += Timer_Tick;
        }

        #region Variables

        private bool AnimateOnClick = false;

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

        private bool _Checked;
        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                try
                {
                    _Checked = value;

                    if (_Checked)
                    {
                        UncheckOthersOnChecked();
                    }

                    CheckedChanged?.Invoke(this);

                    Invalidate();
                }
                catch
                {
                }
            }
        }


        private Image _image;
        public Image Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                Invalidate();
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

        protected override CreateParams CreateParams
        {
            get
            {
                var cpar = base.CreateParams;
                if (!DesignMode)
                {
                    cpar.ExStyle |= 0x20;
                    return cpar;
                }
                else
                {
                    return cpar;
                }
            }
        }

        #endregion

        #region Events

        public event CheckedChangedEventHandler CheckedChanged;

        public delegate void CheckedChangedEventHandler(object sender);

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
            AnimateOnClick = true;
            Checked = true;
            State = MouseState.Down;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void RadioImage_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void RadioImage_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void RadioImage_HandleCreated(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    FindForm().Shown += Showed;
                    Parent.BackColorChanged += RefreshColorPalette;
                    Parent.VisibleChanged += RefreshColorPalette;
                    Parent.EnabledChanged += RefreshColorPalette;
                    VisibleChanged += RefreshColorPalette;
                    EnabledChanged += RefreshColorPalette;
                }
            }
            catch
            {
            }

            try
            {
                alpha = 0;
            }
            catch
            {
            }
        }

        private void RadioImage_HandleDestroyed(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    FindForm().Shown -= Showed;
                    Parent.BackColorChanged -= RefreshColorPalette;
                    Parent.VisibleChanged -= RefreshColorPalette;
                    Parent.EnabledChanged -= RefreshColorPalette;
                    VisibleChanged -= RefreshColorPalette;
                    EnabledChanged -= RefreshColorPalette;
                }
            }
            catch
            {
            }
        }

        public void Showed(object sender, EventArgs e)
        {
            Invalidate();
        }

        public void RefreshColorPalette(object sender, EventArgs e)
        {
            Invalidate();
        }

        #endregion

        #region Voids/Functions
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

        private int alpha;
        private readonly int Factor = 25;
        private Timer Timer;

        private void Timer_Tick(object sender, EventArgs e)
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
                        AnimateOnClick = false;
                    }

                    System.Threading.Thread.Sleep(1);
                    Invalidate();
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
                        AnimateOnClick = false;
                    }

                    System.Threading.Thread.Sleep(1);
                    Invalidate();
                }
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Parent is null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;
            DoubleBuffered = true;

            var MainRect = new Rectangle(0, 0, Width - 1, Height - 1);
            var MainRectInner = new Rectangle(1, 1, Width - 3, Height - 3);
            Rectangle PaddingRect = Rectangle.FromLTRB(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);
            Rectangle TextAndImageRect = new(3 + PaddingRect.X, 3 + PaddingRect.Y, Width - 7 - PaddingRect.Width * 2, Height - 7 - PaddingRect.Height * 2);

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Color back = Color.FromArgb(alpha, scheme.Colors.Back_Checked_Hover);
            Color line = Color.FromArgb(255 - alpha, _Checked ? scheme.Colors.Line_Checked : scheme.Colors.Line);
            Color line_hover = Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover);

            G.FillRoundedRect(_Checked ? scheme.Brushes.Back_Checked : scheme.Brushes.Back, MainRectInner);
            using (SolidBrush br = new(back)) { G.FillRoundedRect(br, MainRect); }

            using (Pen P = new(line)) { G.DrawRoundedRect_LikeW11(P, MainRectInner); }

            using (Pen P = new(line_hover)) { G.DrawRoundedRect_LikeW11(P, MainRect); }

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

                    else if (string.IsNullOrWhiteSpace(Text) && Image != null)
                    {
                        Rectangle imageRect = GetImageRectangle(TextAndImageRect, Image.Size, ImageAlign);

                        //Fix image maladjustment in center position
                        if (ImageAlign == ContentAlignment.MiddleCenter)
                        {
                            if (Width % Image.Width != 0) imageRect.Offset(1, 0);
                            if (Height % Image.Height != 0) imageRect.Offset(0, 1);
                        }

                        G.DrawImage(Image, imageRect);
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
                            int innerSpacing = 5;
                            Size ImageSize = Image.Size - new Size(1, 1);
                            SizeF TextSizeF = G.MeasureString(Text, Font, TextAndImageRect.Size, sf);
                            Size TextSize = new((int)TextSizeF.Width + innerSpacing, (int)TextSizeF.Height);

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