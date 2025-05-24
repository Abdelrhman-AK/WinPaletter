using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    /// <summary>
    /// Custom control for testing purposes
    /// </summary>
    //[ToolboxItem(false)]
    public class TestControl : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestControl"/> class.
        /// </summary>
        public TestControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Font = new("Segoe UI", 9f);
            Text = string.Empty;
        }

        /// <summary>
        /// States of the control
        /// </summary>
        public enum States
        {
            /// <summary>
            /// Default state
            /// </summary>
            None,

            /// <summary>
            /// Hover state
            /// </summary>
            Hover,

            /// <summary>
            /// Checked state with a cursor hover
            /// </summary>
            CheckedHover,

            /// <summary>
            /// Checked state
            /// </summary>
            Checked,

            /// <summary>
            /// Button normal state
            /// </summary>
            ButtonNone,

            /// <summary>
            /// Button hover state
            /// </summary>
            ButtonOver,

            /// <summary>
            /// Button down state
            /// </summary>
            ButtonDown
        }

        #region Properties

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the scheme of the control
        /// </summary>
        public Config.Scheme Scheme
        {
            get => _scheme;
            set
            {
                _scheme = value;
                Invalidate();
            }
        }
        private Config.Scheme _scheme = Program.Style.Schemes.Main;

        /// <summary>
        /// Gets or sets the state of the control
        /// </summary>
        public States State
        {
            get => _state;
            set
            {
                _state = value;
                Invalidate();
            }
        }
        private States _state = States.None;

        /// <summary>
        /// Creates and gets the parameters of the control
        /// </summary>
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

        /// <summary>
        /// Occurs when the parent of the control changes to update the parent level
        /// </summary>
        /// <param name="e"></param>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }

        /// <summary>
        /// Paints the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
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

        /// <summary>
        /// Gets the line color of the control
        /// </summary>
        /// <param name="baseColor"></param>
        /// <returns></returns>
        private Color LineColor(Color baseColor = default)
        {
            return State switch
            {
                States.ButtonNone => baseColor.CB(baseColor.IsDark() ? 0.02f : -0.04f),
                States.ButtonOver => baseColor.CB(baseColor.IsDark() ? 0.07f : -0.1f),
                States.ButtonDown => baseColor.CB(baseColor.IsDark() ? 0.1f : -0.08f),
                _ => baseColor.CB(baseColor.IsDark() ? 0.02f : -0.04f),
            };
        }
    }
}