﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("AnimatedBox with two colors for WinPaletter UI")]
    [DefaultEvent("Click")]
    public class AnimatedBox : ContainerControl
    {
        public AnimatedBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Text = string.Empty;
            BackColor = Color.Transparent;
            SetColors();
            HandleCreated += AnimatedBox_HandleCreated;
            HandleDestroyed += AnimatedBox_HandleDestroyed;
            Timer.Tick += Timer_Tick;
            ControlAdded += AnimatedBox_ControlAdded;
        }

        #region Variables
        private Color LineColor = Color.FromArgb(120, 150, 150, 150);
        private Color C1, C2;
        private float _Angle = 0f;
        private bool _Focused = true;
        private readonly TextureBrush Noise = new(Properties.Resources.GaussianBlur.Fade(0.9d));
        public enum Styles
        {
            SwapColors,
            MixedColors
        }
        #endregion

        #region Properties

        private Color _Color1 = Color.DodgerBlue;
        public Color Color1
        {
            get
            {
                return _Color1;
            }
            set
            {
                _Color1 = value;
                SetColors();
            }
        }

        private Color _Color2 = Color.Crimson;
        public Color Color2
        {
            get
            {
                return _Color2;
            }
            set
            {
                _Color2 = value;
                SetColors();
            }
        }

        private Color _Color = default;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Color Color
        {
            get
            {
                if (_Color == default)
                {
                    return C1;
                }
                else
                {
                    return _Color;
                }
            }

            set
            {
                _Color = value;
                SetColors();
            }
        }

        private void SetColors()
        {
            this.C1 = Program.Style.DarkMode ? Color1.Dark(0.15f) : Color1.Light(0.6f);
            this.C2 = Program.Style.DarkMode ? Color2.Dark(0.15f) : Color2.Light(0.6f);
        }

        public Styles Style { get; set; } = Styles.SwapColors;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        protected override CreateParams CreateParams
        {
            get
            {
                var cpar = base.CreateParams;
                if (!DesignMode)
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
        #endregion

        #region Animator
        private readonly Timer Timer = new() { Enabled = false, Interval = 25 };
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (_Angle + 2f > 360f)
                {
                    _Angle = 0f;

                    if (Style == Styles.SwapColors)
                    {
                        if (Color == C1 | Color == Color1)
                        {
                            System.Threading.Tasks.Task.Run(() => { FluentTransitions.Transition.With(this, nameof(Color), C2).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); });
                        }
                        else
                        {
                            System.Threading.Tasks.Task.Run(() => { FluentTransitions.Transition.With(this, nameof(Color), C1).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); });
                        }
                    }
                }

                else { _Angle += 2f; }

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
                try { FindForm().Activated += Form_GotFocus; }
                catch { }

                try { FindForm().Shown += Form_Shown; }
                catch { }

                try { FindForm().Deactivate += Form_LostFocus; }
                catch { }

                Timer.Enabled = true;
                Timer.Start();
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
                try { FindForm().Activated -= Form_GotFocus; }
                catch { }

                try { FindForm().Shown -= Form_Shown; }
                catch { }

                try { FindForm().Deactivate -= Form_LostFocus; }
                catch { }
            }
        }

        public void Form_GotFocus(object sender, EventArgs e)
        {
            _Focused = true;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        public void Form_Shown(object sender, EventArgs e)
        {
            _Focused = true;
            SetColors();
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

        private void AnimatedBox_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.DoubleBuffer();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            Rectangle Rect = new(0, 0, Width, Height);
            Rectangle BorderRect = new(Rect.X, Rect.Y, Rect.Width - 1, Rect.Height - 1);

            if (!DesignMode && _Focused)
            {
                Color Cx2 = Style == Styles.SwapColors ? BackColor : C2;

                using (LinearGradientBrush l = new(Rect, Color, Cx2, _Angle, false))
                {
                    if (Dock == DockStyle.None)
                    {
                        G.FillRoundedRect(l, Rect);
                        G.FillRoundedRect(Noise, Rect);
                        using (Pen P = new(LineColor))
                        {
                            G.DrawRoundedRect(P, BorderRect);
                        }
                    }
                    else
                    {
                        G.FillRectangle(l, Rect);
                        G.FillRectangle(Noise, Rect);
                    }
                }
            }

            base.OnPaint(e);
        }
    }

}