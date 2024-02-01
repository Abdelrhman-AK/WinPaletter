using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("Themed ComboBox for WinPaletter UI")]
    public class ComboBox : System.Windows.Forms.ComboBox
    {
        public ComboBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Size = new(190, 27);
            DrawMode = DrawMode.OwnerDrawVariable;
            ItemHeight = 20;

            ForeColor = Color.White;
            DropDownStyle = ComboBoxStyle.DropDownList;
            Font = new("Segoe UI", 9f);

            _alpha = 0; _alpha2 = 0;
        }

        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override Color BackColor { get; set; }

        private int _focusAlpha = 255;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int FocusAlpha
        {
            get => _focusAlpha;
            set
            {
                _focusAlpha = value;
                Refresh();
            }
        }
        #endregion

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private readonly TextureBrush Noise = new(Properties.Resources.Noise.Fade(0.3f));
        private readonly TextureBrush Noise2 = new(Properties.Resources.Noise.Fade(0.9f));

        private MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Methods
        protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Graphics G)
        {
            List<Point> points = new() { FirstPoint, SecondPoint, ThirdPoint };
            using (SolidBrush br = new(Clr))
            {
                G.FillPolygon(br, points.ToArray());
            }
        }
        #endregion

        #region Events/Overrides

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) { this.DroppedDown = !this.DroppedDown; }

            base.OnKeyDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = MouseState.Down;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = MouseState.None;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    if (SelectedIndex < Items.Count - 1)
                    {
                        if (e.Delta <= -240)
                            SelectedIndex += 1;
                    }
                }
                else if (SelectedIndex > 0)
                {
                    if (e.Delta >= 240)
                        SelectedIndex -= 1;
                }
            }
            catch { }

            base.OnMouseWheel(e);
        }

        protected override void OnDropDown(EventArgs e)
        {
            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha2), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha2 = 0; }

            base.OnDropDown(e);
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha2), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha2 = 255; }

            base.OnDropDownClosed(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Noise?.Dispose();
            Noise2?.Dispose();
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }

        #endregion

        #region Animator

        private int _alpha = 0;
        private int _alpha2 = 255;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Refresh(); }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha2
        {
            get => _alpha2;
            set { _alpha2 = value; Refresh(); }
        }
        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Rectangle Rect_Fix1 = new(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width + 2, e.Bounds.Height + 2);
            Rectangle Rect_Fix2 = new(e.Bounds.X - 3, e.Bounds.Y - 3, e.Bounds.Width + 6, e.Bounds.Height + 6);

            using (SolidBrush br = new(scheme.Colors.Back(parentLevel)))
            {
                if (((e.State & DrawItemState.Focus) == DrawItemState.Focus) || ((e.State & DrawItemState.Selected) == DrawItemState.Selected) || ((e.State & DrawItemState.HotLight) == DrawItemState.HotLight))
                {
                    e.DrawBackground();
                    G.FillRectangle(br, Rect_Fix1);
                }
                else
                {
                    G.FillRectangle(br, Rect_Fix2);
                }
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                G.FillRectangle(scheme.Brushes.Line_Checked, e.Bounds);
                G.FillRectangle(Noise2, e.Bounds);
            }

            if (e.Index >= 0)
            {
                Rectangle Rect = e.Bounds; Rect.X += 2; Rect.Y += 1; Rect.Width -= 2;

                using (StringFormat sf = new() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
                {
                    using (SolidBrush br = new(ForeColor))
                    {
                        G.DrawString(GetItemText(Items[e.Index]), e.Font, br, Rect, sf);
                        Invalidate();
                    }
                }
            }

            base.OnDrawItem(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Rectangle OuterRect = new(0, 0, Width - 1, Height - 1);
            Rectangle InnerRect = new(1, 1, Width - 3, Height - 3);
            Rectangle TextRect = new(5, 0, Width - 1, Height - 1);

            Color FadeInColor = Color.FromArgb(alpha, scheme.Colors.Line_Checked);
            Color FadeOutColor = Color.FromArgb(Math.Max(FocusAlpha - alpha, 0), scheme.Colors.Line(parentLevel));

            Color BackColor0 = Program.Style.DarkMode ? scheme.Colors.Back(parentLevel) : scheme.Colors.Back_Hover(parentLevel);
            Color BackColor1 = Program.Style.DarkMode ? scheme.Colors.Back_Hover(parentLevel) : scheme.Colors.Back(parentLevel);
            Color HoverColor0 = Program.Style.DarkMode ? Color.FromArgb(alpha, scheme.Colors.Back_Checked_Hover) : Color.FromArgb(alpha, scheme.Colors.Back_Checked);
            Color HoverColor1 = Program.Style.DarkMode ? Color.FromArgb(alpha, scheme.Colors.Back_Checked) : Color.FromArgb(alpha, scheme.Colors.Back_Checked_Hover);

            using (LinearGradientBrush lgb0 = new(InnerRect, BackColor0, BackColor1, LinearGradientMode.Vertical))
            using (LinearGradientBrush lgb1 = new(OuterRect, HoverColor0, HoverColor1, LinearGradientMode.Horizontal))
            {
                G.FillRoundedRect(lgb0, InnerRect);
                G.FillRoundedRect(lgb1, OuterRect);
            }

            G.FillRoundedRect(Noise, InnerRect);

            using (Pen P = new(FadeInColor)) { G.DrawRoundedRect_LikeW11(P, OuterRect); }

            using (Pen P = new(FadeOutColor)) { G.DrawRoundedRect_LikeW11(P, InnerRect); }

            int ArrowHeight = 5;
            int Arrow_Y_1 = (int)Math.Round((Height - ArrowHeight) / 2d - 1d);
            int Arrow_Y_2 = Arrow_Y_1 + ArrowHeight;

            Point point_TopRight = new(Width - 18, Arrow_Y_1);
            Point point_TopLeft = new(Width - 10, Arrow_Y_1);

            Point point_BottomRight = new(Width - 18, Arrow_Y_2);
            Point point_BottomLeft = new(Width - 10, Arrow_Y_2);

            Point point_CenterBottom = new(Width - 14, Arrow_Y_2);

            Point point_CenterTop = new(Width - 14, Arrow_Y_1);

            if (Focused) { G.DrawRoundedRect(scheme.Pens.Line_Checked, InnerRect); }

            Color Triangle1 = Color.FromArgb(Math.Max(FocusAlpha - alpha2, 0), !Focused ? ForeColor : scheme.Colors.Line_Checked_Hover);
            DrawTriangle(Triangle1, point_TopRight, point_CenterBottom, point_TopLeft, G);

            Color Triangle2 = Color.FromArgb(alpha2, scheme.Colors.Line_Checked_Hover);
            DrawTriangle(Triangle2, point_BottomRight, point_CenterTop, point_BottomLeft, G);

            using (StringFormat sf = new() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
            {
                using (SolidBrush br = new(ForeColor))
                {
                    G.DrawString((SelectedItem ?? string.Empty).ToString(), Font, br, TextRect, sf);
                }
            }

            base.OnPaint(e);
        }
    }
}