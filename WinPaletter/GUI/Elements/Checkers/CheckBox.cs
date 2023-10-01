using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Themed CheckBox for WinPaletter UI")]
    [DefaultEvent("CheckedChanged")]
    public class CheckBox : Control
    {


        public CheckBox()
        {
            Timer1 = new Timer() { Enabled = false, Interval = 1 };
            Timer2 = new Timer() { Enabled = false, Interval = 1 };
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9f);
            ForeColor = Color.White;
            MouseEnter += CheckBox_MouseEnter;
            MouseLeave += CheckBox_MouseLeave;
            HandleCreated += Checkbox_HandleCreated;
            HandleDestroyed += CheckBox_HandleDestroyed;
            Timer1.Tick += Timer1_Tick;
            Timer2.Tick += Timer2_Tick;
        }

        #region Variables

        private readonly int Radius = 5;
        private bool AnimateOnClick = false;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties

        private bool _Checked;
        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                try
                {
                    _Checked = value;
                    CheckedChanged?.Invoke(this);
                    if (AnimateOnClick)
                    {
                        Timer2.Enabled = true;
                        Timer2.Start();
                    }
                    else
                    {
                        alpha2 = Checked ? 255 : 0;
                    }
                    Refresh();
                }
                catch
                {
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; }
        #endregion

        #region Events

        public event CheckedChangedEventHandler CheckedChanged;

        public delegate void CheckedChangedEventHandler(object sender);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = MouseState.Down;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            AnimateOnClick = true;
            Checked = !Checked;
            State = MouseState.Down;
            Timer2.Enabled = true;
            Timer2.Start();
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void CheckBox_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void CheckBox_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void Checkbox_HandleCreated(object sender, EventArgs e)
        {
            try
            {
                alpha = DesignMode ? 255 : 0;
                alpha2 = Checked ? 255 : 0;

                if (!DesignMode)
                {
                    try
                    {
                        FindForm().Shown += Showed;
                        Parent.BackColorChanged += RefreshColorPalette;
                        Parent.VisibleChanged += RefreshColorPalette;
                        Parent.EnabledChanged += RefreshColorPalette;
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }

        }

        private void CheckBox_HandleDestroyed(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                try
                {
                    FindForm().Shown -= Showed;
                    Parent.BackColorChanged -= RefreshColorPalette;
                    Parent.VisibleChanged -= RefreshColorPalette;
                    Parent.EnabledChanged -= RefreshColorPalette;
                }
                catch
                {
                }
            }
        }

        public void Showed(object sender, EventArgs e)
        {
            Invalidate();
        }

        public void RefreshColorPalette(object sender, EventArgs e)
        {
            Invalidate();
        }

        #endregion

        #region Animator

        private int alpha, alpha2;
        private readonly int Factor = 25;
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

                    System.Threading.Thread.Sleep(1);
                    Refresh();
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

                    System.Threading.Thread.Sleep(1);
                    Refresh();
                }
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {

                if (Checked)
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
                        AnimateOnClick = false;
                    }

                    System.Threading.Thread.Sleep(1);
                    Refresh();
                }

                if (!Checked)
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
                        AnimateOnClick = false;
                    }

                    System.Threading.Thread.Sleep(1);
                    Refresh();

                }
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (Parent is null)
                    return;
                BackColor = Parent.BackColor;

                var G = e.Graphics;
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;
                DoubleBuffered = true;

                // ################################################################################# Customizer
                var format = new StringFormat();

                var SZ1 = G.MeasureString(Text, Font);
                var PT1 = new PointF(Height - 1, (long)Math.Round(Height - SZ1.Height) / 2L + 1L);

                var OuterCheckRect = new Rectangle(3, 4, Height - 8, Height - 8);
                var InnerCheckRect = new Rectangle(4, 5, Height - 10, Height - 10);
                var TextRect = new Rectangle(Height - 1, (int)((long)Math.Round(Height - SZ1.Height) / 2L + 1L), Width - InnerCheckRect.Width, Height - 1);

                #region Colors System
                Color HoverRect_Color;
                Color HoverCheckedRect_Color;
                Color CheckRect_Color;
                Color NonHoverRect_Color;
                Color BackRect_Color;
                var ParentColor = this.GetParentColor();

                if (Enabled)
                {
                    HoverRect_Color = Color.FromArgb(alpha2, My.Env.Style.Colors.Back_Checked);
                    HoverCheckedRect_Color = Color.FromArgb(alpha, My.Env.Style.Colors.Border_Checked_Hover);
                    CheckRect_Color = Color.FromArgb(alpha2, My.Env.Style.Colors.Core);
                    NonHoverRect_Color = My.Env.Style.Colors.Border;
                    BackRect_Color = My.Env.Style.Colors.Back;
                }
                else
                {
                    HoverRect_Color = Color.FromArgb(alpha2, My.Env.Style.Disabled_Colors.Back_Checked);
                    HoverCheckedRect_Color = Color.FromArgb(alpha, My.Env.Style.Disabled_Colors.Border_Checked_Hover);
                    CheckRect_Color = Color.FromArgb(alpha2, My.Env.Style.Disabled_Colors.Core);
                    NonHoverRect_Color = My.Env.Style.Disabled_Colors.Border;
                    BackRect_Color = My.Env.Style.Disabled_Colors.Back;
                }

                #endregion

                bool RTL = (int)RightToLeft == 1;

                if (RTL)
                {
                    format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                    OuterCheckRect.X = Width - OuterCheckRect.X - OuterCheckRect.Width;
                    InnerCheckRect.X = Width - InnerCheckRect.X - InnerCheckRect.Width;
                    TextRect.Width = Width - InnerCheckRect.Width - 10;
                    TextRect.X = 0;
                }

                #region Check Sign x,y system
                int x1_Left = InnerCheckRect.X + 3;
                int y1_Left = (int)Math.Round(0.8d * InnerCheckRect.Height);
                int x2_Left = x1_Left;
                int y2_Left = InnerCheckRect.Y + InnerCheckRect.Height - 3;

                int x1_Right = x2_Left;
                int y1_Right = y2_Left;
                int x2_Right = InnerCheckRect.Right - 2;
                int y2_Right = y1_Left - 3;

                using (var CheckSignPen = new Pen(CheckRect_Color, 1.8f))
                {
                    #endregion
                    // #################################################################################

                    G.Clear(ParentColor);

                    using (var br = new SolidBrush(My.Env.Style.Colors.Back))
                    {
                        G.FillRoundedRect(br, InnerCheckRect, Radius);
                    }

                    if (_Checked)
                    {
                        using (var br = new SolidBrush(HoverRect_Color))
                        {
                            G.FillRoundedRect(br, InnerCheckRect, Radius);
                        }
                        using (var br = new SolidBrush(Color.FromArgb(alpha, HoverRect_Color)))
                        {
                            G.FillRoundedRect(br, OuterCheckRect, Radius);
                        }

                        using (var P = new Pen(Color.FromArgb(255 - alpha, HoverCheckedRect_Color)))
                        {
                            G.DrawRoundedRect(P, InnerCheckRect, Radius);
                        }
                        using (var P = new Pen(Color.FromArgb(alpha, HoverCheckedRect_Color)))
                        {
                            G.DrawRoundedRect(P, OuterCheckRect, Radius);
                        }

                        G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left);
                        G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right);
                    }
                    else
                    {
                        using (var br = new SolidBrush(HoverRect_Color))
                        {
                            G.FillRoundedRect(br, OuterCheckRect, Radius);
                        }
                        using (var P = new Pen(HoverCheckedRect_Color))
                        {
                            G.DrawRoundedRect(P, OuterCheckRect, Radius);
                        }

                        G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left);
                        G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right);

                        using (var P = new Pen(Color.FromArgb(255 - alpha, My.Env.Style.Colors.Back_Hover)))
                        {
                            G.DrawRoundedRect(P, InnerCheckRect, Radius);
                        }
                    }

                    if (Checked)
                    {
                        using (var br = new SolidBrush(Color.FromArgb(255 - alpha, ForeColor)))
                        {
                            G.DrawString(Text, Font, br, TextRect, format);
                        }
                        using (var br = new SolidBrush(Color.FromArgb(alpha, CheckRect_Color)))
                        {
                            G.DrawString(Text, Font, br, TextRect, format);
                        }
                    }
                    else
                    {
                        using (var br = new SolidBrush(ForeColor))
                        {
                            G.DrawString(Text, Font, br, TextRect, format);
                        }
                    }
                }
            }
            catch
            {
            }
        }

    }

}