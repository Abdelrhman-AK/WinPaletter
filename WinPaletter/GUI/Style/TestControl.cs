using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    //[ToolboxItem(false)]
    public class TestControl : Control
    {
        public TestControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Font = new("Segoe UI", 9f);
            Text = string.Empty;
        }

        public enum States
        {
            None,
            Hover,
            CheckedHover,
            Checked,
            ButtonNone,
            ButtonOver,
            ButtonDown
        }

        #region Properties
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        private Config.Scheme _scheme = Program.Style.Schemes.Main;
        public Config.Scheme Scheme
        {
            get => _scheme;
            set
            {
                _scheme = value;
                Invalidate();
            }
        }

        private States _state = States.None;
        public States State
        {
            get => _state;
            set
            {
                _state = value;
                Invalidate();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cpar = base.CreateParams;
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


        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Rectangle MainRect = new(0, 0, Width - 1, Height - 1);

            Color bkC = default; Color lC = default;

            switch (State)
            {
                case States.None:
                    bkC = Scheme.Colors.Back(parentLevel);
                    lC = Scheme.Colors.Line(parentLevel);
                    break;

                case States.Hover:
                    bkC = Scheme.Colors.Back_Hover(parentLevel);
                    lC = Scheme.Colors.Line_Hover(parentLevel);
                    break;

                case States.Checked:
                    bkC = Scheme.Colors.Back_Checked;
                    lC = Scheme.Colors.Line_Checked;
                    break;

                case States.CheckedHover:
                    bkC = Scheme.Colors.Back_Checked_Hover;
                    lC = Scheme.Colors.Line_Checked_Hover;
                    break;

                case States.ButtonNone:
                    bkC = Scheme.Colors.Button;
                    lC = LineColor(Scheme.Colors.Button);
                    break;

                case States.ButtonOver:
                    bkC = Scheme.Colors.Button_Over;
                    lC = LineColor(Scheme.Colors.Button_Over);
                    break;

                case States.ButtonDown:
                    bkC = Scheme.Colors.Button_Down;
                    lC = LineColor(Scheme.Colors.Button_Down);
                    break;
            }

            using (SolidBrush br = new(bkC)) { G.FillRoundedRect(br, MainRect); }

            using (Pen P = new(lC)) { G.DrawRoundedRect_LikeW11(P, MainRect); }

            using (SolidBrush br = new(bkC.IsDark() ? Color.White : Color.Black)) { G.DrawString(Text, Font, br, MainRect, ContentAlignment.MiddleCenter.ToStringFormat()); }
        }

        private Color LineColor(Color baseColor = default)
        {
            switch (State)
            {
                case States.ButtonNone:
                    return baseColor.CB(baseColor.IsDark() ? 0.02f : -0.04f);

                case States.ButtonOver:
                    return baseColor.CB(baseColor.IsDark() ? 0.07f : -0.1f);

                case States.ButtonDown:
                    return baseColor.CB(baseColor.IsDark() ? 0.1f : -0.08f);

                default:
                    return baseColor.CB(baseColor.IsDark() ? 0.02f : -0.04f);
            }
        }
    }
}