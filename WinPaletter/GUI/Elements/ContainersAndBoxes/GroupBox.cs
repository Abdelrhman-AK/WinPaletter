using ImageProcessor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("Themed GroupBox for WinPaletter UI")]
    [DefaultEvent("Click")]
    public class GroupBox : Panel
    {
        public GroupBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Text = string.Empty;
        }

        #region Properties

        private TextureBrush pattern;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        #endregion


        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
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
                        bmp = null;
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

            if (bmp != null) pattern = new(bmp); else pattern = null;

            Refresh();
        }


        private bool useSharpStyle = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool UseSharpStyle
        {
            get => useSharpStyle;
            set
            {
                useSharpStyle = value;
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle Rect = new(0, 0, Width - 1, Height - 1);

            if (!DesignMode)
            {
                Color ParentColor = this.GetParentColor();

                G.Clear(ParentColor);
                BackColor = ParentColor.CB((float)(ParentColor.IsDark() ? 0.04d : -0.05d));

                using (SolidBrush br = new(BackColor))
                using (Pen P = new(ParentColor.CB((float)(ParentColor.IsDark() ? 0.06d : -0.07d))))
                {
                    if (!useSharpStyle)
                    {
                        G.FillRoundedRect(br, Rect);
                        if (pattern != null) G.FillRoundedRect(pattern, Rect);
                        G.DrawRoundedRect(P, Rect);
                    }
                    else
                    {
                        G.FillRectangle(br, Rect);
                        if (pattern != null) G.FillRectangle(pattern, Rect);
                        G.DrawRectangle(P, Rect);
                    }
                }

            }
            else
            {
                using (SolidBrush br = new(Program.Style.Schemes.Main.Colors.Back(parentLevel)))
                using (Pen P = new(Program.Style.Schemes.Main.Colors.Line(parentLevel)))
                {
                    G.FillRectangle(br, Rect);
                    G.DrawRectangle(P, Rect);
                }
            }

            base.OnPaint(e);
        }
    }
}