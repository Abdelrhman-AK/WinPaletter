using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Management.Instrumentation;
using System.Windows.Forms;
using static WinPaletter.TypesExtensions.GraphicsExtensions;

namespace WinPaletter.UI.WP
{
    [Description("Themed GroupBox")]
    public class GroupBox : Panel
    {
        private TextureBrush _patternBrush;
        private int _cachedPatternVal = -1;
        private bool _cachedDarkMode = false;
        private int parentLevel = 0;
        private RoundedCorners corners = RoundedCorners.All;

        public GroupBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);

            BackColor = Color.Transparent;
            Text = string.Empty;
        }

        private bool useDecorationPattern;
        public bool UseDecorationPattern
        {
            get => useDecorationPattern;
            set
            {
                useDecorationPattern = value;
                UpdatePattern();
                Invalidate();
            }
        }

        private bool useSharpStyle = false;
        [Browsable(false)]
        public bool UseSharpStyle
        {
            get => useSharpStyle;
            set 
            { 
                useSharpStyle = value;
                UpdateRegion();
                Invalidate(); 
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            parentLevel = this.Level();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Listen to global theme/pattern changes
            Config.PatternChanged += OnGlobalStyleChanged;

            OnGlobalStyleChanged();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Config.PatternChanged -= OnGlobalStyleChanged;

            base.OnHandleDestroyed(e); 
        }

        public void UpdatePattern(int? patternNO = null)
        {
            patternNO ??= Program.Style.Pattern;
            int val = (!DesignMode && useDecorationPattern) ? (int)patternNO : 0;

            if (val == _cachedPatternVal && _cachedDarkMode == Program.Style.DarkMode) return;

            _cachedPatternVal = val;
            _cachedDarkMode = Program.Style.DarkMode;

            _patternBrush?.Dispose();
            _patternBrush = null;

            if (val == 0) return;

            Bitmap src = val switch
            {
                1 => Assets.Store.Pattern1,
                2 => Assets.Store.Pattern2,
                3 => Assets.Store.Pattern3,
                4 => Assets.Store.Pattern4,
                5 => Assets.Store.Pattern5,
                6 => Assets.Store.Pattern6,
                7 => Assets.Store.Pattern7,
                8 => Assets.Store.Pattern8,
                9 => Assets.Store.Pattern9,
                10 => Assets.Store.Pattern10,
                11 => Assets.Store.Pattern11,
                12 => Assets.Store.Pattern12,
                13 => Assets.Store.Pattern13,
                14 => Assets.Store.Pattern14,
                15 => Assets.Store.Pattern15,
                16 => Assets.Store.Pattern16,
                17 => Assets.Store.Pattern17,
                _ => null
            };

            if (src == null) return;

            Bitmap processed = src;
            if (!Program.Style.DarkMode)
                processed = processed.Contrast(0.5f)?.Invert()?.Fade(0.8f);

            if (processed != null)
                _patternBrush = new TextureBrush(processed, WrapMode.Tile);
        }

        /// <summary>
        /// Handle global style changes
        /// </summary>
        private void OnGlobalStyleChanged()
        {
            if (UseDecorationPattern) UpdatePattern(Program.Style.Pattern);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            if (IsDisposed) return;

            Rectangle rect = new(0, 0, Width, Height);

            Region?.Dispose();

            if (DesignMode || useSharpStyle)
            {
                Region = new Region(rect);
                return;
            }

            corners = this.UndockedCorners();

            using (GraphicsPath path = rect.Round(corners: corners)) Region = new(path);
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

                ParentColor = Enabled ? ParentColor : Program.Style.Schemes.Disabled.Colors.Back(parentLevel);

                BackColor = ParentColor.CB(ParentColor.IsDark() ? 0.04f : -0.05f);

                using (SolidBrush br = new(BackColor))
                using (Pen P = new(ParentColor.CB(ParentColor.IsDark() ? 0.09f : -0.09f)))
                {
                    if (!useSharpStyle)
                    {
                        G.FillRoundedRect(br, Rect, corners: corners);
                        if (_patternBrush != null) G.FillRoundedRect(_patternBrush, Rect);
                        G.DrawRoundedRect(P, Rect, corners: corners);
                    }
                    else
                    {
                        G.FillRectangle(br, Rect);
                        if (_patternBrush != null) G.FillRectangle(_patternBrush, Rect);
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
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _patternBrush?.Dispose();

            base.Dispose(disposing);
        }
    }
}