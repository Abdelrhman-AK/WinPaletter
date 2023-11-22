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
            Timer1 = new Timer() { Enabled = false, Interval = 1 };
            Timer2 = new Timer() { Enabled = false, Interval = 1 };

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Size = new Size(190, 27);
            DrawMode = DrawMode.OwnerDrawVariable;
            ItemHeight = 20;

            ForeColor = Color.White;
            DropDownStyle = ComboBoxStyle.DropDownList;
            Font = new Font("Segoe UI", 9f);
            MouseWheel += ComboBox_MouseWheel;
            MouseDown += ComboBox_MouseDown;
            MouseUp += ComboBox_Click;
            HandleCreated += ComboBox_HandleCreated;
            HandleDestroyed += ComboBox_HandleDestroyed;
            DropDown += ComboBox_DropDown;
            DropDownClosed += ComboBox_DropDownClosed;
            DrawItem += ComboBox_DrawItem;
            Timer1.Tick += Timer1_Tick;
            Timer2.Tick += Timer2_Tick;
        }

        #region Variables

        private readonly TextureBrush Noise = new(Properties.Resources.GaussianBlur.Fade(0.4d));
        private readonly TextureBrush Noise2 = new(Properties.Resources.GaussianBlur.Fade(0.9d));

        private bool _Shown = false;

        private MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Voids

        protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Graphics G)
        {
            var points = new List<Point>() { FirstPoint, SecondPoint, ThirdPoint };
            using (SolidBrush br = new(Clr))
            {
                G.FillPolygon(br, points.ToArray());
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cpar = base.CreateParams;
                if (!DesignMode)
                {
                    cpar.ExStyle |= 0x20;
                    return cpar;
                }
                else
                {
                    return cpar;
                }
            }
        }

        #endregion

        #region Events

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) { this.DroppedDown = !this.DroppedDown; }
            base.OnKeyDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            _Shown = true;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            _Shown = true;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void ComboBox_MouseWheel(object sender, MouseEventArgs e)
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
            catch
            {
            }
        }

        private void ComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            State = MouseState.Down;
            _Shown = true;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void ComboBox_Click(object sender, EventArgs e)
        {
            State = MouseState.Over;
            _Shown = true;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void ComboBox_HandleCreated(object sender, EventArgs e)
        {
            alpha = 0;
            alpha2 = 0;
            try
            {
                if (!DesignMode)
                {
                    if (Parent is not null)
                        Parent.BackColorChanged += Invalidator;
                    BackColorChanged += Invalidator;
                }
            }
            catch
            {
            }
        }

        private void ComboBox_HandleDestroyed(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    if (Parent is not null)
                        Parent.BackColorChanged -= Invalidator;
                    BackColorChanged -= Invalidator;
                }
            }
            catch
            {
            }
        }

        public void Invalidator(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ComboBox_DropDown(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Timer2.Enabled = true;
                Timer2.Start();
            }
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Timer2.Enabled = true;
                Timer2.Start();
            }
        }

        #endregion

        #region Animator

        private int alpha, alpha2;
        private readonly int Factor = 20;
        private Timer Timer1, Timer2;

        private void Timer1_Tick(object sender, EventArgs e)
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
                        Timer1.Enabled = false;
                        Timer1.Stop();
                    }

                    if (_Shown)
                    {
                        System.Threading.Thread.Sleep(1);
                        Invalidate();
                    }
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
                        Timer1.Enabled = false;
                        Timer1.Stop();
                    }

                    if (_Shown)
                    {
                        System.Threading.Thread.Sleep(1);
                        Invalidate();
                    }
                }
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {

                if (DroppedDown)
                {
                    if (alpha2 + Factor <= 255)
                    {
                        alpha2 += Factor;
                    }
                    else if (alpha2 + Factor > 255)
                    {
                        alpha2 = 255;
                        Timer2.Enabled = false;
                        Timer2.Stop();
                    }

                    if (_Shown)
                    {
                        System.Threading.Thread.Sleep(1);
                        Invalidate();
                    }
                }

                if (!DroppedDown)
                {
                    if (alpha2 - Factor >= 0)
                    {
                        alpha2 -= Factor;
                    }
                    else if (alpha2 - Factor < 0)
                    {
                        alpha2 = 0;
                        Timer2.Enabled = false;
                        Timer2.Stop();
                    }

                    if (_Shown)
                    {
                        System.Threading.Thread.Sleep(1);
                        Invalidate();
                    }
                }
            }
        }

        #endregion

        public void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Rectangle Rect_Fix1 = new(e.Bounds.X - 1, e.Bounds.Y - 1, e.Bounds.Width + 2, e.Bounds.Height + 2);
            Rectangle Rect_Fix2 = new(e.Bounds.X - 3, e.Bounds.Y - 3, e.Bounds.Width + 6, e.Bounds.Height + 6);

            if (((e.State & DrawItemState.Focus) == DrawItemState.Focus) || ((e.State & DrawItemState.Selected) == DrawItemState.Selected) || ((e.State & DrawItemState.HotLight) == DrawItemState.HotLight))
            {
                e.DrawBackground();
                G.FillRectangle(scheme.Brushes.Back, Rect_Fix1);
            }
            else
            {
                G.FillRectangle(scheme.Brushes.Back, Rect_Fix2);
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                G.FillRectangle(scheme.Brushes.Line_Checked, e.Bounds);
                G.FillRectangle(Noise2, e.Bounds);
            }

            if (e.Index >= 0)
            {
                var Rect = e.Bounds; Rect.X += 2; Rect.Y += 1; Rect.Width -= 2;

                using (StringFormat sf = new() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
                {
                    using (SolidBrush br = new(ForeColor))
                    {
                        G.DrawString(GetItemText(Items[e.Index]), e.Font, br, Rect, sf);
                        Invalidate();
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;
            DoubleBuffered = true;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Rectangle OuterRect = new(0, 0, Width - 1, Height - 1);
            Rectangle InnerRect = new(1, 1, Width - 3, Height - 3);
            Rectangle TextRect = new(5, 0, Width - 1, Height - 1);

            Color FadeInColor = Color.FromArgb(alpha, scheme.Colors.Line_CheckedHover);
            Color FadeOutColor = Color.FromArgb(255 - alpha, scheme.Colors.Line);

            G.FillRoundedRect(scheme.Brushes.Back, InnerRect);

            using (SolidBrush br = new(Color.FromArgb(alpha, scheme.Colors.Back_Checked_Hover))) { G.FillRoundedRect(br, OuterRect); }

            G.FillRoundedRect(Noise, InnerRect);

            using (Pen P = new(FadeInColor)) { G.DrawRoundedRect_LikeW11(P, OuterRect); }

            using (Pen P = new(FadeOutColor)) { G.DrawRoundedRect_LikeW11(P, InnerRect); }

            int ArrowHeight = 4;
            int Arrow_Y_1 = (int)Math.Round((Height - ArrowHeight) / 2d - 1d);
            int Arrow_Y_2 = Arrow_Y_1 + ArrowHeight;

            Point point_TopRight = new(Width - 18, Arrow_Y_1);
            Point point_TopLeft = new(Width - 10, Arrow_Y_1);

            Point point_BottomRight = new(Width - 18, Arrow_Y_2);
            Point point_BottomLeft = new(Width - 10, Arrow_Y_2);

            Point point_CenterBottom = new(Width - 14, Arrow_Y_2);
            Point point_CenterBottomFixer = new(point_CenterBottom.X, point_CenterBottom.Y + 1);

            Point point_CenterTop = new(Width - 14, Arrow_Y_1);
            Point point_CenterTopFixer = new(point_CenterTop.X, point_CenterTop.Y + 1);

            if (Focused) { G.DrawRoundedRect(scheme.Pens.Line_CheckedHover, OuterRect); }

            using (Pen P = new(Color.FromArgb(255 - alpha2, !Focused ? ForeColor : FadeInColor), 2f))
            {
                G.DrawLine(P, point_TopRight, point_CenterBottom);
                G.DrawLine(P, point_CenterBottom, point_TopLeft);
                G.DrawLine(P, point_CenterBottomFixer, point_CenterBottom);
            }

            using (Pen P = new(Color.FromArgb(alpha2, FadeInColor), 2f))
            {
                G.DrawLine(P, point_BottomRight, point_CenterTop);
                G.DrawLine(P, point_CenterTop, point_BottomLeft);
                G.DrawLine(P, point_CenterTopFixer, point_CenterTop);
            }

            using (StringFormat sf = new() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
            {
                using (SolidBrush br = new(ForeColor))
                {
                    G.DrawString((SelectedItem ?? "").ToString(), Font, br, TextRect, sf);
                }
            }
        }
    }
}