using FluentTransitions;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
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
            SetStyle(ControlStyles.UserPaint |  ControlStyles.SupportsTransparentBackColor |  ControlStyles.OptimizedDoubleBuffer |  ControlStyles.AllPaintingInWmPaint |  ControlStyles.ResizeRedraw, true);
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
        private bool WasMoving = false;
        private readonly int DarkLight_TogglerSize = 13;
        
        private Rectangle _mainRect;
        private int _min = 4;
        private int _max;
        private float _lastAlpha = -1;
        private bool _lastDarkLightMode = false;
        private bool _lastCheckedState = false;

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
            set 
            { 
                if (_checkerX != value)
                {
                    _checkerX = value; 
                    Invalidate();
                }
            }
        }

        #endregion

        #region Events/Overrides

        public event EventHandler CheckedChanged;

        protected virtual void OnCheckedChanged()
        {
            CheckedChanged?.Invoke(this, EventArgs.Empty);

            if (CanAnimate)
            {
                Transition.With(this, nameof(CheckerX), _checked ? Width - 17f : 4f).CriticalDamp(Program.AnimationSpan_Quick);
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

        protected override void OnResize(EventArgs e)
        {
            Height = 20;
            if (Width < 40) Width = 40;

            _mainRect = new Rectangle(0, 0, Width - 1, Height - 1);
            _max = Width - 17;

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

                float newCheckerX;
                if (i <= 4) newCheckerX = 4;
                else if (i >= Width - 17) newCheckerX = Width - 17;
                else newCheckerX = i;

                if (Math.Abs(_checkerX - newCheckerX) > 0.1f)
                {
                    _checkerX = newCheckerX;
                    CheckC.X = CheckerX;
                    CheckC.Y = 4;
                    Invalidate();
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
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
            if (CheckC.Width != 13)
            {
                CheckC.Width = 13;
                Invalidate();
            }

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

            // Initialize cached values if needed
            if (_mainRect.Width != Width - 1 || _mainRect.Height != Height - 1)
            {
                _mainRect = new Rectangle(0, 0, Width - 1, Height - 1);
                _max = Width - 17;
            }

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            CheckC.X = CheckerX;

            // Calculate alpha value - optimized to avoid redundant calculations
            float val = (float)(CheckC.X / _max);
            if (val < 0f) val = 0f;
            if (val > 1f) val = 1f;
            if (CheckC.X <= _min) val = 0f;
            if (CheckC.X >= _max) val = 1f;

            int alpha = (int)(255f * val);

            // Skip painting if nothing changed (except for animations)
            if (!CanAnimate && alpha == _lastAlpha && DarkLight_Toggler == _lastDarkLightMode && Checked == _lastCheckedState) return;

            _lastAlpha = alpha;
            _lastDarkLightMode = DarkLight_Toggler;
            _lastCheckedState = Checked;

            if (!DarkLight_Toggler)
            {
                // Checked part - reuse brushes to reduce allocations
                Color accentColor = Color.FromArgb(alpha, scheme.Colors.AccentAlt);
                Color foreColor = Color.FromArgb(255 - alpha, scheme.Colors.ForeColor);
                Color checkerColor = Color.FromArgb(alpha, scheme.Colors.AccentAlt.IsDark() ? Color.Black : Color.White);

                using (SolidBrush accentBrush = new(accentColor))
                using (SolidBrush foreBrush = new(foreColor))
                using (SolidBrush checkerBrush = new(checkerColor))
                using (Pen accentPen = new(accentColor))
                using (Pen forePen = new(foreColor))
                {
                    G.FillRoundedRect(accentBrush, _mainRect, 9, true);
                    G.DrawRoundedRectBeveled(accentPen, _mainRect, 9, true);
                    G.FillEllipse(checkerBrush, CheckC);
                    G.FillEllipse(foreBrush, CheckC);
                    G.DrawRoundedRect(forePen, _mainRect, 9, true);
                }
            }
            else
            {
                // DarkLight mode - optimize LinearGradientBrush creation
                Color checkedColor1 = Color.FromArgb(alpha, scheme.Colors.Accent);
                Color checkedColor2 = Color.FromArgb(alpha, scheme.Colors.AccentAlt);
                Color nonCheckedColor1 = Color.FromArgb(255 - alpha, scheme.Colors.AccentAlt);
                Color nonCheckedColor2 = Color.FromArgb(255 - alpha, scheme.Colors.Accent);

                using (LinearGradientBrush lgbChecked = new(_mainRect, checkedColor1, checkedColor2, LinearGradientMode.ForwardDiagonal))
                using (LinearGradientBrush lgborderChecked = new(_mainRect, checkedColor1, checkedColor2, LinearGradientMode.BackwardDiagonal))
                using (LinearGradientBrush lgbNonChecked = new(_mainRect, nonCheckedColor1, nonCheckedColor2, LinearGradientMode.BackwardDiagonal))
                using (LinearGradientBrush lgborderNonChecked = new(_mainRect, nonCheckedColor1, nonCheckedColor2, LinearGradientMode.ForwardDiagonal))
                {
                    G.FillRoundedRect(lgbChecked, _mainRect, 9, true);
                    G.FillRoundedRect(lgbNonChecked, _mainRect, 9, true);

                    // Optimize bitmap operations
                    Bitmap baseBitmap = scheme.Colors.Line(parentLevel).IsDark() ? Resources.darkmode_light : Resources.darkmode_dark;
                    Bitmap overlayBitmap = scheme.Colors.Line(parentLevel).IsDark() ? Resources.lightmode_light : Resources.lightmode_dark;

                    if (!Checked)
                    {
                        baseBitmap = scheme.Colors.AccentAlt.IsDark() ? Resources.darkmode_light : Resources.darkmode_dark;
                        overlayBitmap = scheme.Colors.AccentAlt.IsDark() ? Resources.lightmode_light : Resources.lightmode_dark;
                    }

                    using (Bitmap b0 = baseBitmap.Fade(val))
                    using (Bitmap b1 = overlayBitmap.Fade(1f - val))
                    {
                        G.DrawImage(b0, CheckC);
                        G.DrawImage(b1, CheckC);
                    }

                    using (Pen P = new(lgborderChecked)) { G.DrawRoundedRectBeveled(P, _mainRect, 9, true); }
                    using (Pen P = new(lgborderNonChecked)) { G.DrawRoundedRectBeveled(P, _mainRect, 9, true); }
                }
            }

            base.OnPaint(e);
        }
    }
}