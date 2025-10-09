using FluentTransitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Properties;

namespace WinPaletter.UI.WP
{
    [Description("Themed ComboBox for WinPaletter UI")]
    public class ComboBox : System.Windows.Forms.ComboBox
    {
        public ComboBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
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

        #endregion

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private static readonly TextureBrush Noise = new(Resources.Noise.Fade(0.3f));
        private static readonly TextureBrush Noise2 = new(Resources.Noise.Fade(0.9f));

        #endregion

        #region Methods
        protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Graphics G)
        {
            List<Point> points = [FirstPoint, SecondPoint, ThirdPoint];
            using (SolidBrush br = new(Clr))
            {
                G.FillPolygon(br, points.ToArray());
            }
        }
        #endregion

        #region Events/Overrides

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) { DroppedDown = !DroppedDown; }

            base.OnKeyDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (CanAnimate) { Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (CanAnimate) { Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (CanAnimate) { Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
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

            base.OnMouseWheel(e);
        }

        protected override void OnDropDown(EventArgs e)
        {
            if (CanAnimate) { Transition.With(this, nameof(alpha2), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha2 = 0; }

            base.OnDropDown(e);
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            if (CanAnimate) { Transition.With(this, nameof(alpha2), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha2 = 255; }

            base.OnDropDownClosed(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
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
            set { _alpha = value; Invalidate(); }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha2
        {
            get => _alpha2;
            set { _alpha2 = value; Invalidate(); }
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
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

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
                using (LinearGradientBrush br = new(e.Bounds, scheme.Colors.Line_Checked, scheme.Colors.Line_Checked_Hover, LinearGradientMode.ForwardDiagonal))
                    G.FillRectangle(br, e.Bounds);
                G.FillRectangle(Noise2, e.Bounds);
            }

            if (e.Index >= 0)
            {
                Rectangle Rect = e.Bounds; Rect.X += 2; Rect.Y += 1; Rect.Width -= 2;

                using (StringFormat sf = new() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
                using (SolidBrush br = new(ForeColor))
                {
                    G.DrawString(GetItemText(Items[e.Index]), e.Font, br, Rect, sf);
                    Invalidate();
                }
            }

            base.OnDrawItem(e);


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Rectangle OuterRect = new(0, 0, Width - 1, Height - 1);
            Rectangle InnerRect = new(1, 1, Width - 3, Height - 3);
            Rectangle TextRect = new(5, 0, Width - 1, Height - 1);

            Color FadeInColor = Color.FromArgb(alpha, scheme.Colors.Line_Checked);
            Color FadeOutColor = Color.FromArgb(255 - alpha, scheme.Colors.Line_Hover(parentLevel + 5));

            Color BackColor0 = Program.Style.DarkMode ? scheme.Colors.Back_Hover(parentLevel) : scheme.Colors.Back(parentLevel);
            Color BackColor1 = Program.Style.DarkMode ? scheme.Colors.Back(parentLevel) : scheme.Colors.Back_Hover(parentLevel);
            Color HoverColor0 = Program.Style.DarkMode ? Color.FromArgb(alpha, scheme.Colors.Back_Checked) : Color.FromArgb(alpha, scheme.Colors.Back_Checked_Hover);
            Color HoverColor1 = Program.Style.DarkMode ? Color.FromArgb(alpha, scheme.Colors.Back_Checked_Hover) : Color.FromArgb(alpha, scheme.Colors.Back_Checked);

            using (LinearGradientBrush lgb0 = new(InnerRect, BackColor0, BackColor1, LinearGradientMode.Vertical))
            using (LinearGradientBrush lgb1 = new(OuterRect, HoverColor0, HoverColor1, LinearGradientMode.Horizontal))
            {
                G.FillRoundedRect(lgb0, InnerRect);
                G.FillRoundedRect(lgb1, OuterRect);
            }

            G.FillRoundedRect(Noise, InnerRect);

            using (Pen P = new(FadeInColor)) { G.DrawRoundedRectBeveled(P, OuterRect); }

            using (Pen P = new(FadeOutColor)) { G.DrawRoundedRectBeveled(P, InnerRect); }

            int ArrowHeight = 5;
            int Arrow_Y_1 = (int)((Height - ArrowHeight) / 2f - 1f);
            int Arrow_Y_2 = Arrow_Y_1 + ArrowHeight;

            Point point_TopRight = new(Width - 18, Arrow_Y_1);
            Point point_TopLeft = new(Width - 10, Arrow_Y_1);

            Point point_BottomRight = new(Width - 18, Arrow_Y_2);
            Point point_BottomLeft = new(Width - 10, Arrow_Y_2);

            Point point_CenterBottom = new(Width - 14, Arrow_Y_2);

            Point point_CenterTop = new(Width - 14, Arrow_Y_1);

            if (Focused) { G.DrawRoundedRect(scheme.Pens.Line_Checked, InnerRect); }

            Color Triangle1 = Color.FromArgb(255 - alpha2, !Focused ? ForeColor : scheme.Colors.Line_Checked_Hover);
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