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
    [Description("Themed toggle for WinPaletter UI")]
    [DefaultEvent("CheckedChanged")]
    public class Toggle : Control
    {
        public Toggle()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Size = new(40, 20);
            Text = string.Empty;

            _checkerX = Checked ? Width - 17 : 4;
            CheckC = new(CheckerX, 4, 11, 11);

            if (DarkLight_Toggler)
            {
                CheckC.Width = DarkLight_TogglerSize;
                CheckC.Height = DarkLight_TogglerSize;
            }
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private RectangleF CheckC = new(4, 4, 11, 11);
        private int MouseState = 0;
        private bool WasMoving = false;
        private readonly int DarkLight_TogglerSize = 13;
        #endregion

        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new Color BackColor { get => Color.Transparent; set {; } }

        public bool DarkLight_Toggler { get; set; } = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        private bool _checked;
        public bool Checked
        {
            get => _checked;
            set
            {
                if (value != _checked)
                {
                    _checked = value;
                    OnCheckedChanged();
                }
            }
        }

        private float _checkerX = 4;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float CheckerX
        {
            get => _checkerX;
            set { _checkerX = value; Invalidate(); }
        }
        #endregion

        #region Events/Overrides
        public event EventHandler CheckedChanged;

        protected virtual void OnCheckedChanged()
        {
            CheckedChanged?.Invoke(this, EventArgs.Empty);

            if (CanAnimate)
            {
                Transition.With(this, nameof(CheckerX), _checked ? Width - 17f : 4f).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                _checkerX = Checked ? Width - 17 : 4;
                Invalidate();
            }

            if (DarkLight_Toggler)
            {
                CheckC.Width = DarkLight_TogglerSize;
                CheckC.Height = DarkLight_TogglerSize;
                Invalidate();
            }
        }

        protected override  void OnResize(EventArgs e)
        {
            Height = 20;
            if (Width < 40) Width = 40;

            if (DarkLight_Toggler)
            {
                CheckC.Width = DarkLight_TogglerSize;
                CheckC.Height = DarkLight_TogglerSize;
            }

            Invalidate();

            base.OnResize(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float i = e.X - 0.5f * CheckC.Width;

                // Only consider it a drag if it moves more than 2 pixels
                if (i - CheckerX > 2f) WasMoving = true;

                MouseState = 1;

                if (i <= 4) _checkerX = 4;
                else if (i >= Width - 17) _checkerX = Width - 17;
                else _checkerX = i;

                CheckC.X = CheckerX;
                CheckC.Y = 4;

                Invalidate();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseState = 0;
            CheckC.Width = DarkLight_Toggler ? DarkLight_TogglerSize : 11;
            CheckC.Height = DarkLight_Toggler ? DarkLight_TogglerSize : 11;

            // Only consider it a drag if it actually moved more than a threshold (e.g., 2px)
            if (WasMoving)
            {
                Checked = e.X >= Width * 0.5f;
                WasMoving = false;
            }
            else
            {
                // Otherwise, toggle on simple click
                Checked = !Checked;
            }

            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            MouseState = 1;
            CheckC.Width = 13;

            Invalidate();

            base.OnMouseDown(e);
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
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

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Rectangle MainRect = new(0, 0, Width - 1, Height - 1);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            CheckC.X = CheckerX;

            int min = 4;
            int max = Width - 17;
            float val = (float)(CheckC.X / max);

            if (val < 0f) val = 0f;
            if (val > 1f) val = 1f;
            if (CheckC.X <= min) val = 0f;
            if (CheckC.X >= max) val = 1f;

            int alpha = (int)(255f * val);

            if (!DarkLight_Toggler)
            {
                // Checked part
                using (SolidBrush br = new(Color.FromArgb(alpha, scheme.Colors.AccentAlt))) { G.FillRoundedRect(br, MainRect, 9, true); }

                using (Pen P = new(Color.FromArgb(alpha, scheme.Colors.AccentAlt))) { G.DrawRoundedRectBeveled(P, MainRect, 9, true); }

                using (SolidBrush br = new(Color.FromArgb(alpha, scheme.Colors.AccentAlt.IsDark() ? Color.Black : Color.White))) { G.FillEllipse(br, CheckC); }

                // Non checked part
                using (SolidBrush br = new(Color.FromArgb(255 - alpha, scheme.Colors.ForeColor))) { G.FillEllipse(br, CheckC); }

                using (Pen P = new(Color.FromArgb(255 - alpha, scheme.Colors.ForeColor))) { G.DrawRoundedRect(P, MainRect, 9, true); }
            }

            else
            {
                LinearGradientBrush lgbChecked =
                    new(MainRect,
                    Color.FromArgb(alpha, scheme.Colors.Accent),
                    Color.FromArgb(alpha, scheme.Colors.AccentAlt),
                    LinearGradientMode.ForwardDiagonal);

                LinearGradientBrush lgborderChecked =
                    new(MainRect,
                    Color.FromArgb(alpha, scheme.Colors.Accent),
                    Color.FromArgb(alpha, scheme.Colors.AccentAlt),
                    LinearGradientMode.BackwardDiagonal);

                LinearGradientBrush lgbNonChecked =
                    new(MainRect, Color.FromArgb(255 - alpha, scheme.Colors.AccentAlt),
                    Color.FromArgb(255 - alpha, scheme.Colors.Accent),
                    LinearGradientMode.BackwardDiagonal);

                LinearGradientBrush lgborderNonChecked =
                    new(MainRect, Color.FromArgb(255 - alpha, scheme.Colors.AccentAlt),
                    Color.FromArgb(255 - alpha, scheme.Colors.Accent),
                    LinearGradientMode.ForwardDiagonal);

                G.FillRoundedRect(lgbChecked, MainRect, 9, true);
                G.FillRoundedRect(lgbNonChecked, MainRect, 9, true);

                if (Checked)
                {
                    using (Bitmap b0 = (scheme.Colors.Line(parentLevel).IsDark() ? Resources.darkmode_light : Resources.darkmode_dark).Fade((float)val))
                    using (Bitmap b1 = (scheme.Colors.Line(parentLevel).IsDark() ? Resources.lightmode_light : Resources.lightmode_dark).Fade((float)(1f - val)))
                    {
                        G.DrawImage(b0, CheckC);
                        G.DrawImage(b1, CheckC);
                    }
                }
                else
                {
                    using (Bitmap b0 = (scheme.Colors.AccentAlt.IsDark() ? Resources.darkmode_light : Resources.darkmode_dark).Fade((float)val))
                    using (Bitmap b1 = (scheme.Colors.AccentAlt.IsDark() ? Resources.lightmode_light : Resources.lightmode_dark).Fade((float)(1f - val)))
                    {
                        G.DrawImage(b0, CheckC);
                        G.DrawImage(b1, CheckC);
                    }
                }

                using (Pen P = new(lgborderChecked)) { G.DrawRoundedRectBeveled(P, MainRect, 9, true); }

                using (Pen P = new(lgborderNonChecked)) { G.DrawRoundedRectBeveled(P, MainRect, 9, true); }
            }

            base.OnPaint(e);


        }
    }
}