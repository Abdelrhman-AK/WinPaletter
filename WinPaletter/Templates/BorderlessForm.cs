using System;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public partial class BorderlessForm : Form
    {
        const uint WM_NCACTIVATE = 0x86U;
        private bool _shown = false;

        public bool CloseOnLostFocus { get; set; } = true;
        public bool CloseOnClick { get; set; } = false;
        public bool Animate { get; set; } = true;

        public BorderlessForm()
        {
            InitializeComponent();
        }

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

        protected override void WndProc(ref Message m)
        {
            if (!DesignMode)
            {
                switch (m.Msg)
                {
                    case DWMAPI.WM_NCPAINT:
                        {
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

        public void Deactivate(bool close = true)
        {
            if (_shown)
            {
                FluentTransitions.Transition
                    .With(this, nameof(Opacity), (double)0)
                    .HookOnCompletionInUiThread(this, () => { if (close) Close(); })
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));

                _shown = false;
            }
        }

        private void BorderlessForm_Load(object sender, System.EventArgs e)
        {
            _shown = false;
            this.LoadLanguage();
            ApplyStyle(this);

            if (!DesignMode && Animate)
            {
                Opacity = (double)0;
                FluentTransitions.Transition
                    .With(this, nameof(Opacity), (double)1)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
        }

        private void BorderlessForm_Shown(object sender, System.EventArgs e)
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
