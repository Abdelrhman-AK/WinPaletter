using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("AnimatedBox with two colors for WinPaletter UI")]
    [DefaultEvent("Click")]
    public class AnimatedBox : Panel
    {

        public AnimatedBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            Text = "";
            HandleCreated += AnimatedBox_HandleCreated;
            HandleDestroyed += AnimatedBox_HandleDestroyed;
            Timer.Tick += Timer_Tick;
        }

        #region Variables

        private Color LineColor;
        private float _Angle = 0f;
        private Color C1, C2;
        private bool _Focused = true;
        private readonly TextureBrush Noise = new TextureBrush(Properties.Resources.GaussianBlur.Fade(0.7d));

        public enum Styles
        {
            SwapColors,
            MixedColors
        }

        #endregion

        #region Properties
        public Color Color1 { get; set; } = Color.DodgerBlue;
        public Color Color2 { get; set; } = Color.Crimson;
        public Color Color { get; set; } = Color.DodgerBlue;
        public Styles Style { get; set; } = Styles.SwapColors;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = "";

        #endregion

        #region Animator
        private Timer Timer = new Timer() { Enabled = false, Interval = 1 };
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {

                if (_Angle + 1.5d > 360d)
                {
                    _Angle = 0f;

                    if (Style == Styles.SwapColors)
                    {
                        Color Cx1, Cx2;

                        if (My.Env.Style.DarkMode)
                        {
                            Cx1 = Color1.Dark(0.15f);
                            Cx2 = Color2.Dark(0.15f);
                        }
                        else
                        {
                            Cx1 = Color1.Light(0.6f);
                            Cx2 = Color2.Light(0.6f);
                        }

                        if (Color == Cx1 | Color == Color1)
                        {
                            Visual.FadeColor(this, "Color", Color, Cx2, 10, 1);
                        }
                        else
                        {
                            Visual.FadeColor(this, "Color", Color, Cx1, 10, 1);
                        }

                    }
                }

                else
                {
                    _Angle = (float)(_Angle + 1.5d);
                }

                Refresh();
            }
            else
            {
                Timer.Enabled = false;
                Timer.Stop();
            }
        }
        #endregion

        #region Events

        private void AnimatedBox_HandleCreated(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Timer.Enabled = true;
                Timer.Start();

                try
                {
                    FindForm().Activated += Form_GotFocus;
                }
                catch
                {
                }
                try
                {
                    FindForm().Deactivate += Form_LostFocus;
                }
                catch
                {
                }
            }

            else
            {
                Timer.Enabled = false;
                Timer.Stop();
            }
        }

        private void AnimatedBox_HandleDestroyed(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                try
                {
                    FindForm().Activated -= Form_GotFocus;
                }
                catch
                {
                }
                try
                {
                    FindForm().Deactivate -= Form_LostFocus;
                }
                catch
                {
                }
            }
        }

        public void Form_GotFocus(object sender, EventArgs e)
        {
            _Focused = true;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        public void Form_LostFocus(object sender, EventArgs e)
        {
            _Focused = false;
            Timer.Enabled = false;
            Timer.Stop();
            Invalidate();
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var G = e.Graphics;
            DoubleBuffered = true;

            G.SmoothingMode = SmoothingMode.AntiAlias;

            var Rect = new Rectangle(0, 0, Width - 1, Height - 1);

            G.Clear(this.GetParentColor());

            if (!DesignMode && _Focused)
            {

                if (Style == Styles.SwapColors)
                {
                    if (My.Env.Style.DarkMode)
                    {
                        C1 = Color.Dark(0.15f);
                    }
                    else
                    {
                        C1 = Color.Light(0.6f);
                    }

                    C2 = this.GetParentColor();
                }
                else if (Style == Styles.MixedColors)
                {

                    if (My.Env.Style.DarkMode)
                    {
                        C1 = Color1.Dark(0.15f);
                        C2 = Color2.Dark(0.15f);
                    }
                    else
                    {
                        C1 = Color1.Light(0.6f);
                        C2 = Color2.Light(0.6f);
                    }

                }

                using (var l = new LinearGradientBrush(Rect, C1, C2, _Angle, false))
                {

                    LineColor = Color.FromArgb(120, 150, 150, 150);

                    if (Dock == DockStyle.None)
                    {
                        G.FillRoundedRect(l, Rect);
                        G.FillRoundedRect(Noise, Rect);
                        using (var P = new Pen(LineColor))
                        {
                            G.DrawRoundedRect(P, Rect);
                        }
                    }
                    else
                    {
                        G.FillRectangle(l, Rect);
                        G.FillRectangle(Noise, Rect);
                    }
                }

            }

        }

    }

}