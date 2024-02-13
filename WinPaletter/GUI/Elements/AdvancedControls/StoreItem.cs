using ImageProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using static WinPaletter.UI.Style.Config;

namespace WinPaletter.UI.Controllers
{
    [ToolboxItem(false)]
    [DefaultEvent("Click")]
    public class StoreItem : Panel
    {
        public StoreItem()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            pattern = new(new Bitmap(Width, Height));
            Font = new("Segoe UI", 9f);
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private TextureBrush Noise = new(Properties.Resources.Noise.Fade(0.65f));
        private List<Bitmap> DesignedFor_Badges = new();
        private TextureBrush pattern;

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

        private Theme.Manager _TM;
        public Theme.Manager TM
        {
            get => _TM;
            set
            {
                _TM?.Dispose();

                if (value is not null)
                {
                    _TM = value;
                    UpdateBadges();
                    UpdatePattern(_TM.Info.Pattern);
                    ThemeManagerChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        public string MD5_ThemeFile { get; set; }
        public string MD5_PackFile { get; set; }
        public string URL_ThemeFile { get; set; }
        public string URL_PackFile { get; set; }
        public string FileName { get; set; }
        public bool DoneByWinPaletter { get; set; } = false;

        #endregion

        #region Events/Overrides

        public event ThemeManagerChangedEventHandler ThemeManagerChanged;

        public delegate void ThemeManagerChangedEventHandler(object sender, EventArgs e);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = MouseState.Down;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

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
            _TM?.Dispose();
            pattern?.Dispose();
            Noise?.Dispose();

            base.Dispose(disposing);
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }

        #endregion

        #region Methods
        public void UpdateBadges()
        {
            DesignedFor_Badges.Clear();
            if (_TM is not null)
            {
                //if (_TM.Info.DesignedFor_Win12) DesignedFor_Badges.Add(Assets.WinPaletter_Store.DesignedFor12);

                if (_TM.Info.DesignedFor_Win11) DesignedFor_Badges.Add(Assets.WinPaletter_Store.DesignedFor11);

                if (_TM.Info.DesignedFor_Win10) DesignedFor_Badges.Add(Assets.WinPaletter_Store.DesignedFor10);

                if (_TM.Info.DesignedFor_Win81) DesignedFor_Badges.Add(Assets.WinPaletter_Store.DesignedFor8);

                if (_TM.Info.DesignedFor_Win7) DesignedFor_Badges.Add(Assets.WinPaletter_Store.DesignedFor7);

                if (_TM.Info.DesignedFor_WinVista) DesignedFor_Badges.Add(Assets.WinPaletter_Store.DesignedForVista);

                if (_TM.Info.DesignedFor_WinXP) DesignedFor_Badges.Add(Assets.WinPaletter_Store.DesignedForXP);
            }
            Refresh();
        }

        public void UpdatePattern(int val)
        {
            Bitmap bmp;

            switch (val)
            {
                case 0:
                    {
                        using (Bitmap x = new(Width, Height)) { bmp = (Bitmap)x.Clone(); }
                        break;
                    }

                case 1:
                    {
                        bmp = Assets.WinPaletter_Store.Pattern1;
                        break;
                    }
                case 2:
                    {
                        bmp = Assets.WinPaletter_Store.Pattern2;
                        break;
                    }
                case 3:
                    {
                        bmp = Assets.WinPaletter_Store.Pattern3;
                        break;
                    }
                case 4:
                    {
                        bmp = Assets.WinPaletter_Store.Pattern4;
                        break;
                    }
                case 5:
                    {
                        bmp = Assets.WinPaletter_Store.Pattern5;
                        break;
                    }
                case 6:
                    {
                        bmp = Assets.WinPaletter_Store.Pattern6;
                        break;
                    }
                case 7:
                    {
                        bmp = Assets.WinPaletter_Store.Pattern7;
                        break;
                    }
                case 8:
                    {
                        bmp = Assets.WinPaletter_Store.Pattern8;
                        break;
                    }
                case 9:
                    {
                        bmp = Assets.WinPaletter_Store.Pattern9;
                        break;
                    }
                case 10:
                    {
                        bmp = Assets.WinPaletter_Store.Pattern10;
                        break;
                    }
                case 11:
                    {
                        bmp = Properties.Resources.Noise;
                        break;
                    }

                default:
                    {
                        using (Bitmap x = new(Width, Height)) { bmp = (Bitmap)x.Clone(); }
                        break;
                    }

            }

            if (!Program.Style.DarkMode)
            {
                using (ImageFactory imgF = new())
                {
                    imgF.Load(bmp);
                    imgF.Contrast(50);
                    bmp = (imgF.Image as Bitmap).Invert().Fade(0.8f);
                }
            }
            pattern = new(bmp);

            Refresh();
        }

        #endregion

        #region Animator
        private int _alpha = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Refresh(); }
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
            G.SmoothingMode = SmoothingMode.HighQuality;

            G.TextRenderingHint = TM is not null ?
                                 (TM.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit)
                                : Program.Style.RenderingHint;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Rectangle rect_outer = new(0, 0, Width - 1, Height - 1);
            Rectangle rect_inner = new(1, 1, Width - 3, Height - 3);

            Color bkC = Color.FromArgb(255 - alpha, scheme.Colors.Back(parentLevel));
            Color bkCC = bkC;

            if (TM is not null)
            {
                using (Colors_Collection colorsCollection = new(TM.Info.Color1, TM.Info.Color1, Program.Style.DarkMode))
                {
                    bkCC = Color.FromArgb(alpha, colorsCollection.Back_Checked_Hover);
                }
            }

            using (SolidBrush br = new(bkC)) { G.FillRoundedRect(br, rect_inner); }
            using (SolidBrush br = new(bkCC)) { G.FillRoundedRect(br, rect_outer); }

            if (pattern is not null) G.FillRoundedRect(pattern, rect_inner);

            float factor_max = 20f;
            float factor1 = (float)((double)factor_max * (alpha / 255d));
            float factor2 = (float)((double)factor_max * ((255 - alpha) / 255d));
            int CircleR = (int)Math.Round(rect_inner.Height * 0.4d);

            Rectangle Circle1 = new((int)Math.Round(rect_inner.X + 10 + factor_max - factor1), (int)Math.Round(rect_inner.Y + (rect_inner.Height - CircleR) / 2d), CircleR, CircleR);
            Rectangle Circle2 = new((int)Math.Round(rect_inner.X + 10 + CircleR + CircleR * 0.4d - (double)factor2), (int)Math.Round(rect_inner.Y + (rect_inner.Height - CircleR) / 2d), CircleR, CircleR);

            if (TM is not null)
            {
                using (SolidBrush br = new(Color.FromArgb(125, TM.Info.Color2)))
                using (Pen P = new(TM.Info.Color2.CB(0.3f * (Program.Style.DarkMode ? +1f : +2f)), 1.75f))
                {
                    G.FillEllipse(br, Circle2);
                    G.DrawEllipse(P, Circle2);
                }

                using (SolidBrush br = new(Color.FromArgb(125, TM.Info.Color1)))
                using (Pen P = new(TM.Info.Color1.CB(0.3f * (Program.Style.DarkMode ? +1f : +2f)), 1.75f))
                {
                    G.FillEllipse(br, Circle1);
                    G.DrawEllipse(P, Circle1);
                }
            }

            if (BackgroundImage is not null) G.DrawRoundImage(BackgroundImage, State == MouseState.Over ? rect_outer : rect_inner);

            if (State != MouseState.None) G.FillRoundedRect(Noise, rect_inner);

            Color lC, lCC;
            if (TM is not null)
            {
                using (Colors_Collection colorsCollection = new(TM.Info.Color1, TM.Info.Color1, Program.Style.DarkMode))
                {
                    bkCC = Color.FromArgb(alpha, colorsCollection.Back_Checked_Hover);

                    lC = Color.FromArgb(255 - alpha, State != MouseState.None ? colorsCollection.Line_Checked : scheme.Colors.Line(parentLevel));
                    lCC = Color.FromArgb(alpha, colorsCollection.Line_Checked_Hover);
                }
            }
            else
            {
                lC = Color.FromArgb(255 - alpha, State != MouseState.None ? scheme.Colors.Line_Checked : scheme.Colors.Line(parentLevel));
                lCC = Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover);
            }

            using (Pen P = new(lC)) { G.DrawRoundedRect_LikeW11(P, rect_inner); }
            using (Pen P = new(lCC)) { G.DrawRoundedRect_LikeW11(P, rect_outer); }

            Rectangle ThemeName_Rect = new(rect_inner.X, rect_inner.Y, rect_inner.Width - 10, 25);
            Rectangle Author_Rect = new(ThemeName_Rect.X, ThemeName_Rect.Bottom, ThemeName_Rect.Width - 20, 15);
            Rectangle OS_Rect = new(0, Author_Rect.Bottom + 7, 16, 16);
            Rectangle Version_Rect = new(ThemeName_Rect.X, OS_Rect.Bottom + 6, ThemeName_Rect.Width - 20, 15);

            if (TM is not null)
            {
                Color FC = Color.FromArgb(Math.Max(125, alpha), bkC.IsDark() ? Color.White : Color.Black);

                using (StringFormat sf = ContentAlignment.MiddleRight.ToStringFormat())
                using (SolidBrush br = new(FC))
                {
                    G.DrawString(TM.Info.ThemeName, new Font(TM.MetricsFonts.CaptionFont.Name, 11f, FontStyle.Bold), br, ThemeName_Rect, sf);
                }

                Rectangle BadgeRect = new(Author_Rect.Right + 2, Author_Rect.Y, 16, 16);
                Rectangle VerRect = new(Version_Rect.Right + 2, Version_Rect.Y - 2, 18, 18);

                if (DoneByWinPaletter)
                {
                    G.DrawImage(Assets.WinPaletter_Store.DoneByWinPaletter, BadgeRect);
                }
                else
                {
                    G.DrawImage(Assets.WinPaletter_Store.DoneByUser, BadgeRect);
                }

                string author = DoneByWinPaletter ? Application.ProductName : TM.Info.Author;
                using (StringFormat sf = ContentAlignment.MiddleRight.ToStringFormat())
                using (Font f = new(TM.MetricsFonts.CaptionFont.Name, 9f, FontStyle.Regular))
                using (SolidBrush br = new(FC))
                {
                    G.DrawString($"{Program.Lang.By} {author}", f, br, Author_Rect, sf);

                    G.DrawImage(Assets.WinPaletter_Store.ThemeVersion, VerRect);
                    G.DrawString(TM.Info.ThemeVersion, Fonts.Console, br, Version_Rect, sf);
                }

                for (int i = 0; i <= DesignedFor_Badges.Count - 1; i++)
                    G.DrawImage(DesignedFor_Badges[i], new Rectangle(BadgeRect.Right - 16 - 18 * i, OS_Rect.Y, OS_Rect.Width, OS_Rect.Height));
            }

            base.OnPaint(e);
        }
    }
}