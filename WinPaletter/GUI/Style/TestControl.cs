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
            DoubleBuffered = true;
            Font = new("Segoe UI", 9f);
            Text = string.Empty;
        }

        public enum States
        {
            None,
            Hover,
            CheckedHover,
            Checked,
            Max
        }

        #region Properties
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        public Config.Scheme Scheme { get; set; } = Program.Style.Schemes.Main;

        public States State { get; set; } = States.None;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            DoubleBuffered = true;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Rectangle MainRect = new(0, 0, Width - 1, Height - 1);

            Color bkC = default; Color lC = default;

            switch (State)
            {
                case States.None:
                    bkC = Scheme.Colors.Back;
                    lC = Scheme.Colors.Line;
                    break;

                case States.Hover:
                    bkC = Scheme.Colors.Back_Hover;
                    lC = Scheme.Colors.Line_Hover;
                    break;

                case States.Max:
                    bkC = Scheme.Colors.Back_Max;
                    lC = Scheme.Colors.Line_Hover;
                    break;

                case States.Checked:
                    bkC = Scheme.Colors.Back_Checked;
                    lC = Scheme.Colors.Line_Checked;
                    break;

                case States.CheckedHover:
                    bkC = Scheme.Colors.Back_Checked_Hover;
                    lC = Scheme.Colors.Line_Checked_Hover;
                    break;
            }

            using (SolidBrush br = new(bkC)) { G.FillRoundedRect(br, MainRect); }

            using (Pen P = new(lC)) { G.DrawRoundedRect_LikeW11(P, MainRect); }

            using (SolidBrush br = new(bkC.IsDark() ? Color.White : Color.Black)) { G.DrawString(Text, Font, br, MainRect, ContentAlignment.MiddleCenter.ToStringFormat()); }

        }
    }

}