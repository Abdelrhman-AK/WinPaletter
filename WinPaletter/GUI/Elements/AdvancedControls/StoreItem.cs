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

        private readonly static TextureBrush Noise = new(Properties.Resources.Noise.Fade(0.65f));
        private TextureBrush pattern;
        private readonly List<Bitmap> DesignedFor_Badges = [];

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

                    sameTheme = TM is not null &&
                                TM.Info.ThemeName.Trim().ToLower() == Program.TM.Info.ThemeName.Trim().ToLower() &&
                                TM.Info.Author.Trim().ToLower() == Program.TM.Info.Author.Trim().ToLower();

                    hasUpdate = TM is not null &&
                                sameTheme && new Version(TM.Info.ThemeVersion) > new Version(Program.TM.Info.ThemeVersion);
                }
                else
                {
                    _TM = null;
                    pattern?.Dispose();
                    sameTheme = false;
                    hasUpdate = false;
                }

                ThemeManagerChanged?.Invoke(this, new EventArgs());
            }
        }

        bool sameTheme = false;

        bool hasUpdate = false;

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

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 128).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 128; }

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
            _TM?.Dispose();
            pattern?.Dispose();

            foreach (Bitmap bmp in DesignedFor_Badges) { bmp?.Dispose(); }

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
            foreach (Bitmap bmp in DesignedFor_Badges) { bmp?.Dispose(); }

            DesignedFor_Badges.Clear();

            if (_TM is not null)
            {
                if (_TM.Info.DesignedFor_Win12) DesignedFor_Badges.Add(Assets.Store.DesignedFor12);
                if (_TM.Info.DesignedFor_Win11) DesignedFor_Badges.Add(Assets.Store.DesignedFor11);
                if (_TM.Info.DesignedFor_Win10) DesignedFor_Badges.Add(Assets.Store.DesignedFor10);
                if (_TM.Info.DesignedFor_Win81) DesignedFor_Badges.Add(Assets.Store.DesignedFor8);
                if (_TM.Info.DesignedFor_Win8) DesignedFor_Badges.Add(Assets.Store.DesignedFor8);
                if (_TM.Info.DesignedFor_Win7) DesignedFor_Badges.Add(Assets.Store.DesignedFor7);
                if (_TM.Info.DesignedFor_WinVista) DesignedFor_Badges.Add(Assets.Store.DesignedForVista);
                if (_TM.Info.DesignedFor_WinXP) DesignedFor_Badges.Add(Assets.Store.DesignedForXP);
            }

            Invalidate();
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
                        bmp = Assets.Store.Pattern1;
                        break;
                    }
                case 2:
                    {
                        bmp = Assets.Store.Pattern2;
                        break;
                    }
                case 3:
                    {
                        bmp = Assets.Store.Pattern3;
                        break;
                    }
                case 4:
                    {
                        bmp = Assets.Store.Pattern4;
                        break;
                    }
                case 5:
                    {
                        bmp = Assets.Store.Pattern5;
                        break;
                    }
                case 6:
                    {
                        bmp = Assets.Store.Pattern6;
                        break;
                    }
                case 7:
                    {
                        bmp = Assets.Store.Pattern7;
                        break;
                    }
                case 8:
                    {
                        bmp = Assets.Store.Pattern8;
                        break;
                    }
                case 9:
                    {
                        bmp = Assets.Store.Pattern9;
                        break;
                    }
                case 10:
                    {
                        bmp = Assets.Store.Pattern10;
                        break;
                    }
                case 11:
                    {
                        bmp = Assets.Store.Pattern11;
                        break;
                    }
                case 12:
                    {
                        bmp = Assets.Store.Pattern12;
                        break;
                    }
                case 13:
                    {
                        bmp = Assets.Store.Pattern13;
                        break;
                    }
                case 14:
                    {
                        bmp = Assets.Store.Pattern14;
                        break;
                    }
                case 15:
                    {
                        bmp = Assets.Store.Pattern15;
                        break;
                    }
                case 16:
                    {
                        bmp = Assets.Store.Pattern16;
                        break;
                    }
                case 17:
                    {
                        bmp = Assets.Store.Pattern17;
                        break;
                    }
                default:
                    {
                        bmp = null;
                        break;
                    }

            }

            if (!Program.Style.DarkMode)
            {
                using (ImageFactory imgF = new())
                {
                    imgF.Load(bmp);
                    imgF.Contrast(40);
                    bmp = (imgF.Image as Bitmap).Invert().Fade(0.7f);
                }
            }

            if (bmp != null) pattern = new(bmp); else pattern = null;

            bmp?.Dispose();

            Invalidate();
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

            G.TextRenderingHint = TM is not null ?
                                 (TM.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit)
                                : Program.Style.TextRenderingHint;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Scheme scheme_tertiary = Enabled ? Program.Style.Schemes.Tertiary : Program.Style.Schemes.Disabled;

            Rectangle rect_outer = new(0, 0, Width - 1, Height - 1);
            Rectangle rect_inner = new(1, 1, Width - 3, Height - 3);

            Color back0 = scheme.Colors.Back(parentLevel);
            Color back1 = scheme.Colors.Back_Hover(parentLevel + 5);
            Color back_hover0 = back0;

            if (TM is not null)
            {
                using (Colors_Collection colorsCollection = new(TM.Info.Color1, TM.Info.Color1, Program.Style.DarkMode))
                {
                    back_hover0 = Color.FromArgb(alpha, colorsCollection.Back_Checked);
                }
            }

            using (LinearGradientBrush br = new(rect_outer, back1, back0, LinearGradientMode.ForwardDiagonal)) { G.FillRoundedRect(br, rect_inner); }
            using (LinearGradientBrush br = new(rect_outer, back_hover0, Color.Transparent, 0f)) { G.FillRoundedRect(br, rect_outer); }

            if (Noise is not null && State != MouseState.None) G.FillRoundedRect(Noise, rect_inner);
            if (!DesignMode && pattern is not null) G.FillRoundedRect(pattern, rect_inner);
            if (BackgroundImage is not null) G.DrawRoundImage(BackgroundImage, State == MouseState.Over ? rect_outer : rect_inner);

            Color line, line_hover0;
            if (TM is not null)
            {
                using (Colors_Collection colorsCollection = new(TM.Info.Color1, TM.Info.Color1, Program.Style.DarkMode))
                {
                    line = Color.FromArgb(255 - alpha, State != MouseState.None ? colorsCollection.Line_Checked : scheme.Colors.Line(parentLevel));
                    line_hover0 = Color.FromArgb(alpha, colorsCollection.Line_Checked_Hover);
                }
            }
            else
            {
                line = Color.FromArgb(255 - alpha, State != MouseState.None ? scheme.Colors.Line_Checked : scheme.Colors.Line(parentLevel));
                line_hover0 = Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover);
            }

            using (Pen P = new(line)) { G.DrawRoundedRectBeveled(P, rect_inner); }
            using (Pen P = new(line_hover0)) { G.DrawRoundedRectBeveled(P, rect_outer); }

            if (TM is not null)
            {
                float CircleR = rect_inner.Height * 0.4f;
                float factor = CircleR * (alpha / 255f) / 4f;

                int rowsNumber = System.IO.File.Exists(URL_ThemeFile) ? 4 : 3;
                if (DesignedFor_Badges.Count > 0) rowsNumber++;
                int titleHeight = 26;
                int itemsHeight = 18;
                int startingY = rect_inner.Y + (rect_inner.Height - (titleHeight + (itemsHeight * (rowsNumber - 1)))) / 2 + 2;

                RectangleF Circle1 = new(rect_inner.Right - CircleR * 1.35f + factor, rect_inner.Y + (rect_inner.Height - CircleR) / 2f - factor, CircleR, CircleR);
                RectangleF Circle2 = new(rect_inner.Right - CircleR * 1.35f - factor, rect_inner.Y + (rect_inner.Height - CircleR) / 2f + factor, CircleR, CircleR);
                RectangleF Circle1_gradientFix = new(Circle1.X - 2, Circle1.Y - 2, Circle1.Width + 4, Circle1.Height + 4);
                RectangleF Circle2_gradientFix = new(Circle2.X - 2, Circle2.Y - 2, Circle2.Width + 4, Circle2.Height + 4);

                Rectangle ThemeName_Rect = Rectangle.FromLTRB(rect_inner.X + 10, startingY, rect_inner.Right - 10, startingY + titleHeight);
                Rectangle Author_Rect = new(ThemeName_Rect.X + 16 + 4, ThemeName_Rect.Bottom + 5, ThemeName_Rect.Width - 20, itemsHeight);
                Rectangle OS_Rect = new(ThemeName_Rect.X + 2, Author_Rect.Bottom + 8, 16, itemsHeight);
                Rectangle File_Rect = new(ThemeName_Rect.X + 16 + 6, DesignedFor_Badges.Count > 0 ? OS_Rect.Bottom + 9 : Author_Rect.Bottom + 9, ThemeName_Rect.Width - 20, itemsHeight);
                Rectangle lowerRect = new(rect_inner.X + 7, rect_inner.Bottom - 20, rect_inner.Width - 14, itemsHeight);
                Rectangle BadgeRect = new(ThemeName_Rect.Left + 2, Author_Rect.Y, 16, 16);
                Rectangle FileRect_Img = new(ThemeName_Rect.Left + 2, File_Rect.Y - 1, 18, 18);

                Color foreColor = Color.FromArgb(Math.Max(Program.Style.DarkMode ? 125 : 175, alpha), back0.IsDark() ? Color.White : Color.Black);

                Color circle1_normal_color = Color.FromArgb(125, TM.Info.Color1);
                Color circle2_normal_color = Color.FromArgb(125, TM.Info.Color2);
                Color circle1_normal_pen_color = circle1_normal_color.CB(0.3f * (Program.Style.DarkMode ? +1f : -1f));
                Color circle2_normal_pen_color = circle2_normal_color.CB(0.3f * (Program.Style.DarkMode ? +1f : -0.25f));

                using (LinearGradientBrush circle1_lgb = new(Circle1_gradientFix, circle1_normal_color, Color.Transparent, 90f + alpha / 255f * 360f))
                using (LinearGradientBrush circle1_lgb_P = new(Circle1_gradientFix, circle1_normal_pen_color, Color.Transparent, 90f + alpha / 255f * 360f))
                using (LinearGradientBrush circle2_lgb = new(Circle2_gradientFix, Color.Transparent, circle2_normal_color, 90f + alpha / 255f * 360f))
                using (LinearGradientBrush circle2_lgb_P = new(Circle2_gradientFix, Color.Transparent, circle2_normal_pen_color, 90f + alpha / 255f * 360f))
                using (Pen circle1_P = new(circle1_lgb_P, 1.75f))
                using (Pen circle2_P = new(circle2_lgb_P, 1.75f))
                using (SolidBrush foreBrush = new(foreColor))
                using (SolidBrush verBrush = new(Color.FromArgb(alpha, foreColor)))
                using (Font fontTitle = new(TM.MetricsFonts.CaptionFont.Name, 11f, FontStyle.Bold))
                using (Font font = new(TM.MetricsFonts.CaptionFont.Name, 9f, FontStyle.Regular))
                using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                using (StringFormat sf_version = ContentAlignment.MiddleRight.ToStringFormat())
                {
                    G.DrawString(TM.Info.ThemeName, fontTitle, foreBrush, ThemeName_Rect, sf);

                    if (DoneByWinPaletter)
                    {
                        G.DrawImage(Assets.Store.DoneByWinPaletter, BadgeRect);
                    }
                    else
                    {
                        G.DrawImage(Assets.Store.DoneByUser, BadgeRect);
                    }

                    G.DrawString($"{Program.Lang.Strings.General.By} {(DoneByWinPaletter ? Application.ProductName : TM.Info.Author)}", font, foreBrush, Author_Rect, sf);

                    if (System.IO.File.Exists(URL_ThemeFile))
                    {
                        G.DrawImage(Assets.Store.Folder, FileRect_Img);
                        G.DrawString(System.IO.Path.GetFileName(URL_ThemeFile), Fonts.Console, foreBrush, File_Rect, sf);
                    }

                    if (hasUpdate)
                    {
                        G.DrawString(Program.Lang.Strings.Updates.NewUpdate, Fonts.Console, verBrush, lowerRect, sf);
                        G.DrawString(TM.Info.ThemeVersion, Fonts.Console, verBrush, lowerRect, sf_version);
                        G.DrawRoundedRectBeveled(scheme_tertiary.Pens.Line_Checked_Hover, rect_inner);
                    }
                    else if (!string.IsNullOrWhiteSpace(TM.Info.ThemeVersion))
                    {
                        G.DrawString(TM.Info.ThemeVersion, Fonts.Console, verBrush, lowerRect, sf_version);
                    }

                    for (int i = 0; i <= DesignedFor_Badges.Count - 1; i++)
                        G.DrawImage(DesignedFor_Badges[i], new Rectangle(OS_Rect.Left + 20 * i, OS_Rect.Y, 16, 16));

                    G.FillEllipse(circle2_lgb, Circle2);
                    G.DrawEllipse(circle2_P, Circle2);

                    G.FillEllipse(circle1_lgb, Circle1);
                    G.DrawEllipse(circle1_P, Circle1);
                }
            }

            base.OnPaint(e);


        }
    }
}