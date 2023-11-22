using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Themed RadioButton for WinPaletter UI")]
    [DefaultEvent("CheckedChanged")]
    public class RadioButton : Control
    {

        public RadioButton()
        {
            Timer1 = new Timer() { Enabled = false, Interval = 1 };
            Timer2 = new Timer() { Enabled = false, Interval = 1 };
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9f);
            ForeColor = Color.White;
            MouseEnter += RadioButton_MouseEnter;
            MouseLeave += RadioButton_MouseLeave;
            HandleCreated += RadioButton_HandleCreated;
            HandleDestroyed += RadioButton_HandleDestroyed;
            Timer1.Tick += Timer1_Tick;
            Timer2.Tick += Timer2_Tick;
        }

        #region Variables

        private bool AnimateOnClick = false;
        private SizeF SZ1;

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

                    if (_Checked)
                    {
                        UncheckOthersOnChecked();
                    }

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
            Checked = true;
            State = MouseState.Down;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            AnimateOnClick = true;
            Checked = true;
            State = MouseState.Down;
            Timer2.Enabled = true;
            Timer2.Start();
            Invalidate();
            base.OnMouseClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void RadioButton_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void RadioButton_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Timer1.Enabled = true;
            Timer1.Start();
            Invalidate();
        }

        private void RadioButton_HandleCreated(object sender, EventArgs e)
        {

            try
            {
                if (!DesignMode)
                {
                    FindForm().Shown += Showed;
                    Parent.BackColorChanged += RefreshColorPalette;
                    Parent.VisibleChanged += RefreshColorPalette;
                    Parent.EnabledChanged += RefreshColorPalette;
                    VisibleChanged += RefreshColorPalette;
                    EnabledChanged += RefreshColorPalette;
                }
            }
            catch
            {
            }

            try
            {
                alpha = 0;
                alpha2 = Checked ? 255 : 0;
            }
            catch
            {
            }
        }

        private void RadioButton_HandleDestroyed(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    FindForm().Shown -= Showed;
                    Parent.BackColorChanged -= RefreshColorPalette;
                    Parent.VisibleChanged -= RefreshColorPalette;
                    Parent.EnabledChanged -= RefreshColorPalette;
                    VisibleChanged -= RefreshColorPalette;
                    EnabledChanged -= RefreshColorPalette;
                }
            }
            catch
            {
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

        #region Voids/Functions

        private void UncheckOthersOnChecked()
        {
            if (Parent is null)
                return;

            foreach (Control C in Parent.Controls)
            {
                if (C != this && C is RadioButton)
                {
                    ((RadioButton)C).Checked = false;
                }
            }
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
                    Invalidate();
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
                    Invalidate();
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
                    Invalidate();
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
                    Invalidate();
                }
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                var G = e.Graphics;
                if (Parent is null)
                    return;
                BackColor = Parent.BackColor;
                var clr = Program.Style.Schemes.Main.Colors.AccentAlt;

                G = e.Graphics;
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;
                DoubleBuffered = true;

                // ################################################################################# Customizer
                SZ1 = G.MeasureString(Text, Font);

                var format = new StringFormat();
                var OuterCircle = new Rectangle(3, 4, Height - 8, Height - 8);
                var InnerCircle = new Rectangle(4, 5, Height - 10, Height - 10);
                var CheckCircle = new Rectangle(7, 8, Height - 16, Height - 16);
                var TextRect = new Rectangle(Height - 1, (int)((long)Math.Round(Height - SZ1.Height) / 2L + 1L), Width - OuterCircle.Width, Height - 1);
                bool RTL = (int)RightToLeft == 1;

                if (RTL)
                {
                    format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                    OuterCircle.X = Width - OuterCircle.X - OuterCircle.Width;
                    InnerCircle.X = Width - InnerCircle.X - InnerCircle.Width;
                    CheckCircle.X = Width - CheckCircle.X - CheckCircle.Width;
                    TextRect.Width -= OuterCircle.Width + 13;
                }

                #region Colors System
                var HoverCircle_Color = Color.FromArgb(alpha2, Program.Style.Schemes.Main.Colors.Back_Checked);
                var HoverCheckedCircle_Color = Color.FromArgb(alpha, Program.Style.Schemes.Main.Colors.Line_CheckedHover);
                var CheckCircle_Color = Color.FromArgb(alpha2, Program.Style.Schemes.Main.Colors.AccentAlt);
                var NonHoverCircle_Color = Program.Style.Schemes.Main.Colors.Back_Hover;
                var BackCircle_Color = Program.Style.Schemes.Main.Colors.Back;
                var ParentColor = this.GetParentColor();
                #endregion
                // #################################################################################

                G.Clear(ParentColor);
                using (var br = new SolidBrush(BackCircle_Color))
                {
                    G.FillEllipse(br, OuterCircle);
                }

                if (Checked)
                {
                    using (var br = new SolidBrush(HoverCircle_Color))
                    {
                        G.FillEllipse(br, OuterCircle);
                    }
                    using (var br = new SolidBrush(CheckCircle_Color))
                    {
                        G.FillEllipse(br, CheckCircle);
                    }
                    using (var P = new Pen(HoverCheckedCircle_Color))
                    {
                        G.DrawEllipse(P, OuterCircle);
                    }
                }
                else
                {
                    using (var br = new SolidBrush(HoverCircle_Color))
                    {
                        G.FillEllipse(br, OuterCircle);
                    }
                    using (var br = new SolidBrush(CheckCircle_Color))
                    {
                        G.FillEllipse(br, CheckCircle);
                    }
                    using (var P = new Pen(Color.FromArgb(255 - alpha, NonHoverCircle_Color)))
                    {
                        G.DrawEllipse(P, InnerCircle);
                    }
                    using (var P = new Pen(Color.FromArgb(alpha, clr)))
                    {
                        G.DrawEllipse(P, OuterCircle);
                    }
                }

                #region Strings
                if (Checked)
                {
                    using (var br = new SolidBrush(Color.FromArgb(255 - alpha, ForeColor)))
                    {
                        G.DrawString(Text, Font, br, TextRect, format);
                    }
                    using (var br = new SolidBrush(Color.FromArgb(alpha, CheckCircle_Color)))
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
                    #endregion
                }
            }
            catch
            {

            }
        }

    }

}