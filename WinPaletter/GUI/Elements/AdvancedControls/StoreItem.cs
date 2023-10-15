using ImageProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.Controllers
{

    [ToolboxItem(false)]
    [DefaultEvent("Click")]
    public class StoreItem : Panel
    {

        public StoreItem()
        {
            pattern = new TextureBrush(new Bitmap(Width, Height));
            Timer = new Timer() { Enabled = false, Interval = 1 };
            Font = new Font("Segoe UI", 9f);
            DoubleBuffered = true;
            MouseEnter += StoreItem_MouseEnter;
            MouseLeave += StoreItem_MouseLeave;
            Timer.Tick += Timer_Tick;
        }

        #region Variables

        private TextureBrush Noise = new TextureBrush(Properties.Resources.GaussianBlur.Fade(0.65d));
        private List<Bitmap> DesignedFor_Badges = new List<Bitmap>();
        private TextureBrush pattern;
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
        private Theme.Manager _TM;
        public Theme.Manager TM
        {
            get
            {
                return _TM;
            }
            set
            {
                if (value is not null)
                {
                    _TM = (Theme.Manager)value.Clone();
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

        #region Events

        public event ThemeManagerChangedEventHandler ThemeManagerChanged;

        public delegate void ThemeManagerChangedEventHandler(object sender, EventArgs e);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            AnimateOnClick = true;
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

        private void StoreItem_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void StoreItem_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        #endregion

        #region Voids/Functions
        public void UpdateBadges()
        {
            DesignedFor_Badges.Clear();
            if (_TM is not null)
            {
                if (_TM.Info.DesignedFor_Win11)
                    DesignedFor_Badges.Add(Properties.Resources.Store_DesignedFor11);
                if (_TM.Info.DesignedFor_Win10)
                    DesignedFor_Badges.Add(Properties.Resources.Store_DesignedFor10);
                if (_TM.Info.DesignedFor_Win81)
                    DesignedFor_Badges.Add(Properties.Resources.Store_DesignedFor8);
                if (_TM.Info.DesignedFor_Win7)
                    DesignedFor_Badges.Add(Properties.Resources.Store_DesignedFor7);
                if (_TM.Info.DesignedFor_WinVista)
                    DesignedFor_Badges.Add(Properties.Resources.Store_DesignedForVista);
                if (_TM.Info.DesignedFor_WinXP)
                    DesignedFor_Badges.Add(Properties.Resources.Store_DesignedForXP);
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
                        using (var x = new Bitmap(Width, Height))
                        {
                            bmp = (Bitmap)x.Clone();
                        }

                        break;
                    }

                case 1:
                    {
                        bmp = Properties.Resources.Store_Pattern1;
                        break;
                    }
                case 2:
                    {
                        bmp = Properties.Resources.Store_Pattern2;
                        break;
                    }
                case 3:
                    {
                        bmp = Properties.Resources.Store_Pattern3;
                        break;
                    }
                case 4:
                    {
                        bmp = Properties.Resources.Store_Pattern4;
                        break;
                    }
                case 5:
                    {
                        bmp = Properties.Resources.Store_Pattern5;
                        break;
                    }
                case 6:
                    {
                        bmp = Properties.Resources.Store_Pattern6;
                        break;
                    }
                case 7:
                    {
                        bmp = Properties.Resources.Store_Pattern7;
                        break;
                    }
                case 8:
                    {
                        bmp = Properties.Resources.Store_Pattern8;
                        break;
                    }
                case 9:
                    {
                        bmp = Properties.Resources.Store_Pattern9;
                        break;
                    }
                case 10:
                    {
                        bmp = Properties.Resources.Store_Pattern10;
                        break;
                    }

                default:
                    {
                        using (var x = new Bitmap(Width, Height))
                        {
                            bmp = (Bitmap)x.Clone();
                        }

                        break;
                    }

            }

            if (!My.Env.Style.DarkMode)
            {
                using (var imgF = new ImageFactory())
                {
                    imgF.Load(bmp);
                    imgF.Contrast(50);
                    bmp = ((Bitmap)imgF.Image).Invert();
                }
            }
            pattern = new TextureBrush(bmp);

            Refresh();
        }

        #endregion

        #region Animator
        private int alpha;
        private readonly int Factor = 40;
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
            var G = e.Graphics;

            G.SmoothingMode = SmoothingMode.HighQuality;

            if (TM is not null)
            {
                G.TextRenderingHint = TM.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;
            }
            else
            {
                G.TextRenderingHint = My.Env.RenderingHint;
            }

            DoubleBuffered = true;
            G.Clear(this.GetParentColor());

            var rect_outer = new Rectangle(0, 0, Width - 1, Height - 1);
            var rect_inner = new Rectangle(1, 1, Width - 3, Height - 3);

            var bkC = Color.FromArgb(255 - alpha, My.Env.Style.Colors.Back);
            var bkCC = bkC;

            if (TM is not null)
            {
                if (My.Env.Style.DarkMode)
                {
                    bkCC = Color.FromArgb(alpha, TM.Info.Color1.Dark());
                }
                else
                {
                    bkCC = Color.FromArgb(alpha, TM.Info.Color1.Light());
                }
            }

            G.FillRoundedRect(new SolidBrush(bkC), rect_inner);
            G.FillRoundedRect(new SolidBrush(bkCC), rect_outer);

            if (pattern is not null)
                G.FillRoundedRect(pattern, rect_inner);

            float factor_max = 20f;
            float factor1 = (float)((double)factor_max * (alpha / 255d));
            float factor2 = (float)((double)factor_max * ((255 - alpha) / 255d));
            int CircleR = (int)Math.Round(rect_inner.Height * 0.4d);

            var Circle1 = new Rectangle((int)Math.Round(rect_inner.X + 10 + factor_max - factor1), (int)Math.Round(rect_inner.Y + (rect_inner.Height - CircleR) / 2d), CircleR, CircleR);
            var Circle2 = new Rectangle((int)Math.Round(rect_inner.X + 10 + CircleR + CircleR * 0.4d - (double)factor2), (int)Math.Round(rect_inner.Y + (rect_inner.Height - CircleR) / 2d), CircleR, CircleR);

            if (TM is not null)
            {
                G.FillEllipse(new SolidBrush(Color.FromArgb(125, TM.Info.Color2)), Circle2);
                G.DrawEllipse(new Pen(TM.Info.Color2.CB(0.3f), 1.75f), Circle2);

                G.FillEllipse(new SolidBrush(Color.FromArgb(125, TM.Info.Color1)), Circle1);
                G.DrawEllipse(new Pen(TM.Info.Color1.CB(0.3f), 1.75f), Circle1);
            }

            if (BackgroundImage is not null)
            {
                switch (State)
                {
                    case MouseState.Over:
                        {
                            G.DrawRoundImage(BackgroundImage, rect_outer);
                            break;
                        }

                    default:
                        {
                            G.DrawRoundImage(BackgroundImage, rect_inner);
                            break;
                        }

                }
            }

            if (State != MouseState.None)
                G.FillRoundedRect(Noise, rect_inner);

            var lC = Color.FromArgb(255 - alpha, State != MouseState.None ? My.Env.Style.Colors.Border_Checked : My.Env.Style.Colors.Border);
            var lCC = Color.FromArgb(alpha, My.Env.Style.Colors.Border_Checked_Hover);

            G.DrawRoundedRect_LikeW11(new Pen(lC), rect_inner);
            G.DrawRoundedRect_LikeW11(new Pen(lCC), rect_outer);

            var ThemeName_Rect = new Rectangle(rect_inner.X, rect_inner.Y, rect_inner.Width - 10, 25);
            var Author_Rect = new Rectangle(ThemeName_Rect.X, ThemeName_Rect.Bottom, ThemeName_Rect.Width - 20, 15);
            var OS_Rect = new Rectangle(0, Author_Rect.Bottom + 7, 16, 16);
            var Version_Rect = new Rectangle(ThemeName_Rect.X, OS_Rect.Bottom + 6, ThemeName_Rect.Width - 20, 15);

            if (TM is not null)
            {
                var FC = Color.FromArgb(Math.Max(100, alpha), bkC.IsDark() ? Color.White : Color.Black);
                G.DrawString(TM.Info.ThemeName, new Font(TM.MetricsFonts.CaptionFont.Name, 11f, FontStyle.Bold), new SolidBrush(FC), ThemeName_Rect, ContentAlignment.MiddleRight.ToStringFormat());

                var BadgeRect = new Rectangle(Author_Rect.Right + 2, Author_Rect.Y, 16, 16);
                var VerRect = new Rectangle(Version_Rect.Right + 2, Version_Rect.Y - 2, 18, 18);

                if (DoneByWinPaletter)
                {
                    G.DrawImage(Properties.Resources.Store_DoneByWinPaletter, BadgeRect);
                }
                else
                {
                    G.DrawImage(Properties.Resources.Store_DoneByUser, BadgeRect);
                }

                string author = DoneByWinPaletter ? My.MyProject.Application.Info.ProductName : TM.Info.Author;
                G.DrawString(My.Env.Lang.By + " " + author, new Font(TM.MetricsFonts.CaptionFont.Name, 9f, FontStyle.Regular), new SolidBrush(FC), Author_Rect, ContentAlignment.MiddleRight.ToStringFormat());

                G.DrawImage(Properties.Resources.Store_ThemeVersion, VerRect);
                G.DrawString(TM.Info.ThemeVersion, My.MyProject.Application.ConsoleFont, new SolidBrush(FC), Version_Rect, ContentAlignment.MiddleRight.ToStringFormat());

                for (int i = 0, loopTo = DesignedFor_Badges.Count - 1; i <= loopTo; i++)
                    G.DrawImage(DesignedFor_Badges[i], new Rectangle(BadgeRect.Right - 16 - 18 * i, OS_Rect.Y, OS_Rect.Width, OS_Rect.Height));

            }


        }

    }

}