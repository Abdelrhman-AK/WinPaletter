using FluentTransitions;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Properties;

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
        }

        #region Variables
        private readonly Color LineColor = Color.FromArgb(120, 150, 150, 150);
        private Color C1, C2;
        private float _Angle = 0f;
        private bool _Focused = true;
        private static readonly TextureBrush Noise = new(Resources.Noise.Fade(0.9f));
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
            get => _Color1;
            set
            {
                if (value != _Color1)
                {
                    _Color1 = value;
                    SetColors();
                }
            }
        }

        private Color _Color2 = Color.Crimson;
        public Color Color2
        {
            get => _Color2;
            set
            {
                if (value != _Color2)
                {
                    _Color2 = value;
                    SetColors();
                }
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
                if (value != _Color)
                {
                    _Color = value;
                    SetColors();
                }
            }
        }

        private void SetColors()
        {
            C1 = Program.Style.DarkMode ? Color1.Dark(0.15f) : Color1.Light(0.6f);
            C2 = Program.Style.DarkMode ? Color2.Dark(0.15f) : Color2.Light(0.6f);
        }

        public Styles Style { get; set; } = Styles.SwapColors;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        #endregion

        #region Animator

        private readonly Timer Timer = new() { Enabled = false, Interval = 40 };
        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                bool needInvalidate = false;

                if (_Angle + 3f > 360f)
                {
                    _Angle = 0f;

                    if (Style == Styles.SwapColors)
                    {
                        if (Color == C1 || Color == Color1)
                        {
                            await Task.Run(() => Transition.With(this, nameof(Color), C2).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)));
                        }
                        else
                        {
                            await Task.Run(() => Transition.With(this, nameof(Color), C1).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)));
                        }
                        needInvalidate = true;
                    }
                }
                else
                {
                    _Angle += 3f;
                    needInvalidate = true;
                }

                if (needInvalidate)
                    Invalidate();
            }
            else
            {
                Timer.Enabled = false;
                Timer.Stop();
            }
        }

        #endregion

        #region Events/Overrides

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                Timer.Tick += Timer_Tick;
                if (FindForm() is not null)
                {
                    FindForm().Activated += Form_GotFocus;
                    FindForm().Shown += Form_Shown;
                    FindForm().Deactivate += Form_LostFocus;
                }

                Timer.Enabled = Enabled;
                if (Enabled) Timer.Start(); else Timer.Stop();
            }
            else
            {
                Timer.Enabled = false;
                Timer.Stop();
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (!DesignMode)
            {
                Timer.Tick -= Timer_Tick;
                if (FindForm() is not null)
                {
                    FindForm().Activated -= Form_GotFocus;
                    FindForm().Shown -= Form_Shown;
                    FindForm().Deactivate -= Form_LostFocus;
                }
            }

            base.OnHandleDestroyed(e);
        }

        public void Form_GotFocus(object sender, EventArgs e)
        {
            _Focused = Enabled;
            Timer.Enabled = Enabled;
            if (Enabled) Timer.Start(); else Timer.Stop();
            Invalidate();
        }

        public void Form_Shown(object sender, EventArgs e)
        {
            _Focused = Enabled;
            SetColors();
            Timer.Enabled = Enabled;
            if (Enabled) Timer.Start(); else Timer.Stop();
            Invalidate();
        }

        public void Form_LostFocus(object sender, EventArgs e)
        {
            _Focused = false;
            Timer.Enabled = false;
            Timer.Stop();
            Invalidate();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.DoubleBuffer();

            base.OnControlAdded(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Timer?.Dispose();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (Enabled)
            {
                _Focused = true;
                Timer.Enabled = true;
                Timer.Start();
            }
            else
            {
                _Focused = false;
                Timer.Enabled = false;
                Timer.Stop();
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

                        // Sometimes, Noise is used anywhere else and will throw an error
                        try
                        {
                            G.FillRectangle(Noise, Rect);
                        }
                        catch { }

                        using (TextureBrush tb = new(Assets.Store.Pattern1))
                        {
                            G.FillRectangle(tb, Rect);
                        }
                    }
                }
            }

            //base.OnPaint(e);
        }
    }
}