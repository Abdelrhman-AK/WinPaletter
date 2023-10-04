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
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            Size = new Size(190, 27);
            DrawMode = DrawMode.OwnerDrawVariable;
            ItemHeight = 20;

            if (My.Env.Style.DarkMode)
                BackColor = Color.FromArgb(55, 55, 55);
            else
                BackColor = Color.FromArgb(225, 225, 225);
            ForeColor = Color.White;
            DropDownStyle = ComboBoxStyle.DropDownList;
            Font = new Font("Segoe UI", 9f);
            DoubleBuffered = true;
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

        private readonly TextureBrush Noise = new TextureBrush(Properties.Resources.GaussianBlur.Fade(0.3d));
        private bool _Shown = false;

        private MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }


        #endregion

        #region Subs

        protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Graphics G)
        {
            var points = new List<Point>() { FirstPoint, SecondPoint, ThirdPoint };
            using (var br = new SolidBrush(Clr))
            {
                G.FillPolygon(br, points.ToArray());
            }
        }

        #endregion

        #region Events

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
            BackColor = My.Env.Style.Colors.Back;
            e.DrawBackground();

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;

            if (base.BackColor.IsDark())
            {
                if (ForeColor != Color.White)
                    ForeColor = Color.White;
            }
            else if (ForeColor != Color.Black)
                ForeColor = Color.Black;

            using (var br = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(br, new Rectangle(e.Bounds.X - 2, e.Bounds.Y - 2, e.Bounds.Width + 4, e.Bounds.Height + 4));
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (var br = new SolidBrush(My.Env.Style.Colors.Border_Checked_Hover))
                {
                    e.Graphics.FillRectangle(br, e.Bounds);
                }
            }

            var Rect = e.Bounds;
            Rect.X += 2;
            Rect.Width -= 2;

            if (e.Index >= 0)
            {
                using (var br = new SolidBrush(ForeColor))
                {
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, br, Rect, ContentAlignment.MiddleLeft.ToStringFormat());
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;
            DoubleBuffered = true;

            var OuterRect = new Rectangle(0, 0, Width - 1, Height - 1);
            var InnerRect = new Rectangle(1, 1, Width - 3, Height - 3);
            var TextRect = new Rectangle(5, 0, Width - 1, Height - 1);

            var FadeInColor = Color.FromArgb(alpha, My.Env.Style.Colors.Border_Checked_Hover);
            var FadeOutColor = Color.FromArgb(255 - alpha, My.Env.Style.Colors.Border);

            G.Clear(this.GetParentColor());

            using (var br = new SolidBrush(My.Env.Style.Colors.Back))
            {
                G.FillRoundedRect(br, InnerRect);
            }
            using (var br = new SolidBrush(Color.FromArgb(alpha, My.Env.Style.Colors.Back_Checked)))
            {
                G.FillRoundedRect(br, OuterRect);
            }
            G.FillRoundedRect(Noise, InnerRect);

            using (var P = new Pen(FadeInColor))
            {
                G.DrawRoundedRect_LikeW11(P, OuterRect);
            }
            using (var P = new Pen(FadeOutColor))
            {
                G.DrawRoundedRect_LikeW11(P, InnerRect);
            }

            using (var br = new SolidBrush(Color.FromArgb(alpha2, My.Env.Style.Colors.Back_Checked)))
            {
                G.FillRoundedRect(br, OuterRect);
            }
            using (var P = new Pen(Color.FromArgb(alpha2, My.Env.Style.Colors.Border_Checked_Hover)))
            {
                G.DrawRoundedRect_LikeW11(P, OuterRect);
            }

            int ArrowHeight = 4;
            int Arrow_Y_1 = (int)Math.Round((Height - ArrowHeight) / 2d - 1d);
            int Arrow_Y_2 = Arrow_Y_1 + ArrowHeight;

            if (Focused & State == MouseState.None)
            {
                using (var P = new Pen(Color.FromArgb(255, FadeInColor)))
                {
                    G.DrawRoundedRect(P, InnerRect);
                    G.DrawLine(P, new Point(Width - 18, Arrow_Y_1), new Point(Width - 14, Arrow_Y_2));
                    G.DrawLine(P, new Point(Width - 14, Arrow_Y_2), new Point(Width - 10, Arrow_Y_1));
                    G.DrawLine(P, new Point(Width - 14, Arrow_Y_2 + 1), new Point(Width - 14, Arrow_Y_2));
                }
            }
            else
            {
                using (var P = new Pen(Color.FromArgb(255 - alpha, ForeColor), 2f))
                {
                    G.DrawLine(P, new Point(Width - 18, Arrow_Y_1), new Point(Width - 14, Arrow_Y_2));
                    G.DrawLine(P, new Point(Width - 14, Arrow_Y_2), new Point(Width - 10, Arrow_Y_1));
                }

                using (var P = new Pen(Color.FromArgb(255 - alpha, ForeColor)))
                {
                    G.DrawLine(P, new Point(Width - 14, Arrow_Y_2 + 1), new Point(Width - 14, Arrow_Y_2));
                }

                if (!DroppedDown)
                {

                    using (var P = new Pen(FadeInColor, 2f))
                    {
                        G.DrawLine(P, new Point(Width - 18, Arrow_Y_1), new Point(Width - 14, Arrow_Y_2));
                        G.DrawLine(P, new Point(Width - 14, Arrow_Y_2), new Point(Width - 10, Arrow_Y_1));
                    }

                    using (var P = new Pen(FadeInColor))
                    {
                        G.DrawLine(new Pen(FadeInColor), new Point(Width - 14, Arrow_Y_2 + 1), new Point(Width - 14, Arrow_Y_2));
                    }
                }

                else
                {
                    using (var P = new Pen(FadeInColor))
                    {
                        G.DrawLine(P, new Point(Width - 14, Arrow_Y_1), new Point(Width - 14, Arrow_Y_1 + 1));
                    }

                    using (var P = new Pen(FadeInColor, 2f))
                    {
                        G.DrawLine(P, new Point(Width - 18, Arrow_Y_2), new Point(Width - 14, Arrow_Y_1));
                        G.DrawLine(P, new Point(Width - 14, Arrow_Y_1), new Point(Width - 10, Arrow_Y_2));
                    }
                }

            }

            using (var br = new SolidBrush(ForeColor))
            {
                G.DrawString(Text, Font, br, TextRect, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near });
            }
        }

    }

}