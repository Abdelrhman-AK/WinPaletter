using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// A form with glass effect using DWM API.
    /// </summary>
    public class GlassWindow : UI.WP.Form
    {
        private bool shownOverAParent = false;
        private bool blurActive = false;

        public GlassWindow()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.Opaque, true);
            BackColor = Color.Black;
            ControlBox = false;
            Font = new("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Maximized;
            PreventFocusSteal = true;
        }

        const int LWA_ALPHA = 0x2;
        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;
        const int WS_EX_NOACTIVATE = 0x08000000;
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOACTIVATE = 0x0010;

        public void Show(Form parent)
        {
            if (parent is null)
            {
                shownOverAParent = false;
                WindowState = FormWindowState.Maximized;
                Show();
                return;
            }

            shownOverAParent = true;

            int fixer_Width = OS.W10 || OS.W11 || OS.W12 ? 15 : 0;
            int fixer_Height = OS.W10 || OS.W11 || OS.W12 ? 7 : 0;

            WindowState = FormWindowState.Normal;
            Location = parent.Location + new Size(fixer_Width / 2, 0);
            Size = parent.Size - new Size(fixer_Width, fixer_Height);

            Owner = parent;

            Show();

            User32.SetWindowPos(Handle, parent.Handle, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BackColor = Color.Black;
            ApplyGlassEffect();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            long exStyle = User32.GetWindowLong(Handle, GWL_EXSTYLE);
            exStyle |= WS_EX_TRANSPARENT | WS_EX_NOACTIVATE;
            User32.SetWindowLong(Handle, GWL_EXSTYLE, exStyle);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!DesignMode && IsHandleCreated) ApplyGlassEffect();
        }

        private void ApplyGlassEffect()
        {
            if (DesignMode || !IsHandleCreated) return;

            if ((OS.W7 || OS.WVista) && DWMAPI.IsCompositionEnabled())
            {
                blurActive = true;
                RemoveLayeredAlpha();

                DWMAPI.MARGINS margins = new() { leftWidth = -1, rightWidth = -1, topHeight = -1, bottomHeight = -1 };
                DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref margins);

                IntPtr hRgn = GDI32.CreateRectRgn(0, 0, Math.Max(1, Width), Math.Max(1, Height));

                DWMAPI.DWM_BLURBEHIND blurBehind = new()
                {
                    dwFlags = DWMAPI.DWM_BB_ENABLE | DWMAPI.DWM_BB_BLURREGION,
                    fEnable = true,
                    hRgnBlur = hRgn,
                    fTransitionOnMaximized = false
                };

                DWMAPI.DwmEnableBlurBehindWindow(Handle, blurBehind);

                if (hRgn != IntPtr.Zero) GDI32.DeleteObject(hRgn);

                Invalidate();
            }
            else if (!OS.WXP && !OS.W8x)
            {
                blurActive = false;

                this.DropEffect(Padding.Empty, shownOverAParent, DWM.DWMStyles.Acrylic, true);

                if (shownOverAParent && (OS.W12 || OS.W11))
                {
                    bool useRoundedCorners = Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors && !OS.WXP && !OS.WVista && !OS.W7 && !OS.W8x && !OS.W10;

                    int argpvAttribute = (int)DWMAPI.FormCornersType.Round;
                    DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));

                    if (useRoundedCorners && !Program.Settings.Appearance.RoundedCorners)
                    {
                        int argpvAttribute1 = (int)DWMAPI.FormCornersType.Rectangular;
                        DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                    }
                }

                ApplyLayeredAlpha();
            }
            else
            {
                blurActive = false;
                this.DropEffect(Padding.Empty, shownOverAParent, DWM.DWMStyles.None);
                ApplyLayeredAlpha();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (blurActive)
            {
                using (SolidBrush brush = new(Color.Black))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        private void ApplyLayeredAlpha()
        {
            long exStyle = User32.GetWindowLong(Handle, GWL_EXSTYLE);
            exStyle |= WS_EX_LAYERED;
            User32.SetWindowLong(Handle, GWL_EXSTYLE, exStyle);

            User32.SetLayeredWindowAttributes(Handle, 0, 128, LWA_ALPHA);
        }

        private void RemoveLayeredAlpha()
        {
            long exStyle = User32.GetWindowLong(Handle, GWL_EXSTYLE);
            if ((exStyle & WS_EX_LAYERED) != 0)
            {
                exStyle &= ~WS_EX_LAYERED;
                User32.SetWindowLong(Handle, GWL_EXSTYLE, exStyle);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GlassWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(484, 351);
            this.Name = "GlassWindow";
            this.ResumeLayout(false);

        }
    }
}