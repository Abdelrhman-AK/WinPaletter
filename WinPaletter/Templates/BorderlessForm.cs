using FluentTransitions;
using System;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// A borderless form with custom styling and animations.
    /// </summary>
    public partial class BorderlessForm : Form
    {
        const uint WM_NCACTIVATE = 0x86U;
        private bool _shown = false;

        /// <summary>
        /// Whether the form should close when it loses focus.
        /// </summary>
        public bool CloseOnLostFocus { get; set; } = true;

        /// <summary>
        /// Whether the form should close when clicked.
        /// </summary>
        public bool CloseOnClick { get; set; } = false;

        /// <summary>
        /// Whether the form should animate when shown or hidden.
        /// </summary>
        public bool Animate { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorderlessForm"/> class.
        /// </summary>
        public BorderlessForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the creation parameters for the form.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode && !DWMAPI.IsCompositionEnabled())
                {
                    cp.ClassStyle |= DWMAPI.CS_DROPSHADOW;
                    cp.ExStyle |= 33554432;
                    return cp;
                }
                else
                {
                    return cp;
                }
            }
        }

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (!DesignMode)
            {
                switch (m.Msg)
                {
                    case DWMAPI.WM_NCPAINT:
                        {
                            // Glass (Aero) effect borders and drop shadow
                            int val = 2;
                            if (DWMAPI.IsCompositionEnabled())
                            {
                                DWMAPI.DwmSetWindowAttribute(Handle, Program.Style.RoundedCorners ? 2 : 1, ref val, 4);
                                DWMAPI.MARGINS bla = new();
                                {
                                    bla.bottomHeight = 1;
                                    bla.leftWidth = 1;
                                    bla.rightWidth = 1;
                                    bla.topHeight = 1;
                                }
                                DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref bla);
                            }
                            break;
                        }
                }

                // Close the form when it loses focus
                if (CloseOnLostFocus && m.Msg == WM_NCACTIVATE && m.WParam == IntPtr.Zero)
                {
                    if (Visible && !RectangleToScreen(DisplayRectangle).Contains(Cursor.Position))
                    {
                        Deactivate();
                    }
                }
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Deactivates the form using an animation.
        /// </summary>
        /// <param name="close"></param>
        public new void Deactivate(bool close = true)
        {
            if (_shown)
            {
                Transition
                    .With(this, nameof(Opacity), 0.0)
                    .HookOnCompletionInUiThread(this, () => { if (close) Close(); })
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick * 0.6f));

                _shown = false;
            }
        }

        private void BorderlessForm_Load(object sender, EventArgs e)
        {
            _shown = false;
            this.Localize();
            ApplyStyle(this);

            // Animate the form when shown
            if (!DesignMode && Animate)
            {
                Opacity = 0;
                Transition
                    .With(this, nameof(Opacity), 1.0)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration * 0.6f));
            }
        }

        private void BorderlessForm_Shown(object sender, EventArgs e)
        {
            _shown = true;
            Invalidate();
        }

        private void BorderlessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DesignMode && Animate) Deactivate(false);
        }

        private void BorderlessForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (CloseOnClick) Close();
        }
    }
}
